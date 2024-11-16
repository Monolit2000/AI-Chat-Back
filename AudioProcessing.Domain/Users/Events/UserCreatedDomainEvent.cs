using AudioProcessing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Domain.Users.Events
{
    public class UserCreatedDomainEvent : DomainEventBase
    {
        public UserId UserId { get; set; }

        public UserCreatedDomainEvent(UserId userId)
        {
            UserId = userId;
        }
    }
}
