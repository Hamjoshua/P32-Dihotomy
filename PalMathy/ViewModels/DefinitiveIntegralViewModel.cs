using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Series;
using PalMathy.Integrals;
using PalMathy.Methods;

namespace PalMathy.ViewModels
{
    public class HideableString
    {
        public HideableString(string title, string result, bool flag)
        {
            Title = title;
            Result = result;
            IsVisible = flag;
        }
        public string Title { get; set; }
        public string Result { get; set; }
        public bool IsVisible { get; set; }
    }

    public class DefinitiveIntegralViewModel : BaseViewModel
    {
        // TODO сделать отдельный базовый метод
        InputForIntegral _graphContainer = new InputForIntegral();
        public List<BaseIntegralClass> IntegralMethods { get; set; } = new List<BaseIntegralClass>()
        {
            new SquaresIntegralClass()
        };
        public ObservableCollection<HideableString> _integralResults = new ObservableCollection<HideableString>();

        private double _result;

        public ObservableCollection<HideableString> IntegralResults
        {
            get
            {
                return _integralResults;
            }
            set
            {
                Set<ObservableCollection<HideableString>>(ref _integralResults, value);
            }
        }

        public BindedValue<double> AMin
        {
            get
            {
                return _graphContainer.A;
            }
            set
            {
                _graphContainer.A.Value = Convert.ToDouble(value);
                OnPropertyChanged(nameof(AMin));
            }
        }

        public BindedValue<double> BMax
        {
            get
            {
                return _graphContainer.B;
            }
            set
            {
                _graphContainer.B.Value = Convert.ToDouble(value); ;
                OnPropertyChanged(nameof(BMax));
            }
        }

        public double Epsilon
        {
            get
            {
                return _graphContainer.Epsilon;
            }
            set
            {
                _graphContainer.Epsilon = Convert.ToDouble(value); ;
                OnPropertyChanged(nameof(Epsilon));
            }
        }

        public BindedValue<double> SubdivCount
        {
            get
            {
                return _graphContainer.C;
            }
            set
            {
                _graphContainer.C.Value = Convert.ToDouble(value); ;
                OnPropertyChanged(nameof(SubdivCount));
            }
        }

        public string Description
        {
            get
            {
                return _graphContainer.Description;
            }
        }

        public PlotModel PageGraph
        {
            get
            {
                return _graphContainer.Graph;
            }
            set
            {
                _graphContainer.Graph = value;
                OnPropertyChanged(nameof(PageGraph));
            }
        }

        public string FunctionString
        {
            get
            {
                return _graphContainer.FunctionString;
            }
            set
            {
                _graphContainer.FunctionString = value;
                OnPropertyChanged(nameof(FunctionString));
            }
        }

        public double Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public ICommand BuildGraph
        {
            get
            {
                IntegralResults.Clear();
                return new DelegateCommand((obj) =>
                {
                    PageGraph = _graphContainer.CalculateGraph();
                });
            }
        }

        public ICommand ShowOrHideSubdivideSeries
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    foreach (var result in IntegralResults)
                    {
                        var subdivide = _graphContainer.Graph.Series.First(d => d.Title == result.Title);
                        subdivide.IsVisible = result.IsVisible;
                    }
                    PageGraph = _graphContainer.Graph;
                });
            }
        }

        private PlotModel SetSubdivisionToGraph(LineSeries subdivision)
        {
            var existingSubdivision = _graphContainer.Graph.Series.FirstOrDefault(d => d.Title == subdivision.Title);
            if (existingSubdivision != null)
            {
                _graphContainer.Graph.Series.Remove(existingSubdivision);
            }

            _graphContainer.Graph.Series.Add(subdivision);

            return _graphContainer.Graph;
        }

        public ICommand CalculateResult
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (!PageGraph.Title.Contains(FunctionString))
                    {
                        var question = MessageBox.Show("График текущей функции еще не построен. Построить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (question == MessageBoxResult.Yes)
                        {
                            PageGraph = _graphContainer.CalculateGraph();
                        }
                        else
                        {
                            return;
                        }
                    }

                    IntegralResults.Clear();

                    foreach (var integralMethod in IntegralMethods)
                    {
                        if (integralMethod.IsEnabled)
                        {
                            double result = integralMethod.CalculateResult(
                            _graphContainer.FunctionString,
                            _graphContainer.B.Value,
                            _graphContainer.A.Value,
                            _graphContainer.C.Value,
                            _graphContainer.Epsilon
                        );
                            // Добавляем сделанные разделения в существующий график
                            PageGraph = SetSubdivisionToGraph(integralMethod.GetSubdivision());

                            // Добавляем результат интегралов на "доску"
                            HideableString hideableString = new HideableString(integralMethod.Title, result.ToString(), true);
                            IntegralResults.Add(hideableString);
                        }
                    }

                    OnPropertyChanged(nameof(PageGraph));
                    OnPropertyChanged(nameof(IntegralResults));
                });
            }
        }
    }
}
