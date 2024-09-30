using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Series;

namespace PalMathy.Methods
{
    abstract class BaseNumericalMethod
    {
        public PlotModel Graph = new PlotModel { Title = "График" };
        public List<DataPoint> Points = new List<DataPoint>();
        public string FunctionString = "log(2,x)-3";

        public double A = 0;
        public double B = 0;
        public double Epsilon = 0;

        public double BeginInterval = -10;
        public double EndInterval = 10;

        public abstract string CalculateResult();

        public bool IsFunctionContinious()
        {
            if (Points.Count > 0)
            {

            }

            return false;
        }

        public PlotModel CalculateGraph()
        {
            ParseFunction();
            PlotModel newGraph = new PlotModel { Title = $"График {FunctionString}" };            
            var medianLine = new LineSeries
            {
                Title = "X",
                Color = OxyColor.FromRgb(255, 0, 0), // Красный цвет
                StrokeThickness = 2
            };

            medianLine.Points.Add(new DataPoint(-10, 0));
            medianLine.Points.Add(new DataPoint(10, 0));

            var absicc = new LineSeries
            {
                Title = "Y",
                Color = OxyColor.FromRgb(255, 0, 0), // Красный цвет
                StrokeThickness = 2,
            };

            absicc.Points.Add(new DataPoint(0, 10));
            absicc.Points.Add(new DataPoint(0, -10));


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
            newGraph.Series.Add(medianLine);
            newGraph.Series.Add(absicc);

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

            for (double counterI = BeginInterval; counterI <= EndInterval; ++counterI)
            {
                Expression e1 = new Expression($"f({counterI})", parsedFunction);
                Points.Add(new DataPoint(counterI, e1.calculate()));
            }
        }
    }
}
