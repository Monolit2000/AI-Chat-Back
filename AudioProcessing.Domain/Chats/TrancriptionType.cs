using AudioProcessing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Domain.Chats
{
    public class TrancriptionType : ValueObject
    {
        public static TrancriptionType FTP = new TrancriptionType(nameof(FTP));


        public string Value { get; set; }

        private static readonly HashSet<string> ValidStatuses = new HashSet<string>
        {
            nameof(FTP),
        };

        private TrancriptionType(string value)
            => Value = value;

        public static TrancriptionType Create(string value)
            => new TrancriptionType(value);


        //public static Result<ConfirmationStatus> Create(string value)
        //{
        //    if (!ValidStatuses.Contains(value))
        //        return Result.Fail($"Invalid status Id: {value}");

        //    return new ConfirmationStatus(value);
        //}

    }
}
