using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Impedance = new ObservableCollection<DataPoint>();
            Phase = new ObservableCollection<DataPoint>();
            ImpedanceView = new ObservableCollection<DataPoint>();
            PhaseView = new ObservableCollection<DataPoint>();
            ImpedanceHistory = new ObservableCollection<ObservableCollection<DataPoint>>();
            PhaseHistory = new ObservableCollection<ObservableCollection<DataPoint>>();
            SmoothPts = new ObservableCollection<DataPoint>();
            HighX = 1e6;
            LowX = 1000;
        }
        private int _numPoints;
        public int NumPoints
        {
            get { return _numPoints; }
            set { _numPoints = value; }
        }
        private ObservableCollection<DataPoint> _smoothPts;
        public ObservableCollection<DataPoint> SmoothPts
        {
            get { return _smoothPts; }
            set { _smoothPts = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ObservableCollection<DataPoint>> _points;
        public ObservableCollection<ObservableCollection<DataPoint>> Points
        {
            get { return _points; }
            set { _points = value; OnPropertyChanged(); }
        }
        public void SmoothData()
        {
            SmoothPts.Clear();
            int dp = 3;
            for(int i = 0; i < Impedance.Count; i += dp)
            {
                double x = 0;
                double y = 0;
                for(int j=0; j < dp; j++)
                {
                    x += Impedance[i + j].X;
                    y += Impedance[i + j].Y;
                }
                x = x / dp;
                y = y / dp;
                SmoothPts.Add(new DataPoint(x,y));
            }
        }
    }
}
