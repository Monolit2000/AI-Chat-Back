using AudioProcessing.Domain.Chats;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.RenameChat
{
    public class RenameChatCommandHendler(
        IChatRepository chatRepository) : IRequestHandler<RenameChatCommand, Result>
    {
        public async Task<Result> Handle(RenameChatCommand request, CancellationToken cancellationToken)
        {

            var chat = await chatRepository.GetByIdAsync(new ChatId(request.ChatId));
            if (chat == null)
                return Result.Fail("Chat not found");

            return Result.Ok();
        }
    }
}
