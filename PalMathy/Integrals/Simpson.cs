using MaterialDesignThemes.Wpf.Converters;
using OxyPlot;
using PalMathy.Methods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Integrals
{
    public class SimpsonIntegralClass : BaseIntegralClass
    {
        public SimpsonIntegralClass()
        {
            Title = "Метод Симпсона";
            Description = "Метод Симпсона (метод парабол). Это более совершенный интегрирования функции, где " +
                "график подынтегральной функции приближается а маленькими параболами. " +
                "Сколько промежуточных отрезков – столько и маленьких парабол.\n\n" +
                "В большинстве случаев метод Симпсона дает более точное приближение, чем метод прямоугольников или метод трапеций.";
            GraphColor = Color.Orange;
        }

        public override double CalculateResult(string functionString, double b, double a, double subdivisionLength, double epsilon)
        {
            // Четная длина отрезков
            if (!IsEvenDigit((int)subdivisionLength))
            {
                subdivisionLength += 1;
            }
            double step = (b - a) / subdivisionLength;

            double sum = SumFromLoop(subdivisionLength, a, step, functionString);           

            sum *= (step / 3);

            return sum;
        }

        bool IsEvenDigit(int digit)
        {
            return digit % 2 == 0;
        }

        private void MakeParabol(double x0, double xend, string functionString)
        {
            double xm = (xend + x0) / 2;
            double y0 = OxyHelper.GetResultFromFunction(functionString, x0);
            double ym = OxyHelper.GetResultFromFunction(functionString, xm);
            double yend = OxyHelper.GetResultFromFunction(functionString, xend);

            double b = ((yend - ym) - (y0 - ym) * (xend * xend - xm * xm) / (x0 * x0 - xm * xm)) /
                   ((xend - xm) * (xend * xend - xm * xm) / (x0 * x0 - xm * xm) - (x0 - xm));
            double a = ((y0 - ym) - b * (x0 - xm)) / (x0 * x0 - xm * xm);
            double c = ym - a * xm * xm - b * xm;

            for(double x = x0; x <= xend; x += 0.25)
            {                
                double y = a * x * x + b * x + c;
                AddSubdivision(x, y);
            }        
        }

        protected override void AddSubdivision(double x1, double y1, double xend = 0, double yend = 0)
        {
            _subdivisionPoints.Add(new DataPoint(x1, y1));
        }

        protected override double SumFromLoop(double length, double x0, double step, string functionString)
        {
            double sum = 0;

            for (int index = 0; index <= length; ++index)
            {
                double x = x0 + (step * index);
                double y = OxyHelper.GetResultFromFunction(functionString, x);

                // Сумма четных
                if (IsEvenDigit(index) && index != 0 && index != length)
                {
                    y *= 2;
                }
                // Сумма нечетных
                else if (!IsEvenDigit(index))
                {
                    y *= 4;
                }

                if (index < length)
                {
                    MakeParabol(x, x + step, functionString);
                }

                sum += y;                
            }

            return sum;            
        }

        protected override double BodyOfLoop(double x, double step, string functionString, double sum)
        {
            throw new NotImplementedException();
        }
    }
}
