using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace PalMathy.Methods
{
    class NewtonMethod : BaseNumericalMethod
    {
        public NewtonMethod() { }

        public NewtonMethod(BaseNumericalMethod method) : base(method) { }

        public override string CalculateResult()
        {
            string result = base.CalculateResult();

            double x = A;
            int countOfIterations = (int) B;
            int currentIterationIndex = 0;

            for (int iterationIndex = 0; iterationIndex < countOfIterations; ++iterationIndex)
            {
                double fX = GetResultFromFunction(GetFunction(), x);
                double fXDerivative = GetResultFromFunction(GetFunction(), x, true);
                double newX = x - (fX / fXDerivative);

                double fNewX = GetResultFromFunction(GetFunction(), newX);

                if((fX == 0) || Math.Abs(newX - x) > Epsilon)
                {
                    currentIterationIndex = iterationIndex; 
                    break;
                }
                else
                {                    
                    x = newX;
                }
            }

            result += $"Корень уравнения: {x}\nКоличество итераций: {currentIterationIndex}";

            return result;

            throw new NotImplementedException();
        }
    }
}
