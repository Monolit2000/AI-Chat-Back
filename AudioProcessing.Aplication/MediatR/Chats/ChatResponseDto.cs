using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats
{
    public class ChatResponseDto
    {
        public Guid ChatId { get; set; }
        public string Prompt { get; set; }
        public string Conetent { get; set; }

        public ChatResponseDto()
        {

        }

        public ChatResponseDto(
            Guid chatId,
            string conetent,
            string prompt = null)
        {
            ChatId = chatId;
            Prompt = prompt;    
            Conetent = conetent;
        }
    }
}
