using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    }
}
