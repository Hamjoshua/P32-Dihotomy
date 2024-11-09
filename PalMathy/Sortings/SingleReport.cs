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
        public long ExecutingTime { get; set; } = -1;
        public DateTime Begin { get; set; }
        public bool SortingIsOver { get; set; } = false;
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
            var elems = new ObservableCollection<int>(Elements.ToList());

            Begin = DateTime.Now;
            var watch = Stopwatch.StartNew();
            Elements = Sorting.Sort(elems);
            watch.Stop();
            ExecutingTime = watch.ElapsedMilliseconds;
            SortingIsOver = true;
        }
    }
}
