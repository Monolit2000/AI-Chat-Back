using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponsesByChatId
{
    public class GetAllChatResponsesByChatIdQueryHandler(
        IChatRepository chatRepository) : IRequestHandler<GetAllChatResponsesByChatIdQuery, Result<List<ChatResponseDto>>>
    {
        public async Task<Result<List<ChatResponseDto>>> Handle(GetAllChatResponsesByChatIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetByIdAsync(new ChatId(request.ChatId));
            if (chat == null) 
                return Result.Fail("Chat not found"); 
            
            if(!chat.ChatResponces.Any())
                return new List<ChatResponseDto>();

            var responsesDto = chat.ChatResponces
                .Select(response => new ChatResponseDto(
                    response.ChatId.Value, 
                    response.Content,
                    response.Promt))
                .ToList();

            return responsesDto;
        }
    }
}
