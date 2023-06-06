using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BodeGUI1.ExceptionHandlers;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Enumerations;
using Prism.Commands;

namespace BodeGUI1.ViewModel.UI
{
    internal class MeasurementParamtersViewModel : ViewModelBase
    {
        public event EventHandler ExportClicked;
        public MeasurementParamtersViewModel()
        {
            SweepModes = new ObservableCollection<string>() { "Logarithmic" , "Linear" };
            SelectedMode = SweepModes[0];
            Export = new DelegateCommand(DataExport);
            SampleIndex = 1;
            HighSweep = 190000;
            LowSweep = 180000;
            RecieverBW = 100000;
            SweepPoints = 201;
            SampleName = String.Empty;
            IndexingIsChecked = false;
            Enable = true;
        }
        private ObservableCollection<string> _sweepModes;
        public ObservableCollection<string> SweepModes
        {
            get { return _sweepModes; }
            set 
            { 
                _sweepModes = value; 
                OnPropertyChanged();
            }
        }
        private string _selectedMode;
        public string SelectedMode
        {
            get { return _selectedMode; }
            set 
            { 
                _selectedMode = value;
                if (_selectedMode == SweepModes[0]) CurSweepMode = SweepMode.Logarithmic;
                else CurSweepMode = SweepMode.Linear;
                OnPropertyChanged(); 
            }
        }
        private SweepMode _curSweepMode;
        public SweepMode CurSweepMode
        {
            get { return _curSweepMode; }
            set { _curSweepMode = value; OnPropertyChanged(); }
        }
        private int SampleIndex;
        private bool _indexingIsChecked;
        public bool IndexingIsChecked
        {
            get { return _indexingIsChecked; }
            set { _indexingIsChecked = value; OnPropertyChanged(); }
        }
        private int _sweepPoints;
        public int SweepPoints
        {
            get { return _sweepPoints; }
            set { _sweepPoints = (int)CheckRange(value, 60000, 100); OnPropertyChanged(); }
        }
        private double _highSweep;
        public double HighSweep
        {
            get { return _highSweep; }
            set 
            {
                _highSweep = CheckRange(value, 40e6, LowSweep);
                OnPropertyChanged(); 
            }
        }
        private double _lowSweep;
        public double LowSweep
        {
            get { return _lowSweep; }
            set 
            {
                _lowSweep = CheckRange(value, HighSweep, 1); 
                OnPropertyChanged(); 
            }
        }
        private double _recieverBW;
        public double RecieverBW
        {
            get { return _recieverBW; }
            set 
            {
                _recieverBW = CheckRange(value,3000000,1000); 
                OnPropertyChanged(); 
            }
        }
        private string _sampleName;
        public string SampleName
        {
            get { return _sampleName; }
            set 
            { 
                _sampleName = value;
                if (IndexingIsChecked == true)
                {
                    if (_sampleName != String.Empty)
                    {
                        string sub = _sampleName.Substring(_sampleName.Length - 2);
                        if (sub == "_" + (SampleIndex - 1).ToString()) _sampleName = _sampleName.Substring(0, _sampleName.Length - 2);
                    }
                    _sampleName += String.Format("_{0}", SampleIndex);
                }
                OnPropertyChanged(); 
            }
        }

        private DelegateCommand _export;
        public DelegateCommand Export
        {
            get { return _export; }
            set { _export = value; OnPropertyChanged(); }
        }
        private void DataExport()
        {
            ExportClicked?.Invoke(this, EventArgs.Empty);
        }
        public double CheckRange(double value, double HighRange, double LowRange)
        {
            try
            {
                if (value < LowRange)
                {
                    value = LowRange;
                    throw new ControlOutOfRangeException("Control out of range");
                } 
                else if (value > HighRange)
                {
                    value = HighRange;
                    throw new ControlOutOfRangeException("Control out of range");
                }
            }
            catch (ControlOutOfRangeException)
            {
                MessageBox.Show(string.Format("Control must be in range {0} to {1}",LowRange,HighRange), "Exception Sample", MessageBoxButton.OK);
            }
            return value;
        }
        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set 
            { 
                _enable = value;
                if (_enable == false) SampleIndex++;
                SampleName = _sampleName;
                OnPropertyChanged(); 
            }
        }
    }
}
