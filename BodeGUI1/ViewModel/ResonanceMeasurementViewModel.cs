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
            BodePlot = new ResonancePlotViewModel();
            SweepData = new ObservableCollection<ResonanceSweepData>();
            DeleteRow = new DelegateCommand(Delete);
            ClearData = new DelegateCommand(ClearList);
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
                    BodePlot.ImpedanceHistory.Clear();
                    BodePlot.PhaseHistory.Clear();
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
                    //foreach(ResonanceSweepData item in SelectedItems)
                    //{
                    //    SweepData.Remove(item);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clear Failed", "Exception Sample", MessageBoxButton.OK);
            }
        }
        public void ClearPlots()
        {
            BodePlot.Impedance.Clear();
            BodePlot.Phase.Clear();
            BodePlot.ImpedanceView.Clear();
            BodePlot.PhaseView.Clear();
        }
    }
}
