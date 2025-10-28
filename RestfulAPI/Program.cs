using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestfulAPI.Helper;
using RestfulAPI.Repos;
using RestfulAPI.Repos.Models;
using RestfulAPI.Service.Implementations;
using RestfulAPI.Service.Interfaces;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Đọc đường dẫn log từ appsettings.json
string logpath = builder.Configuration.GetSection("Logging:Logpath").Value;

// 🟢 Tạo logger toàn cục
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console() // log ra console cho dễ debug
    .WriteTo.File(logpath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
    .CreateLogger();

// 🟢 Thay thế logger mặc định bằng Serilog
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        var controllerActionDescriptor = apiDesc.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
        return controllerActionDescriptor?.ControllerTypeInfo
            .GetCustomAttributes(typeof(ApiControllerAttribute), true)
            .Any() ?? false;
    });
});


// Transient
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IRefreshHandlerService, RefreshHandlerService>();
builder.Services.AddScoped<Sieve.Services.ISieveProcessor, Sieve.Services.SieveProcessor>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITracksService, TracksService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<LearndataContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("apicon"))
);

builder.Services.AddAutoMapper(typeof(AutoMapperHandler));

// Rate limiter
builder.Services.AddRateLimiter(_ => _.AddFixedWindowLimiter(policyName: "Fixedwindow", options =>
{
    options.Window = TimeSpan.FromSeconds(10);
    options.PermitLimit = 1;
    options.QueueLimit = 1;
    options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
}));

// Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;

}).AddEntityFrameworkStores<LearndataContext>().AddDefaultTokenProviders();

// Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.Name = ".AspNetCore.Identity.Application";
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/mvcauth/login";
        options.LogoutPath = "/";
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("Corspolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("Corspolicy");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Log.Information("Application started successfully!");

app.Run();

Log.CloseAndFlush();
