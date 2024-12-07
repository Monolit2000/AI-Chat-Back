using AudioProcessing.Aplication.Services.Ollama;
using Microsoft.SemanticKernel;
using OllamaSharp.Models;


namespace AudioProcessing.Aplication.Common.Contract
{
    public interface IOllamaService
    {
        Task<string> GenerateTextContentResponce(OllamaRequest request, CancellationToken cancellationToken = default);
        IAsyncEnumerable<GenerateResponseStream?> GenerateStreameTextContentResponce(OllamaRequest request, CancellationToken cancellationToken = default);
        IAsyncEnumerable<string> GenerateStreamingChatResponse(OllamaRequest request, IEnumerable<(string Role, string Message)> chatMessages, CancellationToken cancellationToken = default);

        //Task<string> GenerateChatResponse(OllamaRequest request, IEnumerable<string> chatMessages);
    }
}
