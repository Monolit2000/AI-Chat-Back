using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Aplication.DTOs;
using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateTrancription
{
    public class CreateTrancriptionCommmandHandler(
        IChatRepository chatRepository,
        IAudioProcessingService transcriptionService) : IRequestHandler<CreateTrancriptionCommmand, Result<AudioTranscriptionDTO>>
    {
        public async Task<Result<AudioTranscriptionDTO>> Handle(CreateTrancriptionCommmand request, CancellationToken cancellationToken)
        {
            var transcriptionResult = await transcriptionService.CreateTranscription(request.AudioStream);

            var chat = await chatRepository.GetByIdAsync(new ChatId(request.ChatId));

            chat.AddChatResponceOnText(transcriptionResult.Text);
            await chatRepository.SaveChangesAsync(cancellationToken);

            return new AudioTranscriptionDTO(transcriptionResult.Text);
        }
    }
}
