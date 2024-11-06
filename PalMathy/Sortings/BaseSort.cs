using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Sortings
{
    abstract class BaseSorting
    {
        public string Name;
        public string Description;
        public bool IsActivated;

        public BaseSorting()
        {

        }
        
        public abstract List<int> Sort(List<int> elements);
    }    
}
