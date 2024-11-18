using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponses
{
    public class GetAllChatResponsesQueryHandler(
        IChatRepository chatRepository) : IRequestHandler<GetAllChatResponsesQuery, Result<List<ChatResponseDto>>>
    {
        public async Task<Result<List<ChatResponseDto>>> Handle(GetAllChatResponsesQuery request, CancellationToken cancellationToken)
        {
            var chats = await chatRepository.GetAllAsync();
            if (chats.Any() is false)
                return Result.Fail("Chat not found");

            var responsesDto = chats
                .SelectMany(x => x.ChatResponces)
                .Select(y => new ChatResponseDto(y.Content))
                .ToList();

            return responsesDto;
        }
    }
}
