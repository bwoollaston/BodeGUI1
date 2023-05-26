using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;

namespace BodeGUI1.ViewModel.UI
{
    class StatusBase : ViewModelBase
    {
        public StatusBase(string Name)
        {
            this.Name = Name;
            Status = false;
            Background = new SolidColorBrush( Colors.Red );
            Width = 400;
            Height = 20;
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private bool _status;
        public bool Status
        {
            get { return _status; }
            set { 
                    _status = value;
                    if (_status) Background = new SolidColorBrush(Colors.Green);
                    else Background = new SolidColorBrush(Colors.Red);
                    OnPropertyChanged(); 
                }
        }
        
        private Brush _background;
        public Brush Background
        {
            get { return _background; }
            set { _background = value; OnPropertyChanged();}
        }
        private double _height;
        public double Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged(); }
        }
        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged(); }
        }

    }
}
