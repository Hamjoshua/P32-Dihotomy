using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Sortings
{
    public class BubbleSorting : BaseSorting
    {
        public BubbleSorting() : base()
        {
            Name = "Метод обменов (пузырьком)";
            Description = "Простейший для понимания и реализации. Выполняется некоторое количество " +
                "проходов по массиву — начиная от начала массива, перебираются пары соседних элементов " +
                "массива. Если 1-й элемент пары больше 2-го, элементы переставляются (выполняется обмен).";
        }

        public override List<int> Sort(List<int> elements)
        {
            return elements;
        }
    }
}
