using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Chats.GetAllChatsByUserId
{
    public class GetAllChatsByUserIdQuery : IRequest<List<ChatDto>>
    {
        public Guid UserId { get; set; }
    }
}
