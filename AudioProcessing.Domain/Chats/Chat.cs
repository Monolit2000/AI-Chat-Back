using FluentResults;
using AudioProcessing.Domain.Users;
using AudioProcessing.Domain.Common;
using AudioProcessing.Domain.Chats.Events;

namespace AudioProcessing.Domain.Chats
{
    public class Chat : Entity, IAggregateRoot 
    {
        public UserId UserId { get; private set; }
        public readonly List<ChatResponce> ChatResponces = [];

        public ChatId Id { get; }
        public string ChatTitel { get; private set; }    
        public DateTime CreatedDate {  get; private set; }


        public Chat() { } //For Ef Core 
            

        private Chat(
            UserId userId, 
            string chatTitel)
        {
            UserId = userId;
            Id = new ChatId(Guid.NewGuid());
            ChatTitel = chatTitel;
            CreatedDate = DateTime.UtcNow;

            AddDomainEvent(new ChatCreatedDomainEvent(this.Id, userId, chatTitel));
        }

        public static Chat Create(UserId userId,string chatTitel)
            => new Chat(userId, chatTitel);

        public Result<ChatResponceId> AddChatResponceOnText(string content, string promt = default)
        {
            var chatResponse = ChatResponce.CreateResponceOnText(this.Id, promt ?? "None", content);
            ChatResponces.Add(chatResponse);

            return chatResponse.Id;
        }

        public Result UpdateChatResponceOnText(ChatResponceId chatResponceId, string content, string promt = default)
        {
            var chatResponse = ChatResponces.FirstOrDefault(x => x.Id == chatResponceId);

            if(chatResponse is null)
                return Result.Ok();

            chatResponse.UpdateContent(content);

            return Result.Ok();
        }



        public Result AddChatResponceOnAudio(AudioId audioId, string promt, string content)
        {
            var chatResponse = ChatResponce.CreateResponceOnAudio(this.Id, audioId, promt, content);
            ChatResponces.Add(chatResponse);    

            return Result.Ok();
        }

        public Result ChengeChatTitel(string titel)
        {
            ChatTitel = titel;
            return Result.Ok();
        }

        public void SetChatTitel(string chatTitel)
        {
            ChatTitel = chatTitel;

            AddDomainEvent(new ChatTitelSetedDomainEvent(this.Id, ChatTitel));
        }
    }
}
