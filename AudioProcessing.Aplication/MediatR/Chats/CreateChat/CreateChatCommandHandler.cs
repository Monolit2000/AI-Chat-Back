using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;
using AudioProcessing.Aplication.Common.Models;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChat
{
    public class CreateChatCommandHandler(
        IChatRepository chatRepository) : IRequestHandler<CreateChatCommand, Result<ChatDto>>
    {
        public async Task<Result<ChatDto>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            //var transcriptionResult = await audioProcessingService.CreateTranscription(request.AudioStream);

            var chat = Chat.Create(new UserId(Guid.NewGuid()), "New сhat");

            await chatRepository.AddAsync(chat, cancellationToken);
            await chatRepository.SaveChangesAsync(cancellationToken);

            var chatDto = new ChatDto(
                chat.Id.Value, 
                chat.ChatTitel, 
                chat.CreatedDate);

            return chatDto;
        }
    }
}
