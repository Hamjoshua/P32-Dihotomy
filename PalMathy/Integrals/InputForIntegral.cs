using PalMathy.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Integrals
{
    class InputForIntegral : BaseNumericalMethod
    {
        public InputForIntegral() {
            A = new BindedValue<double>(2, "Нижний предел А", true);
            B = new BindedValue<double>(5, "Верхний предел B", true);
            C = new BindedValue<double>(3, "Кол-во разбиений", true);
            FunctionString = "1/ln(x)";
            Epsilon = 0.001;

            Description = "Определённый интеграл является числом, равным пределу сумм особого вида";
        }


    }
}
