using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Methods
{
    class GoldenRatioMethod : BaseNumericalMethod
    {
        public double GoldenRatio = (1 + Math.Sqrt(5)) / 2;
        
        public override string CalculateResult()
        {
            double a = A;
            double b = B;
            double x1 = b - ((b - a) / GoldenRatio);
            double x2 = a + ((b - a) / GoldenRatio);
            
            while (Math.Abs(b - a) > Epsilon)
            {                
                double f1 = GetResultFromFunction(GetFunction(), x1);
                double f2 = GetResultFromFunction(GetFunction(), x2);
                
                if (f1 < f2)
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
            
            return $"Минимум функции: {(a + b) / 2}";
        }
    }
}
