using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public Task SaveFileStreamAsync(Stream stream, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task GetFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
