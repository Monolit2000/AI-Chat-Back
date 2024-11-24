using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChatResponseOnText
{
    public class CreateChatResponseOnTextCommand : IRequest<Result<ChatResponseDto>>
    {
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
        public string Promt { get; set; }
    }
}
