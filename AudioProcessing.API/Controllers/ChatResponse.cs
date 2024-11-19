namespace AudioProcessing.API.Controllers
{
    public class ChatResponse
    {
        public string ChatId { get; set; }
        public IFormFile audioFile { get; set; }
    }
}
