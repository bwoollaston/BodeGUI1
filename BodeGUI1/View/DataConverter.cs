using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BodeGUI1.View
{
    public class BandwidthConverter : IValueConverter
    {
        public object Convert(object value, Type String, object parameter, CultureInfo culture)
        {
            if (value == null) throw new NotImplementedException();
            double ret = ((double)value) / 1000;
            return ret.ToString();
        }
        public object ConvertBack(object value, Type String, object parameter, CultureInfo culture)
        {

        }
    }
    public class MagnitudeConverter : IValueConverter
    {
        public MagnitudeConverter()
        {
            unitType = "unit";
        }
        public object Convert(object value, Type String, object parameter ,CultureInfo culture)
        {
            if (value == null) throw new NotImplementedException();
            double val = (double)value;
            string mag;
            if (val < 1) 
            {
                if(val*1e3 > 1 )
                {
                    mag = "m";
                } else if(val*1e6 > 1)
                {
                    mag = "u";
                } else if (val * 1e9 > 1)
                {
                    mag = "n";
                } else if (val * 1e12 > 1)
                {
                    mag = "p";
                }
            }
            else
            {
                if(val < 1e3)
                {
                    mag = "";
                } else if (val < 1e6)
                {
                    mag = "k";
                } else if (val < 1e9)
                {
                    mag = "M";
                }
                else if (val < 1e12)
                {
                    mag = "G";
                }
            }
        }
        public object ConvertBack(object value, Type String, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public string Append(string mag)
        {
            if(unitType==null) return(mag);
            return (mag + unitType);
        }
        public string unitType { get; set; }
    }
}
