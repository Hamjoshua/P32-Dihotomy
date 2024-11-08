using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Sortings
{
    public class SingleReport
    {
        public BaseSorting Sorting { get; set; }
        public long ExecutingTime { get; set; }
        public DateTime Begin { get; set; }
        public DateTime Difference;
        public ObservableCollection<int> Elements { get; set; }
        public SingleReport(BaseSorting sorting, ObservableCollection<int> elements)
        {
            Sorting = sorting;
            Elements = elements;
        }
        
        // TODO проверка на правильную сортировку

        // TODO отмена действия
        public async Task BeginSort()
        {
            Begin = DateTime.Now;
            var watch = Stopwatch.StartNew();
            Sorting.Sort(Elements);            
            watch.Stop();
            ExecutingTime = watch.ElapsedMilliseconds;
        }
    }
}
