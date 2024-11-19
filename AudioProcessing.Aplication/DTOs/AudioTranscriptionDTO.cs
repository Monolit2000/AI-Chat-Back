using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.DTOs
{
    public class AudioTranscriptionDTO
    {
        public Guid ChatId { get; set; }
        public string conetent { get; set; }

        public AudioTranscriptionDTO(Guid chatId ,string transcription)
        {
            ChatId = chatId;
            conetent = transcription;
        }
    }
}



