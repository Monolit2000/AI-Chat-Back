using AudioProcessing.Aplication.DTOs;
using FluentResults;
using MediatR;
using NAudio.Wave;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChatWithTranscription
{
    public class CreateChatWithTranscriptionCommand : IRequest<Result<AudioTranscriptionDTO>>
    {
        public Guid UserId { get; set; }

        public string Promt { get; set; } = default;   

        public Stream AudioStream { get; set; }

    }
}
