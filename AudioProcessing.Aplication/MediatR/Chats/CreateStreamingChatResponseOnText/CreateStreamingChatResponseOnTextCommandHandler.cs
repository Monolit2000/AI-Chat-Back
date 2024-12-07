using AudioProcessing.Aplication.Common.Contract;
using AudioProcessing.Domain.Chats;
using MediatR;

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

            IAsyncEnumerable<string> streaming;


            //if (prompt.Trim().StartsWith("@"))
            //    prompt = prompt.Trim().Substring(1).Trim();


            if (!prompt.Trim().StartsWith("@"))
            {
                //prompt = prompt.Trim().Substring(1).Trim();
                streaming = ConvertToStringsAsync(
                    ollamaService.GenerateStreameTextContentResponce(new OllamaRequest(prompt), cancellationToken));
            }
            else
            {
                prompt = prompt.Trim().Substring(1).Trim();
                streaming = ollamaService.GenerateStreamingChatResponse(
                    new OllamaRequest(prompt),
                    chat.ChatResponces.SelectMany(x => new List<(string Role, string Message)>
                    {
                        (Role: "user", Message: x.Promt),
                        (Role: "chat", Message: x.Content)
                    }),
                    cancellationToken);
            }


            //var stream = ollamaService.GenerateStreameTextContentResponce(new OllamaRequest(prompt), cancellationToken);

            //var stream = ollamaService.GenerateStreamingChatResponse(
            //    new OllamaRequest(prompt),
            //    chat.ChatResponces.SelectMany(x => new List<(string Role, string Message)> { (Role: "chat", Message: x.Content), (Role: "user", Message: x.Promt) }), 
            //    cancellationToken);

            var fullResponse = string.Empty;

            


            await foreach (var response in streaming.WithCancellation(cancellationToken))
            {
                if (response != null)
                {
                    //fullResponse += response.Response;
                    //yield return new ChatResponseDto(chat.Id.Value, response.Response, prompt);

                    fullResponse += response;
                    yield return new ChatResponseDto(chat.Id.Value, response, prompt);
                }
            }


           

            if (!string.IsNullOrWhiteSpace(fullResponse))
            {
                chat.AddChatResponceOnText(fullResponse, prompt);
                await chatRepository.SaveChangesAsync(cancellationToken);
            }
        }

        private async IAsyncEnumerable<string> ConvertToStringsAsync(
            IAsyncEnumerable<OllamaSharp.Models.GenerateResponseStream?> responses)
        {
            await foreach (var response in responses)
            {
                if (response != null)
                {
                    yield return response.Response; 
                }
            }
        }
    }
}
