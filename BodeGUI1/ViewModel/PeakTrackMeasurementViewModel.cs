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
            BodePlot = new PeakPlotViewModel();
        }
        private PeakPlotViewModel _bodePlot;
        public PeakPlotViewModel BodePlot
        {
            get { return _bodePlot; }
            set { _bodePlot = value; OnPropertyChanged(); }
        }
        public void ClearPlots()
        {
            BodePlot.Impedance.Clear();
            BodePlot.Phase.Clear();
            BodePlot.SmoothPts.Clear();
            BodePlot.ImpedanceView.Clear();
            BodePlot.PhaseView.Clear();
        }
    }
}
