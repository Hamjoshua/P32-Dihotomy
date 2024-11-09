using PalMathy.Sortings;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PalMathy.ViewModels
{
    public class SortingViewModel : BaseViewModel
    {
        private ObservableCollection<WholeReport> _wholeReports = new ObservableCollection<WholeReport>();
        private List<BaseSorting> _allSortings = new List<BaseSorting>
        {
            new BubbleSorting(),
            new InsertSorting(),
            new QuickSorting(),
            new ShakeSorting(),
            new BogoSorting()
        };
        private ObservableCollection<int> _elements = new ObservableCollection<int>()
        {
            111, 2, 3, 4, 0, -1, -2, 8, -14, 1, -63, 32, -52, 321, 32, -9, 21, -6
        };

        public ObservableCollection<int> Elements
        {
            get { return _elements; }
            set
            {
                Set<ObservableCollection<int>>(ref _elements, value);
            }
        }
        public List<BaseSorting> AllSortings
        {
            get { return _allSortings; }
        } 

        public ObservableCollection<WholeReport> WholeReports { 
            get { return _wholeReports; }
            set
            {
                OnPropertyChanged(nameof(WholeReports));
                // Set(ref _wholeReports, value);
            }
        }

        public ICommand SortElements
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    bool anyOfSortingsIsActivated = AllSortings.Any(x => x.IsActivated);

                    if (!anyOfSortingsIsActivated)
                    {
                        MessageBox.Show("Не выбраны методы сортировки!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var elems = new ObservableCollection<int>(Elements.ToList());
                    WholeReport newWholeReport = new WholeReport(elems, _allSortings);
                    WholeReports.Add(newWholeReport);
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
