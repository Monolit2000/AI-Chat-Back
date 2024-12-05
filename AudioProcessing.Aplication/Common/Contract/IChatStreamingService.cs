using AudioProcessing.Aplication.MediatR.Chats;
using AudioProcessing.Aplication.MediatR.Chats.CreateChatResponseOnText;

namespace AudioProcessing.Aplication.Common.Contract
{
    public interface IChatStreamingService
    {
        IAsyncEnumerable<ChatResponseDto> CreateStreamingChatResponseOnText(CreateChatResponseOnTextCommand request, CancellationToken cancellationToken = default);
    }
}
