﻿using MediatR;
using FluentResults;
using AudioProcessing.Domain.Chats;
using AudioProcessing.Aplication.Common.Models;
using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChatResponseOnText
{
    public class CreateChatResponseOnTextCommandHendler(
        IChatRepository chatRepository,
        IAudioProcessingService transcriptionService) : IRequestHandler<CreateChatResponseOnTextCommand, Result<ChatResponseDto>>
    {
        public async Task<Result<ChatResponseDto>> Handle(CreateChatResponseOnTextCommand request, CancellationToken cancellationToken)
        {
            //var transcriptionResult = await transcriptionService.CreateTranscription(request.AudioStream);

            var defaultContent = $"CreateTrancriptionCommmand {Guid.NewGuid().ToString()} +" +
                $" Promt: {(string.IsNullOrWhiteSpace(request.Promt) ? "None" : request.Promt)}";

            var transcriptionResult = new AudioTranscriptionResponce(defaultContent);

            var chat = await chatRepository.GetByIdAsync(new ChatId(request.ChatId));

            chat.AddChatResponceOnText(transcriptionResult.Text, request.Promt);
            await chatRepository.SaveChangesAsync(cancellationToken);

            return new ChatResponseDto(chat.Id.Value, transcriptionResult.Text);
        }
    }
}