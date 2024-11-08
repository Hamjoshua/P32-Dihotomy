using PalMathy.Sortings;
using System.Windows.Input;

namespace PalMathy.ViewModels
{
    public class SortingViewModel : BaseViewModel
    {
        private List<WholeReport> _wholeReports = new List<WholeReport>();
        private List<BaseSorting> _allSortings = new List<BaseSorting> {
            new BubbleSorting()
        };
        private List<int> _elements = new List<int>();

        public List<BaseSorting> AllSortings
        {
            get { return _allSortings; }
        }

        public ICommand SortElements
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    WholeReport newWholeReport = new WholeReport(_elements.ToList(), _allSortings);
                    _wholeReports.Add(newWholeReport);
                    newWholeReport.MakeReports();
                });
            }
        }

        public ICommand ShuffleElements
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                });
            }
        }

        public ICommand ParseElementsFromFile
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                });
            }
        }
    }
}
