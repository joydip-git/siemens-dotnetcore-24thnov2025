using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Siemens.DotNetCore.PmsApp.API.Models;
using Siemens.DotNetCore.PmsApp.DTOs;
using Siemens.DotNetCore.PmsApp.Repository;
using Siemens.DotNetCore.PmsApp.ServiceManager;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
RegisterDbContext(builder);

RegisterAppServices(builder);

RegisterCoreAPIServices(builder);

RegisterCORSService(builder);

RegisterJwtAuthService(builder);

//create host
var app = builder.Build();

//register middlewares for request pipeline
ConfigureMiddlewares(app);

app.Run();

//local static methods

static void RegisterDbContext(WebApplicationBuilder builder)
{
    Action<DbContextOptionsBuilder> optionsBuilder
        = optBuilder => optBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SiemensDbConStr"));

    builder.Services.AddDbContext<SiemensDbContext>(optionsBuilder, ServiceLifetime.Singleton, ServiceLifetime.Singleton);
}

static void RegisterAppServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IAsyncAuthServiceManager, AuthAsyncServiceManager>();
    builder.Services.AddSingleton<ITokenManager, JwtTokenManager>();
    builder.Services.AddScoped<IAsyncServiceManager<ProductDTO, int>, ProductAsyncServiceManager>();
}

static void RegisterCoreAPIServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddOpenApi("pmsappapi.json");
}

static void RegisterCORSService(WebApplicationBuilder builder)
{
    //builder.Services.AddCors();
    Action<CorsOptions> corsOptions = options =>
    {
        options
        .AddPolicy(
            "AllowAll",
            policyBuilder =>
            {
                policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
    };
    builder.Services.AddCors(corsOptions);
}

static void RegisterJwtAuthService(WebApplicationBuilder builder)
{
    builder
        .Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "apiy1Wx2Pe5oFkrs68y0iTyUTGFNxwvdY8eekFfYXCm4lm4iwgF2FoogxAjeF3PTH4FNEMw5YXwTHetcJCXTOQuWiiiIUR30wPBJR0L0oC5wBzhZ35LpmlWTPcIyURXl"))
                };
            }
        );
}

static void ConfigureMiddlewares(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    //Action<CorsPolicyBuilder> corsPolicyBuilder = policyBuilder =>
    //{
    //    policyBuilder
    //        .AllowAnyOrigin()
    //        .AllowAnyMethod()
    //        .AllowAnyHeader();
    //};
    //app.UseCors(corsPolicyBuilder);

    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}