using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace PalMathy.Methods
{
    class DihotomyMethod : BaseNumericalMethod
    {
        public override string CalculateResult()
        {
            if (!IsFunctionContinious())
            {
                return "Данная функция не является непрерывной, расчет корня уравнения невозможен";
            }

            double a = A;
            double b = B;
            double middle = (a + b) / 2;

            Function parsedFunction = GetFunction();

            double resultA = GetResultFromFunction(parsedFunction, a);
            double resultB = GetResultFromFunction(parsedFunction, b);

            if(resultA == 0)
            {
                return $"Корень уравнения: {a}";
            }

            if (resultB == 0)
            {
                return $"Корень уравнения: {b}";
            }

            while (b - a > Epsilon)
            {
                middle = (a + b) / 2;
                resultA = GetResultFromFunction(parsedFunction, a);
                double resultMiddle = GetResultFromFunction(parsedFunction, middle);

                if (resultMiddle == 0)
                {
                    break;
                }
                else if (resultA * resultMiddle < 0)
                {
                    b = middle;
                }
                else
                {
                    a = middle;
                }
            }

            return $"Корень уравнения: {middle}";
        }
    }

}

