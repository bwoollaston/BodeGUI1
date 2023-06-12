using BodeGUI1.ViewModel.Data;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel.DataModel
{
    internal class ResonanceSweepData : TableData
    {
        public ResonanceSweepData()
        {
            Time = DateTime.Now.ToString();
            Name = "Name";
            ImpdedancePlot = new List<DataPoint>();
            PhasePlot = new List<DataPoint>();
            PeakDataTable = new List<TableData> { };
            for(int i = 0; i < 10; i++)
            {
                PeakDataTable.Add(RandCloneTableData());
            }
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
        private List<DataPoint> _impdedancePlot;
        public List<DataPoint> ImpdedancePlot
        {
            get { return _impdedancePlot; }
            set { _impdedancePlot = value;OnPropertyChanged(); }
        }
        private List<DataPoint> _threshline;
        public List<DataPoint> Threshline
        {
            get { return _threshline; }
            set { _threshline = value;OnPropertyChanged(); }
        }
        private List<TableData> _peakDataTable;
        public List<TableData> PeakDataTable
        {
            get { return _peakDataTable; }
            set { _peakDataTable = value;OnPropertyChanged(); }
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
        public TableData RandCloneTableData()
        {
            Random random = new Random();
            return new TableData()
            {
                Index = Index,
                Capacitance = random.Next(1, 100) * 1e-11,
                Resfreq = (random.NextDouble() * 2000) + 180000,
                Antifreq = (random.NextDouble() * 2000) + 180000,
                Res_impedance = random.NextDouble() * 2000,
                Anti_impedance = random.NextDouble() * 2000,
                QualityFactor = random.NextDouble() * 2,
                Phase = random.NextDouble() * 180
            };
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
