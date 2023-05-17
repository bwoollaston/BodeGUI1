using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BodeGUI1.ViewModel.UI
{
    internal class BodeStatusViewModel : ViewModelBase
    {
        public BodeStatusViewModel()
        {
            StatusCollection = new ObservableCollection<StatusBase> { new StatusBase("Connect"),new StatusBase("Open"),
                                                                      new StatusBase("Short") , new StatusBase("Load") };
        }
        private ObservableCollection<StatusBase> _statusCollection;
        public ObservableCollection<StatusBase> StatusCollection
        {
            get { return _statusCollection; }
            set { _statusCollection = value; OnPropertyChanged(); }
        }
        private double _controlHeight;
        public double ControlHeight
        {
            get { return _controlHeight; }
            set 
            { 
                _controlHeight = value; 
                ItemHeight = value/4;
                OnPropertyChanged(); 
            }
        }
        private double _itemHeight;
        public double ItemHeight
        {
            get { return _itemHeight; }
            set { _itemHeight = value; OnPropertyChanged(); }
        }
    }
}
