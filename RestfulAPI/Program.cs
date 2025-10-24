using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Transient
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddDbContext<LearndataContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("apicon"))
);

builder.Services.AddAutoMapper(typeof(AutoMapperHandler));

builder.Services.AddCors(p => p.AddPolicy("Corspolicy", build =>
{
    build.WithOrigins("https://domain1.com", "https://domain2.com").AllowAnyMethod().AllowAnyHeader();
}));

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

// JWT Authentication
var _authkey = builder.Configuration.GetValue<string>("JwtSettings:SecurityKey");
builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{
    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Cấu hình pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Corspolicy");

app.UseRateLimiter();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// 🟢 Ghi log thử để kiểm tra
Log.Information("Application started successfully!");

app.Run();

// 🟢 Đảm bảo flush log khi app tắt
Log.CloseAndFlush();
