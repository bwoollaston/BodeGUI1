using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodeGUI1.ViewModel.DataModel;

namespace BodeGUI1.ViewModel.Plots
{
    internal class PeakPlotViewModel : PlotViewModelBase
    { 
        public PeakPlotViewModel()
        {
            Title = "Peak Tracking Plot";
            ImpedanceView = new ObservableCollection<DataPoint>();
            PhaseView = new ObservableCollection<DataPoint>();
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
            for(int i = 0; i < SelectedData.ImpdedancePlot.Count; i += dp)
            {
                double x = 0;
                double y = 0;
                for(int j=0; j < dp; j++)
                {
                    x += SelectedData.ImpdedancePlot[i + j].X;
                    y += SelectedData.ImpdedancePlot[i + j].Y;
                }
                x = x / dp;
                y = y / dp;
                SmoothPts.Add(new DataPoint(x,y));
            }
        }
    }
}
