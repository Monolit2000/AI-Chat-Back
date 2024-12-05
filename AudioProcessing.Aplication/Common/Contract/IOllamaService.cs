using AudioProcessing.Aplication.Services.Ollama;
using Microsoft.SemanticKernel;
using OllamaSharp.Models;


namespace AudioProcessing.Aplication.Common.Contract
{
    public interface IOllamaService
    {
        Task<string> GenerateTextContentResponce(OllamaRequest request);

        IAsyncEnumerable<GenerateResponseStream?> GenerateStreameTextContentResponce(OllamaRequest request);
        //Task<string> GenerateChatResponse(OllamaRequest request, IEnumerable<string> chatMessages);
        //IAsyncEnumerable<StreamingChatMessageContent> GenerateStreamingChatResponse(OllamaRequest request, IEnumerable<string> chatMessages);
    }
}
