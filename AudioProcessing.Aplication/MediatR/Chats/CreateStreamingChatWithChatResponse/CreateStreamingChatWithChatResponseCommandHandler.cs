using MediatR;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;
using System.Runtime.CompilerServices;
using AudioProcessing.Aplication.Common.Contract;
using AudioProcessing.Aplication.MediatR.Chats.CreateChatWithChatResponse;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateStreamingChatWithChatResponse
{
    public class CreateStreamingChatWithChatResponseCommandHandler(
        IOllamaService ollamaService,
        IChatRepository chatRepository) : IStreamRequestHandler<CreateStreamingChatWithChatResponseCommand, ChatWithChatResponseDto>
    {
        public async IAsyncEnumerable<ChatWithChatResponseDto> Handle(CreateStreamingChatWithChatResponseCommand request, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var chat = Chat.Create(new UserId(Guid.NewGuid()), "New streaming chat");
            await chatRepository.AddAsync(chat, cancellationToken);
            await chatRepository.SaveChangesAsync(cancellationToken);

            var prompt = FielterPrompt(request.Promt);

            var stream = ollamaService.GenerateStreameTextContentResponce(new OllamaRequest(prompt));

            var fullResponse = string.Empty;

            await foreach (var response in stream.WithCancellation(cancellationToken))
            {
                if (response != null)
                {
                    fullResponse += response.Response;

                  
                    yield return new ChatWithChatResponseDto(
                        new ChatDto(
                            chat.Id.Value, 
                            chat.ChatTitel, 
                            chat.CreatedDate),
                        response.Response,
                        prompt);
                }
            }

            if (!string.IsNullOrWhiteSpace(fullResponse))
            {
                chat.AddChatResponceOnText(fullResponse, prompt);
                await chatRepository.SaveChangesAsync(cancellationToken);
            }
        }

        private string FielterPrompt(string prompt)
        {
            if (!string.IsNullOrWhiteSpace(prompt) && prompt.Trim().StartsWith("@"))
            {
                prompt = prompt.Trim().Substring(1).Trim();
            }

            return prompt;
        }
    }
}
