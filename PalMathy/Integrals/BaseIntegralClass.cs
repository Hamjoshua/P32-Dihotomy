using OxyPlot;
using OxyPlot.Series;
using PalMathy.Methods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Integrals
{
    class BaseIntegralClass : BaseNumericalMethod
    {
        public Color GraphColor { get; set; }
        public string Title { get; set; }
        public BaseIntegralClass()
        {
            A = new BindedValue<double>(-1, "А (нижний предел)", true);
            B = new BindedValue<double>(1, "B (верхний предел)", true);
        }

        protected List<DataPoint> _subdivisionPoints = new List<DataPoint>();

        protected void AddSubdivisionPoint(double x, double y)
        {
            _subdivisionPoints.Add(new DataPoint(x, y));
        }

        protected LineSeries GetSubdivision()
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
    }
}
