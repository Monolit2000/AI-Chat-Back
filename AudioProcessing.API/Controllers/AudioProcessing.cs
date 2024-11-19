using MediatR;
using Microsoft.AspNetCore.Mvc;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChats;
using AudioProcessing.Aplication.MediatR.Chats.CreateTrancription;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChatsByUserId;
using AudioProcessing.Aplication.MediatR.Chats.CreateChatWithTranscription;
using AudioProcessing.Aplication.MediatR.Chats.CreateChat;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponsesByChatId;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponses;
using AudioProcessing.Domain.Chats;

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

        [HttpPost("createTrancription")]
        [RequestSizeLimit(100000000)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateTrancription([FromForm] ChatRequest chatRequest/*, Guid userId*/)
        {

            var command = new CreateTrancriptionCommmand
            {
                ChatId =  Guid.Parse(chatRequest.ChatId),
                AudioStream = chatRequest.AudioFile.OpenReadStream()
            };

            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);   
        } 
        
        [HttpPost("createChatWithTranscription")]
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


        [HttpPost("CreateChat")]
        public async Task<IActionResult> CreateChat()
        {

            var result = await _mediator.Send(new CreateChatCommand());

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }


        [HttpGet("GetAllChatsByUserId")]
        public async Task<IActionResult> GetAllChatsByUserId([FromQuery] GetAllChatsByUserIdQuery getAllChatsByUserIdQuery)
        {
            var rsult = await _mediator.Send(getAllChatsByUserIdQuery);

            return Ok(rsult);   
        }


        [HttpGet("getAllChatResponsesByChatId/{chatId}")]
        public async Task<IActionResult> GetAllChatsByUserId(string chatId)
        {
            var result = await _mediator.Send(new GetAllChatResponsesByChatIdQuery() { ChatId = Guid.Parse(chatId)});

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);   
        }

        [HttpGet("getAllChatResponses")]
        public async Task<IActionResult> GetAllChatsByUserId()
        {
            var result = await _mediator.Send(new GetAllChatResponsesQuery());

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);   
        }




        [HttpGet("GetAllChats")]
        public async Task<IActionResult> GetAllChats()
        {
            var rsult = await _mediator.Send(new GetAllChatsQuery());

            return Ok(rsult);   
        } 
    }
}
