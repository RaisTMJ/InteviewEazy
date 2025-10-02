
using Microsoft.EntityFrameworkCore;
using Recipe.API.Middleware;
using Recipe.Application.Features;
using Recipe.Application.Interface;
using Recipe.Application.Interfaces;
using Recipe.Infrastructure.Persistance;
using Recipe.Infrastructure.Persistence;
using Recipe.Infrastructure.Security;
using Azure.Storage.Blobs;
using Recipe.Infrastructure.Service;

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


            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();




            app.MapControllers();

            app.Run();
        }
    }
}
