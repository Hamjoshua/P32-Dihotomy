using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Methods
{
    class GoldenRatioMethod : BaseNumericalMethod
    {
        public GoldenRatioMethod() { }

        public GoldenRatioMethod(BaseNumericalMethod method) : base(method) { }

        public double GoldenRatio = (1 + Math.Sqrt(5)) / 2;
        
        public override string CalculateResult()
        {
            string result = base.CalculateResult();

            result += $"Минимум функции: {GetResult(true)}\n" +
                $"Максимум функции: {GetResult(false)}";
            
            return result;
        }

        private double GetResult(bool findMin)
        {
            double a = A;
            double b = B;
            double x1 = b - ((b - a) / GoldenRatio);
            double x2 = a + ((b - a) / GoldenRatio);

            while (Math.Abs(b - a) > Epsilon)
            {
                double f1 = GetResultFromFunction(GetFunction(), x1);
                double f2 = GetResultFromFunction(GetFunction(), x2);

                if ((f1 < f2 && findMin) || (f1 > f2 && !findMin))
                {
                    b = x2;
                    x2 = x1;
                    x1 = b - (b - a) / GoldenRatio;
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    x2 = a + (b - a) / GoldenRatio;
                }
            }

            return (a + b) / 2;
        }
    }
}
