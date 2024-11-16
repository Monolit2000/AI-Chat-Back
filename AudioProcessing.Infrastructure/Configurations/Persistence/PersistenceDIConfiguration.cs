using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using AudioProcessing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using AudioProcessing.Infrastructure.Configurations.Persistence.StrongIdType;

namespace AudioProcessing.Infrastructure.Configurations.Persistence
{
    public static class PersistenceDIConfiguration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
                options.UseNpgsql(configuration.GetConnectionString("Database"));
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            });

            // services.AddScoped<TreatmentContext>();

            return services;
        }
    }
}
