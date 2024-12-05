using AudioProcessing.Aplication.Common.Contract;
using AudioProcessing.Domain.Chats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateStreamingChatResponseOnText
{
    public class CreateStreamingChatResponseOnTextCommandHandler(
        IOllamaService ollamaService,
        IChatRepository chatRepository,
        IAudioProcessingService transcriptionService) : IStreamRequestHandler<CreateStreamingChatResponseOnTextCommand, ChatResponseDto>
    {
        public async IAsyncEnumerable<ChatResponseDto> Handle(CreateStreamingChatResponseOnTextCommand request, CancellationToken cancellationToken)
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
                    yield return new ChatResponseDto(chat.Id.Value, response.Response, prompt);
                }
            }

            if (!string.IsNullOrWhiteSpace(fullResponse))
            {
                chat.AddChatResponceOnText(fullResponse, prompt);
                await chatRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
