using AudioProcessing.Aplication.DTOs;
using AudioProcessing.Aplication.MediatR.Chats.GetAllChatResponsesByChatId;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateTrancription
{
    public class CreateTrancriptionCommmand : IRequest<Result<ChatResponseDto>>
    {
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
        public Stream AudioStream { get; set; }

    }

    //public class TranscriptionSetting
    //{
    //    public float Temperature { get; set; } = default;
    //    public string Model { get; set; } = string.Empty;
    //    public string Prompt { get; set; } = string.Empty;
    //    public string FileId { get; set; } = string.Empty;
    //    public string Language { get; set; } = string.Empty;
    //    public string AudioName { get; set; } = string.Empty;
    //}
}
