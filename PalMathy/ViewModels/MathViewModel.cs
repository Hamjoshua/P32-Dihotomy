using OxyPlot;
using PalMathy.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PalMathy.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected void Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            if (EqualityComparer<T>.Default.Equals(field, newValue)) return;
            field = newValue;
            OnPropertyChanged(propertyName);
        }
    }

    public class MathViewModel : BaseViewModel
    {
        BaseMethod method = new DihotomyMethod();

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
                method.A= value;
                OnPropertyChanged(nameof(method.A));
            }
        }

        public double B
        {
            get { return method.B; }
            set
            {
                method.B = value;
                OnPropertyChanged(nameof(method.B));
            }
        }

        public double Epsilon
        {
            get { return method.Epsilon; }
            set
            {
                method.Epsilon = value;
                OnPropertyChanged(nameof(method.Epsilon));
            }
        }

        public double EndInterval
        {
            get { return method.EndInterval; }
            set
            {
                OnPropertyChanged(nameof(method.EndInterval));
            }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                MessageBox.Show(_result);
                OnPropertyChanged(nameof(_result));
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
    }
}
