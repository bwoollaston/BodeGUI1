using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel.DataModel
{
    internal class ResonanceSweepDataViewModel : ViewModelBase
    {
        public ResonanceSweepDataViewModel()
        {
            Index = 0;
            Time = "";
            Name = "Name";
            Capacitance = 0;
            Resfreq = 0;
            Antifreq = 0;
            Res_impedance = 0;
            Anti_impedance = 0;
            QualityFactor = 0;
            Phase = 0;
        }

        public ResonanceSweepDataViewModel Clone()
        {
            return new ResonanceSweepDataViewModel()
            {
                Index = Index,
                Time = Time,
                Name = Name,
                Capacitance = Capacitance,
                Resfreq = Resfreq,
                Antifreq = Antifreq,
                Res_impedance=Res_impedance,
                Anti_impedance=Anti_impedance,
                QualityFactor = QualityFactor,
                Phase = Phase
            };
        }

        private int _index;
        public int Index
        {
            get { return _index; }
            set { _index = value; OnPropertyChanged(); }
        }
        private string _time;
        public string Time
        {
            get { return _time; }
            set { _time = value; OnPropertyChanged(); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
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
            set {_resistance = value;OnPropertyChanged();}
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

        //used to autoscale plot
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
    }
}
