using AudioProcessing.Aplication.Common.Models;

namespace AudioProcessing.Aplication.Common.Contract
{
    public interface IAudioTranscriptionService
    {
        public Task<AudioTranscriptionResponce> TranscribeAudioAcync(AudioTranscriptionRequest request);
    }
}
