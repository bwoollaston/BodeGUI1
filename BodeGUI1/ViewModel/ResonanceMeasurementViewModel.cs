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
    internal class ResonanceMeasurementViewModel : MeasurementFrameViewModelBase
    {

        public ResonanceMeasurementViewModel()
        {
            SweepData = new ObservableCollection<ResonanceSweepData>();
            BodePlot = new ResonancePlotViewModel();
            DeleteRow = new DelegateCommand(Delete);
            ClearData = new DelegateCommand(ClearList);
        }
        private ObservableCollection<ResonanceSweepData> _sweepData;
        public ObservableCollection<ResonanceSweepData> SweepData
        {
            get { return _sweepData; }
            set 
            { 
                _sweepData = value;
                if(BodePlot!=null)BodePlot.SweepData = _sweepData;
                if(_sweepData.Count != 0) SelectedItems = value.Last();
                OnPropertyChanged(); 
            }
        }
        private ResonancePlotViewModel _bodePlot;
        public ResonancePlotViewModel BodePlot
        {
            get { return _bodePlot; }
            set { _bodePlot = value; OnPropertyChanged(); }
        }
        private ResonanceSweepData _selectedItems;
        public ResonanceSweepData SelectedItems
        {
            get { return _selectedItems; }
            set 
            {
                if (value == _selectedItems) ;
                else
                {
                    _selectedItems = value;
                    BodePlot.SelectedData = _selectedItems;
                    OnPropertyChanged();
                }
            }
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
                    SweepData.Remove(SelectedItems);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clear Failed", "Exception Sample", MessageBoxButton.OK);
            }
        }
        public void ClearPlots()
        {
            BodePlot.ImpedanceView.Clear();
            BodePlot.PhaseView.Clear();
        }
    }
}
