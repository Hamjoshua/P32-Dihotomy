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
    class BaseMethod
    {
        PlotModel Graph = new PlotModel { Title = "График" };
        List<DataPoint> Points;
        string FunctionString;

        double BeginInterval = 0;
        double EndInterval = 10;
        
        void CalculateGraph()
        {
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
            Graph.Series.Add(lineSeries);
            Graph.Series.Add(medianLine);
            Graph.Series.Add(absicc);

        }

        void ParseFunction()
        {
            Function parsedFunction = new Function("f(x) = " + FunctionString);

            for (double counterI = BeginInterval; counterI <= EndInterval; ++counterI)
            {
                Expression e1 = new Expression($"f({counterI})", parsedFunction);
                Points.Add(new DataPoint(counterI, e1.calculate()));
            }

        }

    }
}
