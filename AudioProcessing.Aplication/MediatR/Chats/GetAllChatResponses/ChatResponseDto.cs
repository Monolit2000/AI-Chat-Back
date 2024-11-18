using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponses
{
    public class ChatResponseDto
    {
        public string Transcription { get; set; }

        public ChatResponseDto(string transcription)
        {
            Transcription = transcription;
        }
    }
}
