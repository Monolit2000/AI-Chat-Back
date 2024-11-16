using AudioProcessing.Domain.Common;
using AudioProcessing.Domain.Users.Events;

namespace AudioProcessing.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; set; }  

        public string Name { get; set; }

        public string Email { get; set; }

        private User() { }// For Ef Core

        public User( string name, string email)
        {
            Id = new UserId(Guid.NewGuid());
            Name = name;
            Email = email;

            AddDomainEvent(new UserCreatedDomainEvent(Id));
        }

        public static User Create(string name, string email) 
            => new User(name, email);   
    }
}
