using AudioProcessing.Aplication.Common.Contract;
using Microsoft.Extensions.DependencyInjection;
using AudioProcessing.Infrastructure.Services;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Infrastructure.Domain.Chats;
using AudioProcessing.Aplication.Services;
using AudioProcessing.Aplication.Services.Ollama;
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


            //services.AddHttpClient<OllamaService>();


            services.AddHttpClient<IOllamaService>(client =>
            {
                //client.BaseAddress = new Uri("http://127.0.0.1:11434");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddScoped<IOllamaService, OllamaService>();

            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IAudioTranscriptionService, TranscriptionService>();

            services.AddScoped<ISplitAudioService, SplitAudioService>();
            services.AddScoped<IAudioProcessingService, AudioProcessingService>();
           

            return services;
        }
    }
}
