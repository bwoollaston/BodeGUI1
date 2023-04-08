using BodeGUI1.ViewModel.DataModel;
using BodeGUI1.ViewModel.Plots;
using BodeGUI1.ViewModel.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel
{
    internal class ResonanceMeasurementViewModel : ViewModelBase
    {
        public ResonanceMeasurementViewModel()
        {
            BodePlot = new ResonancePlotViewModel();
        }
        private ResonancePlotViewModel _bodePlot;
        public ResonancePlotViewModel BodePlot
        {
            get { return _bodePlot; }
            set { _bodePlot = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ResonanceSweepDataViewModel> _sweepData;
        public ObservableCollection<ResonanceSweepDataViewModel> SweepData
        {
            get { return _sweepData; }
            set { _sweepData = value; OnPropertyChanged(); }
        }
    }
}
