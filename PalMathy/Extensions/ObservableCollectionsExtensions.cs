using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Extensions
{
    public static class ObservableCollectionsExtensions
    {
        public static void Shuffle<T>(this ObservableCollection<T> elements)
        {
            var count = elements.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = Random.Shared.Next(i, count);
                var tmp = elements[i];
                elements[i] = elements[r];
                elements[r] = tmp;
            }
        }

        public static void FromString<T>(this ObservableCollection<T> elements, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return;
            }
            value = value.Replace(" ", "");
            string[] myStrings = value.Split(",");
            elements.Clear();
            foreach (string str in myStrings)
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {                    
                    elements.Add((T) converter.ConvertFromString(str));
                }                
            }            
        }

        public static bool IsSorted(this ObservableCollection<int> elements, bool isBiggerMode)
        {
            for (int i = 0; i < elements.Count - 1; ++i)
            {
                if (isBiggerMode)
                {
                    if (elements[i] > elements[i + 1])
                    {
                        return false;
                    }
                }
                else
                {
                    if (elements[i] < elements[i + 1])
                    {
                        return false;
                    }
                }
                
            }

            return true;
        }
    }
}
