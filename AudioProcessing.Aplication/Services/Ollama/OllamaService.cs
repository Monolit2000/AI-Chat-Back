using OllamaSharp;
using OllamaSharp.Models;
using Microsoft.Extensions.AI;
using AudioProcessing.Aplication.Common.Contract;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Linq;
using NAudio.CoreAudioApi;

namespace AudioProcessing.Aplication.Services.Ollama
{
    public class OllamaService : IOllamaService
    {
        private readonly OllamaApiClient _ollamaApiClient;

        public OllamaService()
        {
            _ollamaApiClient = new OllamaApiClient("http://host.docker.internal:11434");

            _ollamaApiClient.SelectedModel = "llama3.2"; 
        }

        public async Task<string> GenerateTextContentResponce(
            OllamaRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _ollamaApiClient.CompleteAsync(request.Prompt, cancellationToken: cancellationToken);

            string response = string.Join(" ", result.Message.Contents.Select(item => item));

            return response;
        }


        public IAsyncEnumerable<GenerateResponseStream?> GenerateStreameTextContentResponce(OllamaRequest request, CancellationToken cancellationToken = default)
        {
            var result = _ollamaApiClient.GenerateAsync(request.Prompt, cancellationToken: cancellationToken);

            return result;
        }

        public IAsyncEnumerable<string> GenerateStreamingChatResponse(
            OllamaRequest request, 
            IEnumerable<(string Role, string Message)> chatMessages, 
            CancellationToken cancellationToken = default)
        {
            var chat = new Chat(_ollamaApiClient);

            chat.Messages = chatMessages.Select(x => new OllamaSharp.Models.Chat.Message(
                x.Role == "chat" ? OllamaSharp.Models.Chat.ChatRole.Assistant : OllamaSharp.Models.Chat.ChatRole.User, 
                x.Message))
                .ToList();

            var result = chat.SendAsync(request.Prompt);

            return result;
        }

        private async Task<string> SelectFirstLocalModelAsync()
        {
            var localModels = await _ollamaApiClient.ListLocalModelsAsync();

            return localModels.Select(x => x.Name).FirstOrDefault();     
        }
    }

}

public record OllamaRequest(string Prompt);
public record OllamaResponse(string Text);

