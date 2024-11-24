using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;

namespace AudioProcessing.Aplication.MediatR.Chats.ChegeChatTitel
{
    public class ChegeChatTitelCommandHandler(
        IChatRepository chatRepository) : IRequestHandler<ChegeChatTitelCommand, Result>
    {
        public async Task<Result> Handle(ChegeChatTitelCommand request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetByIdAsync(new ChatId(request.ChatId), cancellationToken);
            if (chat == null)
                return Result.Fail("Chat not found");

            var result = chat.ChengeChatTitel(request.NewChatTitel);
         
            return result;
        }
    }
}
