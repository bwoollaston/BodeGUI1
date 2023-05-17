﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BodeGUI1.ExceptionHandlers;
using Prism.Commands;

namespace BodeGUI1.ViewModel.UI
{
    internal class MeasurementParamtersViewModel : ViewModelBase
    {
        public MeasurementParamtersViewModel()
        {
            HighSweep = 190000;
            LowSweep = 180000;
            RecieverBW = 100000;
            SampleName = "";
            IndexingIsChecked = false;
            Enable = true;
        }
        private bool _indexingIsChecked;
        public bool IndexingIsChecked
        {
            get { return _indexingIsChecked; }
            set { _indexingIsChecked = value; OnPropertyChanged(); }
        }
        private double _highSweep;
        public double HighSweep
        {
            get { return _highSweep; }
            set 
            {
                _highSweep = CheckRange(value, 40e6, LowSweep);
                OnPropertyChanged(); 
            }
        }
        private double _lowSweep;
        public double LowSweep
        {
            get { return _lowSweep; }
            set 
            {
                _lowSweep = CheckRange(value, HighSweep, 1); 
                OnPropertyChanged(); 
            }
        }
        private double _recieverBW;
        public double RecieverBW
        {
            get { return _recieverBW; }
            set 
            {
                _recieverBW = CheckRange(value,3000000,1000); 
                OnPropertyChanged(); 
            }
        }
        private string _sampleName;
        public string SampleName
        {
            get { return _sampleName; }
            set { _sampleName = value; OnPropertyChanged(); }
        }
        public double CheckRange(double value, double HighRange, double LowRange)
        {
            try
            {
                if (value < LowRange)
                {
                    value = LowRange;
                    throw new ControlOutOfRangeException("Control out of range");
                } 
                else if (value > HighRange)
                {
                    value = HighRange;
                    throw new ControlOutOfRangeException("Control out of range");
                }
            }
            catch (ControlOutOfRangeException)
            {
                MessageBox.Show(string.Format("Control must be in range {0} to {1}",LowRange,HighRange), "Exception Sample", MessageBoxButton.OK);
            }
            return value;
        }
        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value;OnPropertyChanged(); }
        }
    }
}
