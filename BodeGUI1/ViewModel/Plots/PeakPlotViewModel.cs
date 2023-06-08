using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel.Plots
{
    internal class PeakPlotViewModel : PlotViewModelBase
    { 
        public PeakPlotViewModel()
        {
            Title = "Peak Tracking Plot";
            HighX = 1e6;
            LowX = 1000;
        }
    }
}
