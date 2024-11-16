using AudioProcessing.Domain.Common;
using AudioProcessing.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Domain.Chats.Events
{
    public class ChatCreatedDomainEvent : DomainEventBase
    {
        public UserId UserId { get; } 
        public ChatId ChatId { get; }
        public string Titel { get; }

        public ChatCreatedDomainEvent(
            ChatId chatId, 
            UserId userId, 
            string titel)
        {
            UserId = userId;
            ChatId = chatId;
            Titel = titel;
        }
    }
}
