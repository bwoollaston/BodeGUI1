using BodeGUI1.ViewModel.DataModel;
using BodeGUI1.ViewModel.Plots;
using BodeGUI1.ViewModel.UI;
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
    internal class ResonanceMeasurementViewModel : ViewModelBase
    {

        public ResonanceMeasurementViewModel()
        {
            Headers = new ObservableCollection<TextBlock>();
            DataTypes = new ObservableCollection<string>() { "Name", "Resonance Frequency [kHz]", "Resonance Impedance [Ω]",
                                                            "Anti-Resonant Frequency [kHz]", "Anti-Resonant Impedance [Ω]",
                                                            "Quality Factor", "Capacitance","Phase [deg]" };
            BodePlot = new ResonancePlotViewModel();
            SweepData = new ObservableCollection<ResonanceSweepDataViewModel>();
            DeleteRow = new DelegateCommand(Delete);
            ClearData = new DelegateCommand(ClearList);
        }
        private ResonancePlotViewModel _bodePlot;
        public ResonancePlotViewModel BodePlot
        {
            get { return _bodePlot; }
            set { _bodePlot = value; OnPropertyChanged(); }
        }
        private ObservableCollection<TextBlock> _headers;
        public ObservableCollection<TextBlock> Headers
        {
            get { return _headers; }
            set { _headers = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _dataTypes;
        public ObservableCollection<string> DataTypes
        {
            get { return _dataTypes; }
            set 
            { 
                _dataTypes = value;
                for(int i = 0;i < _dataTypes.Count; i++)
                {
                    Headers.Add(new TextBlock() { Text = _dataTypes[i], TextWrapping = TextWrapping.Wrap, FontSize=10, HorizontalAlignment=HorizontalAlignment.Center});
                }
                OnPropertyChanged(); 
            }
        }
        private ObservableCollection<ResonanceSweepDataViewModel> _sweepData;
        public ObservableCollection<ResonanceSweepDataViewModel> SweepData
        {
            get { return _sweepData; }
            set { _sweepData = value; OnPropertyChanged(); }
        }
        private List<ResonanceSweepDataViewModel> _selectedItems;
        public List<ResonanceSweepDataViewModel> SelectedItems
        {
            get { return _selectedItems; }
            set { _selectedItems = value; OnPropertyChanged(); }
        }
        private DelegateCommand _clearData;
        public DelegateCommand ClearData
        {
            get { return _clearData; }
            set { _clearData = value; OnPropertyChanged(); }
        }
        private DelegateCommand _deleteRow;
        public DelegateCommand DeleteRow
        {
            get { return _deleteRow; }
            set { _deleteRow=value; OnPropertyChanged(); } 
        }
        private void ClearList()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to clear data?", "Application Shutdown Sample", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)                     //check to make sure the user really wants to clear data
                {
                    SweepData.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clear Failed", "Exception Sample", MessageBoxButton.OK);
            }
        }
        private void Delete()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete selected items?", "Application Shutdown Sample", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)                     //check to make sure the user really wants to clear data
                {
                    foreach(ResonanceSweepDataViewModel item in SelectedItems)
                    {
                        SweepData.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clear Failed", "Exception Sample", MessageBoxButton.OK);
            }
        }
        private double _columnWidth;
        public double ColumnWidth
        {
            get { return _columnWidth; }
            set { _columnWidth = value; OnPropertyChanged(); }
        }
        private double _listWidth;
        public double ListWidth
        {
            get { return _listWidth; }
            set 
            { 
                _listWidth = value-61; 
                ColumnWidth = _listWidth/8;
                OnPropertyChanged(nameof(ListWidth)); 
            } 
        }
    }
}
