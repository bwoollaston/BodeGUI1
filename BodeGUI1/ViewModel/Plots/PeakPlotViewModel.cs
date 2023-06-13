using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodeGUI1.ViewModel.DataModel;
using System.Runtime.Intrinsics.Arm;

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
            ThreshPoints = new ObservableCollection<DataPoint>();
            SmoothPtsView = new ObservableCollection<DataPoint>();
            DeltaP = 3;
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
        private ObservableCollection<DataPoint> _threshPoints;
        public ObservableCollection<DataPoint> ThreshPoints
        {
            get { return _threshPoints; }
            set 
            { 
                _threshPoints = value;
                OnPropertyChanged(); 
            } 
        }
        private ObservableCollection<DataPoint> _smoothPtsView;
        public ObservableCollection<DataPoint> SmoothPtsView
        {
            get { return _smoothPtsView; }
            set { _smoothPtsView = value; OnPropertyChanged(); }
        }
        //Used to set degree of data smoothing
        private int _deltaP;
        public int DeltaP
        {
            get { return _deltaP; }
            set { _deltaP = value; OnPropertyChanged(); }
        }
        public void SmoothData()
        {
            SmoothPts.Clear();
            int dp = DeltaP;
            int rangeFlag = 0;
            int remainder = SelectedData.ImpdedancePlot.Count % dp;
            for (int i = 0; i < SelectedData.ImpdedancePlot.Count; i += dp)
            {
                double x = 0;
                double y = 0;
                for(int j=0; j < dp; j++)
                {
                    if (j + i >= SelectedData.ImpdedancePlot.Count) { rangeFlag = 1; break; }
                    x += SelectedData.ImpdedancePlot[i + j].X;
                    y += SelectedData.ImpdedancePlot[i + j].Y;
                }
                if (rangeFlag == 0) { x = x / dp; y = y / dp; }
                else { x = x / remainder; y = y / remainder; }
                SmoothPts.Add(new DataPoint(x,y));
            }
        }
        public async void UpdateUI()
        {
            ImpedanceView.Clear();
            PhaseView.Clear();
            SmoothPtsView.Clear();
            int delay = 2000;
            int dt = delay / SelectedData.ImpdedancePlot.Count;
            int i = 0;
            int k = 0;
            foreach (DataPoint element in SmoothPts)
            {
                for(int j=0; j < DeltaP; j++)
                {
                    if (k + j >= SelectedData.ImpdedancePlot.Count) break;
                    ImpedanceView.Add(SelectedData.ImpdedancePlot[k+j]);
                    //PhaseView.Add(SelectedData.PhasePlot[i]);
                }
                SmoothPtsView.Add(SmoothPts[i]);
                await Task.Delay(dt);
                k += DeltaP;
                i++;
            }
        }
    }
}
