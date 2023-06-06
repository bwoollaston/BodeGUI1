using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ViewModel.UI
{
    internal class BodeSettingsViewModel : ViewModelBase
    {
        public event EventHandler ConnectClicked;
        public event EventHandler OpenClicked;
        public event EventHandler ShortClicked;
        public event EventHandler LoadClicked;
        public BodeSettingsViewModel()
        {
            Enable = true;
            CalResistor = 100;
            Connect = new DelegateCommand(BodeConnect);
            Open = new DelegateCommand(CalOpen);
            Short = new DelegateCommand(CalShort);
            Load = new DelegateCommand(CalLoad);

        }
        private void BodeConnect()
        {
            ConnectClicked?.Invoke(this, EventArgs.Empty);
        }
        private void CalOpen()
        {
            OpenClicked?.Invoke(this, EventArgs.Empty);
        }
        private void CalShort()
        {
            ShortClicked?.Invoke(this, EventArgs.Empty);
        }
        private void CalLoad()
        {
            LoadClicked?.Invoke(this, EventArgs.Empty);
        }
        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; OnPropertyChanged(); }
        }
        private DelegateCommand _connect;
        public DelegateCommand Connect
        {
            get { return _connect; }
            set { _connect = value; OnPropertyChanged(); }
        }
        private DelegateCommand _open;
        public DelegateCommand Open
        {
            get { return _open; }
            set { _open = value; OnPropertyChanged(); }
        }
        private DelegateCommand _short;
        public DelegateCommand Short
        {
            get { return _short; }
            set { _short = value; OnPropertyChanged(); }
        }
        private DelegateCommand _load;
        public DelegateCommand Load
        {
            get { return _load; }
            set { _load = value; OnPropertyChanged(); }
        }
        private double _calResistor;
        public double CalResistor
        {
            get { return _calResistor; }
            set { _calResistor = value; OnPropertyChanged(); }
        }
    }
}
