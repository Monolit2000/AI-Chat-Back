using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChats
{
    public class ChatDto
    {
        public Guid ChatId { get; set; }
        public string ChatTitel { get; set; }
        public DateTime CreatedDate { get; set; }

        public ChatDto(
            Guid chatId,
            string chatTitel,
            DateTime createdDate)
        {
            ChatId = chatId;    
            ChatTitel = chatTitel;
            CreatedDate = createdDate;
        }
    }
}
//(click) = "handleChatClick(chat)"