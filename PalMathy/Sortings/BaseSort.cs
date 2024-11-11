using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Sortings
{
    public abstract class BaseSorting
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActivated { get; set; }

        public BaseSorting()
        {
            IsActivated = false;
        }

        public abstract SortingResult Sort(ObservableCollection<int> elements);
    }

    public struct SortingResult
    {
        public ObservableCollection<int> Elements { get; set; }
        public int IterationsCount { get; set; }
        public SortingResult(ObservableCollection<int> elements, int iterationsCount)
        {
            Elements = elements;
            IterationsCount = iterationsCount;
        }
    }
}
