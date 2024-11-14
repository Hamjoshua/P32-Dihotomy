using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Sortings
{
    public class WholeReport
    {
        public ObservableCollection<int> Elements { get; set; }
        public DateTime BeginTime { get; set; }
        List<BaseSorting> Sortings { get; set; }
        public ObservableCollection<SingleReport> OutReports { get; set; }

        public WholeReport(ObservableCollection<int> elements, List<BaseSorting> sortings)
        {
            BeginTime = DateTime.Now;
            Elements = elements;
            Sortings = sortings.Where(d => d.IsActivated == true).ToList();
            OutReports = new ObservableCollection<SingleReport>();
        }
        
        public async Task MakeReports(bool isBiggerMode)
        {
            Task[] sortTasks = new Task[Sortings.Count];

            for (int sortingIndex = 0; sortingIndex < Sortings.Count; ++sortingIndex)
            {
                BaseSorting sorting = Sortings[sortingIndex];
                var elems = new ObservableCollection<int>(Elements.ToList());
                SingleReport report = new SingleReport(sorting, elems);
                OutReports.Add(report);

                
                sortTasks[sortingIndex] = Task.Run(() => report.BeginSort(isBiggerMode), 
                    CancelToken.Instance.cancellationTokenSource.Token);                
            }

            await Task.WhenAll(sortTasks);
        }
    }
}
