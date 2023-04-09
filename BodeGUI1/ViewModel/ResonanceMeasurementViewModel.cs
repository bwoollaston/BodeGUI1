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
            ColumnWidth = 40;
            DataTypes = new ObservableCollection<string>() { "Name", "Resonance Frequency [kHz]", "Resonance Impedance [Ω]",
                                                            "Anti-Resonant Frequency [kHz]", "Anti-Resonant Impedance [Ω]",
                                                            "Quality Factor", "Capacitance" };
            BodePlot = new ResonancePlotViewModel();
        }
        private ResonancePlotViewModel _bodePlot;
        public ResonancePlotViewModel BodePlot
        {
            get { return _bodePlot; }
            set { _bodePlot = value; OnPropertyChanged(); }
        }
        private string _sampleName;
        public string SampleName
        {
            get { return _sampleName; }
            set { _sampleName = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _dataTypes;
        public ObservableCollection<string> DataTypes
        {
            get { return _dataTypes; }
            set { _dataTypes = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ResonanceSweepDataViewModel> _sweepData;
        public ObservableCollection<ResonanceSweepDataViewModel> SweepData
        {
            get { return _sweepData; }
            set { _sweepData = value; OnPropertyChanged(); }
        }
        private double _columnWidth;
        public double ColumnWidth
        {
            get { return _columnWidth; }
            set { _columnWidth = value; OnPropertyChanged(); }
        }
    }
}
