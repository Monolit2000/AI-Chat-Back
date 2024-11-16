using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.Common.Contract
{
    public interface ISplitAudioService
    {
        public Task<MemoryStream> TrimAudioAsync(MemoryStream audioStream, int startSample, int endSample);
    }
}
