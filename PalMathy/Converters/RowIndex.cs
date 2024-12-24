using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PalMathy.Converters
{
    public class RowIndexConverter : IValueConverter
    {
        static int _hash = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DataGridRow row)
            {
                //for(int index = 0; index < row.GetValue)
                //string rowVal = (string) row.Get;
                if(_hash == 0)
                {
                    _hash = row.GetHashCode();
                    return 0;
                }
                else
                {
                    return 1;
                }                
            }
            return -1; // default if not DataGridRow
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
