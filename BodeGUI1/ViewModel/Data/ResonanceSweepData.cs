using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel.DataModel
{
    internal class ResonanceSweepData : ViewModelBase
    {
        public ResonanceSweepData()
        {
            Index = 0;
            Time = DateTime.Now.ToString();
            Name = "Name";
            Capacitance = 0;
            Resfreq = 0;
            Antifreq = 0;
            Res_impedance = 0;
            Anti_impedance = 0;
            QualityFactor = 0;
            Phase = 0;
            ImpdedancePlot = new List<DataPoint>();
            PhasePlot = new List<DataPoint>();
        }

        public ResonanceSweepData Clone()
        {
            List<DataPoint> temp1 = new List<DataPoint>(ImpdedancePlot);
            List<DataPoint> temp2 = new List<DataPoint>(PhasePlot);
            return new ResonanceSweepData()
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
                Phase = Phase,
                ImpdedancePlot = temp1,
                PhasePlot = temp2,
                HighX = HighX,
                LowX = LowX
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
        private List<DataPoint> _impdedancePlot;
        public List<DataPoint> ImpdedancePlot
        {
            get { return _impdedancePlot; }
            set { _impdedancePlot = value;OnPropertyChanged(); }
        }
        private List<DataPoint> _phasePlot;
        public List<DataPoint> PhasePlot
        {
            get { return _phasePlot; }
            set { _phasePlot = value;OnPropertyChanged(); }
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
        public ResonanceSweepData RandClone()
        {
            Random random = new Random();
            List<DataPoint> temp = new List<DataPoint>();
            for (int j = 0; j < 10; j++)
            {
                temp.Add(new DataPoint(Math.Pow(2, j), random.NextDouble() * 1000));
            }
            return new ResonanceSweepData()
            {
                Index = Index,
                Time = Time,
                Name = Name,
                Capacitance = random.Next(1,100)*1e-11,
                Resfreq = (random.NextDouble()*2000)+180000,
                Antifreq = (random.NextDouble() * 2000) + 180000,
                Res_impedance = random.NextDouble() * 2000,
                Anti_impedance = random.NextDouble() * 2000,
                QualityFactor = random.NextDouble() * 2,
                Phase = random.NextDouble() * 180,
                HighX = 190000,
                LowX = 180000,
                ImpdedancePlot = temp
            };
        }
    }
}
