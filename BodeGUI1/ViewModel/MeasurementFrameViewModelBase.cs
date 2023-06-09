using BodeGUI1.ViewModel.DataModel;
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
    internal class MeasurementFrameViewModelBase : ViewModelBase
    {
        public MeasurementFrameViewModelBase()
        {

            Headers = new ObservableCollection<TextBlock>();
            DataTypes = new ObservableCollection<string>() { "Name", "Resonance Frequency [kHz]", "Resonance Impedance [Ω]",
                                                            "Anti-Resonant Frequency [kHz]", "Anti-Resonant Impedance [Ω]",
                                                            "Quality Factor", "Capacitance [pF]","Phase [deg]","Time" };
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
                for (int i = 0; i < _dataTypes.Count; i++)
                {
                    Headers.Add(new TextBlock() { Text = _dataTypes[i], TextWrapping = TextWrapping.Wrap, FontSize = 10, HorizontalAlignment = HorizontalAlignment.Center });
                }
                OnPropertyChanged();
            }
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
                _listWidth = value - 61;
                ColumnWidth = _listWidth / DataTypes.Count;
                OnPropertyChanged(nameof(ListWidth));
            }
        }
    }
}
