using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Interfaces;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Interfaces.Measurements;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.Enumerations;
using OmicronLab.VectorNetworkAnalysis.AutomationInterface.DataTypes;
using Makaretu.Dns.Resolving;
using System.Diagnostics.Metrics;
using System.Collections.ObjectModel;

namespace BodeGUI1.ViewModel
{
    internal class MeasurementViewModelBase : ViewModelBase
    {
        public OnePortMeasurement measurement;
        public BodeDevice bode;
        public ExecutionState state;
        public BodeAutomationInterface auto = new BodeAutomation();
        public MeasurementViewModelBase()
        {
            Connected = false;
        }

        private bool _connected;
        public bool Connected
        {
            get { return _connected; }
            set { _connected = value; OnPropertyChanged(); }
        }
        public void Connect()
        {
            bode = auto.Connect();
            measurement = bode.Impedance.CreateOnePortMeasurement();
        }
        public void Disconnect()
        {

        }

    }

}
