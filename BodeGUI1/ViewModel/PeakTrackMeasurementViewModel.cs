using BodeGUI1.ViewModel.Data;
using BodeGUI1.ViewModel.DataModel;
using BodeGUI1.ViewModel.Plots;
using BodeGUI1.ViewModel.UI;
using OxyPlot;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace BodeGUI1.ViewModel
{
    internal class PeakTrackMeasurementViewModel : MeasurementFrameViewModelBase
    {
        public PeakTrackMeasurementViewModel()
        {
            SweepData = new ObservableCollection<ResonanceSweepData>();
            CurrentData = new ObservableCollection<TableData>();
            BodePlot = new PeakPlotViewModel();
            IndexDataRight = new DelegateCommand(IndexRight);
            IndexDataLeft = new DelegateCommand(IndexLeft);
        }
        private int _tableDataIndex;
        public int TableDataIndex
        {
            get { return _tableDataIndex; }
            set 
            { 
                _tableDataIndex = value;
                CurrentData = new ObservableCollection<TableData>(SweepData[_tableDataIndex].PeakDataTable);
                BodePlot.SelectedData = SweepData[_tableDataIndex];
                OnPropertyChanged(); 
            }
        }
        private DelegateCommand _indexDataRight;
        public DelegateCommand IndexDataRight
        {
            get { return _indexDataRight; }
            set { _indexDataRight = value; OnPropertyChanged(); }
        }
        private DelegateCommand _indexDataLeft;
        public DelegateCommand IndexDataLeft
        {
            get { return _indexDataLeft; }
            set { _indexDataLeft = value; OnPropertyChanged(); }
        }
        private ObservableCollection<TableData> _currentData;
        public ObservableCollection<TableData> CurrentData
        {
            get { return _currentData; }
            set { _currentData = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ResonanceSweepData> _sweepData;
        public ObservableCollection<ResonanceSweepData> SweepData
        {
            get { return _sweepData; }
            set
            {
                _sweepData = value;
                if(_sweepData.Count!=0) CurrentData = new ObservableCollection<TableData>(_sweepData[_sweepData.Count - 1].PeakDataTable); 
                OnPropertyChanged(); 
            }
        }
        private PeakPlotViewModel _bodePlot;
        public PeakPlotViewModel BodePlot
        {
            get { return _bodePlot; }
            set { _bodePlot = value; OnPropertyChanged(); }
        }
        private void IndexRight()
        {
            if (TableDataIndex < SweepData.Count - 1) TableDataIndex++;
        }
        private void IndexLeft()
        {
            if (TableDataIndex > 0) TableDataIndex--;
        }
        public void ClearPlots()
        {
            BodePlot.SmoothPts.Clear();
            BodePlot.ImpedanceView.Clear();
            BodePlot.PhaseView.Clear();
        }
    }
}
