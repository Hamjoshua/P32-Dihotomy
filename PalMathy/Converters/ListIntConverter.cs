using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PalMathy.Converters
{
    public class ListIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((List<int>) value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] myStrings = ((string) value).Split(',');
            List<int> list = new List<int>();
            foreach (string str in myStrings) { 
                list.Add(int.Parse(str));
            }

            return list;
        }
    }
}
