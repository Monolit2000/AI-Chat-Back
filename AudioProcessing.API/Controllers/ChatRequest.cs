namespace AudioProcessing.API.Controllers
{
    public class ChatRequest
    {
        public string ChatId { get; set; }
        public IFormFile AudioFile { get; set; }
    }
}
