using AudioProcessing.Aplication.DTOs;
using FluentResults;
using MediatR;
using NAudio.Wave;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChatWithChatResponse
{
    public class CreateChatWithChatResponseCommand : IRequest<Result<ChatWithChatResponseDto>>
    {
        
        public string Promt { get; set; } = default;   

        public Stream? AudioStream { get; set; }

    }
}
