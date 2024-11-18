using MediatR;
using FluentResults;


namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponsesByChatId
{
    public class GetAllChatResponsesByChatIdQuery : IRequest<Result<List<ChatResponseDto>>>
    {
        public Guid ChatId { get; set; }
    }
}
