using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Sortings
{
    class WholeReport
    {
        List<int> Elements;
        DateTime BeginTime;
        List<BaseSorting> Sortings;
        List<SingleReport> OutReports;

        WholeReport(List<int> elements, List<BaseSorting> sortings)
        {
            BeginTime = DateTime.Now;
            Sortings = sortings.Where(d => d.IsActivated == false).ToList();
            OutReports = new List<SingleReport>();
        }
        
        async void MakeReports()
        {
            foreach (BaseSorting sorting in Sortings)
            {
                SingleReport report = new SingleReport(sorting);
                OutReports.Add(report);
                await report.BeginSort(Elements);
            }
        }
    }
}
