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
        protected bool MakeComparsion(int elem1, int elem2, bool isBiggerMode)
        {
            if (isBiggerMode)
            {
                return elem1 > elem2;
            }
            return elem1 < elem2;
        }

        public abstract SortingResult Sort(ObservableCollection<int> elements, bool isBiggerMode);
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
