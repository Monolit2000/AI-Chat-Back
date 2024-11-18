using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponsesByChatId
{
    public class ChatResponseDto
    {
        public Guid ChatId { get; set; }    
        public string Conetent { get; set; }

        public ChatResponseDto()
        {
            
        }

        public ChatResponseDto(
            Guid chatId, 
            string conetent)
        {
            ChatId = chatId;
            Conetent = conetent;
        }
    }
}
