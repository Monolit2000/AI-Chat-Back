using AudioProcessing.Domain.Common;

namespace AudioProcessing.Domain.Chats
{
    public class PromtType : ValueObject
    {
        public static PromtType Audio = new PromtType(nameof(Audio));
        public static PromtType Text = new PromtType(nameof(Text));
        public string Value { get; }

        public PromtType(string value)
        {
            Value = value;  
        }
    }
}
