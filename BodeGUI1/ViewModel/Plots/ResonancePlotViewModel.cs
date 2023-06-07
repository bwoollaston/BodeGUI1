using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace BodeGUI1.ViewModel.Plots
{
    internal class ResonancePlotViewModel : ViewModelBase
    {
        public ResonancePlotViewModel()
        {
            Title = "Bode Impedance vs. Frequency Plot";
            Impedance = new ObservableCollection<DataPoint>();
            Phase = new ObservableCollection<DataPoint>();
            ImpedanceView = new ObservableCollection<DataPoint>();
            PhaseView = new ObservableCollection<DataPoint>();
            HighX = 190000;
            LowX = 180000;
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        private ObservableCollection<DataPoint> _impedance;
        public ObservableCollection<DataPoint> Impedance
        {
            get { return _impedance; }
            set 
            { 
                _impedance = value; 
                OnPropertyChanged(); 
            }
        }
        private ObservableCollection<DataPoint> _phase;
        public ObservableCollection<DataPoint> Phase
        {
            get { return _phase; }
            set 
            { 
                _phase = value;
                OnPropertyChanged(); 
            }
        }
        private ObservableCollection<DataPoint> _impedanceView;
        public ObservableCollection<DataPoint> ImpedanceView
        {
            get { return _impedanceView; }
            set { _impedanceView = value; OnPropertyChanged(); }
        }
        private ObservableCollection<DataPoint> _phaseView;
        public ObservableCollection<DataPoint> PhaseView
        {
            get { return _phaseView; }
            set { _phaseView = value; OnPropertyChanged(); }
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
        public async void UpdateUI()
        {
            int delay = 2000;
            int dt = delay/Impedance.Count;
            int i = 0;
            foreach(DataPoint element in Impedance)
            {
                ImpedanceView.Add(element);
                PhaseView.Add(Phase[i]);
                await Task.Delay(dt);
                i++;
            }
        }
    }
}
