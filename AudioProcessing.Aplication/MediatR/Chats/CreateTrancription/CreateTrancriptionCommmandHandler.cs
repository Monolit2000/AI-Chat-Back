using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Aplication.DTOs;
using AudioProcessing.Aplication.Common.Models;
using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateTrancription
{
    public class CreateTrancriptionCommmandHandler(
        IChatRepository chatRepository,
        ISplitAudioService splitAudioService,
        IAudioTranscriptionService transcriptionService) : IRequestHandler<CreateTrancriptionCommmand, Result<AudioTranscriptionDTO>>
    {
        public async Task<Result<AudioTranscriptionDTO>> Handle(CreateTrancriptionCommmand request, CancellationToken cancellationToken)
        {
            var transcriptionResult = await CreateTranscription(request.AudioStream);

            var chat = await chatRepository.GetByIdAsync(new ChatId(request.ChatId));

            chat.AddChatResponceOnText(transcriptionResult.Text);
            await chatRepository.SaveChangesAsync(cancellationToken);

            return new AudioTranscriptionDTO(transcriptionResult.Text);
        }


        private async Task<AudioTranscriptionResponce> CreateTranscription(Stream audioStream/*MemoryStream trimmedAudio*/)
        {
            var trimmedAudio = await ProcessAudioStream(audioStream);

            var transcriptionRequest = new AudioTranscriptionRequest { AudioStream = trimmedAudio };

            var content = await transcriptionService.TranscribeAudioAcync(transcriptionRequest);
            return content;
        }


        private async Task<MemoryStream> ProcessAudioStream(Stream audioStream, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
                await audioStream.CopyToAsync(memoryStream, cancellationToken);
                memoryStream.Seek(0, SeekOrigin.Begin);

                int startPosition = (int)(memoryStream.Length / 4);
                int endPosition = (int)(memoryStream.Length / 2);

                return await splitAudioService.TrimAudioAsync(memoryStream, startPosition, endPosition);
            }
        }


    }
}

//using MediatR;
//using FluentResults;
//using AudioProcessing.Domain.Chats;
//using AudioProcessing.Aplication.DTOs;
//using AudioProcessing.Aplication.Common.Models;
//using AudioProcessing.Aplication.Common.Contract;
//using AudioProcessing.Domain.Users;

//namespace AudioProcessing.Aplication.MediatR.Commands.CreateTrancription
//{
//    public class CreateTrancriptionCommmandHandler(
//        IChatRepository chatRepository,
//        ISplitAudioService splitAudioService,
//        IAudioTranscriptionService _transcriptionService) : IRequestHandler<CreateTrancriptionCommmand, Result<AudioTranscriptionDTO>>
//    {
//        public async Task<Result<AudioTranscriptionDTO>> Handle(CreateTrancriptionCommmand request, CancellationToken cancellationToken)
//        {
//            using (MemoryStream memoryStream = new MemoryStream())
//            {
//                await request.AudioStream.CopyToAsync(memoryStream);

//                memoryStream.Seek(0, SeekOrigin.Begin);

//                var lenghts = memoryStream.Length;

//                var trimAudio = await splitAudioService.TrimAudioAsyncAndSave(
//                    memoryStream,
//                    (int)(memoryStream.Length / 4),
//                    (int)memoryStream.Length / 2);

//                var transcriptionResult = await _transcriptionService.RecognizeAudioAcync(
//                    new AudioTranscriptionRequest { AudioStream = trimAudio });

//                var chat = Chat.Create(new UserId(Guid.NewGuid()), "New Chat");

//                chat.AddTrancription(transcriptionResult.Text);

//                await chatRepository.AddAsync(chat, cancellationToken);
//                await chatRepository.SaveChangesAsync(cancellationToken);

//                var responce = new AudioTranscriptionDTO(transcriptionResult.Text);

//                return responce;
//            }
//        }
//    }
//}
