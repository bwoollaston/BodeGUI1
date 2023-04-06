using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel
{
    internal class ProgViewModel : ViewModelBase
    {
        public ProgViewModel()
        {
            TabItems = new ObservableCollection<string>() { "Resonance Measurement","Peak Tracking" };
            SelectedTab = TabItems[0]; 
            CurrentContent = new ResonanceMeasurementViewModel();
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
    }
}
