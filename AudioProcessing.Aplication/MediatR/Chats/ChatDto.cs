

namespace AudioProcessing.Aplication.MediatR.Chats
{
    public class ChatDto
    {
        public Guid ChatId { get; set; }
        public string ChatTitel { get; set; }
        public DateTime CreatedDate { get; set; }

        public ChatDto(
            Guid chatId,
            string chatTitel,
            DateTime createdDate)
        {
            ChatId = chatId;
            ChatTitel = chatTitel;
            CreatedDate = createdDate;
        }
    }
}
