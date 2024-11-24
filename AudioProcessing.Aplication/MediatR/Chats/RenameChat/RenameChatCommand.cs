using FluentResults;
using MediatR;


namespace AudioProcessing.Aplication.MediatR.Chats.RenameChat
{
    public class RenameChatCommand : IRequest<Result>
    {
        public Guid ChatId { get; set; }
        public string NewName { get; set; }
    }
}
