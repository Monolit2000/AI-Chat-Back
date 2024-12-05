using OllamaSharp;
using OllamaSharp.Models;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextGeneration;
using Microsoft.SemanticKernel.ChatCompletion;
using AudioProcessing.Aplication.Common.Contract;
using Atc.SemanticKernel.Connectors.Ollama.Extensions;

namespace AudioProcessing.Aplication.Services.Ollama
{
    public class OllamaService : IOllamaService
    {
        private ITextGenerationService _textGenerationService;
        private IChatCompletionService _chatCompletionService;
        private readonly OllamaApiClient _ollamaApiClient;

        public OllamaService()
        {
            Kernel kernel = Kernel.CreateBuilder()
            .AddOllamaTextGeneration(
                    modelId: "phi3",
                    endpoint: "http://host.docker.internal:11434")
            .AddOllamaChatCompletion(
                    modelId: "phi3",
                    endpoint: "http://host.docker.internal:11434")
                .Build();

            _textGenerationService = kernel.GetRequiredService<ITextGenerationService>();

            _chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

            _ollamaApiClient = new OllamaApiClient("http://host.docker.internal:11434");

            _ollamaApiClient.SelectedModel = "phi3";
        }

        public async Task<string> GenerateTextContentResponce(OllamaRequest request)
        {
            //var result = await _textGenerationService.GetTextContentsAsync(request.Prompt);
            var result = await _ollamaApiClient.CompleteAsync(request.Prompt);

            string response = string.Join(" ", result.Message.Contents.Select(item => item));

            return response;
        }


        public IAsyncEnumerable<GenerateResponseStream?> GenerateStreameTextContentResponce(OllamaRequest request)
        {
            var result = _ollamaApiClient.GenerateAsync(request.Prompt);

            return result;
        }


        //public async Task<string> GenerateChatResponse(OllamaRequest request, IEnumerable<string> chatMessages)
        //{
        //    var chatHistory = new ChatHistory(chatMessages.Select( x => new ChatMessageContent(AuthorRole.User, x)));

        //    var result = await _chatCompletionService.GetChatMessageContentsAsync(chatHistory);

        //    var response = string.Join(" ",  result.Select(item => item.Content)) ?? "Error receiving response";

        //    return response;
        //}

        //public IAsyncEnumerable<StreamingChatMessageContent> GenerateStreamingChatResponse(OllamaRequest request, IEnumerable<string> chatMessages)
        //{
        //    var chatHistory = new ChatHistory(chatMessages.Select(x => new ChatMessageContent(AuthorRole.User, x)));

        //    var result = _chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory);
            
        //    return result;
        //}
    }

}

public record OllamaRequest(string Prompt);
public record OllamaResponse(string Text);

