using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.DeleteChat
{
    public class DeleteChatCommand : IRequest<Result>
    {
       public Guid ChatId { get; set; } 
    }
}
