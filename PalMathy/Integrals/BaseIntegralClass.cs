using OxyPlot;
using OxyPlot.Series;
using System.Drawing;


namespace PalMathy.Integrals
{
    // TODO интегральные методы содержат ненужные функциональности, нужен полный рефакторинг
    public abstract class BaseIntegralClass
    {
        public bool IsEnabled { get; set; }
        protected int GetEpsilonZeroCount(double epsilon)
        {

            int countOfZeros = ",".Split(epsilon.ToString())[1].Count();
            return countOfZeros;
        }
        public Color GraphColor { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        protected List<DataPoint> _subdivisionPoints = new List<DataPoint>();

        protected void AddSubdivisionPoint(double x, double y)
        {
            _subdivisionPoints.Add(new DataPoint(x, y));
        }

        public LineSeries GetSubdivision()
        {
            var subdivisionSeries = new LineSeries()
            {
                Title = Title,
                Color = OxyColor.FromRgb(GraphColor.R, GraphColor.G, GraphColor.B),
                LineStyle = LineStyle.Dash
            };
            subdivisionSeries.Points.AddRange(_subdivisionPoints);

            return subdivisionSeries;
        }

        public abstract double CalculateResult(string functionString, double b, double a, double subdivisionLength, double epsilon);
    }
}
