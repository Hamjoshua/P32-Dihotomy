using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Series;

namespace PalMathy.Methods
{
    public class BindedValue<T>
    {
        public BindedValue(T value, string hint, bool isVisible)
        {
            Value = value;
            Hint = hint;
            IsVisible = isVisible;
        }
        public T Value { get; set; }
        public string Hint { get; set; }
        public bool IsVisible { get; set; }
    }

    static class OxyHelper
    {
        // TODO сделать в виде синглтона
        static string FormatDouble(double value)
        {
            return value.ToString().Replace(",", ".");
        }

        static public Function GetFunctionFrom(string functionString, bool withMinus = false)
        {
            string function = functionString;
            if (withMinus)
            {
                function = $"-({function})";
            }
            return new Function($"f(x) = {function}");
        }

        static public double GetResultFromFunction(string functionString, double value, bool derivative = false)
        {
            if (derivative)
            {
                Expression derivExpression = new Expression($"der({functionString}, x, {FormatDouble(value)})");
                return derivExpression.calculate();
            }
            else
            {
                return (new Expression($"f({FormatDouble(value)})", GetFunctionFrom(functionString))).calculate();

            }
        }
    }

    abstract class BaseNumericalMethod
    {
        protected const string NO_ZEROS = "Пересечений с осью Х нет\n";

        public PlotModel Graph = new PlotModel { Title = "График" };
        public List<DataPoint> Points = new List<DataPoint>();
        public string FunctionString = "log(2,x)-3";

        public BindedValue<double> A;
        public BindedValue<double> B;
        public BindedValue<double> C;
        public double Epsilon = 0.5;

        public double BeginInterval = -10;
        public double EndInterval = 10;

        public int MaxGraphY = 20;
        public int MinGraphY = -20;
        public int MaxGraphX = 20;
        public int MinGraphX = -20;

        public string Description;

        protected int _countOfZeros;

        public BaseNumericalMethod()
        {
            A = new BindedValue<double>(-5, "A", true);
            B = new BindedValue<double>(5, "B", true);
            C = new BindedValue<double>(10, "C", false);
        }

        public BaseNumericalMethod(BaseNumericalMethod method)
        {
            A = method.A;
            A.Hint = "A";

            B = method.B;
            B.Hint = "B";

            C = method.C;
            C.Hint = "C";
            C.IsVisible = false;

            Epsilon = method.Epsilon;
            BeginInterval = method.BeginInterval;
            EndInterval = method.EndInterval;
            FunctionString = method.FunctionString;
        }

        public virtual string CalculateResult()
        {            
            string result = "";
            if (_countOfZeros == 0)
            {
                result += NO_ZEROS;
            }
            if (_countOfZeros > 1)
            {
                result += "Внимание! Функция содержит больше одного корня. Расчет может быть некорректен.\n";
            }

            return result;
        }

        protected double GetResultFromFunction(Function function, double value, bool derivative = false)
        {
            if (derivative)
            {
                Expression derivExpression = new Expression($"der({FunctionString}, x, {FormatDouble(value)})");
                return derivExpression.calculate();
            }
            else
            {
                return (new Expression($"f({FormatDouble(value)})", function)).calculate();

            }
        }

        protected string FormatDouble(double value)
        {
            return value.ToString().Replace(",", ".");
        }

        public LineSeries MakeXLine()
        {
            var xLine = new LineSeries
            {
                Title = "X",
                Color = OxyColor.FromRgb(255, 0, 0), // Красный цвет
                StrokeThickness = 2
            };

            xLine.Points.Add(new DataPoint(MaxGraphX, 0));
            xLine.Points.Add(new DataPoint(MinGraphX, 0));

            return xLine;
        }

        public LineSeries MakeYLine()
        {
            var yLine = new LineSeries
            {
                Title = "Y",
                Color = OxyColor.FromRgb(255, 0, 0), // Красный цвет
                StrokeThickness = 2
            };

            yLine.Points.Add(new DataPoint(0, MinGraphY * 2));
            yLine.Points.Add(new DataPoint(0, MaxGraphY * 2));

            return yLine;
        }

        public PlotModel CalculateGraph()
        {
            ParseFunction(BeginInterval, EndInterval);
            Graph = new PlotModel { Title = $"График {FunctionString}" };

            // Создаем серию точек графика
            var lineSeries = new LineSeries
            {
                Title = "f(x)",
                Color = OxyColor.FromRgb(0, 0, 255), // Синий цвет линии
                LineStyle = LineStyle.Solid
            };

            // Добавляем все точки в серию
            lineSeries.Points.AddRange(Points);

            // Добавляем серию точек к модели графика
            Graph.Series.Add(lineSeries);
            Graph.Series.Add(MakeXLine());
            Graph.Series.Add(MakeYLine());

            return Graph;
        }
        protected Function GetFunction(bool withMinus = false)
        {
            string function = FunctionString;
            if (withMinus)
            {
                function = $"-({function})";
            }
            return new Function($"f(x) = {function}");
        }

        void ParseFunction(double start, double end)
        {
            _countOfZeros = 0;
            Points = new List<DataPoint>();
            Function parsedFunction = GetFunction();
            double prevY = 0;

            for (double counterI = start; counterI <= end; counterI += 1)
            {
                Expression e1 = new Expression($"f({counterI.ToString().Replace(",", ".")})", parsedFunction);
                double newY = e1.calculate();
                Points.Add(new DataPoint(counterI, newY));

                if (newY == 0)
                {
                    _countOfZeros += 1;
                }
                else if (prevY != 0)
                {
                    if (prevY * newY < 0)
                    {
                        _countOfZeros += 1;
                    }
                }
                prevY = newY;
            }
        }
    }
}
