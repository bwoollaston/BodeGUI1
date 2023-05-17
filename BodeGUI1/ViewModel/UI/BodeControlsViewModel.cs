using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BodeGUI1.ViewModel;
using Prism.Commands;

namespace BodeGUI1.ViewModel.UI
{
    internal class BodeControlsViewModel : ViewModelBase
    {
        public BodeControlsViewModel()
        {
            Status = new BodeStatusViewModel();
            Run = new DelegateCommand(AdminMeasurement);
        }
        private void AdminMeasurement()
        {
            Enabled = false;
        }
        private BodeStatusViewModel _status;
        public BodeStatusViewModel Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }
        private DelegateCommand _run;
        public DelegateCommand Run
        {
            get { return _run; }
            set { _run = value; OnPropertyChanged();}
        }
        private Visibility _programingActive;
        public Visibility ProgramingActive
        {
            get { return _programingActive; }
            set { _programingActive = value; OnPropertyChanged(); }
        }
        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; OnPropertyChanged();}
        }
    }
}
