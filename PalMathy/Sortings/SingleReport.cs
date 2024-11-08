using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Sortings
{
    class SingleReport
    {
        public BaseSorting Sorting;
        public long ExecutingTime;
        public DateTime Begin;
        public DateTime Difference;
        public List<int> Elements;
        public SingleReport(BaseSorting sorting, List<int> elements)
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
