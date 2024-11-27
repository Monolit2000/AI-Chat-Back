
using AudioProcessing.Aplication.Common.Contract;
using System.Text;
using System.Text.Json;

namespace AudioProcessing.Aplication.Services.Ollama
{
    public class OllamaService : IOllamaService
    {
        private readonly HttpClient _httpClient;

        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateResponce(OllamaRequest request)
        {
            try
            {
                // Сериализация запроса в JSON
                var jsonRequest = JsonSerializer.Serialize(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Логируем отправляемый запрос
                Console.WriteLine($"Sending request to Ollama: http://ollama-service:11434/generate");

                // Отправка POST запроса к Ollama сервису
                var response = await _httpClient.PostAsync("http://ollama-service:11434/api/generate", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error calling Ollama service: {response.StatusCode} - {errorResponse}");
                }

                // Получение и десериализация ответа
                var responseContent = await response.Content.ReadAsStringAsync();
                var ollamaResponse = JsonSerializer.Deserialize<OllamaResponse>(responseContent);

                return ollamaResponse?.Text ?? "No response from Ollama";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
    }

    public record OllamaRequest(string Prompt, string Model = "llama2");
    public record OllamaResponse(string Text);
}
