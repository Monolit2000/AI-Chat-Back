using AudioProcessing.Aplication.Services.Ollama;


namespace AudioProcessing.Aplication.Common.Contract
{
    public interface IOllamaService
    {
        public Task<string> GenerateResponce(OllamaRequest request);
    }
}
