//using PalMathy.Sortings;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PalMathy.Async
//{
//    abstract public class BaseWholeReport<T> where T: new()
//    {
//        public List<T> Methods { get; set; }
//        public ObservableCollection<BaseSingleReport<T>> OutReports { get; set; }
//        public async Task MakeReports()
//        {
//            Task[] sortTasks = new Task[Methods.Count];

//            for (int methodIndex = 0; methodIndex < Methods.Count; ++methodIndex)
//            {
//                T sorting = Methods[methodIndex];
//                var elems = new ObservableCollection<int>(Elements.ToList());
//                BaseSingleReport report = new SingleReport(sorting, elems);
//                OutReports.Add(report);
//            }

//            foreach (var report in OutReports)
//            {
//                await Task.Run(() => report.BeginSort(isBiggerMode));
//            }
//        }
//    }

//    public class WholeReport
//    {
//        public ObservableCollection<int> Elements { get; set; }
//        public DateTime BeginTime { get; set; }
//        List<BaseSorting> Sortings { get; set; }
//        public ObservableCollection<SingleReport> OutReports { get; set; }

//        public WholeReport(ObservableCollection<int> elements, List<BaseSorting> sortings)
//        {
//            BeginTime = DateTime.Now;
//            Elements = elements;
//            Sortings = sortings.Where(d => d.IsActivated == true).ToList();
//            OutReports = new ObservableCollection<SingleReport>();
//        }

//        public async Task MakeReports(bool isBiggerMode)
//        {
//            Task[] sortTasks = new Task[Sortings.Count];

//            for (int sortingIndex = 0; sortingIndex < Sortings.Count; ++sortingIndex)
//            {
//                BaseSorting sorting = Sortings[sortingIndex];
//                var elems = new ObservableCollection<int>(Elements.ToList());
//                SingleReport report = new SingleReport(sorting, elems);
//                OutReports.Add(report);
//            }

//            foreach (var report in OutReports)
//            {
//                await Task.Run(() => report.BeginSort(isBiggerMode));
//            }
//        }
//    }
//}
