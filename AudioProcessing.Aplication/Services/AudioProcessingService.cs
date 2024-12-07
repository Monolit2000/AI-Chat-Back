using AudioProcessing.Aplication.Common.Models;
using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Aplication.Services
{
    public class AudioProcessingService(
        ISplitAudioService splitAudioService,
        IAudioTranscriptionService audioTranscriptionService) : IAudioProcessingService
    {
        public async Task<AudioTranscriptionResponce> CreateTranscription(Stream audioStream/*MemoryStream trimmedAudio*/)
        {
            var trimmedAudio = await ProcessAudioStream(audioStream);

            var transcriptionRequest = new AudioTranscriptionRequest();
            transcriptionRequest.AudioStream = trimmedAudio;

            var content = await audioTranscriptionService.TranscribeAudioAcync(transcriptionRequest);
            return content;
        }


        public async Task<MemoryStream> ProcessAudioStream(Stream audioStream, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
                await audioStream.CopyToAsync(memoryStream, cancellationToken);
                memoryStream.Seek(0, SeekOrigin.Begin);

                int startPosition = (int)(memoryStream.Length / 4);
                int endPosition = (int)(memoryStream.Length / 2);

                return await splitAudioService.TrimAudioAsync(memoryStream, startPosition, endPosition);
            }
        }

    }
}
