using AudioProcessing.Aplication.Common.Contract;
using AudioProcessing.Aplication.MediatR.Chats;
using AudioProcessing.Aplication.MediatR.Chats.CreateChatResponseOnText;
using AudioProcessing.Domain.Chats;

namespace AudioProcessing.Aplication.Services.Chats
{
    public class ChatStreamingService(
        IOllamaService ollamaService,
        IChatRepository chatRepository,
        IAudioProcessingService transcriptionService) : IChatStreamingService
    {
        public async IAsyncEnumerable<ChatResponseDto> CreateStreamingChatResponseOnText(CreateChatResponseOnTextCommand request, CancellationToken cancellationToken = default)
        {
            var chat = await chatRepository.GetByIdAsync(new ChatId(request.ChatId), cancellationToken);

            if (chat == null)
                throw new InvalidOperationException($"Chat with ID {request.ChatId} not found.");

            var prompt = request.Promt;

            if (string.IsNullOrWhiteSpace(prompt))
                yield break;

            if (prompt.Trim().StartsWith("@"))
                prompt = prompt.Trim().Substring(1).Trim();

            var stream = ollamaService.GenerateStreameTextContentResponce(new OllamaRequest(prompt));

            var fullResponse = string.Empty;

            await foreach (var response in stream.WithCancellation(cancellationToken))
            {
                if (response != null)
                {
                    fullResponse += response.Response; 
                    yield return new ChatResponseDto(chat.Id.Value, response.Response, prompt) ;   
                }
            }

            if (!string.IsNullOrWhiteSpace(fullResponse))
            {
                chat.AddChatResponseOnText(fullResponse, prompt);
                await chatRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
