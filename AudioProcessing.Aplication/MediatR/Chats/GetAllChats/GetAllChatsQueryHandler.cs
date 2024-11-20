using MediatR;
using AudioProcessing.Domain.Chats;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChats
{
    public class GetAllChatsQueryHandler(
        IChatRepository chatRepository) : IRequestHandler<GetAllChatsQuery, List<ChatDto>>
    {
        public async Task<List<ChatDto>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
        {
            var chats = await chatRepository.GetAllAsync(cancellationToken);

            if (!chats.Any())
                return new List<ChatDto>();

            var chatsDtos = chats
                .OrderByDescending(x => x.CreatedDate) 
                .Select(x => new ChatDto(x.Id.Value, x.ChatTitel, x.CreatedDate))
                .ToList();

            return chatsDtos;
        }
    }
}
