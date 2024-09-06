using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Blazor.Data;
using Microsoft.EntityFrameworkCore;
using ARClassLibrary;
using Blazor.Components;
using Blazor.Repository;
using Blazored.LocalStorage;

namespace Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<UserRepo>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ArWeb", Version = "v1" });
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
            });

            // Register ISqlDataAccess with its implementation
            builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Ensure antiforgery middleware is placed correctly
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery(); // This line is crucial

            app.UseSwagger(); // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArWeb v1");
            });

            app.MapControllers(); // Ensure controllers are mapped.

            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}


