using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.ChegeChatTitel
{
    public class ChegeChatTitelCommand : IRequest<Result>
    {
        public Guid ChatId { get; set; }        
        public string NewChatTitel { get; set; }        
    }
}
