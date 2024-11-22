using AudioProcessing.Domain.Chats;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.DeleteChat
{
    public class DeleteChatCommandHandler(
        IChatRepository chatRepository) : IRequestHandler<DeleteChatCommand, Result>
    {
        public async Task<Result> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            await chatRepository.DeleteAsync(new ChatId(request.ChatId), cancellationToken);

            await chatRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
