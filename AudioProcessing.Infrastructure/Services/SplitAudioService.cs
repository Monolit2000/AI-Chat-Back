using NAudio.Lame;
using NAudio.Wave;
using AudioProcessing.Aplication.Common.Contract;

namespace AudioProcessing.Infrastructure.Services
{
    public class SplitAudioService : ISplitAudioService
    {
        public async Task<MemoryStream> TrimAudioAsync
            (MemoryStream audioStream, int startSample, int endSample)
        {
            // Переместить указатель потока на начало
            audioStream.Seek(0, SeekOrigin.Begin);

            // Размер каждой выборки аудио данных в байтах (например, 16 бит = 2 байта)
            int bytesPerSample = 2;

            // Позиция начала и конца аудио в байтах
            int startPosition = startSample * bytesPerSample;
            int endPosition = endSample * bytesPerSample;

            // Размер обрезанного аудио в байтах
            int length = endPosition - startPosition;

            // Создать новый MemoryStream для хранения обрезанных данных
            MemoryStream trimmedStream = new MemoryStream();

            // Считать данные аудио из исходного потока
            byte[] audioData = new byte[audioStream.Length];
            audioStream.Read(audioData, 0, audioData.Length);

            // Обрезать данные аудио
            byte[] trimmedData = new byte[length];
            Array.Copy(audioData, startPosition, trimmedData, 0, length);

            // Записать обрезанные данные в новый поток
            await trimmedStream.WriteAsync(trimmedData, 0, trimmedData.Length);

            // Переместить указатель потока на начало
            trimmedStream.Seek(0, SeekOrigin.Begin);

            return trimmedStream;
        }


        private void SaveTrimmedAudioToFile(MemoryStream trimmedStream)
        {
            var path = "C:\\Users\\Andry\\Desktop\\SaveAudio\\TestTrimp.mp3";

            // Записываем содержимое обрезанного потока в файл
            using (FileStream fileStream = File.Create(path))
            {
                trimmedStream.CopyTo(fileStream);
            }

            //using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            //{
            //    // Write a byte array to a file
            //    byte[] bytes = memoryStream.ToArray();
            //    await fileStream.WriteAsync(bytes, 0, bytes.Length);
            //}
        }






        public List<MemoryStream> SplitMp3ToStreamsByTime(Stream fileStream, int timeInterval = 3, bool isSecond = false)
        {
            var list = SplitAudioAsync(new Mp3FileReader(fileStream), timeInterval, isSecond);

            return list;
        }

        public async Task<List<string>> SplitMp3ToStreamsByTime(string inputFile, int timeInterval = 3, bool isSecond = false)
        {
            var list = SplitAudioAsync(new Mp3FileReader(inputFile), timeInterval, isSecond);

            //var test = new Mp3FileReader();

            return new List<string>();

            //return await CreateFile(inputFile, list);
        }


        private List<MemoryStream> SplitAudioAsync(Mp3FileReader mp3FileReader, int timeInterval = 3, bool isSecond = false)
        {
            Console.WriteLine("Start SplitAudioAsync");

            using (var reader = mp3FileReader)
            {
                var list = new List<MemoryStream>();

                int secondsPerBlock = isSecond ? timeInterval : timeInterval * 60;

                int blockSize = reader.WaveFormat.SampleRate * reader.WaveFormat.Channels * secondsPerBlock; // Block size in samples

                while (reader.Position < reader.Length)
                {
                    Console.WriteLine("SplitAudioAsync");
                    var stream = new MemoryStream();
                    using (var writer = new LameMP3FileWriter(stream, reader.WaveFormat, LAMEPreset.VBR_90))
                    {
                        // blockSize / 2 it's just because
                        byte[] buffer = new byte[blockSize / 2 * reader.WaveFormat.BlockAlign];
                        int bytesRead = reader.Read(buffer, 0, buffer.Length);
                        writer.Write(buffer, 0, bytesRead);

                        //stream.Seek(0, SeekOrigin.Begin);
                    }
                    list.Add(stream);
                }

                Console.WriteLine("Separation completed.");
                return list;
            }
        }




        //private async Task<List<string>> CreateFile(string outputFile, List<MemoryStream> streamFile)
        //{

        //    var list = new List<string>();
        //    var fileName = Path.GetFileName(outputFile);

        //    foreach (MemoryStream stream in streamFile)
        //    {
        //        fileName = $"{fileName}_part_{streamFile.IndexOf(stream)}";

        //        var filePath = await _fileService.CreateFileByMemoryStreamAsunc(stream, fileName);

        //        list.Add(filePath);

        //    }
        //    return list;
    }


}

