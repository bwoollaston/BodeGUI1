using BodeGUI1.ViewModel.UI;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using System.Windows;

namespace BodeGUI1.ViewModel
{
    internal class ProgViewModel : MeasurementViewModelBase
    {
        public ProgViewModel()
        {
            BodeControlsHeight = 100;
            ResonanceMeasurementViewModel = new ResonanceMeasurementViewModel();
            PeakTrackMeasurementViewModel = new ResonanceMeasurementViewModel();
            BodeConnection = new BodeSettingsViewModel();
            BodeControls = new BodeControlsViewModel();
            Parameters = new MeasurementParamtersViewModel();
            TabItems = new ObservableCollection<string>() { "Resonance Measurement","Peak Tracking","Connection Settings" };
            SelectedTab = TabItems[2];
            CurrentContent = BodeConnection;

            BodeConnection.ConnectClicked += Connect;
            BodeConnection.OpenClicked += OpenCal;
            BodeConnection.ShortClicked += ShortCal;
            BodeConnection.LoadClicked += LoadCal;
            BodeControls.StartMeasurementClicked += BodeControls_StartMeasurementClicked;
            this.StatusBasePropertyChanged += UpdateStatus;
        }

        private async void BodeControls_StartMeasurementClicked(object? sender, EventArgs e)
        {
            switch (SelectedTab)
            {
                case "Resonance Measurement":
                    var p = Parameters;
                    p.Enable = false;
                    SweepData.Name = p.SampleName;
                    await Task.Run(() => Sweep(p.LowSweep, p.HighSweep, p.SweepPoints, SweepMode.Logarithmic, p.RecieverBW));
                    ResonanceMeasurementViewModel.SweepData.Add(SweepData);
                    ResonanceMeasurementViewModel.BodePlot.Points.Clear();
                    ResonanceMeasurementViewModel.BodePlot.Points = new ObservableCollection<OxyPlot.DataPoint>(BodePoints);
                    BodeControls.ProgramingActive = Visibility.Collapsed;
                    break;
                case "Peak Tracking":
                    break;

                default: break;
            }
        }

        private string _selectedTab;
        public string SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (_selectedTab == value) return;
                _selectedTab = value;
                if (_selectedTab == TabItems[0]) 
                {
                    CurrentContent = ResonanceMeasurementViewModel;
                }
                else if (_selectedTab == TabItems[1])
                {
                    CurrentContent = PeakTrackMeasurementViewModel;
                }
                else if (SelectedTab == TabItems[2]) CurrentContent = BodeConnection;
                OnPropertyChanged();
            }
        }
        private MeasurementParamtersViewModel _parameters;
        public MeasurementParamtersViewModel Parameters
        {
            get { return _parameters; }
            set { _parameters = value; OnPropertyChanged(); }
        }
        private ViewModelBase _currentContent;
        public ViewModelBase CurrentContent
        {
            get { return _currentContent; }
            set { _currentContent = value; OnPropertyChanged(); }
        }
        private ResonanceMeasurementViewModel _measurement;
        public ResonanceMeasurementViewModel ResonanceMeasurementViewModel
        {
            get { return _measurement; }
            set { _measurement = value;OnPropertyChanged(); }
        }
        private ResonanceMeasurementViewModel _tracking;
        public ResonanceMeasurementViewModel PeakTrackMeasurementViewModel
        {
            get { return _tracking; }
            set { _tracking = value;OnPropertyChanged(); }
        }
        private BodeSettingsViewModel _bodeConnection;
        public BodeSettingsViewModel BodeConnection
        {
            get { return _bodeConnection; }
            set { _bodeConnection = value;OnPropertyChanged(); }
        }
        private ObservableCollection<string> _tabItems;
        public ObservableCollection<string> TabItems
        {
            get { return _tabItems; }
            set { _tabItems = value; OnPropertyChanged(); }
        }
        private BodeControlsViewModel _bodeControls;
        public BodeControlsViewModel BodeControls
        {
            get { return _bodeControls; }
            set 
            { 
                _bodeControls=value;
                _bodeControls.Status = BodeStatusViewModel;
                OnPropertyChanged();
            }
        }
        private void UpdateStatus(object? sender, EventArgs e)
        {
            BodeControls.Status = BodeStatusViewModel;
        }
        private double _bodeControlsHeight;
        public double BodeControlsHeight
        {
            get { return _bodeControlsHeight; }
            set { _bodeControlsHeight = value; OnPropertyChanged(); }
        }
        private double _currentContentWidth;
        public double CurrentContentWidth
        {
            get { return _currentContentWidth; }
            set { _currentContentWidth = value; OnPropertyChanged(); }
        }
    }
}
