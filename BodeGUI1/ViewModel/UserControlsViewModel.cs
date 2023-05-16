using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel
{
    public class UserControlsViewModel
    {
        public UserControlsViewModel()
        {
            LowFreqRange = 180000;
            HighFreqRange = 190000;
            BandWidth = 100;
            Name = "Sample Name";
            Indexing = false;
        }
        public double LowFreqRange { get; set; }
        public double HighFreqRange { get; set;}
        public double BandWidth { get; set; }
        public string Name { get; set; }
        public bool Indexing { get; set; }
    }
}
