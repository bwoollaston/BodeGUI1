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

namespace BodeGUI1.ViewModel
{
    internal class MeasurementViewModelBase : ViewModelBase
    {
        public OnePortMeasurement measurement;
        public BodeDevice bode;
        public ExecutionState state;
        public BodeAutomationInterface auto = new BodeAutomation();
        public event EventHandler StatusBasePropertyChanged;
        public MeasurementViewModelBase()
        {
            BodeStatusViewModel = new BodeStatusViewModel();
            BodePoints = new List<DataPoint>();
            PhasePoints = new List<DataPoint>();
            SweepData = new ResonanceSweepDataViewModel();
            CalResistor = 100;
        }
        private double _calResistor;
        public double CalResistor
        {
            get { return _calResistor; }
            set { _calResistor = value; }
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
        public ResonanceSweepDataViewModel SweepData { get; private set; }
        public List<DataPoint> BodePoints { get; private set; }
        public List<DataPoint> PhasePoints { get; private set; }
        public void Connect(object? sender, EventArgs e)
        {
            try
            {
                if (BodeStatusViewModel.StatusCollection[0].Status == true)
                {
                    Disconnect(this, EventArgs.Empty);
                }
                else
                {
                    bode = auto.Connect();
                    measurement = bode.Impedance.CreateOnePortMeasurement();
                    BodeStatusViewModel.StatusCollection[0].Status = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bode connection failed", "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
                BodeStatusViewModel.StatusCollection[0].Status = false;
            }
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
            SweepData = new ResonanceSweepDataViewModel();
            measurement.ReceiverBandwidth = (ReceiverBandwidth)bandwidth;       //sets UI bandwidth value to be bandwidth of measurement
            SweepPtMeasurement(LowFreq, HighFreq, NumPts, Type);
            ClearPlotData(BodePoints);
            ClearPlotData(PhasePoints);
            FillPlotData(BodePoints, measurement.Results.MeasurementFrequencies.ToList(), measurement.Results.Magnitude(MagnitudeUnit.Lin).ToList(), measurement.Results.MeasurementFrequencies.Length);
            FillPlotData(PhasePoints, measurement.Results.MeasurementFrequencies.ToList(), measurement.Results.Phase(AngleUnit.Degree).ToList() , measurement.Results.MeasurementFrequencies.Length);
            SweepData.Resfreq = measurement.Results.CalculateFResQValues(false, true, FResQFormats.Magnitude).ResonanceFrequency;
            SweepData.Antifreq = measurement.Results.CalculateFResQValues(true, true, FResQFormats.Magnitude).ResonanceFrequency;
            SinglePtMeasurement(SweepData.Resfreq);
            SweepData.Res_impedance = measurement.Results.MagnitudeAt(0, MagnitudeUnit.Lin);
            SweepData.QualityFactor = measurement.Results.QAt(0);
            SweepData.Phase = measurement.Results.PhaseAt(0,AngleUnit.Degree);
            SinglePtMeasurement(SweepData.Antifreq);
            SweepData.Anti_impedance = measurement.Results.MagnitudeAt(0, MagnitudeUnit.Lin);
            SinglePtMeasurement(1000);
            SweepData.Capacitance = measurement.Results.CsAt(0)*1e12;
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
        public double TestCal()
        {
            SinglePtMeasurement(1000);
            return measurement.Results.MagnitudeAt(0, MagnitudeUnit.Lin);
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
        public void OpenCal(object? sender, EventArgs e)
        {
            /* Bode Automation Suite method runs open calibration */
            try
            {
                ExecutionState state = measurement.Calibration.FullRange.ExecuteOpen();
                BodeStatusViewModel.StatusCollection[1].Status = true;
            }
            catch (Exception ex)
            {
                BodeStatusViewModel.StatusCollection[1].Status = false;
                MessageBox.Show("Open calibration failed", "Exception Sample", MessageBoxButton.OK);
            }
        }
        public void ShortCal(object? sender, EventArgs e)
        {
            try
            {
                ExecutionState state = measurement.Calibration.FullRange.ExecuteShort();
                BodeStatusViewModel.StatusCollection[2].Status = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Short calibration failed", "Exception Sample", MessageBoxButton.OK);
                BodeStatusViewModel.StatusCollection[2].Status = false;
            }
        }
        public void LoadCal(object? sender, EventArgs e)
        {
            try
            {
                measurement.Calibration.Load = CalResistor;
                ExecutionState state = measurement.Calibration.FullRange.ExecuteLoad();
                BodeStatusViewModel.StatusCollection[3].Status = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Load calibration failed", "Exception Sample", MessageBoxButton.OK);
                BodeStatusViewModel.StatusCollection[3].Status = false;
            }
        }
    }
}
