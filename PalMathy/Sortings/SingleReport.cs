using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Sortings
{
    public class SingleReport : INotifyPropertyChanged
    {
        public BaseSorting Sorting { get; set; }
        public long ExecutingTime { get; set; } = -1;
        public DateTime Begin { get; set; }
        public bool SortingIsOver { get; set; } = false;
        public int IterationsCount { get; set; } = 0;
        public ObservableCollection<int> Elements { get; set; }
        public SingleReport(BaseSorting sorting, ObservableCollection<int> elements)
        {
            Sorting = sorting;
            Elements = elements;
        }

        public async Task BeginSort(bool isBiggerMode)
        {
            var elems = new ObservableCollection<int>(Elements.ToList());

            Begin = DateTime.Now;
            var watch = Stopwatch.StartNew();
            SortingResult result = Sorting.Sort(elems, isBiggerMode);
            Elements = result.Elements;
            IterationsCount = result.IterationsCount;
            watch.Stop();            
            ExecutingTime = watch.ElapsedMilliseconds;
            SortingIsOver = true;

            // TODO использовать OnPropertyChanged в модели - харам. Нужно переделать структуру, сейчас это костыль
            OnPropertyChanged(nameof(IterationsCount));
            OnPropertyChanged(nameof(ExecutingTime));
            OnPropertyChanged(nameof(SortingIsOver));
            OnPropertyChanged(nameof(Elements));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
