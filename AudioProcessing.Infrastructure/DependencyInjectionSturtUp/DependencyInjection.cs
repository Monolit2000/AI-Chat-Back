using AudioProcessing.Aplication.Common.Contract;
using Microsoft.Extensions.DependencyInjection;
using AudioProcessing.Infrastructure.Services;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Infrastructure.Domain.Chats;
namespace AudioProcessing.Aplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAudioProcessingServices(this IServiceCollection services/*, IConfiguration configuration*/)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(IAplication).Assembly);
            });

            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IAudioTranscriptionService, TranscriptionService>();

            services.AddScoped<ISplitAudioService, SplitAudioService>();
           

            return services;
        }
    }
}
