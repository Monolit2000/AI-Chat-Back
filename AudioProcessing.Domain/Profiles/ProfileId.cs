using AudioProcessing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Domain.Profiles
{
    public class ProfileId : TypedIdValueBase
    {
        public ProfileId(Guid value)
            : base(value)
        {
        }
    }
}
