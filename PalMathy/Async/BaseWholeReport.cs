using PalMathy.Sortings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Async
{
    abstract public class BaseWholeReport<T, D> where T : class 
                                                where D : BaseSingleReport<T>
    {
        public ObservableCollection<int> Elements { get; set; } = new ObservableCollection<int>();
        public List<T> Methods { get; set; }
        public ObservableCollection<D> OutReports { get; set; }

        public async Task MakeReports(object parameter=null)
        {
            Task[] sortTasks = new Task[Methods.Count];

            for (int methodIndex = 0; methodIndex < Methods.Count; ++methodIndex)
            {
                T method = Methods[methodIndex];
                D report = (D)Activator.CreateInstance(typeof(D), method, Elements); 
                //new BaseSingleReport<T>(method, Elements);
                OutReports.Add(report);
            }

            foreach (var report in OutReports)
            {
                await Task.Run(() => report.MakeAction(parameter));
            }
        }
    }

    public class WholeReport : BaseWholeReport<BaseSorting, SingleReport>
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
    }
}
