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
    public class SimpsonIntegralClass : BaseIntegralClass
    {
        public SimpsonIntegralClass()
        {
            Title = "Метод Симпсона";
            Description = "Метод Симпсона (метод парабол). Это более совершенный способ – " +
                "график подынтегральной функции приближается не ломаной линией, а маленькими параболками. " +
                "Сколько промежуточных отрезков – столько и маленьких парабол.\n\n" +
                "В большинстве случаев метод Симпсона дает более точное приближение, чем метод прямоугольников или метод трапеций.";
            GraphColor = Color.CadetBlue;
        }

        public override double CalculateResult(string functionString, double b, double a, double subdivisionLength, double epsilon)
        {
            _subdivisionPoints.Clear();

            // Четная длина отрезков
            if (!IsEvenDigit((int)subdivisionLength))
            {
                subdivisionLength -= 1;
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
                AddSubdivision(x, y);
                ++index;
            }

            result *= (step / 3);

            return result;
        }

        bool IsEvenDigit(int digit)
        {
            return digit % 2 == 0;
        }

        protected override void AddSubdivision(double x1, double y1, double x2 = 0, double y2 = 0)
        {
            _subdivisionPoints.Add(new DataPoint(x1, y1));
        }
    }
}
