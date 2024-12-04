using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextGeneration;
using AudioProcessing.Aplication.Common.Contract;
using Atc.SemanticKernel.Connectors.Ollama.Extensions;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AudioProcessing.Aplication.Services.Ollama
{
    public class OllamaService : IOllamaService
    {
        private ITextGenerationService _textGenerationService;
        private IChatCompletionService _chatCompletionService;

        public OllamaService()
        {

            Kernel kernel = Kernel.CreateBuilder()
            .AddOllamaTextGeneration(
                    modelId: "llama3.2",
                    endpoint: "http://host.docker.internal:11434")
            .AddOllamaChatCompletion(
                    modelId: "llama3.2",
                    endpoint: "http://host.docker.internal:11434")
                .Build();

            _textGenerationService = kernel.GetRequiredService<ITextGenerationService>();

            _chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
        }

        public async Task<string> GenerateTextContentResponce(OllamaRequest request)
        {
            var result = await _textGenerationService.GetTextContentsAsync(request.Prompt);

            string response = string.Join(" ", result.Select(item => item.Text));

            return response;
        }

        public async Task<string> GenerateChatResponse(OllamaRequest request, IEnumerable<string> chatMessages)
        {
            var chatHistory = new ChatHistory(chatMessages.Select( x => new ChatMessageContent(AuthorRole.User, x)));

            var result = await _chatCompletionService.GetChatMessageContentsAsync(chatHistory);

            var response = string.Join(" ",  result.Select(item => item.Content)) ?? "Error receiving response";

            return response;
        }
    }

}

public record OllamaRequest(string Prompt);
public record OllamaResponse(string Text);

