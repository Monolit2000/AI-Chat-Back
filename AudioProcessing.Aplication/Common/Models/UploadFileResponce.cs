using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.Common.Models
{
    public record UploadFileResponce(string Conteiner, Guid FileId, string Uri, string Directory = default);
}
