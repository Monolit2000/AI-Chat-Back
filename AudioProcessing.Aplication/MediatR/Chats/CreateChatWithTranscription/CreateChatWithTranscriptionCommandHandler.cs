using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Domain.Users;
using AudioProcessing.Aplication.DTOs;
using AudioProcessing.Aplication.Common.Models;
using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChatWithTranscription
{
    public class CreateChatWithTranscriptionCommandHandler(
        IChatRepository chatRepository,
        ISplitAudioService splitAudioService,
        IAudioTranscriptionService audioTranscriptionService) : IRequestHandler<CreateChatWithTranscriptionCommand, Result<AudioTranscriptionDTO>>
    {
        public async Task<Result<AudioTranscriptionDTO>> Handle(CreateChatWithTranscriptionCommand request, CancellationToken cancellationToken)
        {
            //var transcriptionResult = await CreateTranscription(request.AudioStream);

            var transcriptionResult = new AudioTranscriptionResponce("Test");   

            var chat = Chat.Create(new UserId(Guid.NewGuid()), "New сhat");

            chat.AddChatResponceOnText(transcriptionResult.Text);

            await chatRepository.AddAsync(chat, cancellationToken);

            await chatRepository.SaveChangesAsync(cancellationToken); 

            return new AudioTranscriptionDTO(transcriptionResult.Text);

        }


        private async Task<AudioTranscriptionResponce> CreateTranscription(Stream audioStream/*MemoryStream trimmedAudio*/)
        {
            var trimmedAudio = await ProcessAudioStream(audioStream);

            var transcriptionRequest = new AudioTranscriptionRequest
            {
                AudioStream = trimmedAudio
            };

            var content = await audioTranscriptionService.TranscribeAudioAcync(transcriptionRequest);
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
