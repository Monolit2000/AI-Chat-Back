using MediatR;
using Microsoft.AspNetCore.Mvc;
using AudioProcessing.Aplication.MediatR.Chats.CreateChat;
using AudioProcessing.Aplication.MediatR.Chats.DeleteChat;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChats;
using AudioProcessing.Aplication.MediatR.Chats.CreateTrancription;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChatsByUserId;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponses;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponsesByChatId;
using AudioProcessing.Aplication.MediatR.Chats.CreateChatWithChatResponse;
using AudioProcessing.Aplication.MediatR.Chats.CreateChatResponseOnText;
using AudioProcessing.Aplication.MediatR.Chats.ChegeChatTitel;
using AudioProcessing.Aplication.Services.Ollama;
using AudioProcessing.Aplication.Common.Contract;
using Newtonsoft.Json;
using System.Net.Http;

namespace AudioProcessing.API.Controllers
{

    public class ModelResponse
    {
        public string[] Models { get; set; }
    }



    [ApiController]
    [Route("[controller]")]
    public class AudioProcessing : ControllerBase
    {
        private IMediator _mediator;
        private IOllamaService _ollamaService;
        private HttpClient _httpClient;

        public AudioProcessing(IMediator mediator, IOllamaService ollamaService, HttpClient httpClient)
        {
            _mediator = mediator;   
            _ollamaService = ollamaService;
            _httpClient = httpClient;
        }


        [HttpGet("models")]
        public async Task<IActionResult> GetModels()
        {
            try
            {
                // Отправка GET запроса к серверу Ollama
                var response = await _httpClient.GetAsync("http://localhost:11434/models");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    // Преобразуем ответ в список моделей (например, предполагаем JSON-формат с массивом моделей)
                    var models = JsonConvert.DeserializeObject<ModelResponse>(content);

                    return Ok(models); // Возвращаем список моделей
                }

                return StatusCode((int)response.StatusCode, "Ошибка при получении моделей");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
            }
        }


        [HttpPost("generate-response")]
        public async Task<IActionResult> GenerateResponse([FromBody] OllamaRequest request)
        {
            var responseText = await _ollamaService.GenerateResponce(request);
            return Ok(new { Response = responseText });
        }



        [HttpPost("createChatResponseOnText")]
        public async Task<IActionResult> CreateChatResponseOnText([FromBody] CreateChatResponseOnTextCommand createChatResponseOnTextCommand/*, Guid userId*/)
        {
            var result = await _mediator.Send(createChatResponseOnTextCommand);

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);
        }


        [HttpPost("createTrancription")]
        [RequestSizeLimit(100000000)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateTrancription([FromForm] ChatRequest chatRequest/*, Guid userId*/)
        {

            var command = new CreateTrancriptionCommmand
            {
                ChatId =  Guid.Parse(chatRequest.ChatId),
                AudioStream = chatRequest.AudioFile.OpenReadStream(),
                Promt = chatRequest.Promt
            };

            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);   
        } 

        
        [HttpPost("createChatWithChatResponse")]
        [RequestSizeLimit(100000000)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateChatWithChatResponseCommand([FromForm] CreateChatWithChatResponsReques createChatWithChatResponsRequest/*, Guid userId*/)
        {

            var command = new CreateChatWithChatResponseCommand
            {
                AudioStream = createChatWithChatResponsRequest.AudioFile?.OpenReadStream(),
                Promt = createChatWithChatResponsRequest.Promt
            };

            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);   
        } 






        
        //[HttpPost("createChatWithTranscription")]
        //[RequestSizeLimit(100000000)]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> CreateChatWithTranscription(IFormFile audioFile)
        //{
        //    var command = new CreateChatWithChatResponseCommand 
        //    { 
        //        AudioStream = audioFile.OpenReadStream()
        //    };

        //    var rsult = await _mediator.Send(command);
        //    return Ok(rsult.Value);   
        //}



        [HttpPost("renameChat")]
        public async Task<IActionResult> RenameChat([FromBody] ChegeChatTitelCommand chegeChatTitelCommand/*, Guid userId*/)
        {
            var result = await _mediator.Send(chegeChatTitelCommand);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }


        [HttpPost("createNewChat")]
        public async Task<IActionResult> CreateChat()
        {
            var result = await _mediator.Send(new CreateChatCommand());

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);
        }

        [HttpPost("deleteChat")]
        public async Task<IActionResult> DeleteChat([FromBody] DeleteChatRequest deleteChatRequest)
        {
            var result = await _mediator.Send(new DeleteChatCommand { ChatId = deleteChatRequest.ChatId/*Guid.Parse(chatId)*/ });

            if (result.IsFailed)
                return BadRequest();

            return Ok(result);
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
