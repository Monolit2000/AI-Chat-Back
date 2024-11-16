using AudioProcessing.Aplication.Common.Models;

namespace AudioProcessing.Aplication.Common.Contract
{
    public  interface IBlobService
    {
        Task<UploadFileResponce> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);

        Task<FileResponce> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);

        Task<string> GetFileUrlAsync(Guid fileId, string directory = "", CancellationToken cancellationToken = default);
    }
}
