using AudioProcessing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Domain.Chats
{
    public class ChatId : TypedIdValueBase
    {
        public ChatId(Guid value) 
            : base(value)
        {
        }
    }
}
