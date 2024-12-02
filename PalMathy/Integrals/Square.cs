using OxyPlot;
using PalMathy.Methods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Integrals
{
    public class SquaresIntegralClass : BaseIntegralClass
    {
        public SquaresIntegralClass() : base()
        {
            Title = "Метод средних прямоугольников";
            Description = "Простейший из методов и наиболее точный";
            GraphColor = Color.RebeccaPurple;
        }

        protected override void AddSubdivision(double x1, double y1, double x2, double y2)
        {
            _subdivisionPoints.Add(new DataPoint(x1, y1));
            _subdivisionPoints.Add(new DataPoint(x1, y2));
            _subdivisionPoints.Add(new DataPoint(x2, y2));
            _subdivisionPoints.Add(new DataPoint(x2, y1));
        }

        public override double CalculateResult(string functionString, double b, double a, double subdivisionLength, double epsilon)
        {
            _subdivisionPoints.Clear();
            double sum = 0;

            double step = (b - a) / subdivisionLength;
            for (double x = a; x < b; x += step)
            {
                double newX = x + (step / 2);
                double y = OxyHelper.GetResultFromFunction(functionString, newX);
                AddSubdivision(x, 0, x + step, y);

                sum += y;
            }
            sum *= step;

            return sum;
        }
    }
}
