using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

// TODO Запихать все текста в xaml файлы или что-нибудь такое. Код бухнет от текста

namespace PalMathy.Sortings
{
    public class BubbleSorting : BaseSorting
    {
        public BubbleSorting() : base()
        {
            Name = "Пузырьковая (обмены)";
            Description = "Простейший для понимания и реализации. Выполняется некоторое количество " +
                "проходов по массиву — начиная от начала массива, перебираются пары соседних элементов " +
                "массива. Если 1-й элемент пары больше 2-го, элементы переставляются (выполняется обмен).";
        }

        public override ObservableCollection<int> Sort(ObservableCollection<int> elements)
        {
            int length = elements.Count;

            for(int j = 1; j < length; ++j)
            {
                bool isSorted = true;

                for(int i = 0; i < length - j; ++i)
                {
                    if (CancelToken.Instance.cancellationTokenSource.IsCancellationRequested)
                    {
                        return elements;
                    }

                    if (elements[i] > elements[i + 1])
                    {
                        int prevElem = elements[i];
                        elements[i] = elements[i + 1];
                        elements[i + 1] = prevElem;
                        isSorted = false;
                    }
                }

                if (isSorted)
                {
                    break;
                }
            }
            
            return elements;
        }
    }

    public class BogoSorting : BaseSorting
    {
        public BogoSorting() : base()
        {
            Name = "Болотная (Bogosort)";
            Description = "Неэффективный алгоритм сортировки, используемый только в образовательных целях и " +
                "противопоставляемый другим, более реалистичным алгоритмам. Принцип работы алгоритма прост:. " +
                "перетряхиваем список случайным образом до тех пор, пока он внезапно не отсортируется.";
        }

        private bool IsSorted(ObservableCollection<int> elements)
        {
            for (int i = 0; i < elements.Count - 1; ++i) {
                if (elements[i] > elements[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        private ObservableCollection<int> Shuffle(ObservableCollection<int> elements)
        {
            var count = elements.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {              
                var r = Random.Shared.Next(i, count);
                var tmp = elements[i];
                elements[i] = elements[r];
                elements[r] = tmp;
            }

            return elements;
        }

        public override ObservableCollection<int> Sort(ObservableCollection<int> elements)
        {
            while (!IsSorted(elements))
            {
                if (CancelToken.Instance.cancellationTokenSource.IsCancellationRequested)
                {
                    return elements;
                }
                elements = Shuffle(elements);
            }

            return elements;
        }
    }

    public class QuickSorting : BaseSorting
    {
        public QuickSorting() : base()
        {
            Name = "Быстрая (сортировка Хоара)";
            Description = "Алгоритм сортировки, разработанный английским информатиком Тони Хоаром во время его работы в МГУ в 1960 году. \nПринцип работы взят с" +
                "пузырьковой сортировки, но с преобразованием: " +
                "1. На очередном шаге выбирается опорный элемент — им может быть любой элемент массива.\r\n" +
                "2. Все остальные элементы массива сравниваются с опорным и те, которые меньше него, ставятся слева от него, а которые больше или равны — справа.\r\n" +
                "3. Для двух получившихся блоков массива (меньше опорного, и больше либо равны опорному) производится точно такая же операция — " +
                "выделяется опорный элемент и всё идёт точно так же, пока в блоке не останется один элемент.";
        }
        public override ObservableCollection<int> Sort(ObservableCollection<int> elements)
        {
            throw new NotImplementedException();
        }
    }

    public class ShakeSorting : BaseSorting
    {
        public ShakeSorting() : base()
        {
            Name = "Шейкерная";
            Description = "Этот алгоритм сортировки – развитие пузырьковой сортировки. " +
                "Отличия от нее заключаются в том, что при прохождении части массива, происходит проверка, были ли перестановки. " +
                "Если их не было, значит, эта часть массива уже упорядочена и она исключается из дальнейшей обработки. Кроме того, " +
                "при прохождении массива от начала к концу, минимальные элементы перемещаются в самое начало, а максимальный элемент сдвигается к концу массива.";
        }
        public override ObservableCollection<int> Sort(ObservableCollection<int> elements)
        {
            throw new NotImplementedException();
        }
    }

    public class InsertSorting : BaseSorting
    {
        public InsertSorting() : base()
        {
            Name = "Вставочная";
            Description = "Сортировка вставками - алгоритм сортировки, в котором элементы входной последовательности просматриваются по одному, " +
                "и каждый новый поступивший элемент размещается в подходящее место среди ранее упорядоченных элементов";
        }
        public override ObservableCollection<int> Sort(ObservableCollection<int> elements)
        {
            throw new NotImplementedException();
        }
    }
}
