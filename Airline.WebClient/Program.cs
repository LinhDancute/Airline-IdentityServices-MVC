﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using App.Services;
using Microsoft.AspNetCore.Builder;
using App.ExtendMethods;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Airline.ModelsService;
using Airline.WebClient.Models;
using App.Data;
using Airline.WebClient.Services;
using Airline.WebClient.Utilities;
using Airline.WebClient.Services.Airline;
using Airline.WebClient;
using Airline.ModelsService.Models;
using Airline.WebClient.Services.IServices.Airline;
using Airline.WebClient.Services.IServices;

var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration.GetConnectionString("AirlineReservationDb") ?? throw new InvalidOperationException("Connection string 'AirlineReservationDb' not found.");

// Load appsettings.json configurations
IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();

// Register the MyBlogContext with the dependency injection container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string not found"))
    );
builder.Services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();

// Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 3 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = true;         //Xác thực tài khoản
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
                    options.CallbackPath = "/dang-nhap-tu-google"; // Relative path instead of absolute URL
                });

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewManageMenu", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireRole(RoleName.Administrator);
    });
});

builder.Services.AddMvc().AddViewOptions(options =>
{
    options.HtmlHelperOptions.ClientValidationEnabled = false;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddControllers();

// Register AutoMapper
builder.Services.ConfigureMapper();

// Register services
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ITicketClassService, TicketClassService>();
builder.Services.AddScoped<IAirlineService, AirlineService>();
builder.Services.AddScoped<IAirportService, AirportService>();
builder.Services.AddScoped<IFlightRouteService, FlightRouteService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IFlightRoute_AirportService, FlightRoute_AirportService>();


// Configure the named HttpClient
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

SD.TicketClassAPIBase = builder.Configuration["ServiceUrls:Airline.Services.CouponAPI"];
SD.AirlineAPIBase = builder.Configuration["ServiceUrls:Airline.Services.ScheduleAPI"];

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowAllOrigins");

app.AddStatusCodePage(); // Tuy bien Response loi: 400 - 599

app.UseRouting();        // EndpointRoutingMiddleware

app.UseAuthentication(); // xac dinh danh tinh 
app.UseAuthorization();  // xac thuc  quyen truy  cap

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();

app.UseEndpoints(endpoints =>
{

    endpoints.MapGet("/sayhi", async (context) =>
    {
        await context.Response.WriteAsync($"Hello ASP.NET MVC {DateTime.Now}");
    });

    // Controller khong co Area
    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

/*
dotnet aspnet-codegenerator area Database
dotnet aspnet-codegenerator controller -name DbManage -outDir Areas/Database/Controllers/ -namespace App.Areas.Database.Controllers

-Tạo các trang Index/CRUD trong Areas/Contact/Views, trang ContactController trong Areas/Contact/Controllers dựa vào trang Contact.cs trong Models/Contact 
  - dotnet aspnet-codegenerator controller -name ContactController -namespace App.Areas.Contact.Controllers -m App.Models.Contacts.Contact -udl -dc App.Models.AppDBContext -outDir Areas/Contact/Controllers
  - mv Views/Contact Areas/Contact/Views/

  dotnet aspnet-codegenerator controller -name CategoryCotrller -m App.Models.Blog.Category -dc App.Models.AppDbContext -udl -outDir Areas/Blog/Controllers/

JSON
    "AirlineReservationDb": "Data Source=127.0.0.1,1433; Initial Catalog=AirlineReservationDB; User ID=SA;Password=yunbrayyunh; TrustServerCertificate=true;",
    "AirlineReservationConnectionString": "Server=(localdb)\\mssqllocaldb;Database=App;Trusted_Connection=True;MultipleActiveResultSets=true"


*/
