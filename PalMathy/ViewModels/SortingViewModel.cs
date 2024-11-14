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
        private bool _isBiggerMode = true;
        private ObservableCollection<WholeReport> _wholeReports = new ObservableCollection<WholeReport>();
        private RandomExpert _randomExpert = new RandomExpert();
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

        public bool IsBiggerMode
        {
            get { return _isBiggerMode; }
            set
            {
                _isBiggerMode = value;
                OnPropertyChanged(nameof(IsBiggerMode));
            }
        }

        public int MinRandomBound
        {
            get { return _randomExpert.MinBound; }
            set
            {
                _randomExpert.MinBound = value;
                OnPropertyChanged(nameof(MinRandomBound));
                OnPropertyChanged(nameof(LengthOfRandomArray));
            }
        }

        public int MaxRandomBound
        {
            get { return _randomExpert.MaxBound; }
            set
            {
                _randomExpert.MaxBound = value;
                OnPropertyChanged(nameof(MaxRandomBound));
                OnPropertyChanged(nameof(LengthOfRandomArray));
            }
        }

        public int LengthOfRandomArray
        {
            get { return _randomExpert.LengthOfArray; }
            set
            {
                _randomExpert.LengthOfArray = value;
                OnPropertyChanged(nameof(LengthOfRandomArray));
            }
        }

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
                    await newWholeReport.MakeReports(IsBiggerMode);

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
                    Elements = _randomExpert.GetRandomArray();
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
                            using (StreamReader reader = new StreamReader(openFileDialog.FileName))
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
