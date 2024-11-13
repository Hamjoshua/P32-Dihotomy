using Microsoft.Win32;
using PalMathy.Extensions;
using PalMathy.Sortings;
using System.Collections.ObjectModel;
using System.IO;
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

        public ObservableCollection<WholeReport> WholeReports
        {
            get { return _wholeReports; }
            set
            {
                // OnPropertyChanged(nameof(WholeReports));
                Set(ref _wholeReports, value);
            }
        }

        public IAsyncCommand SortElements
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    bool anyOfSortingsIsActivated = AllSortings.Any(x => x.IsActivated);

                    if (!anyOfSortingsIsActivated)
                    {
                        MessageBox.Show("Не выбраны методы сортировки!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var elems = new ObservableCollection<int>(Elements.ToList());
                    WholeReport newWholeReport = new WholeReport(elems, _allSortings);
                    WholeReports.Insert(0, newWholeReport);
                    await newWholeReport.MakeReports();

                    OnPropertyChanged(nameof(WholeReports));
                });
            }
        }

        public ICommand GenerateList
        {
            get
            {
                return new Commands((obj) =>
                {
                    int length = Random.Shared.Next(100);
                    ObservableCollection<int> list = new ObservableCollection<int>();
                    for(int i = 0; i < length; ++i)
                    {
                        list.Add(i);
                    }
                    list.Shuffle();
                    Elements = list;
                });
            }
        }
        public ICommand ShuffleElements
        {
            get
            {
                return new Commands((obj) =>
                {
                    Elements.Shuffle();
                    OnPropertyChanged(nameof(Elements));
                });
            }
        }

        public ICommand CancelSortings
        {
            get
            {
                return new Commands((obj) =>
                {
                    CancelToken.Instance.Cancel();
                });
            }
        }

        public ICommand ParseElementsFromFile
        {
            get
            {
                return new Commands((obj) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Текстовый файл (*.txt)|*.txt";

                    if (openFileDialog.ShowDialog() == true)
                    {
                        if (!String.IsNullOrEmpty(openFileDialog.FileName))
                        {
                            using(StreamReader reader = new StreamReader(openFileDialog.FileName))
                            {
                                string text = reader.ReadToEnd();
                                text.Replace("[", "");
                                text.Replace("[", "");
                                text.Replace("}", "");
                                text.Replace("{", "");

                                try
                                {
                                    Elements.FromString<int>(text);
                                }
                                catch (NotSupportedException)
                                {
                                    MessageBox.Show("Неверный формат списка! Нужно перечисление целых чисел через запятую");
                                }
                                OnPropertyChanged(nameof(Elements));
                            }
                        }
                        
                    }

                });
            }
        }
    }
}
