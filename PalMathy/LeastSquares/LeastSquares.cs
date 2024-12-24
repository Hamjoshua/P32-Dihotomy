using OxyPlot;
using OxyPlot.Series;
using PalMathy.Integrals;
using PalMathy.Methods;
using PalMathy.Slau;
using System.Collections.ObjectModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;


namespace PalMathy.LeastSquares
{
    public class LeastSquaresMethod
    {
        public LeastSquaresMethod()
        {
            _method.FunctionString = "";
        }
        public void SetBoundsToGraph(int max, int min)
        {
            _method.MaxGraphX = max;
            _method.MinGraphX = min;
            _method.MaxGraphY = max;
            _method.MinGraphY = min;
        }

        public PlotModel PlotModel
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
        public PlotModel CalculateGraph(ObservableCollection<ObservableCollection<double>> data)
        {
            List<DataPoint> points = new List<DataPoint>();

            (double a, double b) coeffLine = FitLine(data, points);
            (double a, double b, double c) coeffParabola = FitParabola(data);
            _method.CalculateGraph();

            PlotModel.Series.Add(AddPoints(points)); ;
            PlotModel.Series.Add(AddLine(coeffLine, data[0].Max(), data[0].Min()));
            PlotModel.Series.Add(AddParabol(coeffParabola, data[0].Max(), data[0].Min()));

            return PlotModel;
        }

        public FunctionSeries AddParabol((double a, double b, double c) coefficients, double max, double min)
        {
            Func<double, double> func = (x) => coefficients.a * x * x + coefficients.b * x + coefficients.c;

            FunctionSeries line = new FunctionSeries(func, min, max, 0.0001)
            {
                Title = "Апроксимация второй степени",
                Color = OxyColor.FromRgb(Color.Violet.R, Color.Violet.G, Color.Violet.B),
                LineStyle = LineStyle.Solid
            };

            return line;
        }

        public LineSeries AddPoints(List<DataPoint> points)
        {
            LineSeries series = new LineSeries()
            {
                Title = "Точки интервала",
                Color = OxyColor.FromRgb(Color.Red.R, Color.Red.G, Color.Red.B),
                LineStyle = LineStyle.None,
                MarkerSize = 5,
                MarkerType = MarkerType.Circle
            };

            series.Points.AddRange(points);

            return series;
        }

        public FunctionSeries AddLine((double a, double b) coefficients, double max, double min)
        {
            Func<double, double> func = (x) => coefficients.a * x + coefficients.b;

            FunctionSeries line = new FunctionSeries(func, min, max, 0.0001)
            {
                Title = "Апроксимация первой степени",
                Color = OxyColor.FromRgb(Color.Blue.R, Color.Blue.G, Color.Blue.B),
                LineStyle = LineStyle.Solid
            };

            return line;
        }

        public static (double a, double b, double c) FitParabola(ObservableCollection<ObservableCollection<double>> data)
        {
            if (data == null || data[0].Count < 3)
            {
                throw new ArgumentException("Для аппроксимации параболой требуется минимум 3 точки.");
            }

            int n = data[0].Count;
            double[] x = data[0].ToArray();
            double[] y = data[1].ToArray();

            // Вычисляем суммы
            double sumX = x.Sum();
            double sumX2 = x.Sum(xi => xi * xi);
            double sumX3 = x.Sum(xi => xi * xi * xi);
            double sumX4 = x.Sum(xi => xi * xi * xi * xi);
            double sumY = y.Sum();
            double sumXY = 0;
            double sumX2Y = 0;

            for (int i = 0; i < n; i++)
            {
                sumXY += x[i] * y[i];
                sumX2Y += x[i] * x[i] * y[i];
            }


            //Формируем матрицу коэффициентов
            List<List<double>> listMatrix = new List<List<double>>() {
                new List<double>() { sumX4, sumX3, sumX2, sumX2Y },
                new List<double>() { sumX3, sumX2, sumX, sumXY },
                new List<double>() { sumX2, sumX, n, sumY }
            };

            //Решаем систему линейных уравнений (метод Гаусса)
            GaussEquation gaussEquation = new GaussEquation();
            ObservableCollection<double> solution = gaussEquation.GetNumbers(listMatrix);


            double a = solution[0];
            double b = solution[1];
            double c = solution[2];

            return (a, b, c);
        }

        public (double a, double b) FitLine(ObservableCollection<ObservableCollection<double>> data, List<DataPoint> points)
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
