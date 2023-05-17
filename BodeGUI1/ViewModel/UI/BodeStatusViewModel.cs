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
            FontSize = 8;
            BorderThicknes = 2;
            BorderColor = new SolidColorBrush(Colors.Black);
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
        private double _borderThicknes;
        public double BorderThicknes
        {
            get { return _borderThicknes; }
            set { _borderThicknes = value; OnPropertyChanged(); }   
        }
        private SolidColorBrush _borderColor;
        public SolidColorBrush BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; OnPropertyChanged(); }
        }

    }
}
