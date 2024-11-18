using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;
using AudioProcessing.Aplication.DTOs;
using AudioProcessing.Aplication.Common.Contract;
using AudioProcessing.Aplication.Common.Models;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChatWithTranscription
{
    public class CreateChatWithTranscriptionCommandHandler(
        IChatRepository chatRepository,
        IAudioProcessingService audioProcessingService) : IRequestHandler<CreateChatWithTranscriptionCommand, Result<AudioTranscriptionDTO>>
    {
        public async Task<Result<AudioTranscriptionDTO>> Handle(CreateChatWithTranscriptionCommand request, CancellationToken cancellationToken)
        {
            //var transcriptionResult = await audioProcessingService.CreateTranscription(request.AudioStream);

            var transcriptionResult = new AudioTranscriptionResponce("Tertertest");   

            var chat = Chat.Create(new UserId(Guid.NewGuid()), "New сhat");

            chat.AddChatResponceOnText(transcriptionResult.Text);

            await chatRepository.AddAsync(chat, cancellationToken);
            await chatRepository.SaveChangesAsync(cancellationToken); 

            return new AudioTranscriptionDTO(transcriptionResult.Text);

        }
    }
}
