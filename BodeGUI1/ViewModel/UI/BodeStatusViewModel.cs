using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace BodeGUI1.ViewModel.UI
{
    internal class BodeStatusViewModel : ViewModelBase
    {
        public BodeStatusViewModel()
        {
            FontSize = 10;
            StatusCollection = new ObservableCollection<StatusBase> { new StatusBase("Connect"),new StatusBase("Open"), new StatusBase("Short") , new StatusBase("Load") };
        }
        private ObservableCollection<StatusBase> _statusCollection;
        public ObservableCollection<StatusBase> StatusCollection
        {
            get { return _statusCollection; }
            set { _statusCollection = value; OnPropertyChanged(); }
        }
        private double _fontSize;
        public double FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; OnPropertyChanged(); }
        }
    }
}
