using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace BodeGUI1.ViewModel.Plots
{
    internal class ResonancePlotViewModel : PlotViewModelBase
    {
        public ResonancePlotViewModel()
        {
            Title = "Bode Impedance vs. Frequency Plot";
            Impedance = new ObservableCollection<DataPoint>();
            Phase = new ObservableCollection<DataPoint>();
            ImpedanceView = new ObservableCollection<DataPoint>();
            PhaseView = new ObservableCollection<DataPoint>();
            ImpedanceHistory = new ObservableCollection<ObservableCollection<DataPoint>>();
            PhaseHistory = new ObservableCollection<ObservableCollection<DataPoint>>();
            HighX = 190000;
            LowX = 180000;
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

        public async void UpdateUI()
        {
            Impedance = ImpedanceHistory[ImpedanceHistory.Count-1];
            Phase = PhaseHistory[PhaseHistory.Count-1];
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
