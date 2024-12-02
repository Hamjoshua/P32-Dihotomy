using org.mariuszgromada.math.mxparser;
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
    public class TrapezoidIntegralClass : BaseIntegralClass
    {
        public TrapezoidIntegralClass()
        {
            Description = "Метод трапеций";
            Title = "Метод трапеций";
            GraphColor = Color.OrangeRed;
        }

        public override double CalculateResult(string functionString, double b, double a, double subdivisionLength, double epsilon)
        {
            _subdivisionPoints.Clear();
            double step = (b - a) / subdivisionLength;
            double result = 0;

            double yBegin = 0;
            double yEnd = 0;


            for(double x = a; x < b; x += step)
            {
                double newX = x + step;
                double y = OxyHelper.GetResultFromFunction(functionString, x);
                double newY = OxyHelper.GetResultFromFunction(functionString, newX);
                AddSubdivision(x, y, newX, newY);

                if (x == a)
                {
                    yBegin = y;
                }
                else if (x == b - step)
                {
                    yEnd = newY;
                }
                else
                {
                    result += y;
                }
            }

            result += (yBegin + yEnd) / 2;
            result *= step;

            return result;
        }

        protected override void AddSubdivision(double x1, double y1, double x2, double y2)
        {
            _subdivisionPoints.Add(new DataPoint(x1, 0));
            _subdivisionPoints.Add(new DataPoint(x1, y1));
            _subdivisionPoints.Add(new DataPoint(x2, y2));
            _subdivisionPoints.Add(new DataPoint(x2, 0));
        }
    }
}
