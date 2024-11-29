using AudioProcessing.Domain.Chats;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.CreateChatWithChatResponse
{
    public class ChatWithChatResponseDto
    {
        public string Prompt { get; set; }
        public string Conetent { get; set; }
        public ChatDto ChatDto { get; set; }

        public ChatWithChatResponseDto(
            ChatDto chatDto,
            string conetent,
            string prompt = null)
        {
            ChatDto = chatDto;
            Prompt = prompt;
            Conetent = conetent;
        }
    }


}
