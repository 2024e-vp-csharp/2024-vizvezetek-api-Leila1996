using Microsoft.EntityFrameworkCore;
using Vizvezetek.API.Context;

namespace Vizvezetek.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Kisbetûs JSON mezõnevek
                });

            builder.Services.AddEndpointsApiExplorer();

            // CORS engedélyezése (ha frontend is van)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // Adatbázis kapcsolat beállítása
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<VizvezetekDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            var app = builder.Build();

            // Adatbázis migráció automatikus futtatása
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<VizvezetekDbContext>();
                dbContext.Database.Migrate(); // Frissíti az adatbázist, ha szükséges
            }


            app.UseCors("AllowAll"); // CORS engedélyezése

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
