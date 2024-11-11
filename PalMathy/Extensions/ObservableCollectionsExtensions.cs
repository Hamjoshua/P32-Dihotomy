﻿using System;
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
            string[] myStrings = value.Split(", ");
            elements.Clear();
            foreach (string str in myStrings)
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {                    
                    elements.Add((T)converter.ConvertFromString(str));
                }                
            }            
        }

        public static void Swap<T>(this ObservableCollection<T> elements, int firstIndex, int secondIndex)
        {
            T temp = elements[firstIndex];
            elements[firstIndex] = elements[secondIndex];
            elements[secondIndex] = temp;
        }
    }
}
