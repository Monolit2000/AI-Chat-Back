using AudioProcessing.Aplication.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.Common.Contract
{
    public interface IFileService
    {
        public Task SaveFileStreamAsync(Stream stream, string fileName);

        public Task DeleteFileAsync(string fileName);

        public Task GetFileAsync(string fileName);
    }
}
