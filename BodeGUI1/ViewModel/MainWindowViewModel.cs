using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BodeGUI1.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            ProgViewModel = new ProgViewModel();
            Height = 900;
            Width = 700;
        }
        private ProgViewModel progViewModel;
        private double _height;
        public double Height
        {
            get { return _height; }
            set { _height = value; ProgViewModel.ControlHeight = _height; OnPropertyChanged(); }
        }
        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; ProgViewModel.ControlWidth = _width; OnPropertyChanged(); }
        }
        public ProgViewModel ProgViewModel
        {
            get { return progViewModel; }
            set
            {
                progViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
