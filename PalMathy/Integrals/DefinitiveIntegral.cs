using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Integrals
{
    public class DefinitiveIntegral
    {
        public double AMin { get; set; } = -5;
        public double BMax { get; set; } = 10;
        public string FunctionString { get; set; } = "2x";

        public double GetResult()
        {
            double result;
            
            Expression expression = new Expression($"int({FunctionString}, x, {AMin}, {BMax})");
            result = expression.calculate();

            return result;
        }
    }
}
    