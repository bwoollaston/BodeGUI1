using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel.Plots
{
    internal class PlotViewModelBase : ViewModelBase
    {
        public PlotViewModelBase()
        {
            Title = "Title";
            HighX = 4e6;
            LowX = 100;
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        private double _lowX;
        public double LowX
        {
            get { return _lowX; }
            set { _lowX = value; OnPropertyChanged(); }
        }
        private double _highX;
        public double HighX
        {
            get { return _highX; }
            set { _highX = value; OnPropertyChanged(); }
        }
        private ObservableCollection<DataPoint> _impedance;
        public ObservableCollection<DataPoint> Impedance
        {
            get { return _impedance; }
            set { _impedance = value; OnPropertyChanged(); }
        }
        private ObservableCollection<DataPoint> _phase;
        public ObservableCollection<DataPoint> Phase
        {
            get { return _phase; }
            set { _phase = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ObservableCollection<DataPoint>> _impedanceHistory;
        public ObservableCollection<ObservableCollection<DataPoint>> ImpedanceHistory
        {
            get { return _impedanceHistory; }
            set
            {
                _impedanceHistory = value;
                //make most recent measurment display
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ObservableCollection<DataPoint>> _phaseHistory;
        public ObservableCollection<ObservableCollection<DataPoint>> PhaseHistory
        {
            get { return _phaseHistory; }
            set
            {
                _phaseHistory = value;
                OnPropertyChanged();
            }
        }
    }
}
