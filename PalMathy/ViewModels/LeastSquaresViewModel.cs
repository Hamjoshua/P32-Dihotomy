using OxyPlot;
using PalMathy.LeastSquares;
using PalMathy.Sortings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace PalMathy.ViewModels
{
    public class LeastSquaresViewModel : BaseViewModel
    {
        private LeastSquaresMethod _method = new LeastSquaresMethod();
        private ObservableCollection<ObservableCollection<double>> _matrix = new ObservableCollection<ObservableCollection<double>>() {
                        new ObservableCollection<double> { 3, 2, -5, -1 },
                        new ObservableCollection<double> { 2, -1, 3, 13 }
        };
        private RandomExpert _randomExpert = new RandomExpert();

        public ObservableCollection<ObservableCollection<double>> Matrix
        {
            get
            {
                return _matrix;
            }
            set
            {
                _matrix = value;
                OnPropertyChanged(nameof(Matrix));
            }
        }

        public PlotModel Graph
        {
            get
            {
                return _method.PlotModel;
            }
            set
            {
                _method.PlotModel = value;
                OnPropertyChanged(nameof(Graph));
            }
        }

        public int MinRandomBound
        {
            get { return _randomExpert.MinBound; }
            set
            {
                _randomExpert.MinBound = value;
                OnPropertyChanged(nameof(MinRandomBound));
            }
        }

        public int MaxRandomBound
        {
            get { return _randomExpert.MaxBound; }
            set
            {
                _randomExpert.MaxBound = value;
                OnPropertyChanged(nameof(MaxRandomBound));
            }
        }

        public ICommand Calculate
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Graph = _method.CalculateGraph(Matrix);
                    OnPropertyChanged(nameof(Graph));
                });
            }
        }

        public ICommand AddColumnToMatrix
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    foreach (var row in Matrix)
                    {
                        row.Add(_randomExpert.GetRandomNumber());
                    };
                });                 
            }
        }

        public ICommand RemoveColumnFromMatrix
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    foreach (var row in Matrix)
                    {
                        row.RemoveAt(0);
                    };
                });
            }
        }

        public ICommand RandomMatrix
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    foreach (var row in Matrix)
                    {
                        for (int valueIndex = 0; valueIndex <= Matrix.Count; ++valueIndex)
                        {
                            row[valueIndex] = _randomExpert.GetRandomNumber();
                        }
                    }
                    OnPropertyChanged(nameof(Matrix));
                });
            }
        }
    }
}
