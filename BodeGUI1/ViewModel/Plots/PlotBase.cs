using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace BodeGUI1.ViewModel.Plots
{
    internal class PlotBase : ViewModelBase
    {
        public PlotBase()
        {
            XLabel = "Frequency [Decades]";
            YLabel = "Impedance [Ω]";
            Xaxes = new LogarithmicAxis();
            Yaxes = new LinearAxis();
            DataPlotModel = new PlotModel();
            DataPlotModel.Axes.Add(Xaxes);
            DataPlotModel.Axes.Add(Yaxes);
            Series1 = new LineSeries();
        }
        private LinearAxis _yaxes;
        public LinearAxis Yaxes
        {
            get { return  _yaxes; }
            set 
            { 
                _yaxes = value;
                Yaxes.Position = AxisPosition.Left;
                Yaxes.Title = YLabel;
            }
        }
        private LogarithmicAxis _xaxes;
        public LogarithmicAxis Xaxes
        {
            get { return _xaxes; }
            set 
            { 
                _xaxes = value;
                Xaxes.Position = AxisPosition.Bottom;
                Xaxes.Title = XLabel;
                OnPropertyChanged();
            }
        }
        private LineSeries _series1;
        public LineSeries Series1
        {
            get { return _series1; }
            set { _series1 = value; OnPropertyChanged(); }
        }
        private PlotModel _dataPlotModel;
        public PlotModel DataPlotModel
        {
            get { return _dataPlotModel; }
            set { _dataPlotModel = value; OnPropertyChanged(); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set 
            {   _title = value;
                DataPlotModel.Title = value;
                OnPropertyChanged(); 
            }
        }
        private string _xlabel;
        public string XLabel
        {
            get { return _xlabel; }
            set 
            {   _xlabel = value; 
                OnPropertyChanged();
            }
        }
        private string _ylabel;
        public string YLabel
        {
            get { return _ylabel; }
            set { _ylabel = value; OnPropertyChanged();}
        }
        private double[] _xrange;
        public double[] Xrange
        {
            get { return _xrange; }
            set 
            {
                if (_xrange.Length != 2) return;
                _xrange = value;
                Xaxes.Minimum = _xrange[0];
                Xaxes.Maximum = _xrange[1];
                OnPropertyChanged();
            }
        }
    }
}
