using System.Windows.Input;
using PalMathy.Integrals;
using PalMathy.Methods;

namespace PalMathy.ViewModels
{
    public struct HideableString
    {
        string Result;
        bool Visibility;
    }

    public class DefinitiveIntegralViewModel : BaseViewModel
    {
        // TODO сделать отдельный базовый метод
        InputForIntegral _graphContainer = new InputForIntegral();
        public List<BaseIntegralClass> IntegralMethods = new List<BaseIntegralClass>()
        {
            new SquaresIntegralClass()
        };
        public List<HideableString> stringResults = new List<HideableString>();
        
        private double _result;        

        public double AMin
        {
            get
            {
                return _graphContainer.A.Value;
            }
            set
            {
                _graphContainer.A.Value = value;
                OnPropertyChanged(nameof(AMin));
            }
        }

        public double BMax
        {
            get
            {
                return _graphContainer.B.Value;
            }
            set
            {
                _graphContainer.B.Value = value;
                OnPropertyChanged(nameof(BMax));
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

        public ICommand CalculateResult
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    string commonResult = "";
                    foreach(var integralMethod in IntegralMethods)
                    {
                        double result = integralMethod.CalculateResult(
                            _graphContainer.FunctionString,
                            _graphContainer.B.Value,
                            _graphContainer.A.Value,
                            _graphContainer.C.Value,
                            _graphContainer.Epsilon
                        );

                        HideableString
                    }
                    
                });
            }
        }
    }
}
