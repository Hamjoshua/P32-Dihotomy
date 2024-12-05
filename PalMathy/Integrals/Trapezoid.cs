using OxyPlot;
using PalMathy.Methods;
using System.Drawing;

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
            ClearPastSubdivisions();
            double step = GetStep(b, a, subdivisionLength);
            double sum = SumFromLoop(subdivisionLength, a, step, functionString);

            double yBegin = OxyHelper.GetResultFromFunction(functionString, a);
            double yEnd = OxyHelper.GetResultFromFunction(functionString, b - step);

            sum += (yBegin + yEnd) / 2;
            sum *= step;

            return sum;
        }

        protected override void AddSubdivision(double x1, double y1, double x2, double y2)
        {
            _subdivisionPoints.Add(new DataPoint(x1, 0));
            _subdivisionPoints.Add(new DataPoint(x1, y1));
            _subdivisionPoints.Add(new DataPoint(x2, y2));
            _subdivisionPoints.Add(new DataPoint(x2, 0));
        }

        protected override double BodyOfLoop(double x, double step, string functionString, double sum)
        {
            double newX = x + step;
            double y = OxyHelper.GetResultFromFunction(functionString, x);
            double newY = OxyHelper.GetResultFromFunction(functionString, newX);
            AddSubdivision(x, y, newX, newY);           

            sum += y;
            return sum;
        }
    }
}
