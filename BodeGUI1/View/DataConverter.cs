
using Mages.Core.Runtime.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace BodeGUI1.View.SizeConverter
{
    public class GridToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double doubleSize = (double)value;
            GridLength gridSize = new GridLength(doubleSize);
            return gridSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double doubleSize;
            GridLength gridSize = (GridLength)value;
            doubleSize = gridSize.Value;
            return doubleSize;
        }
    }
}

namespace BodeGUI1.View.BWConverter
{
    public class BandwidthConverter : IValueConverter
    {
        public object Convert(object value, Type String, object parameter, CultureInfo culture)
        {
            ConverterFunctions converterFunctions = new ConverterFunctions();
            MagnitudeType magnitudeType = new MagnitudeType();
            if (value == null) throw new NotImplementedException();
            string unitType = (string)parameter;
            double val = (double)value / 1000;
            magnitudeType = converterFunctions.MagnitudeFunction(val);
            string unit = magnitudeType.Munit;
            val = val / magnitudeType.Mval;
            if (unit == "base") unit = "";
            string mag = unit + unitType;
            mag = val.ToString() + " " + mag + "Hz";
            return mag;
        }
        public object ConvertBack(object value, Type String, object parameter, CultureInfo culture)
        {
            try
            {
                double magnitude = 1;
                ConverterFunctions converterFunctions = new ConverterFunctions();
                if (value == null) throw new NotImplementedException();
                MagnitudeType magnitudeType = converterFunctions.MagStringToMagType((string)value);
                double val = magnitudeType.Mval;
                magnitude = converterFunctions.GetMagnitude(magnitudeType);
                return val * magnitude * 1000;
            }
            catch
            {
                MessageBox.Show("NotImplementedException", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return Binding.DoNothing;
            }
        }
    }
}
    namespace BodeGUI1.View.MagConverter
    {
        public class MagnitudeUnitConverter : IValueConverter
        {
            public object Convert(object value, Type String, object parameter, CultureInfo culture)
            {
                ConverterFunctions converterFunctions = new ConverterFunctions();
                MagnitudeType magnitudeType = new MagnitudeType();
                if (value == null) throw new NotImplementedException();
                string unitType = (string)parameter;
                double val = (double)value;
                magnitudeType = converterFunctions.MagnitudeFunction(val);
                string unit = magnitudeType.Munit;
                val = val / magnitudeType.Mval;
                if (unit == "base") unit = "";
                string mag = Append(unit, unitType);
                mag = val.ToString() + " " + mag;
                return mag;
            }
            public object ConvertBack(object value, Type String, object parameter, CultureInfo culture)
            {
                try
                {
                    double magnitude = 1;
                    ConverterFunctions converterFunctions = new ConverterFunctions();
                    if (value == null) throw new NotImplementedException();
                    MagnitudeType magnitudeType = converterFunctions.MagStringToMagType((string)value);
                    double val = magnitudeType.Mval;
                    magnitude = converterFunctions.GetMagnitude(magnitudeType);
                    return val * magnitude;
                }
                catch
                {
                    MessageBox.Show("NotImplementedException", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return Binding.DoNothing;
                }
            }

            private string Append(string mag, string uType)
            {
                if (uType == null) return (mag);
                return (mag + uType);
            }
        }
    }
namespace BodeGUI1.View
{
    public class ConverterFunctions
    {
        public ConverterFunctions() { }
        public MagnitudeType MagnitudeFunction(double val)
        {
            MagnitudeTypes types = new MagnitudeTypes();
            MagnitudeType mag = new MagnitudeType();
            foreach (MagnitudeType element in types)
            {
                if (val >= element.Mval)
                {
                    mag = element;
                    break;
                }
                else if (val < 1e12) mag = types.Last();
            }
            return mag;
        }
        public MagnitudeType MagStringToMagType(string magString)
        {
            StringBuilder ValueString = new StringBuilder();
            StringBuilder UnitString = new StringBuilder();
            foreach (char c in magString)
            {
                if (char.IsDigit(c)) ValueString.Append(c);
                else
                {
                    if (char.IsWhiteSpace(c) != true)
                    {
                        if ("pnumkMGT".Contains(c) != true) UnitString.Append("base");
                        else UnitString.Append(c);
                        break;
                    }
                }
            }
            MagnitudeType magnitudeType = new MagnitudeType()
            {
                Munit = UnitString.ToString(),
                Mval = StringToDouble(ValueString)
            };
            return magnitudeType;
        }
        public double StringToDouble(StringBuilder numericString)
        {
            double result;

            if (double.TryParse(numericString.ToString(), out result))
            {
                return result;
            }
            else
            {
                // Handle the case when the conversion fails.
                // You can throw an exception, return a default value, or take appropriate action.
                throw new InvalidOperationException("Invalid numeric string.");
            }
        }
        public double GetMagnitude(MagnitudeType mType)
        {
            double returnVal = 1;
            MagnitudeTypes magnitudeTypes = new MagnitudeTypes();
            foreach (MagnitudeType element in magnitudeTypes)
            {
                if (element.Munit == mType.Munit) returnVal = element.Mval;
            }
            return returnVal;
        }
    }

    public class MagnitudeTypes : List<MagnitudeType>
    {
        public MagnitudeTypes()
        {
            this.Add(new MagnitudeType() { Munit = "T", Mval = 1e12 });
            this.Add(new MagnitudeType() { Munit = "G", Mval = 1e9 });
            this.Add(new MagnitudeType() { Munit = "M", Mval = 1e6 });
            this.Add(new MagnitudeType() { Munit = "k", Mval = 1e3 });
            this.Add(new MagnitudeType() { Munit = "base", Mval = 1 });
            this.Add(new MagnitudeType() { Munit = "m", Mval = 1e-3 });
            this.Add(new MagnitudeType() { Munit = "u", Mval = 1e-6 });
            this.Add(new MagnitudeType() { Munit = "n", Mval = 1e-9 });
            this.Add(new MagnitudeType() { Munit = "p", Mval = 1e-12 });
        }
    }
    public class MagnitudeType
    {
        public MagnitudeType() { Munit = string.Empty; }
        public double Mval { get; set; }
        public string Munit { get; set; }
    }


    /* Keeping For Later */

    public class UnitTypes
    {
        public UnitTypes()
        {
            Mass = "g";
            Frequency = "Hz";
            Impedance = new ImpedanceUnit();
        }
        public string Mass { get; private set; }
        public string Frequency { get; private set; }
        public ImpedanceUnit Impedance { get; private set; }
    }
    public class ImpedanceUnit
    {
        public ImpedanceUnit()
        {
            Linear = "Ω";
            dB = "dB";
        }
        public string Linear;
        public string dB;
    }
}