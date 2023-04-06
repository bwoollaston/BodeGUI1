using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel
{
    internal class BodeStatusViewModel : ViewModelBase
    {
        public BodeStatusViewModel()
        {
            CalibrationStatus = false;
            ConnectionStatus = false;
            LoadCalibrationStatus = false;
            ShortCalibrationStatus = false;
            OpenCalibrationStatus = false;
        }
        private bool _calibrationStatus;
        public bool CalibrationStatus
        {
            get { return _calibrationStatus; }
            set { _calibrationStatus = value; OnPropertyChanged(); }
        }
        private bool _connectionStatus;
        public bool ConnectionStatus
        {
            get { return _connectionStatus; }
            set { _connectionStatus = value; OnPropertyChanged(); }
        }
        private bool _loadCalibrationStatus;
        public bool LoadCalibrationStatus
        {
            get { return _loadCalibrationStatus; }
            set { _loadCalibrationStatus = value; OnPropertyChanged(); }
        }
        private bool _shortCalibrationStatus;
        public bool ShortCalibrationStatus
        {
            get { return _shortCalibrationStatus; }
            set { _shortCalibrationStatus=value; OnPropertyChanged(); }
        }
        private bool _openCalibrationStatus;
        public bool OpenCalibrationStatus
        {
            get { return _openCalibrationStatus; }
            set { _openCalibrationStatus = value; OnPropertyChanged(); }
        }
    }
}
