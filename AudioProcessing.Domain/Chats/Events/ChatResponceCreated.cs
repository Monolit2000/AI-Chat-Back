using AudioProcessing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Domain.Chats.Events
{
    public class ChatResponceCreated : DomainEventBase
    {
        public ChatResponceId ChatResponceId { get; }
        public ChatId ChatId { get; }

        public ChatResponceCreated(ChatResponceId chatResponceId, ChatId chatId)
        {
            ChatResponceId = chatResponceId;    
            ChatId = chatId;
        }
    }
}
