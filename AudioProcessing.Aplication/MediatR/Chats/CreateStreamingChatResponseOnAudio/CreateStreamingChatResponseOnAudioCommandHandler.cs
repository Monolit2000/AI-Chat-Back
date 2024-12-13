using AudioProcessing.Aplication.Common.Contract;
using AudioProcessing.Domain.Chats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateStreamingChatResponseOnAudio
{
    public class CreateStreamingChatResponseOnAudioCommandHandler(
        IOllamaService ollamaService,
        IChatRepository chatRepository,
        IAudioProcessingService transcriptionService) : IRequestHandler<CreateStreamingChatResponseOnAudioCommand, ChatResponseOnAudioDto>
    {
        public Task<ChatResponseOnAudioDto> Handle(CreateStreamingChatResponseOnAudioCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
