using AudioProcessing.Aplication.Services.Ollama;


namespace AudioProcessing.Aplication.Common.Contract
{
    public interface IOllamaService
    {
        Task<string> GenerateTextContentResponce(OllamaRequest request);
        Task<string> GenerateChatResponse(OllamaRequest request, IEnumerable<string> chatMessages);
    }
}
