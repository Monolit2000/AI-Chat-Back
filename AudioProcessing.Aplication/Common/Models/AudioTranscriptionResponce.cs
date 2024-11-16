using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.Common.Models
{
    public class AudioTranscriptionResponce
    {
        public string Text { get; set; }
        public AudioTranscriptionResponce(string text)
        {
            Text = text;
        }
    }
}
