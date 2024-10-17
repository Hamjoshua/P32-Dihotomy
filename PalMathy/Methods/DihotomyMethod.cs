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
        public DihotomyMethod()
        {
            Description = "Делит отрезок поиска пополам, выбирая для следующего шага ту половину," +
                    " где функция принимает меньшее (для минимума) или большее (для максимума) значение";
        }

        public DihotomyMethod(BaseNumericalMethod method) : base(method)
        {
            Description = "Делит отрезок поиска пополам, выбирая для следующего шага ту половину," +
                    " где функция принимает меньшее (для минимума) или большее (для максимума) значение";
        }

        public override string CalculateResult()
        {
            string result = base.CalculateResult();

            if (result == NO_ZEROS)
            {
                return result;
            }

            double a = A;
            double b = B;
            double middle = (a + b) / 2;

            Function parsedFunction = GetFunction();

            double resultA = GetResultFromFunction(parsedFunction, a);
            double resultB = GetResultFromFunction(parsedFunction, b);

            if (resultA == 0)
            {
                result += $"Корень уравнения: {a}";
                return result;
            }

            if (resultB == 0)
            {
                result += $"Корень уравнения: {b}";
                return result;
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

            result += $"Корень уравнения: {middle}";

            return result;
        }
    }

}

