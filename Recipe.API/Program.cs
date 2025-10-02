
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Recipe.API.Middleware;
using Recipe.Application.Features;
using Recipe.Application.Interface;
using Recipe.Application.Interfaces;
using Recipe.Infrastructure.Persistance;
using Recipe.Infrastructure.Persistence;
using Recipe.Infrastructure.Security;
using Recipe.Infrastructure.Service;
using System.Text;

namespace Recipe.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var _configuration = builder.Configuration;
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(
// IMPORTANT: Get the connection string from your appsettings.json
    _configuration. GetConnectionString("DefaultConnection")));
            

            builder.Services.AddSingleton(x => new BlobServiceClient(_configuration.GetConnectionString("AzureStorage")));


            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IFileStorageService, FileStorageService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });



            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen( options=>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer" // JWT scheme name
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement { 
                    { 
                        new OpenApiSecurityScheme 
                        { 
                            Reference = new OpenApiReference 
                            { 
                                Type = ReferenceType.SecurityScheme, 
                                Id = "Bearer" 
                            } 
                        }, 
                        Array.Empty<string>() 
                    } });
            });

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during database migration/seeding.");
                }
            }

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();




            app.MapControllers();

            app.Run();
        }
    }
}
