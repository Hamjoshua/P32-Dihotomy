using Microsoft.VisualBasic;
using PalMathy.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PalMathy.Converters
{
    public class ListIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Join(", ", (ObservableCollection<int>)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<int> list = new ObservableCollection<int>();

            try
            {
                list.FromString<int>((string)value);
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат списка! Нужно перечисление целых чисел через запятую", "Внимание", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return list;
        }
    }

    public class ListToLinearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Join(", ", (ObservableCollection<int>)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
