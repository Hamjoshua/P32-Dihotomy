using OxyPlot;
using OxyPlot.Series;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace PalMathy.Integrals
{
    // TODO интегральные методы содержат ненужные функциональности, нужен полный рефакторинг
    public abstract class BaseIntegralClass
    {
        public bool IsEnabled { get; set; }
        protected int GetEpsilonZeroCount(double epsilon)
        {
            if(epsilon > 0 && epsilon < 1)
            {                
                int countOfZeros = BitConverter.GetBytes(decimal.GetBits((decimal)epsilon)[3])[2];
                if(countOfZeros > 15)
                {
                    countOfZeros = 15;
                }
                return countOfZeros;
            }
            return 0;
        }
        public Color GraphColor { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        protected List<DataPoint> _subdivisionPoints = new List<DataPoint>();

        protected abstract void AddSubdivision(double x1, double y1, double x2, double y2);

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
