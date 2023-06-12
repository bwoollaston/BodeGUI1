using BodeGUI1.ViewModel.UI;
using BodeGUI1.ViewModel.DataModel;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using System.Windows;
using System.IO;
using CsvHelper;
using System.Globalization;
using BodeGUI1.ExceptionHandlers;
using BodeGUI1.Utility;
using OxyPlot;
using BodeGUI1.ViewModel.Data;

namespace BodeGUI1.ViewModel
{
    internal class ProgViewModel : ViewModelBase
    {
        public ProgViewModel()
        {
            BodeControlsHeight = 100;
            ResonanceMeasurementViewModel = new ResonanceMeasurementViewModel();
            PeakTrackMeasurementViewModel = new PeakTrackMeasurementViewModel();
            BodeEvents = new MeasurementFunctions();
            BodeConnection = new BodeSettingsViewModel();
            BodeControls = new BodeControlsViewModel();
            Parameters = new MeasurementParamtersViewModel();
            TabItems = new ObservableCollection<string>() { "Resonance Measurement","Peak Tracking","Connection Settings" };
            SelectedTab = TabItems[2];
            CurrentContent = BodeConnection;
            BodeConnection.ConnectClicked += ConnectEvent;
            BodeConnection.OpenClicked += OpenEvent;
            BodeConnection.ShortClicked += ShortEvent;
            BodeConnection.TestClicked += CalTest;
            BodeConnection.LoadClicked += LoadEvent;
            Parameters.ExportClicked += ExportData;
            BodeControls.StartMeasurementClicked += BodeControls_StartMeasurementClicked;
            BodeEvents.StatusBasePropertyChanged += UpdateStatus;

            GenRandData();
        }
        public void GenRandData()
        {
            Random random = new Random();
            for(int i = 0; i < 10; i++)
            {
                ResonanceMeasurementViewModel.SweepData.Add(BodeEvents.SweepData.RandClone());
                PeakTrackMeasurementViewModel.SweepData.Add(BodeEvents.SweepData.RandClone());
            }
            PeakTrackMeasurementViewModel.TableDataIndex = 9; 
        }
        private async void BodeControls_StartMeasurementClicked(object? sender, EventArgs e)
        {
            var p = Parameters;
            Parameters.Enable = false;
            switch (SelectedTab)
            {
                case "Resonance Measurement":
                    BodeEvents.SweepData.Name = p.SampleName;
                    BodeEvents.SweepData.LowX = p.LowSweep;
                    BodeEvents.SweepData.HighX = p.HighSweep;
                    BodeEvents.SweepData.Time = DateTime.Now.ToString("g");
                    try
                    {
                        await Task.Run(() => BodeEvents.Sweep(p.LowSweep, p.HighSweep, p.SweepPoints, p.CurSweepMode, p.RecieverBW));
                        ResonanceMeasurementViewModel.SweepData.Add(BodeEvents.SweepData.Clone());
                        ResonanceMeasurementViewModel.BodePlot.SweepData.Add(BodeEvents.SweepData.Clone());
                        ResonanceMeasurementViewModel.BodePlot.SelectedData = ResonanceMeasurementViewModel.SweepData.Last();
                        ResonanceMeasurementViewModel.BodePlot.UpdateUI();
                    }
                    catch(ResNotFoundException ex)
                    {
                        ResonanceMeasurementViewModel.ClearPlots();
                        MessageBox.Show(ex.Message, "Exception Sample", MessageBoxButton.OK);
                    }
                    Parameters.Enable = true;
                    BodeControls.ProgramingActive = Visibility.Collapsed;
                    break;
                case "Peak Tracking":
                    BodeEvents.SweepData.Name = p.SampleName;
                    BodeEvents.SweepData.LowX = p.LowSweep;
                    BodeEvents.SweepData.HighX = p.HighSweep;
                    try
                    {
                        await Task.Run(() => BodeEvents.PeakSweep(p.LowSweep, p.HighSweep, p.SweepPoints, p.CurSweepMode, p.RecieverBW));
                        PeakTrackMeasurementViewModel.SweepData.Add(BodeEvents.SweepData.Clone());
                        PeakTrackMeasurementViewModel.BodePlot.SweepData.Add(BodeEvents.SweepData.Clone());
                        PeakTrackMeasurementViewModel.BodePlot.SelectedData = PeakTrackMeasurementViewModel.SweepData.Last();
                        PeakTrackMeasurementViewModel.BodePlot.SmoothData();
                        PeakTrackMeasurementViewModel.BodePlot.UpdateUI();
                    }
                    catch (ResNotFoundException ex)
                    {
                        PeakTrackMeasurementViewModel.ClearPlots();
                        MessageBox.Show(ex.Message, "Exception Sample", MessageBoxButton.OK);
                    }
                    Parameters.Enable = true;
                    BodeControls.ProgramingActive = Visibility.Collapsed;
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
                ParamVisibility = Visibility.Visible;
                if (_selectedTab == TabItems[0])
                {
                    CurrentContent = ResonanceMeasurementViewModel;
                }
                else if (_selectedTab == TabItems[1])
                {
                    CurrentContent = PeakTrackMeasurementViewModel;
                }
                else if (SelectedTab == TabItems[2])
                {
                    ParamVisibility = Visibility.Collapsed;
                    CurrentContent = BodeConnection;
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
        private ResonanceMeasurementViewModel _measurement;
        public ResonanceMeasurementViewModel ResonanceMeasurementViewModel
        {
            get { return _measurement; }
            set { _measurement = value;OnPropertyChanged(); }
        }
        private PeakTrackMeasurementViewModel _tracking;
        public PeakTrackMeasurementViewModel PeakTrackMeasurementViewModel
        {
            get { return _tracking; }
            set { _tracking = value;OnPropertyChanged(); }
        }
        private BodeSettingsViewModel _bodeConnection;
        public BodeSettingsViewModel BodeConnection
        {
            get { return _bodeConnection; }
            set 
            { 
                _bodeConnection = value;
                BodeEvents.CalResistor = _bodeConnection.CalResistor;
                OnPropertyChanged(); 
            }
        }
        private MeasurementFunctions _bodeEvents;
        public MeasurementFunctions BodeEvents
        {
            get { return _bodeEvents; }
            set { _bodeEvents = value; OnPropertyChanged(); }
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
                _bodeControls.Status = BodeEvents.BodeStatusViewModel;
                OnPropertyChanged();
            }
        }
        private Visibility _paramVisibility;
        public Visibility ParamVisibility
        {
            get { return _paramVisibility; }
            set { _paramVisibility = value; OnPropertyChanged(); }
        }
        private void UpdateStatus(object? sender, EventArgs e)
        {
            BodeControls.Status = BodeEvents.BodeStatusViewModel;
        }
        private async void ExportData(object? sender, EventArgs e)
        {
            try
            {
                string fp = await Task.Run(() => BodeEvents.ExportPath());
                using (var writer = new StreamWriter(fp))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    if (SelectedTab == TabItems[0]) csv.WriteRecords(ResonanceMeasurementViewModel.SweepData);
                    else if (SelectedTab == TabItems[1]) csv.WriteRecords(PeakTrackMeasurementViewModel.SweepData);
                }
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show("No FilePath Provided", "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        private double _bodeControlsHeight;
        public double BodeControlsHeight
        {
            get { return _bodeControlsHeight; }
            set { _bodeControlsHeight = value; OnPropertyChanged(); }
        }
        private void ConnectEvent(object? sender, EventArgs e)
        {
            BodeEvents.Connect((BodeSettingsViewModel)sender, e);
        }
        private void OpenEvent(object? sender, EventArgs e)
        {
            BodeEvents.OpenCal((BodeSettingsViewModel)sender, e);
        }
        private void ShortEvent(object? sender, EventArgs e)
        {
            BodeEvents.ShortCal((BodeSettingsViewModel)sender, e);
        }
        private void LoadEvent(object? sender, EventArgs e)
        {
            BodeEvents.CalResistor = _bodeConnection.CalResistor;
            BodeEvents.LoadCal((BodeSettingsViewModel)sender, e);
        }
        private void CalTest(object? sender, EventArgs e)
        {
            BodeEvents.TestCal((BodeSettingsViewModel)sender, e);
            BodeConnection.CalTestValue = BodeEvents.TestValue;
        }
    }
}
