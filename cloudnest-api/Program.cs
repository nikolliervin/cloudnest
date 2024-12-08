using CloudNest.Api.Interfaces;
using CloudNest.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CloudNest.Api.Helpers;
using CloudNest.Api.Middlewares;
using Serilog;
using CloudNest.Api.Models;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDirectoryService, DirectoryService>();
builder.Services.AddScoped<IDirectoryShareService, DirectoryShareService>();
builder.Services.AddScoped<IPermissionsService, PermissionsService>();
builder.Services.AddSingleton<JwtTokenHelper>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHealthChecks();
builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthorizationHandler, DirectoryPermissionHandler>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var secretKey = jwtSettings["SecretKey"];
    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        IssuerSigningKey = symmetricSecurityKey
    };
});




builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserHelper>();
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/Log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("CloudNest")
               .WithPreferredScheme("Bearer");
    });

}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors("AllowAll");
app.Run();

