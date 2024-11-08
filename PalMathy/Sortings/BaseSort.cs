using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Sortings
{
    public abstract class BaseSorting
    {
        public string Name;
        public string Description;
        public bool IsActivated;

        public BaseSorting()
        {
            IsActivated = false;
        }
        
        public abstract List<int> Sort(List<int> elements);
    }    
}
