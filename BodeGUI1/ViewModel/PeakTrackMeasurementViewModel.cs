using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodeGUI1.ViewModel.Plots;

namespace BodeGUI1.ViewModel
{
    internal class PeakTrackMeasurementViewModel : ViewModelBase
    {
        public PeakTrackMeasurementViewModel()
        {
            BodePlot = new PlotBase();
        }
        private PlotBase _bodePlot;
        public PlotBase BodePlot
        {
            get { return _bodePlot; }
            set { _bodePlot = value; OnPropertyChanged(); }
        }
    }
}
