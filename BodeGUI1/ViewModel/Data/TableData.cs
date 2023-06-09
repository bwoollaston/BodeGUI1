using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel.Data
{
    internal class TableData : ViewModelBase
    {
        public TableData()
        {
            Index = 0;
            Capacitance = 0;
            Resfreq = 0;
            Antifreq = 0;
            Res_impedance = 0;
            Anti_impedance = 0;
            QualityFactor = 0;
            Phase = 0;
        }
        private int _index;
        public int Index
        {
            get { return _index; }
            set { _index = value; OnPropertyChanged(); }
        }
        private double _resfreq;
        public double Resfreq
        {
            get { return _resfreq; }
            set { _resfreq = value; OnPropertyChanged(); }
        }
        private double _antifreq;
        public double Antifreq
        {
            get { return _antifreq; }
            set { _antifreq = value; OnPropertyChanged(); }
        }
        private double _res_impedance;
        public double Res_impedance
        {
            get { return _res_impedance; }
            set { _res_impedance = value; OnPropertyChanged(); }
        }
        private double _anti_impedance;
        public double Anti_impedance
        {
            get { return _anti_impedance; }
            set { _anti_impedance = value; OnPropertyChanged(); }
        }
        private double _capacitance;
        public double Capacitance
        {
            get { return _capacitance; }
            set { _capacitance = value; OnPropertyChanged(); }
        }
        private double _resistance;
        public double Resistance
        {
            get { return _resistance; }
            set { _resistance = value; OnPropertyChanged(); }
        }
        private double _qualityFactor;
        public double QualityFactor
        {
            get { return _qualityFactor; }
            set { _qualityFactor = value; OnPropertyChanged(); }
        }
        private double _phase;
        public double Phase
        {
            get { return _phase; }
            set { _phase = value; OnPropertyChanged(); }
        }
    }
}
