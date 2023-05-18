using BodeGUI1.ViewModel.UI;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;

namespace BodeGUI1.ViewModel
{
    internal class ProgViewModel : MeasurementViewModelBase
    {
        public ProgViewModel()
        {
            TabItems = new ObservableCollection<string>() { "Resonance Measurement","Peak Tracking","Bode Settings" };
            SelectedTab = TabItems[0]; 
            CurrentContent = new ResonanceMeasurementViewModel();
            BodeControls = new BodeControlsViewModel();
            Parameters = new MeasurementParamtersViewModel();
            BodeControls.StartMeasurementClicked += BodeControls_StartMeasurementClicked;
            this.StatusBasePropertyChanged += UpdateStatus;
            CurrentContentWidth = 1000;
            BodeControlsHeight = 80;
        }

        private void BodeControls_StartMeasurementClicked(object? sender, EventArgs e)
        {
            switch (SelectedTab)
            {
                case "Resonance Measurement":
                    Parameters.Enable = false;
                    SweepData.Name = Parameters.SampleName;
                    Sweep(Parameters.LowSweep, Parameters.HighSweep, 201, SweepMode.Logarithmic, Parameters.RecieverBW);
                    ResonanceMeasurementViewModel Content = (ResonanceMeasurementViewModel)CurrentContent;
                    Content.SweepData.Add(SweepData);
                    Content.BodePlot.Points.Clear();
                    Content.BodePlot.Points = new ObservableCollection<OxyPlot.DataPoint>(BodePoints);
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
                if (_selectedTab == TabItems[0]) CurrentContent = new ResonanceMeasurementViewModel() { ListWidth = CurrentContentWidth };
                else if (_selectedTab == TabItems[1]) CurrentContent = new ResonanceMeasurementViewModel() { ListWidth = CurrentContentWidth };
                else if (SelectedTab == TabItems[2])
                {
                    CurrentContent = new BodeSettingsViewModel();
                    BodeSettingsViewModel Content = (BodeSettingsViewModel)CurrentContent;
                    Content.ConnectClicked += Connect;
                    Content.OpenClicked += OpenCal;
                    Content.ShortClicked += ShortCal;
                    Content.LoadClicked += LoadCal;
                    CurrentContent = Content;
                }
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
