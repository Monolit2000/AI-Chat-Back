using AudioProcessing.Domain.Common;
using AudioProcessing.Domain.Users;

namespace AudioProcessing.Domain.Profiles
{
    public class Profile : Entity, IAggregateRoot
    {
        public UserId UserId { get; private set; }  

        public ProfileId Id { get; private set; }   

        public string ProfileName { get; private set; }     

        public DateTime CreatedAt { get; private set; }

        public bool IsDeleted { get; private set; }
        
        public DateTime DeletedAt { get; private set; } 
        public Profile() { } // For Ef Core

        private Profile(UserId userId, string profileName)
        {
            UserId = userId;
            Id = new ProfileId(Guid.NewGuid());
            ProfileName = profileName;
            CreatedAt = DateTime.UtcNow;
        }

        public static Profile Create(UserId userId, string profileName)
            => new Profile(userId, profileName);
    }
}
