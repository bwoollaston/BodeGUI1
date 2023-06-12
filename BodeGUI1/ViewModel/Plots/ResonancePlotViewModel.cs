using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using BodeGUI1.ViewModel.DataModel;

namespace BodeGUI1.ViewModel.Plots
{
    internal class ResonancePlotViewModel : PlotViewModelBase
    {
        public ResonancePlotViewModel()
        {
            Title = "Bode Impedance vs. Frequency Plot";
            //HighX = 190000;
            //LowX = 180000;
        }
        public async void UpdateUI()
        {
            ImpedanceView.Clear();
            PhaseView.Clear();
            int delay = 2000;
            int dt = delay/SelectedData.ImpdedancePlot.Count;
            int i = 0;
            foreach(DataPoint element in SelectedData.ImpdedancePlot)
            {
                ImpedanceView.Add(element);
                PhaseView.Add(SelectedData.PhasePlot[i]);
                await Task.Delay(dt);
                i++;
            }
        }
    }
}
