﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;

namespace BodeGUI1.ViewModel.UI
{
    internal class BodeControlsViewModel : ViewModelBase
    {
        public event EventHandler StartProgrammingClicked;
        CancellationTokenSource cts = new CancellationTokenSource();
        public BodeControlsViewModel()
        {
            ProgramingActive = Visibility.Collapsed;
            Enabled = true;
            CurrentProgress = 0;
            Status = new BodeStatusViewModel();
            Run = new DelegateCommand(AdminMeasurement);

        }
        private void AdminMeasurement()
        {
            StartProgrammingClicked?.Invoke(this,EventArgs.Empty);
            Enabled = false;
            CurrentProgress = 0;
            ProgramingActive = Visibility.Visible;
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
        private int _currentProgress;
        public int CurrentProgress
        {
            get { return _currentProgress; }
            set { _currentProgress = value; OnPropertyChanged(); }
        }
        public async Task AnimateBar()
        {
            while (!cts.Token.IsCancellationRequested)
            {
                CurrentProgress++;
                await Task.Delay(200);
            }
        }
    }
}
