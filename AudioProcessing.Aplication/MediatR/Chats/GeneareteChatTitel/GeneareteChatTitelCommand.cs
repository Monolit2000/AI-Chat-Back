using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.GeneareteChatTitel
{
    public class GeneareteChatTitelCommand : IRequest<Result<ChatTitelDto>>
    {
        public Guid ChatId { get; set; }    
        public string Prompt { get; set; }  
    }
}
