using System;
using BodeGUI1.ViewModel.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Interfaces;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Interfaces.Measurements;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Enumerations;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.DataTypes;
using Makaretu.Dns.Resolving;
using System.Diagnostics.Metrics;
using System.Collections.ObjectModel;
using System.Windows;
using OxyPlot;
using BodeGUI1.ViewModel.DataModel;
using BodeGUI1.ExceptionHandlers;
using BodeGUI1.ViewModel;

namespace BodeGUI1.Utility
{
    internal class MeasurementFunctions : ViewModelBase
    {
        public OnePortMeasurement measurement;
        public BodeDevice bode;
        public ExecutionState state;
        public BodeAutomationInterface auto = new BodeAutomation();
        public event EventHandler StatusBasePropertyChanged;
        public MeasurementFunctions()
        {
            BodeStatusViewModel = new BodeStatusViewModel();
            SweepData = new ResonanceSweepData();
            CalResistor = 100;
        }
        private double _calResistor;
        public double CalResistor
        {
            get { return _calResistor; }
            set { _calResistor = value; }
        }
        private double _testValue;
        public double TestValue
        {
            get { return _testValue; }
            set { _testValue = value; }
        }
        private BodeStatusViewModel _bodeStatusViewModel;
        public BodeStatusViewModel BodeStatusViewModel
        {
            get { return _bodeStatusViewModel; }
            set 
            { 
                _bodeStatusViewModel = value;
                StatusBasePropertyChanged?.Invoke(this, EventArgs.Empty);
                OnPropertyChanged();
            }
        }
        public ResonanceSweepData SweepData { get; private set; }
        public async void Connect(BodeSettingsViewModel sender, EventArgs e)
        {
            try
            {
                sender.Enable = false;
                if (BodeStatusViewModel.StatusCollection[0].Status == true)
                {
                    Disconnect(this, EventArgs.Empty);
                    BodeStatusViewModel.StatusCollection[0].Status = false;
                }
                else
                {
                    bode = await Task.Run(() => auto.Connect());
                    measurement = bode.Impedance.CreateOnePortMeasurement();
                    BodeStatusViewModel.StatusCollection[0].Status = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bode connection failed", "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
                BodeStatusViewModel.StatusCollection[0].Status = false;
            }
            sender.Enable = true;
        }
        public void Disconnect(object? sender, EventArgs e)
        {
            try { if (bode != null) bode.ShutDown(); BodeStatusViewModel.StatusCollection[0].Status = false; }
            catch (Exception ex)
            {
                MessageBox.Show("Shutdown did not go as planned", "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SweepPtMeasurement(double Low, double High, int NumPts, SweepMode Type)
        {
            measurement.ConfigureSweep(Low, High, NumPts, Type);
            state = measurement.ExecuteMeasurement();
            if (state != ExecutionState.Ok)
            {
                Disconnect(this, EventArgs.Empty);
                //throw new ExecuteStateException("Frequency Sweep");
            }
        }
        private void SinglePtMeasurement(double frequency)
        {
            measurement.ConfigureSinglePoint(frequency);
            state = measurement.ExecuteMeasurement();
            if (state != ExecutionState.Ok)
            {
                Disconnect(this, EventArgs.Empty);
                //throw new ExecuteStateException("Resonant Frequency Measurement");
            }
        }
        public void Sweep(double LowFreq, double HighFreq, int NumPts, SweepMode Type,double bandwidth)
        {
            InitializeSweepMeasurement( LowFreq,  HighFreq,  NumPts,  Type,  bandwidth);
            SweepData.Resfreq = measurement.Results.CalculateFResQValues(false, true, FResQFormats.Magnitude).ResonanceFrequency;
            SweepData.Antifreq = measurement.Results.CalculateFResQValues(true, true, FResQFormats.Magnitude).ResonanceFrequency;
            if (SweepData.Resfreq<=0) throw new ResNotFoundException(SweepData.Resfreq);
            if(SweepData.Antifreq <= 0) throw new ResNotFoundException(SweepData.Antifreq);
            SinglePtMeasurement(SweepData.Resfreq);
            SweepData.Res_impedance = measurement.Results.MagnitudeAt(0, MagnitudeUnit.Lin);
            SweepData.QualityFactor = measurement.Results.QAt(0);
            SweepData.Phase = measurement.Results.PhaseAt(0,AngleUnit.Degree);
            SinglePtMeasurement(SweepData.Antifreq);
            SweepData.Anti_impedance = measurement.Results.MagnitudeAt(0, MagnitudeUnit.Lin);
            SinglePtMeasurement(1000);
            SweepData.Capacitance = measurement.Results.CsAt(0)*1e12;
        }
        public void PeakSweep(double LowFreq, double HighFreq, int NumPts, SweepMode Type, double bandwidth)
        {
            InitializeSweepMeasurement(LowFreq, HighFreq, NumPts, Type, bandwidth);
        }
        private void InitializeSweepMeasurement(double LowFreq, double HighFreq, int NumPts, SweepMode Type, double bandwidth)
        {
            measurement.ReceiverBandwidth = (ReceiverBandwidth)bandwidth;       //sets UI bandwidth value to be bandwidth of measurement
            SweepPtMeasurement(LowFreq, HighFreq, NumPts, Type);
            ClearPlotData(SweepData.ImpdedancePlot);
            ClearPlotData(SweepData.PhasePlot);
            FillPlotData(SweepData.ImpdedancePlot, measurement.Results.MeasurementFrequencies.ToList(), measurement.Results.Magnitude(MagnitudeUnit.Lin).ToList(), measurement.Results.MeasurementFrequencies.Length);
            FillPlotData(SweepData.PhasePlot, measurement.Results.MeasurementFrequencies.ToList(), measurement.Results.Phase(AngleUnit.Degree).ToList(), measurement.Results.MeasurementFrequencies.Length);
        }
        private void FillPlotData(List<DataPoint> Pts, List<double> Frequencies, List<double> Data, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Pts.Add(new DataPoint(Frequencies[i], Data[i]));
            }
        }
        private void ClearPlotData(List<DataPoint> Pts)
        {
            Pts.Clear();
        }
        public void TestCal(BodeSettingsViewModel sender,EventArgs e)
        {
            try
            {
                sender.Enable = false;
                SinglePtMeasurement(1000);
                TestValue = measurement.Results.MagnitudeAt(0, MagnitudeUnit.Lin);
            }
            catch(Exception ex)
            {
                TestValue = 0;
                MessageBox.Show("Open calibration failed", "Exception Sample", MessageBoxButton.OK);
            }
            sender.Enable = true;
        }
        public string ExportPath()
        {
            string fileSelected = "";
            Microsoft.Win32.SaveFileDialog openFileDialog = new Microsoft.Win32.SaveFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.Title = "Select file loaction";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                fileSelected = openFileDialog.FileName;
            }
            return fileSelected;
        }
        public async void OpenCal(BodeSettingsViewModel sender, EventArgs e)
        {
            /* Bode Automation Suite method runs open calibration */
            try
            {
                sender.Enable = false;
                ExecutionState state = await Task.Run(() => measurement.Calibration.FullRange.ExecuteOpen());
                BodeStatusViewModel.StatusCollection[1].Status = true;
            }
            catch (Exception ex)
            {
                BodeStatusViewModel.StatusCollection[1].Status = false;
                MessageBox.Show("Open calibration failed", "Exception Sample", MessageBoxButton.OK);
            }
            sender.Enable = true;
        }
        public async void ShortCal(BodeSettingsViewModel sender, EventArgs e)
        {
            try
            {
                sender.Enable = false;
                ExecutionState state = await Task.Run(() => measurement.Calibration.FullRange.ExecuteShort());
                BodeStatusViewModel.StatusCollection[2].Status = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Short calibration failed", "Exception Sample", MessageBoxButton.OK);
                BodeStatusViewModel.StatusCollection[2].Status = false;
            }
            sender.Enable = true;
        }
        public async void LoadCal(BodeSettingsViewModel sender, EventArgs e)
        {
            try
            {
                sender.Enable = false;
                measurement.Calibration.Load = CalResistor;
                ExecutionState state = await Task.Run(() => measurement.Calibration.FullRange.ExecuteLoad());
                BodeStatusViewModel.StatusCollection[3].Status = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Load calibration failed", "Exception Sample", MessageBoxButton.OK);
                BodeStatusViewModel.StatusCollection[3].Status = false;
            }
            sender.Enable = true;
        }

    }
}
