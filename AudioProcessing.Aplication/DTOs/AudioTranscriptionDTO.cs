using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.DTOs
{
    public class AudioTranscriptionDTO
    {
        public string Transcription { get; set; }

        public AudioTranscriptionDTO(string transcription)
        {
            Transcription = transcription;
        }
    }
}



