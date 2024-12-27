using PalMathy.Extensions;
using PalMathy.Slau;
using PalMathy.Sortings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PalMathy.Async
{
    abstract public class BaseSingleReport<T, D> : INotifyPropertyChanged where T : class where D : notnull
    {
        public T Method { get; set; }
        public long ExecutingTime { get; set; } = -1;
        public DateTime Begin { get; set; }
        public bool IsOver { get; set; } = false;
        public int IterationsCount { get; set; } = 0;
        public ObservableCollection<D> Elements { get; set; }
        public BaseSingleReport(T method, ObservableCollection<D> elements)
        {
            Method = method;
            Elements = elements;
        }

        public virtual async Task MakeAction(object parameter = null)
        {

        }

        protected void UpdateValues()
        {
            OnPropertyChanged(nameof(IterationsCount));
            OnPropertyChanged(nameof(ExecutingTime));
            OnPropertyChanged(nameof(IsOver));
            OnPropertyChanged(nameof(Elements));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class SingleReport : BaseSingleReport<BaseSorting, int>
    {
        public SingleReport(BaseSorting method, ObservableCollection<int> elements) : base(method, elements)
        {

        }

        public override async Task MakeAction(object isBiggerMode)
        {
            var isBigger = (bool)isBiggerMode;
            var elems = new ObservableCollection<int>(Elements.ToList());

            Begin = DateTime.Now;
            var watch = Stopwatch.StartNew();
            SortingResult result = Method.Sort(elems, isBigger);
            Elements = result.Elements;
            IterationsCount = result.IterationsCount;
            watch.Stop();
            ExecutingTime = watch.ElapsedMilliseconds;
            IsOver = true;

            // TODO использовать OnPropertyChanged в модели - харам. Нужно переделать структуру, сейчас это костыль
            UpdateValues();
        }
    }

    public class SlauReport : BaseSingleReport<BaseLinearEquation, double>
    {
        public SlauReport(BaseLinearEquation method, ObservableCollection<double> elements) : base(method, elements)
        {

        }

        public override async Task MakeAction(object objMatrix)
        {            
            ObservableCollection<ObservableCollection<double>> matrix = ((ObservableCollection<ObservableCollection<double>>) objMatrix).DeepCopy();

            List<List<double>> listMatrix = new List<List<double>>();
            foreach(var element in matrix)
            {
                listMatrix.Add(element.ToList());
            }

            Begin = DateTime.Now;
            var watch = Stopwatch.StartNew();
            try
            {
                Elements = Method.GetNumbers(listMatrix);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            watch.Stop();
            ExecutingTime = watch.ElapsedMilliseconds;
            IsOver = true; 
            UpdateValues();
        }
    }
}
