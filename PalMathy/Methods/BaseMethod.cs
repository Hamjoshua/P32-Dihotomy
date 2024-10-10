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
    abstract class BaseNumericalMethod
    {
        protected const string NO_ZEROS = "Пересечений с осью Х нет\n";
            
        public PlotModel Graph = new PlotModel { Title = "График" };
        public List<DataPoint> Points = new List<DataPoint>();
        public string FunctionString = "log(2,x)-3";

        public double A = -5;
        public double B = 10;
        public double Epsilon = 0.5;

        public double BeginInterval = -10;
        public double EndInterval = 10;

        protected int _countOfZeros;

        public BaseNumericalMethod()
        {

        }

        public BaseNumericalMethod(BaseNumericalMethod method)
        {
            A = method.A;
            B = method.B;
            Epsilon = method.Epsilon;
            BeginInterval = method.BeginInterval;
            EndInterval = method.EndInterval;
            FunctionString = method.FunctionString;
        }

        public virtual string CalculateResult()
        {
            ParseFunction();
            string result = "";
            if(_countOfZeros == 0)
            {
                result += NO_ZEROS;
            }
            if(_countOfZeros > 1)
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

            xLine.Points.Add(new DataPoint(10 ^ 5, 0));
            xLine.Points.Add(new DataPoint(-10 ^ 5, 0));

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

            yLine.Points.Add(new DataPoint(0, 10 ^ 5));
            yLine.Points.Add(new DataPoint(0, -10 ^ 5));

            return yLine;
        }

        public PlotModel CalculateGraph()
        {
            ParseFunction();
            PlotModel newGraph = new PlotModel { Title = $"График {FunctionString}" };

            // Создаем серию точек графика
            var lineSeries = new LineSeries
            {
                Title = "f(x)",
                Color = OxyColor.FromRgb(0, 0, 255), // Синий цвет линии
                LineStyle = LineStyle.Dot
            };

            // Добавляем все точки в серию
            lineSeries.Points.AddRange(Points);

            // Добавляем серию точек к модели графика
            newGraph.Series.Add(lineSeries);
            newGraph.Series.Add(MakeXLine());
            newGraph.Series.Add(MakeYLine());

            return newGraph;
        }
        protected Function GetFunction()
        {
            return new Function("f(x) = " + FunctionString);
        }

        void ParseFunction()
        {
            Points = new List<DataPoint>();
            Function parsedFunction = GetFunction();
            double prevY = 0;

            for (double counterI = BeginInterval; counterI <= EndInterval; counterI += 0.15)
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
                    if(prevY * newY < 0)
                    {
                        _countOfZeros += 1;
                    }
                }
                prevY = newY;
            }
        }        
    }
}
