using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChatsByUserId
{
    public class ChatDto
    {
        public string ChatTitel { get; set; }
        public DateTime CreatedDate { get; set; }

        public ChatDto(
            string chatTitel,
            DateTime createdDate)
        {
            ChatTitel = chatTitel;
            CreatedDate = createdDate;
        }
    }
}
