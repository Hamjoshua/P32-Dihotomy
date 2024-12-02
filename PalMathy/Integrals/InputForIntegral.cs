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
            A = new BindedValue<double>(1, "Нижний предел А", true);
            B = new BindedValue<double>(5, "Вернхий предел B", true);
            C = new BindedValue<double>(5, "Кол-во разбиений", true);

            Description = "Определённый интеграл является числом, равным пределу сумм особого вида";
        }


    }
}
