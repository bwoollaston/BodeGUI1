﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
        private double _highSweep;
        public double HighSweep
        {
            get { return _highSweep; }
            set 
            {
                if (value <= LowSweep) value = LowSweep + (RecieverBW/1000);
                _highSweep = value; 
                OnPropertyChanged(); 
            }
        }
        private double _lowSweep;
        public double LowSweep
        {
            get { return _lowSweep; }
            set 
            {
                if (value >= HighSweep) value = HighSweep - (RecieverBW/1000);
                _lowSweep = value; 
                OnPropertyChanged(); 
            }
        }
        private double _recieverBW;
        public double RecieverBW
        {
            get { return _recieverBW; }
            set 
            {
                value *= 1000;
                if(value < 1000 ) value = 1000;
                else if (value > 3000000) value = 3000000;
                _recieverBW = value; 
                OnPropertyChanged(); 
            }
        }
        private string _sampleName;
        public string SampleName
        {
            get { return _sampleName; }
            set { _sampleName = value; OnPropertyChanged(); }
        }
    }
}