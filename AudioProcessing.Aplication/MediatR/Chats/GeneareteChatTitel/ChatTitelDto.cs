using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.GeneareteChatTitel
{
    public class ChatTitelDto
    {
        public Guid ChatId { get; set; }
        public string NewTitel { get; set; }

        public ChatTitelDto(
            Guid chatId, 
            string newTitel)
        {
            ChatId = chatId;
            NewTitel = newTitel;
        }
    }
}
