using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System;
using Airline.ModelsService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Airline.ModelsService.Models;
using Airline.WebClient.Services.IServices;
using Airline.WebClient.Services;
using Airline.WebClient.Services.IServices.Airline;
using Airline.WebClient.Services.Airline;
using Microsoft.AspNetCore.DataProtection;
using App.Services;
using Airline.WebClient;

var builder = WebApplication.CreateBuilder(args);

// Enable PII (Personally Identifiable Information) logging for identity model
IdentityModelEventSource.ShowPII = true;

// Load appsettings.json configurations
IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string not found"))
);

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = true;
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromSeconds(5);
});

var mailSetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSetting);
builder.Services.AddSingleton<IEmailSender, SendMailService>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login/";
    options.LogoutPath = "/logout/";
    options.AccessDeniedPath = "/khongduoctruycap.html";
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    // Trên 30 giây truy cập lại sẽ nạp lại thông tin User (Role)
    // SecurityStamp trong bảng User đổi -> nạp lại thông tinn Security
    options.ValidationInterval = TimeSpan.FromSeconds(5);
});

builder.Services.AddAuthentication().AddCookie()
                .AddGoogle(options =>
                {
                    var gconfig = configuration.GetSection("Authentication:Google");
                    options.ClientId = gconfig["ClientId"];
                    options.ClientSecret = gconfig["ClientSecret"];
                    options.CorrelationCookie.SameSite = SameSiteMode.Lax;
                    options.CallbackPath = "/dang-nhap-tu-google";
                });

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = "oidc";
//})
//.AddCookie(options =>
//{
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//    options.LoginPath = "/Auth/Login";
//    options.AccessDeniedPath = "/Auth/AccessDenied";
//    options.SlidingExpiration = true;
//})
//.AddOpenIdConnect("oidc", options =>
//{
//    options.Authority = "https://localhost:7006"; 
//    options.ClientId = "airline_webclient";
//    options.ClientSecret = "secret";
//    options.ResponseType = "code";
//    options.GetClaimsFromUserInfoEndpoint = true;
//    options.SaveTokens = true;

//    options.TokenValidationParameters.NameClaimType = "name";
//    options.TokenValidationParameters.RoleClaimType = "role";
//    options.Scope.Add("airline");
//    options.Scope.Add("offline_access");


//    options.Events = new OpenIdConnectEvents
//    {
//        OnTicketReceived = context =>
//        {
//            // Redirect to AuthController after successful login
//            var returnUrl = context.Properties.RedirectUri ?? "~/";
//            context.HandleResponse();
//            context.Response.Redirect(returnUrl);
//            return Task.CompletedTask;
//        },
//        OnRemoteFailure = context =>
//        {
//            context.Response.Redirect("/");
//            context.HandleResponse();
//            return Task.CompletedTask;
//        }
//    };
//});



//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ITicketClassService, TicketClassService>();
builder.Services.AddScoped<IAirlineService, AirlineService>();
builder.Services.AddScoped<IAirportService, AirportService>();
builder.Services.AddScoped<IFlightRouteService, FlightRouteService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IFlightRoute_AirportService, FlightRoute_AirportService>();
builder.Services.AddScoped<IBaggageService, BaggageService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IUnitPriceService, UnitPriceService>();

//builder.Services.AddHttpClient("Airline.Services.AuthAPI", client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:Airline.Services.AuthAPI"]);
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//});

builder.Services.AddHttpClient("Airline.Services.CouponAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:Airline.Services.CouponAPI"]);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("Airline.Services.ScheduleAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:Airline.Services.ScheduleAPI"]);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewManageMenu", policy =>
    {
        policy.RequireRole("Administrator");
    });
});

builder.Services.AddMvc().AddViewOptions(options =>
{
    options.HtmlHelperOptions.ClientValidationEnabled = false;
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.ConfigureMapper();
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();