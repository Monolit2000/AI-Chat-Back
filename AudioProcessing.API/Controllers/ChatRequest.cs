namespace AudioProcessing.API.Controllers
{
    public class ChatRequest
    {
        public string ChatId { get; set; }
        public IFormFile AudioFile { get; set; }

        public string? Promt { get; set; }
    }

    public class DeleteChatRequest
    {
        public Guid ChatId { get; set; }
    }



}
