using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PalMathy.Integrals;

namespace PalMathy.ViewModels
{
    public class DefinitiveIntegralViewModel : BaseViewModel
    {
        private DefinitiveIntegral _definitiveIntegral = new DefinitiveIntegral();
        private double _result;

        public double AMin
        {
            get
            {
                return _definitiveIntegral.AMin;
            }
            set
            {
                _definitiveIntegral.AMin = value;
                OnPropertyChanged(nameof(AMin));
            }
        }

        public double BMax
        {
            get
            {
                return _definitiveIntegral.BMax;
            }
            set
            {
                _definitiveIntegral.BMax = value;
                OnPropertyChanged(nameof(BMax));
            }
        }

        public string FunctionString
        {
            get
            {
                return _definitiveIntegral.FunctionString;
            }
            set
            {
                _definitiveIntegral.FunctionString = value;
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
                    Result = _definitiveIntegral.GetResult();
                });
            }
        }
    }
}
