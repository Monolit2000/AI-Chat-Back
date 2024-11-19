using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Aplication.DTOs;
using AudioProcessing.Aplication.Common.Contract;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponsesByChatId;
using AudioProcessing.Aplication.Common.Models;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateTrancription
{
    public class CreateTrancriptionCommmandHandler(
        IChatRepository chatRepository,
        IAudioProcessingService transcriptionService) : IRequestHandler<CreateTrancriptionCommmand, Result<ChatResponseDto>>
    {
        public async Task<Result<ChatResponseDto>> Handle(CreateTrancriptionCommmand request, CancellationToken cancellationToken)
        {
            //var transcriptionResult = await transcriptionService.CreateTranscription(request.AudioStream);

            var transcriptionResult = new AudioTranscriptionResponce($"CreateTrancriptionCommmand {Guid.NewGuid().ToString()}");

            var chat = await chatRepository.GetByIdAsync(new ChatId(request.ChatId));

            chat.AddChatResponceOnText(transcriptionResult.Text);
            await chatRepository.SaveChangesAsync(cancellationToken);

            return new ChatResponseDto(chat.Id.Value, transcriptionResult.Text);
        }
    }
}
