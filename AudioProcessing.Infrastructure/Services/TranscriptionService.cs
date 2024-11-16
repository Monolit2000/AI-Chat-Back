using Polly;
using OpenAI;
using System.Net;
using Polly.Retry;
using OpenAI.Audio;
using System.Diagnostics;
using AudioProcessing.Aplication.Common.Models;
using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Infrastructure.Services
{
    public class TranscriptionService : IAudioTranscriptionService
    {
        private int retryCount = 10;
        private readonly AsyncRetryPolicy retryPolicy;
        private readonly TimeSpan delay = TimeSpan.FromSeconds(5);
        private OpenAIClient api = new OpenAIClient("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");

        public TranscriptionService() 
        {
            retryPolicy = Policy
                .Handle<HttpRequestException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(retryCount, retryAttempt => delay,
                    (exception, timeSpan, retryCount, context) =>
                        Trace.TraceError($"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before next retry."));
        }

        public async Task<AudioTranscriptionResponce> TranscribeAudioAcync(AudioTranscriptionRequest request)
        {
            return await retryPolicy.ExecuteAsync(() =>
            {
                AudioTranscription resultSRT = api.GetAudioClient("whisper-1")
                    .TranscribeAudio( audio: request.AudioStream,audioFilename: "file.mp3");
                return Task.FromResult(new AudioTranscriptionResponce(resultSRT.Text));
            });
        }
    }
}




//using Polly;
//using System.Net;
//using OpenAI.Interfaces;
//using OpenAI.Managers;
//using OpenAI.ObjectModels.RequestModels;
//using Polly.Retry;
//using System.Diagnostics;
//using AudioProcessing.Aplication.Common.Models;
//using AudioProcessing.Aplication.Common.Contract;

//namespace AudioProcessing.Infrastructure.Services
//{
//    public class TranscriptionService : IAudioTranscriptionService
//    {
//        private readonly int _retryCount = 10;
//        private readonly TimeSpan _delay = TimeSpan.FromSeconds(5);
//        private readonly AsyncRetryPolicy _retryPolicy;
//        private readonly IOpenAIService _openAIService;

//        public TranscriptionService()
//        {
//            // Создаем экземпляр OpenAIService
//            _openAIService = new OpenAIService(new OpenAI.OpenAiOptions
//            {
//                ApiKey = "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"
//            });

//            // Настройка политики повторных попыток с Polly
//            _retryPolicy = Policy
//                .Handle<HttpRequestException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
//                .WaitAndRetryAsync(_retryCount, retryAttempt => _delay,
//                    (exception, timeSpan, retryCount, context) =>
//                        Trace.TraceError($"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before next retry."));
//        }

//        public async Task<AudioTranscriptionResponce> TranscribeAudioAcync(AudioTranscriptionRequest request)
//        {
//            return await _retryPolicy.ExecuteAsync(async () =>
//            {
//                await Console.Out.WriteLineAsync("Attempting transcription...");

//                // Отправляем аудиофайл в OpenAI для получения транскрипции
//                var transcriptionResult = await _openAIService.Audio.CreateTranscription(new AudioCreateTranscriptionRequest
//                {
//                    FileName = "file.mp3", // Можно указать любое имя файла
//                    File = request.AudioStream,
//                    Model = "whisper-1", // Whisper модель для транскрипции
//                    ResponseFormat = "text" // Возвращаем текстовый результат
//                });

//                if (transcriptionResult == null || string.IsNullOrEmpty(transcriptionResult.Text))
//                {
//                    throw new Exception("Transcription failed");
//                }

//                // Создаем и возвращаем объект с транскрипцией
//                var response = new AudioTranscriptionResponce(transcriptionResult.Text);
//                return response;
//            });
//        }
//    }
//}

