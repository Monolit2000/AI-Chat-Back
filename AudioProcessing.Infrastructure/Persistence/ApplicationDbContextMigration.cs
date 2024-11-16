using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AudioProcessing.Infrastructure.Persistence
{
    public static class ApplicationDbContextMigration
    {
        public static void ApplyWorkContextMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext workContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            workContext.Database.Migrate();
        }
    }
}
