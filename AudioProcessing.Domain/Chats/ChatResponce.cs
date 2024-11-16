using AudioProcessing.Domain.Chats.Events;
using AudioProcessing.Domain.Common;

namespace AudioProcessing.Domain.Chats
{
    public class ChatResponce : Entity
    {
        public ChatId ChatId { get; private set; }  
        public AudioId AudioId { get; private set; }        

        public ChatResponceId Id { get; }
        public PromtType PromtType { get; }    
        public string Promt { get; private set; }
        public string Content { get; private set; } 
        public DateTime CreatedAt { get; private set; }

        public ChatResponce() { }

        private ChatResponce(
            ChatId chatId, 
            PromtType promtType, 
            string promt,
            string content,
            AudioId audioId = null)
        {
            ChatId = chatId;    
            Id = new ChatResponceId(Guid.NewGuid());
            Promt = promt;  
            Content = content;
            CreatedAt = DateTime.UtcNow;   
            PromtType = promtType;
            AudioId = audioId;  

            AddDomainEvent(new ChatResponceCreated(this.Id, ChatId));

        }

        public static ChatResponce CreateResponceOnText(ChatId chatId, string promt, string content) 
            => new ChatResponce(chatId, PromtType.Text, promt, content);

        public static ChatResponce CreateResponceOnAudio(ChatId chatId, AudioId audioId, string promt, string content) 
            => new ChatResponce(chatId, PromtType.Audio, promt, content, audioId);
    }
}
