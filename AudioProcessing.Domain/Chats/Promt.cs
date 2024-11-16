using AudioProcessing.Domain.Common;

namespace AudioProcessing.Domain.Chats
{
    public class Promt : Entity, IAggregateRoot
    {
        public PromtId Id { get;}

        public string UserPromt { get; private set; }

        public ChatResponce ChatResponce { get; set; }


        private Promt()
        {
            Id = new PromtId(Guid.NewGuid()); 
        }
    }
}
