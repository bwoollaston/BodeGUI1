﻿using BodeGUI1.ViewModel.UI;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel
{
    internal class ProgViewModel : MeasurementViewModelBase
    {
        public ProgViewModel()
        {
            TabItems = new ObservableCollection<string>() { "Resonance Measurement","Peak Tracking" };
            SelectedTab = TabItems[0]; 
            CurrentContent = new ResonanceMeasurementViewModel();
            BodeControls = new BodeControlsViewModel();
            Parameters = new MeasurementParamtersViewModel();
            BodeControls.StartProgrammingClicked += BodeControls_StartProgrammingClicked;
            CurrentContentWidth = 1000;
            BodeControlsHeight = 80;
        }

        private void BodeControls_StartProgrammingClicked(object? sender, EventArgs e)
        {
            switch (SelectedTab)
            {
                case "Resonance Measurement":
                    Parameters.Enable = false;
                    SweepData.Name = Parameters.SampleName;
                    Sweep(Parameters.LowSweep, Parameters.HighSweep, 201, SweepMode.Logarithmic, Parameters.RecieverBW);
                    ResonanceMeasurementViewModel Content = (ResonanceMeasurementViewModel)CurrentContent;
                    Content.SweepData.Add(SweepData);
                    Content.BodePlot.Points.Clear();
                    Content.BodePlot.Points = new ObservableCollection<OxyPlot.DataPoint>(BodePoints);
                    break;
                case "Peak Tracking":
                    break;
            }
        }

        private string _selectedTab;
        public string SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (_selectedTab == value) return;
                _selectedTab = value;
                if(_selectedTab == TabItems[0]) CurrentContent = new ResonanceMeasurementViewModel() { ListWidth = CurrentContentWidth };
                else if (_selectedTab == TabItems[1]) CurrentContent = new PeakTrackMeasurementViewModel();
                OnPropertyChanged();
            }
        }
        private MeasurementParamtersViewModel _parameters;
        public MeasurementParamtersViewModel Parameters
        {
            get { return _parameters; }
            set { _parameters = value; OnPropertyChanged(); }
        }
        private ViewModelBase _currentContent;
        public ViewModelBase CurrentContent
        {
            get { return _currentContent; }
            set { _currentContent = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _tabItems;
        public ObservableCollection<string> TabItems
        {
            get { return _tabItems; }
            set { _tabItems = value; OnPropertyChanged(); }
        }
        private BodeControlsViewModel _bodeControls;
        public BodeControlsViewModel BodeControls
        {
            get { return _bodeControls; }
            set { _bodeControls=value; OnPropertyChanged();}
        }

        private double _bodeControlsHeight;
        public double BodeControlsHeight
        {
            get { return _bodeControlsHeight; }
            set { _bodeControlsHeight = value; OnPropertyChanged(); }
        }
        private double _currentContentWidth;
        public double CurrentContentWidth
        {
            get { return _currentContentWidth; }
            set { _currentContentWidth = value; OnPropertyChanged(); }
        }
    }
}
