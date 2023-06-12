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
    internal class PlotViewModelBase : ViewModelBase
    {
        public PlotViewModelBase()
        {
            SweepData = new ObservableCollection<ResonanceSweepData>();
            ImpedanceView = new ObservableCollection<DataPoint>();
            PhaseView = new ObservableCollection<DataPoint>();
            ThreshView = new ObservableCollection<DataPoint>();
            SelectedData = new ResonanceSweepData();
            Title = "Title";
            //HighX = 4e6;
            //LowX = 100;
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
        private ResonanceSweepData _selectedData;
        public ResonanceSweepData SelectedData
        {
            get { return _selectedData; }
            set 
            { 
                _selectedData = value;
                if (_selectedData != null)
                {
                    ImpedanceView = new ObservableCollection<DataPoint>(_selectedData.ImpdedancePlot);
                    PhaseView = new ObservableCollection<DataPoint>(_selectedData.PhasePlot);
                    ThreshView = new ObservableCollection<DataPoint>(_selectedData.Threshline);
                }
                else
                {
                    ImpedanceView.Clear();
                    PhaseView.Clear();
                }
                OnPropertyChanged(); 
            }
        }
        private ObservableCollection<ResonanceSweepData> _sweepData;
        public ObservableCollection<ResonanceSweepData> SweepData
        {
            get { return _sweepData; }
            set { _sweepData = value; OnPropertyChanged(); }
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
        private ObservableCollection<DataPoint> _threshView;
        public ObservableCollection<DataPoint> ThreshView
        {
            get { return _threshView; }
            set { _threshView = value; OnPropertyChanged(); }
        }
    }
}
