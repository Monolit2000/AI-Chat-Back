using MediatR;
using FluentResults;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponses
{
    public class GetAllChatResponsesQuery : IRequest<Result<List<ChatResponseDto>>>
    {
    }
}
