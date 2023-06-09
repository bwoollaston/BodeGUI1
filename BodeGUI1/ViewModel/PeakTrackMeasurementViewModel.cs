using BodeGUI1.ViewModel.DataModel;
using BodeGUI1.ViewModel.Plots;
using BodeGUI1.ViewModel.UI;
using Prism.Commands;
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
    internal class PeakTrackMeasurementViewModel : MeasurementFrameViewModelBase
    {
        public PeakTrackMeasurementViewModel()
        {
            SweepData = new ObservableCollection<ResonanceSweepData>();
            BodePlot = new PeakPlotViewModel();
        }
        private ObservableCollection<ResonanceSweepData> _sweepData;
        public ObservableCollection<ResonanceSweepData> SweepData
        {
            get { return _sweepData; }
            set { _sweepData = value; OnPropertyChanged(); }
        }
        private PeakPlotViewModel _bodePlot;
        public PeakPlotViewModel BodePlot
        {
            get { return _bodePlot; }
            set { _bodePlot = value; OnPropertyChanged(); }
        }
        public void ClearPlots()
        {
            BodePlot.SmoothPts.Clear();
            BodePlot.ImpedanceView.Clear();
            BodePlot.PhaseView.Clear();
        }
    }
}
