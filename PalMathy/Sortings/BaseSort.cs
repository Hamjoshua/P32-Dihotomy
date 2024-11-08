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
        
        public abstract ObservableCollection<int> Sort(ObservableCollection<int> elements);
    }    
}
