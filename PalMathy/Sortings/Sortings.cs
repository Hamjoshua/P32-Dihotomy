﻿using PalMathy.Extensions;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;

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
                "массива. Если 1-й элемент пары больше 2-го, элементы переставляются (выполняется обмен)." +
                "\n\nАлгоритм пузырьковой сортировки считается учебным и практически не применяется вне учебной литературы, а на практике применяются более эффективные.";
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
                "противопоставляемый другим, более реалистичным алгоритмам. Принцип работы алгоритма прост: " +
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

        public override ObservableCollection<int> Sort(ObservableCollection<int> elements)
        {
            while (!IsSorted(elements))
            {
                if (CancelToken.Instance.cancellationTokenSource.IsCancellationRequested)
                {
                    return elements;
                }
                elements.Shuffle();
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

        int Partition(ObservableCollection<int> array, int start, int end)
        {
            int marker = start; // divides left and right subarrays
            for (int i = start; i < end; i++)
            {
                if (array[i] < array[end]) // array[end] is pivot
                {
                    (array[marker], array[i]) = (array[i], array[marker]);
                    marker += 1;
                }
            }
            // put pivot(array[end]) between left and right subarrays            
            (array[marker], array[end]) = (array[end], array[marker]);
            return marker;
        }

        void QuickSort(ObservableCollection<int> array, int start, int end)
        {
            if (start >= end)
                return;

            int pivot = Partition(array, start, end);
            QuickSort(array, start, pivot - 1);
            QuickSort(array, pivot + 1, end);
        }

        public override ObservableCollection<int> Sort(ObservableCollection<int> elements)
        {
            QuickSort(elements, 0, elements.Count - 1);            

            return elements;
        }
    }

    public class ShakeSorting : BaseSorting
    {
        public ShakeSorting() : base()
        {
            Name = "Шейкерная";
            Description = "Этот алгоритм сортировки – развитие пузырьковой сортировки. Его еще называют двусторонним пузырчатым методом сортировки. " +
                "В ней пределы той части массива, в которой есть перестановки, сужаются. Также внутренние циклы проходят по массиву то в одну, " +
                "то в другую сторону, поднимая самый легкий элемент вверх и опуская самый тяжелый элемент в самый низ за одну итерацию внешнего цикла. ";
        }

        private void SwapElements(ObservableCollection<int> elements, int index)
        {
            int tempElement = elements[index];
            elements[index] = elements[index - 1];
            elements[index - 1] = tempElement;
        }

        public override ObservableCollection<int> Sort(ObservableCollection<int> elements)
        {
            int firstMark = 1;
            int lastMark = elements.Count - 1;

            while (firstMark <= lastMark) {
                if (CancelToken.Instance.cancellationTokenSource.IsCancellationRequested)
                {
                    return elements;
                }

                for(int descIndex = lastMark; descIndex >= firstMark; --descIndex)
                {
                    if (elements[descIndex] < elements[descIndex - 1])
                    {
                        SwapElements(elements, descIndex);  
                    }
                }

                for (int ascIndex = firstMark; ascIndex <= lastMark; ++ascIndex)
                {
                    if (elements[ascIndex] < elements[ascIndex - 1])
                    {
                        SwapElements(elements, ascIndex);
                    }
                }

                --lastMark;
            }

            return elements;
        }
    }

    public class InsertSorting : BaseSorting
    {
        public InsertSorting() : base()
        {
            Name = "Вставочная";
            Description = "Сортировка вставками - алгоритм сортировки, в котором элементы входной последовательности просматриваются по одному, " +
                "и каждый новый поступивший элемент размещается в подходящее место среди ранее упорядоченных элементов" +
                "\n\nСортировка простыми вставками наиболее эффективна, " +
                "когда список уже частично отсортирован и элементов массива немного. Если элементов в списке меньше 10, то этот алгоритм — один из самых быстрых.";
        }
        public override ObservableCollection<int> Sort(ObservableCollection<int> elements)
        {
            for(int unsortedIndex = 1; unsortedIndex < elements.Count; ++unsortedIndex)
            {
                if (CancelToken.Instance.cancellationTokenSource.IsCancellationRequested)
                {
                    return elements;
                }

                int currentElem = elements[unsortedIndex];                
                int sortedIndex = unsortedIndex;

                while (sortedIndex >= 1 && elements[sortedIndex - 1] > currentElem)
                {
                    elements[sortedIndex] = elements[sortedIndex - 1];
                    --sortedIndex;                        
                }
                elements[sortedIndex] = currentElem;                
            }

            return elements;
        }
    }
}
