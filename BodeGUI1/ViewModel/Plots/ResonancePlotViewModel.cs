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
            Points = new ObservableCollection<DataPoint>();
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        private ObservableCollection<DataPoint> _points;
        public ObservableCollection<DataPoint> Points
        {
            get { return _points; }
            set { _points = value; OnPropertyChanged(); }
        }
    }
}
