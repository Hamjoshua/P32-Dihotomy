using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Methods
{
    class CoordinateDescentMethod : BaseNumericalMethod
    {
        public CoordinateDescentMethod(BaseNumericalMethod method) : base()
        {
            Description = "Простейший метод нахождения минимума и максимума функции.";
        }

        public override string CalculateResult()
        {
            string result = base.CalculateResult();

            double x = A;
            double step = B;

            double maxPoint = GetPoint(x, step, true);
            double minPoint = GetPoint(x, step, false);

            result += $"Максимум функции: {maxPoint}\nМинимум функции: {minPoint}";

            return result;
        }

        public double GetPoint(double x, double step, bool isMax=true, int iterations = 10)
        {
            int direction = 1;
            double newX = x + (step * direction);

            while (Math.Abs(x - newX) > Epsilon && (x > BeginInterval & x < EndInterval) && iterations > 0)
            {
                double fx = GetResultFromFunction(GetFunction(!isMax), x);
                double fNewX = GetResultFromFunction(GetFunction(!isMax), newX);                

                if (fx > fNewX)
                {
                    if (Double.IsNaN(fNewX))
                    {
                        break;
                    }
                    x = newX;
                    newX = x + (step * direction);                    
                }
                else
                {
                    direction *= -1;
                    newX = x + (step * direction);
                }
                iterations -= 1;
            }

            return newX;
        }
    }
}
