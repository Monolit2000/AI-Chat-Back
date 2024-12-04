using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Aplication.Common.Models;
using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChatResponseOnText
{
    public class CreateChatResponseOnTextCommandHendler(
        IChatRepository chatRepository,
        IAudioProcessingService transcriptionService,
        IOllamaService ollamaService) : IRequestHandler<CreateChatResponseOnTextCommand, Result<ChatResponseDto>>
    {
        public async Task<Result<ChatResponseDto>> Handle(CreateChatResponseOnTextCommand request, CancellationToken cancellationToken)
        {
            //var transcriptionResult = await transcriptionService.CreateTranscription(request.AudioStream);

            var transcriptionResult = await HendlePromptAsync(request.Promt);

            var prompt = FielterPrompt(request.Promt);


            var chat = await chatRepository.GetByIdAsync(new ChatId(request.ChatId));

            chat.AddChatResponceOnText(transcriptionResult.Text, prompt);
            await chatRepository.SaveChangesAsync(cancellationToken);

            return new ChatResponseDto(chat.Id.Value, transcriptionResult.Text, prompt);
        }


        private async Task<AudioTranscriptionResponce> HendlePromptAsync(string prompt)
        {
            var defaultContent = $"CreateTrancriptionCommmand {Guid.NewGuid().ToString()} +" +
           $" Promt: {(string.IsNullOrWhiteSpace(prompt) ? "None" : prompt)}";

            AudioTranscriptionResponce transcriptionResult;

            if (!string.IsNullOrWhiteSpace(prompt) && prompt.Trim().StartsWith("@"))
            {
                prompt = prompt.Trim().Substring(1).Trim();

                var specialResponse = await ollamaService.GenerateTextContentResponce(new OllamaRequest(prompt));
                transcriptionResult = new AudioTranscriptionResponce(specialResponse);
            }
            else
            {
                transcriptionResult = new AudioTranscriptionResponce(defaultContent);
            }

            return transcriptionResult;
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
