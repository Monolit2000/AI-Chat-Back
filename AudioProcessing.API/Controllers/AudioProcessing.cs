using MediatR;
using Microsoft.AspNetCore.Mvc;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChats;
using AudioProcessing.Aplication.MediatR.Chats.CreateTrancription;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChatsByUserId;
using AudioProcessing.Aplication.MediatR.Chats.CreateChatWithTranscription;

namespace AudioProcessing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AudioProcessing : ControllerBase
    {
        private IMediator _mediator;

        public AudioProcessing(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpPost("CreateTrancription")]
        [RequestSizeLimit(100000000)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateTrancription(IFormFile audioFile, Guid ChatId, Guid UserId)
        {
            var rsult = await _mediator.Send(new CreateTrancriptionCommmand { AudioStream = audioFile.OpenReadStream()});

            return Ok(rsult);   
        } 
        
        [HttpPost("CreateChatWithTranscription")]
        [RequestSizeLimit(100000000)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateChatWithTranscription(IFormFile audioFile)
        {
            var command = new CreateChatWithTranscriptionCommand 
            { 
                UserId = Guid.NewGuid(),
                AudioStream = audioFile.OpenReadStream()
            };

            var rsult = await _mediator.Send(command);
            return Ok(rsult.Value);   
        }



        [HttpPost("CreateChatWithTranscriptionV2")]

        public async Task<IActionResult> CreateChatWithTranscriptionV2(IFormFile audioFile)
        {
            return Ok();
        }


        [HttpGet("GetAllChatsByUserId")]
        public async Task<IActionResult> GetAllChatsByUserId([FromQuery] GetAllChatsByUserIdQuery getAllChatsByUserIdQuery)
        {
            var rsult = await _mediator.Send(getAllChatsByUserIdQuery);

            return Ok(rsult);   
        }

        [HttpGet("GetAllChats")]
        public async Task<IActionResult> GetAllChats()
        {
            var rsult = await _mediator.Send(new GetAllChatsQuery());

            return Ok(rsult);   
        } 
    }
}
