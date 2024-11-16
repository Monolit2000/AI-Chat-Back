using Google.Apis.Download;
using Google.Apis.Services;
using System.Text.RegularExpressions;
using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Infrastructure.Services
{
    public class DriveService : IDriveService
    {
        public async Task<MemoryStream> DownloadAudio(string url)
        {
            var fileId = ExtractFileId(url);

            await Console.Out.WriteLineAsync(fileId);

            try
            {
                // Create Drive API service.
                var service = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer
                {
                    ApiKey = "AIzaSyCIMiGWlIyPgSO1kyvku1EZWuDumzMCeQY",
                    ApplicationName = "VoiceSynthesizer17"
                });

                var request = service.Files.Get(fileId);
                // var file = request.Execute();

                var stream = new MemoryStream();

                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("DownloadAudio complete.");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("DownloadAudio failed.");
                                    break;
                                }
                        }
                    };

                await request.DownloadAsync(stream).ConfigureAwait(false);

                stream.Seek(0, SeekOrigin.Begin);

                return stream;
            }
            catch (Exception e)
            {
                // TODO(developer) - handle error appropriately
                if (e is AggregateException)
                {
                    Console.WriteLine("Credential Not found");
                }
                else
                {
                    throw;
                }
            }
            return null;
        }

        private string ExtractFileId(string url)
        {
            string pattern = @"(?:drive\.google\.com\/(?:file\/d\/|open\?id=|drive\/folders\/|drive\/u\/\d\/folders\/|drive\/u\/\d\/files\/d\/))(.*?)(?:\/|$|\?|&|\s)";

            Regex regex = new Regex(pattern);
            Match match = regex.Match(url);

            if (match.Success)
                return match.Groups[1].Value;

            else
            {
                // If it was not possible to restore file ID
                Console.WriteLine("Unable to extract File ID from the URL.");
                return null;
            }
        }
    }
}
