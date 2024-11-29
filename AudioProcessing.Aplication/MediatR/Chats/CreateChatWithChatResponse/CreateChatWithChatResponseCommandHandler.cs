using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;
using AudioProcessing.Aplication.Common.Contract;
using AudioProcessing.Aplication.Common.Models;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChatWithChatResponse
{
    public class CreateChatWithChatResponseCommandHandler(
        IChatRepository chatRepository,
        IAudioProcessingService audioProcessingService) : IRequestHandler<CreateChatWithChatResponseCommand, Result<ChatWithChatResponseDto>>
    {
        public async Task<Result<ChatWithChatResponseDto>> Handle(CreateChatWithChatResponseCommand request, CancellationToken cancellationToken)
        {
            //var transcriptionResult = await audioProcessingService.CreateTranscription(request.AudioStream);

            var transcriptionResult = new AudioTranscriptionResponce($"CreateChatWithTranscriptionCommand {Guid.NewGuid().ToString()}");

            var chat = Chat.Create(new UserId(Guid.NewGuid()), "New сhat");

            chat.AddChatResponceOnText(transcriptionResult.Text);

            await chatRepository.AddAsync(chat, cancellationToken);
            await chatRepository.SaveChangesAsync(cancellationToken);

            var chatDto = new ChatDto(
               chat.Id.Value,
               chat.ChatTitel,
               chat.CreatedDate);

            return new ChatWithChatResponseDto(chatDto, transcriptionResult.Text, request.Promt);

        }
    }
}
