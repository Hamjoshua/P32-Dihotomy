using MaterialDesignThemes.Wpf.Converters;
using OxyPlot;
using PalMathy.Methods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            Description = "Метод Симпсона (метод парабол). Это более совершенный способ – " +
                "график подынтегральной функции приближается не ломаной линией, а маленькими параболками. " +
                "Сколько промежуточных отрезков – столько и маленьких парабол.\n\n" +
                "В большинстве случаев метод Симпсона дает более точное приближение, чем метод прямоугольников или метод трапеций.";
            GraphColor = Color.Orange;
        }

        public override double CalculateResult(string functionString, double b, double a, double subdivisionLength, double epsilon)
        {
            _subdivisionPoints.Clear();

            // Четная длина отрезков
            if (!IsEvenDigit((int)subdivisionLength))
            {
                subdivisionLength += 1;
            }
            double step = (b - a) / subdivisionLength;
            double result = 0;
            //List<double> functionResults = new List<double>();            
            int index = 0;

            for (double x = a; x <= b; x += step)
            {
                double y = OxyHelper.GetResultFromFunction(functionString, x);                

                // Сумма четных
                if (IsEvenDigit(index) && index != 0 && index != subdivisionLength)
                {
                    y *= 2;
                }
                // Сумма нечетных
                else if (!IsEvenDigit(index))
                {
                    y *= 4;
                }
                result += y;
                ++index;

                if(x != b)
                {
                    MakeParabol(x, x + step, functionString);
                }                
            }

            

            result *= (step / 3);

            return result;
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
    }
}
