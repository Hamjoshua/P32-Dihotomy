using OxyPlot;
using OxyPlot.Series;
using PalMathy.Integrals;
using PalMathy.Methods;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Drawing;
using System.Security.Policy;

namespace PalMathy.LeastSquares
{
    class LeastSquares
    {
        PlotModel PlotModel
        {
            get
            {
                return _method.Graph;
            }
            set
            {
                _method.Graph = value;
            }
        }

        InputForIntegral _method = new InputForIntegral();
        public void CalculateGraph(ObservableCollection<ObservableCollection<int>> data)
        {
            List<DataPoint> points = new List<DataPoint>();

            (double a, double b) coefficients = GetCoefs(data, points);
            _method.CalculateGraph();

            _method.Graph.Series.Add(AddPoints(points)); ;
            _method.Graph.Series.Add(AddLine(coefficients, data[0].Max(), data[0].Min()));
        }

        public LineSeries AddPoints(List<DataPoint> points)
        {
            LineSeries series = new LineSeries()
            {
                Title = "Точки интервала",
                Color = OxyColor.FromRgb(Color.Orange.R, Color.Orange.G, Color.Orange.B),
                LineStyle = LineStyle.None,
                MarkerSize = 10
            };

            series.Points.AddRange(points);

            return series;
        }

        public FunctionSeries AddLine((double a, double b) coefficients, double max, double min)
        {
            Func<double, double> func = (x) => coefficients.a * x + coefficients.b;

            FunctionSeries line = new FunctionSeries(func, min, max, 0.0001)
            {
                Title = "Примерная прямая",
                Color = OxyColor.FromRgb(Color.Beige.R, Color.Beige.G, Color.Beige.B),
                LineStyle = LineStyle.Solid
            };

            return line;
        }

        public (double a, double b) GetCoefs(ObservableCollection<ObservableCollection<int>> data, List<DataPoint> points)
        {
            int n = data[0].Count;
            if (n < 2) // Нужно хотя бы 2 точки для вычисления прямой
            {
                throw new ArgumentException("Нужно как минимум два элемента в коллекции для метода наименьших квадратов.");
            }

            double sumX = 0;
            double sumY = 0;
            double sumXY = 0;
            double sumX2 = 0;

            for (int i = 0; i < n; ++i)
            {
                double x = data[0][i];    // Индекс как значение X
                double y = data[1][i]; // Значение из ObservableCollection как значение Y

                points.Add(new DataPoint(x, y));

                sumX += x;
                sumY += y;
                sumXY += x * y;
                sumX2 += x * x;
            }

            double denominator = n * sumX2 - sumX * sumX;
            if (Math.Abs(denominator) < 1e-9) // Проверка на деление на ноль
            {
                throw new Exception("Невозможно вычислить коэффициенты, так как знаменатель равен нулю.");
            }


            double a = (n * sumXY - sumX * sumY) / denominator;
            double b = (sumY * sumX2 - sumX * sumXY) / denominator;

            return (a, b);
        }
    }
}
