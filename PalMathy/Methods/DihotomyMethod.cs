using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if(resultA * resultB < 0)
            {
                while(b - a > Epsilon)
                {
                    middle = (a + b) / 2;
                    double resultMiddle = GetResultFromFunction(parsedFunction, middle);

                    if(resultMiddle == 0)
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

            return $"Корень не может быть найден в этом интервале";
        }

    }
}
