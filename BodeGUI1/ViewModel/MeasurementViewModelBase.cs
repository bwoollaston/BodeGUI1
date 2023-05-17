using System;
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
        public MeasurementViewModelBase()
        {
            Connected = false;
            Calibrated = false;
            BodePoints = new List<DataPoint>();
            SweepData = new ResonanceSweepDataViewModel();
        }

        private bool _connected;
        public bool Connected
        {
            get { return _connected; }
            set { _connected = value; OnPropertyChanged(); }
        }
        private bool _calibrated;
        public bool Calibrated
        {
            get { return _calibrated; }
            set { _calibrated = value; OnPropertyChanged(); }
        }
        public ResonanceSweepDataViewModel SweepData { get; private set; }
        public List<DataPoint> BodePoints { get; private set; }
        public void Connect()
        {
            try
            {
                bode = auto.Connect();
                measurement = bode.Impedance.CreateOnePortMeasurement();
                Connected = true;
            }
            catch (Exception ex)
            {
                Connected = false;
                MessageBox.Show("Bode connection failed", "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Disconnect()
        {
            try { if (bode != null) bode.ShutDown(); Connected = false; }
            catch (Exception ex)
            {
                MessageBox.Show("Shutdowm did not go as planned", "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SweepPtMeasurement(double Low, double High, int NumPts, SweepMode Type)
        {
            measurement.ConfigureSweep(Low, High, NumPts, Type);
            state = measurement.ExecuteMeasurement();
            if (state != ExecutionState.Ok)
            {
                Disconnect();
                //throw new ExecuteStateException("Frequency Sweep");
            }
        }

        private void SinglePtMeasurement(double frequency)
        {
            measurement.ConfigureSinglePoint(frequency);
            state = measurement.ExecuteMeasurement();
            if (state != ExecutionState.Ok)
            {
                Disconnect();
                //throw new ExecuteStateException("Resonant Frequency Measurement");
            }
        }
        public void Sweep(double LowFreq, double HighFreq, int NumPts, SweepMode Type,double bandwidth)
        {
            SweepData = new ResonanceSweepDataViewModel();
            measurement.ReceiverBandwidth = (ReceiverBandwidth)bandwidth;       //sets UI bandwidth value to be bandwidth of measurement
            SweepPtMeasurement(LowFreq, HighFreq, NumPts, Type);
            ClearPlotData(BodePoints);
            FillPlotData(BodePoints, measurement.Results.MeasurementFrequencies.ToList(), measurement.Results.Magnitude(MagnitudeUnit.Lin).ToList(), measurement.Results.MeasurementFrequencies.Length);
            SweepData.Resfreq = measurement.Results.CalculateFResQValues(false, true, FResQFormats.Magnitude).ResonanceFrequency;
            SweepData.Antifreq = measurement.Results.CalculateFResQValues(true, true, FResQFormats.Magnitude).ResonanceFrequency;
            SinglePtMeasurement(SweepData.Resfreq);
            SweepData.Res_impedance = measurement.Results.MagnitudeAt(0, MagnitudeUnit.Lin);
            SweepData.QualityFactor = measurement.Results.QAt(0);
            SinglePtMeasurement(SweepData.Antifreq);
            SweepData.Anti_impedance = measurement.Results.MagnitudeAt(0, MagnitudeUnit.Lin);
            SinglePtMeasurement(1000);
            SweepData.Capacitance = measurement.Results.CsAt(0);
        }
        private void FillPlotData(List<DataPoint> Pts, List<double> Frequencies, List<double> Impedances, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Pts.Add(new DataPoint(measurement.Results.MeasurementFrequencies[i], measurement.Results.Magnitude(MagnitudeUnit.Lin)[i]));
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
        public void OpenCal()
        {
            /* Bode Automation Suite method runs open calibration */
            ExecutionState state = measurement.Calibration.FullRange.ExecuteOpen();
        }
        public void ShortCal()
        {
            ExecutionState state = measurement.Calibration.FullRange.ExecuteShort();
        }
        public void LoadCal()
        {
            ExecutionState state = measurement.Calibration.FullRange.ExecuteLoad();
        }
    }

}
