using AudioProcessing.Aplication.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.Common.Contract
{
    public interface IAudioProcessingService
    {
        public Task<AudioTranscriptionResponce> CreateTranscription(Stream audioStream/*MemoryStream trimmedAudio*/);

        public Task<MemoryStream> ProcessAudioStream(Stream audioStream, CancellationToken cancellationToken = default);
    }
}
