using System.Windows;
using System.Windows.Input;
using OxyPlot;
using PalMathy.Integrals;
using PalMathy.Methods;

namespace PalMathy.ViewModels
{
    public struct HideableString
    {
        public HideableString(string title, string result, bool flag)
        {
            Title = title;
            Result = result;
            IsVisible = flag;
        }
        public string Title;
        public string Result;
        public bool IsVisible;
    }

    public class DefinitiveIntegralViewModel : BaseViewModel
    {
        // TODO сделать отдельный базовый метод
        InputForIntegral _graphContainer = new InputForIntegral();
        public List<BaseIntegralClass> IntegralMethods = new List<BaseIntegralClass>()
        {
            new SquaresIntegralClass()
        };
        public List<HideableString> IntegralResults { get; set; } = new List<HideableString>();

        private double _result;

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
                        var subdivide = PageGraph.Series.First(d => d.Title == result.Title);
                        subdivide.IsVisible = result.IsVisible;
                    }
                });
            }
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
                            PageGraph.Series.Add(integralMethod.GetSubdivision());

                            HideableString hideableString = new HideableString(integralMethod.Title, result.ToString(), true);
                            IntegralResults.Add(hideableString);
                        }                        
                    }
                });
            }
        }
    }
}
