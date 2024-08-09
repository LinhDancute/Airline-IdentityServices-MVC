using Airline.ModelsService;
using Airline.ModelsService.Models;
using Airline.Services.CouponAPI.Repositories;
using Airline.Services.CouponAPI.Repositories.Implements;
using Airline.Services.CouponAPI.Services;
using Airline.Services.CouponAPI.Services.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stripe;
using Airline.ModelsService.Models.Payment;
using AirlineInvoiceService = Airline.Services.CouponAPI.Services.Implements.InvoiceService;
using StripeInvoiceService = Stripe.InvoiceService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            options.JsonSerializerOptions.MaxDepth = 64;
        });

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Start configuring services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string not found"))
);

// Add Identity & JWT authentication
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddRoles<IdentityRole>();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(x => {
//        x.RequireHttpsMetadata = false;
//        x.SaveToken = true;
//        x.Authority = "https://localhost:7006/";
//        x.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });

// Register repositories and services
builder.Services.AddScoped<ITicketClassRepository, TicketClassRepository>();
builder.Services.AddScoped<ITicketClassService, TicketClassService>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IBaggageRepository, BaggageRepository>();
builder.Services.AddScoped<IBaggageService, BaggageService>();
builder.Services.AddScoped<IUnitPriceRepository, UnitPriceRepository>();
builder.Services.AddScoped<IUnitPriceService, UnitPriceService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IBoardingPassRepository, BoardingPassRepository>();
builder.Services.AddScoped<IBoardingPassService, BoardingPassService>();
builder.Services.AddScoped<IInvoiceService, AirlineInvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceDetailRepository, InvoiceDetailRepository>();
builder.Services.AddScoped<IInvoiceDetailService, InvoiceDetailService>();


// Register AutoMapper
builder.Services.ConfigureAutoMapper();

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CouponAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CouponAPI V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();