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

            var defaultContent = $"CreateTrancriptionCommmand {Guid.NewGuid().ToString()} +" +
                $" Promt: {(string.IsNullOrWhiteSpace(request.Promt) ? "None" : request.Promt)}";

            AudioTranscriptionResponce transcriptionResult;

            if (!string.IsNullOrWhiteSpace(request.Promt) 
                && (request.Promt.Trim().StartsWith("@o", StringComparison.OrdinalIgnoreCase) 
                || request.Promt.Trim().StartsWith("@")))
            {
                var cleanedPrompt = request.Promt.Trim().Substring(2).Trim();

                var specialResponse = await ollamaService.GenerateResponce(new OllamaRequest(cleanedPrompt));
                transcriptionResult = new AudioTranscriptionResponce(specialResponse);
            }
            else
            {
                transcriptionResult = new AudioTranscriptionResponce(defaultContent);
            }

            var chat = await chatRepository.GetByIdAsync(new ChatId(request.ChatId));

            chat.AddChatResponceOnText(transcriptionResult.Text, request.Promt);
            await chatRepository.SaveChangesAsync(cancellationToken);

            return new ChatResponseDto(chat.Id.Value, transcriptionResult.Text, request.Promt);
        }
    }
}
