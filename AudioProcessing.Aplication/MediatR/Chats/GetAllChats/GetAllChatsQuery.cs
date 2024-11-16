using MediatR;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChats
{
    public class GetAllChatsQuery : IRequest<List<ChatDto>>
    {
    }
}
