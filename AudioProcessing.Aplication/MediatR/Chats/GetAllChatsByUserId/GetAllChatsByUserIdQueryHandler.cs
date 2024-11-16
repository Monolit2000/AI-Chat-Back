using MediatR;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChatsByUserId
{
    public class GetAllChatsByUserIdQueryHandler(
        IChatRepository chatRepository) : IRequestHandler<GetAllChatsByUserIdQuery, List<ChatDto>>
    {
        public async Task<List<ChatDto>> Handle(GetAllChatsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var chats = await chatRepository.GetAllByUserIdAsync(new UserId(request.UserId), cancellationToken);

            if (!chats.Any())
                return new List<ChatDto>();

            var chatsDtos = chats.Select(x => new ChatDto(x.ChatTitel, x.CreatedDate)).ToList();

            return chatsDtos;
        }
    }
}
