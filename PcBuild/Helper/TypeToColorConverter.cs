using PcBuild.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PcBuild.Helper
{
    public class TypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var comp = (PartType)value;
            if (comp == PartType.CPU)
                return Brushes.Red;
            if (comp == PartType.SSD)
                return Brushes.Blue;
            if (comp == PartType.RAM)
                return Brushes.Green;
            if (comp == PartType.MOTHERBOARD)
                return Brushes.Gold;
            else
                return null;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
