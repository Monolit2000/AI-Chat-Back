using AudioProcessing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Domain.Chats.Events
{
    public class ChatTitelSetedDomainEvent : DomainEventBase
    {
        public ChatId ChatId { get; set; }
        public string Titel { get; set; }

        public ChatTitelSetedDomainEvent(
            ChatId chatId,
            string titel)
        {
            ChatId = chatId;
            Titel = titel;
        }
    }
}
