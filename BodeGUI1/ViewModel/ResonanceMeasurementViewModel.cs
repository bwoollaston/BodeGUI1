using BodeGUI1.ViewModel.DataModel;
using BodeGUI1.ViewModel.Plots;
using BodeGUI1.ViewModel.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BodeGUI1.ViewModel
{
    internal class ResonanceMeasurementViewModel : ViewModelBase
    {
        public ResonanceMeasurementViewModel()
        {
            ListWidth = 1000;
            Headers = new ObservableCollection<TextBlock>();
            DataTypes = new ObservableCollection<string>() { "Name", "Resonance Frequency [kHz]", "Resonance Impedance [Ω]",
                                                            "Anti-Resonant Frequency [kHz]", "Anti-Resonant Impedance [Ω]",
                                                            "Quality Factor", "Capacitance" };
            BodePlot = new ResonancePlotViewModel();
            SweepData = new ObservableCollection<ResonanceSweepDataViewModel>();
        }
        private ResonancePlotViewModel _bodePlot;
        public ResonancePlotViewModel BodePlot
        {
            get { return _bodePlot; }
            set { _bodePlot = value; OnPropertyChanged(); }
        }
        private ObservableCollection<TextBlock> _headers;
        public ObservableCollection<TextBlock> Headers
        {
            get { return _headers; }
            set { _headers = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _dataTypes;
        public ObservableCollection<string> DataTypes
        {
            get { return _dataTypes; }
            set 
            { 
                _dataTypes = value;
                for(int i = 0;i < _dataTypes.Count; i++)
                {
                    Headers.Add(new TextBlock() { Text = _dataTypes[i], TextWrapping = TextWrapping.Wrap, FontSize=10, HorizontalAlignment=HorizontalAlignment.Center});
                }
                OnPropertyChanged(); 
            }
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
        private double _listWidth;
        public double ListWidth
        {
            get { return _listWidth; }
            set 
            { 
                _listWidth = value-61; 
                ColumnWidth = _listWidth/7;
                OnPropertyChanged(nameof(ListWidth)); 
            } 
        }
    }
}
