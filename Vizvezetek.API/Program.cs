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
                    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Kisbet�s JSON mez�nevek
                });

            builder.Services.AddEndpointsApiExplorer();

            // CORS enged�lyez�se (ha frontend is van)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // Adatb�zis kapcsolat be�ll�t�sa
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<VizvezetekDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            var app = builder.Build();

            // Adatb�zis migr�ci� automatikus futtat�sa
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<VizvezetekDbContext>();
                dbContext.Database.Migrate(); // Friss�ti az adatb�zist, ha sz�ks�ges
            }


            app.UseCors("AllowAll"); // CORS enged�lyez�se

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
