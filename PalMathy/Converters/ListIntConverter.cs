using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            return String.Join(", ", (ObservableCollection<int>) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] myStrings = ((string) value).Split(", ");
            ObservableCollection<int> list = new ObservableCollection<int>();
            foreach (string str in myStrings) {
                try
                {
                    list.Add(int.Parse(str));
                }
                catch
                {

                }
            }

            return list;
        }
    }
}
