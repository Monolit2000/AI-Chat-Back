using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;
using AudioProcessing.Aplication.DTOs;
using AudioProcessing.Aplication.Common.Contract;
using AudioProcessing.Aplication.Common.Models;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponsesByChatId;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChatWithTranscription
{
    public class CreateChatWithTranscriptionCommandHandler(
        IChatRepository chatRepository,
        IAudioProcessingService audioProcessingService) : IRequestHandler<CreateChatWithTranscriptionCommand, Result<ChatResponseDto>>
    {
        public async Task<Result<ChatResponseDto>> Handle(CreateChatWithTranscriptionCommand request, CancellationToken cancellationToken)
        {
            //var transcriptionResult = await audioProcessingService.CreateTranscription(request.AudioStream);

            var transcriptionResult = new AudioTranscriptionResponce($"CreateChatWithTranscriptionCommand {Guid.NewGuid().ToString()}");


            var chat = Chat.Create(new UserId(Guid.NewGuid()), "New сhat");

            chat.AddChatResponceOnText(transcriptionResult.Text);

            await chatRepository.AddAsync(chat, cancellationToken);
            await chatRepository.SaveChangesAsync(cancellationToken); 

            return new ChatResponseDto(chat.Id.Value, transcriptionResult.Text);

        }
    }
}
