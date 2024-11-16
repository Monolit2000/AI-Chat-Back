using AudioProcessing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Domain.Chats
{
    public class AudioId : TypedIdValueBase
    {
        public AudioId(Guid value) 
            : base(value)
        {
        }
    }
}
