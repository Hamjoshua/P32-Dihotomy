using OxyPlot;
using PalMathy.Methods;
using System.Windows;
using System.Windows.Input;

namespace PalMathy.ViewModels
{
    public class MathViewModel : BaseViewModel
    {
        BaseNumericalMethod method = new DihotomyMethod();

        private string _chosenMethod = "D";
        public string ChosenMethod
        {
            get { return _chosenMethod; }
            set
            {
                _chosenMethod = value;
                switch (_chosenMethod)
                {
                    case "D":
                        method = new DihotomyMethod(method);
                        break;
                    case "G":
                        method = new GoldenRatioMethod(method);
                        break;
                    case "N":
                        method = new NewtonMethod(method);
                        break;
                }
                Set(ref _chosenMethod, value);                
            }
        }

        public string FunctionString
        {
            get { return method.FunctionString; }
            set
            {
                method.FunctionString = value;
                OnPropertyChanged(nameof(method.FunctionString));
            }
        }

        public double BeginInterval
        {
            get { return method.BeginInterval; }
            set
            {
                method.BeginInterval = value;
                OnPropertyChanged(nameof(method.BeginInterval));
            }
        }

        public double A
        {
            get { return method.A; }
            set
            {
                method.A = Convert.ToDouble(value);
                OnPropertyChanged(nameof(method.A));
            }
        }

        public double B
        {
            get { return method.B; }
            set
            {
                method.B = Convert.ToDouble(value);
                OnPropertyChanged(nameof(method.B));
            }
        }

        public double Epsilon
        {
            get { return method.Epsilon; }
            set
            {
                method.Epsilon = Convert.ToDouble(value);
                OnPropertyChanged(nameof(method.Epsilon));
            }
        }

        public double EndInterval
        {
            get { return method.EndInterval; }
            set
            {
                method.EndInterval = value;
                OnPropertyChanged(nameof(method.EndInterval));
            }
        }

        private string _result = "Пока здесь ничего нет...";
        public string Result
        {
            get { return _result; }
            set
            {                
                Set(ref _result, value);
            }
        }
        public PlotModel Plot
        {
            get { return method.Graph; }
            set
            {
                Set(ref method.Graph, value);
            }
        }

        public ICommand CalculateResult
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Result = method.CalculateResult();
                });
            }
        }

        public ICommand CalculateFunction
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Plot = method.CalculateGraph();
                });
            }
        }

        public ICommand ClearFields
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    FunctionString = "";
                    A = 0;
                    B = 0;
                    Epsilon = 0;
                });
            }
        }
    }
}
