using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Series;

namespace PalMathy.Methods
{
    class BaseMethod
    {
        PlotModel Graph = new PlotModel { Title = "График" };
        List<DataPoint> Points;
        string FunctionString;

        double BeginInterval;
        double EndInterval;
        
        void ParseFunction()
        {
            Function parsedFunction = new Function("f(x) = " + FunctionString);

            for (double counterI = BeginInterval; counterI <= EndInterval; ++counterI)
            {
                Expression e1 = new Expression($"f({counterI})", parsedFunction);
                Points.Add(new DataPoint(counterI, e1.calculate()));
            }

        }

    }
}
