using BodeGUI1.ViewModel.UI;
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
        }
        private string _selectedTab;
        public string SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (_selectedTab == value) return;
                _selectedTab = value;
                if(_selectedTab == TabItems[0]) CurrentContent = new ResonanceMeasurementViewModel();
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
        private double _controlHeight;
        public double ControlHeight
        {
            get { return _controlHeight; }
            set { _controlHeight = value; OnPropertyChanged(); }
        }
        private double _controlWidth;
        public double ControlWidth
        {
            get { return _controlWidth; }
            set { _controlWidth = value; OnPropertyChanged(); }
        }
    }
}
