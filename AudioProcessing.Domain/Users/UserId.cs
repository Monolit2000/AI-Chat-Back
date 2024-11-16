using AudioProcessing.Domain.Common;

namespace AudioProcessing.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value)
            : base(value)
        {
        }
    }
}
