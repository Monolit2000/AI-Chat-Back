using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using AudioProcessing.Aplication.Common.Models;
using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Infrastructure.Services
{
    public class BlobService(
        IConfiguration _configuration,
        BlobServiceClient _blobServiceClient) : IBlobService
    {

        private const string ContainerName = "Test";

        public async Task<UploadFileResponce> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

            containerClient.CreateIfNotExists();

            var fileId = Guid.NewGuid();
            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);

            var uri = blobClient.Uri.ToString();
            return new UploadFileResponce(ContainerName, fileId, uri);
        }

        public async Task<FileResponce> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            BlobDownloadResult responce = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

            return new FileResponce(responce.Content.ToStream(), responce.Details.ContentType);
        }

        public async Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }

        public async Task SetAccessTierAsync(Guid fileId, AccessTier accessTier, CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.SetAccessTierAsync(accessTier, cancellationToken: cancellationToken);
        }

        public Task<string> GetFileUrlAsync(Guid fileId, string directory = "", CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

            string blobName = string.IsNullOrEmpty(directory) ? fileId.ToString() : $"{directory}/{fileId}";

            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            return Task.FromResult(blobClient.Uri.ToString());
        }
    }
}
