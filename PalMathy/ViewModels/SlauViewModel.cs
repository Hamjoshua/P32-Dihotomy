using PalMathy.Async;
using PalMathy.Slau;
using PalMathy.Sortings;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PalMathy.ViewModels
{

    public class SlauViewModel : BaseViewModel
    {
        private string _sizeOfMatrix = "3x3";
        private RandomExpert _randomExpert = new RandomExpert();
        private ObservableCollection<SlauWholeReport> _wholeReports = new ObservableCollection<SlauWholeReport>();
        private ObservableCollection<ObservableCollection<double>> _matrix = new ObservableCollection<ObservableCollection<double>>() {
                        new ObservableCollection<double> { 3, 2, -5, -1 },
                        new ObservableCollection<double> { 2, -1, 3, 13 },
                        new ObservableCollection<double> { 1, 2, -1, 9 }};

        public int MinRandomBound
        {
            get { return _randomExpert.MinBound; }
            set
            {
                _randomExpert.MinBound = value;
                OnPropertyChanged(nameof(MinRandomBound));                
            }
        }

        public int MaxRandomBound
        {
            get { return _randomExpert.MaxBound; }
            set
            {
                _randomExpert.MaxBound = value;
                OnPropertyChanged(nameof(MaxRandomBound));                
            }
        }

        public List<BaseLinearEquation> Slaus { get; set; } = new List<BaseLinearEquation>()
        {
            new GaussEquation(),
            new GaussJordanaEquation(),
            new KramerEquation()
        };
        public List<string> Sizes { get; set; } = new List<string>()
        {
            "2x2", "3x3", "4x4", "5x5", "6x6", "7x7", "8x8", "9x9"
        };

        public ObservableCollection<SlauWholeReport> WholeReports
        {
            get { return _wholeReports; }
            set
            {
                // OnPropertyChanged(nameof(WholeReports));
                Set(ref _wholeReports, value);
            }
        }
        public ObservableCollection<ObservableCollection<double>> Matrix
        {
            get
            {
                return _matrix;
            }
            set
            {
                _matrix = value;
                OnPropertyChanged(nameof(Matrix));
            }
        }

        public string SizeOfMatrix
        {
            get
            {
                return _sizeOfMatrix;
            }
            set
            {
                int firstChar = Math.Abs('0' - value[0]);

                ResizeMatrix(firstChar);

                _sizeOfMatrix = value;
                OnPropertyChanged(SizeOfMatrix);
            }
        }

        public void ResizeMatrix(int size)
        {
            if (Matrix.Count != size)
            {
                // Регулируем строки
                for (int index = Math.Abs(Matrix.Count - size); index > 0; --index)
                {
                    if (Matrix.Count > size)
                    {
                        Matrix.RemoveAt(index);
                    }
                    else if (Matrix.Count < size)
                    {
                        ObservableCollection<double> row = new ObservableCollection<double>();
                        for (int _ = 0; _ < size + 1; ++_)
                        {
                            row.Add(0);
                        }
                        Matrix.Add(row);
                    }
                    else
                    {
                        continue;
                    }
                }

                // Регулируем отделньные ячейки
                foreach (var row in Matrix)
                {
                    while(row.Count != size + 1)
                    {
                        if (row.Count > size + 1)
                        {
                                row.RemoveAt(0);
                        }
                        else if (row.Count < size + 1)
                        {
                                row.Insert(0, 0);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                OnPropertyChanged(nameof(Matrix));
            }
        }

        public ICommand ParseElementsFromFile
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    // TODO
                    MessageBox.Show("Функциональности нет! Но держите анекдот: Если у вас нет проблем, проверьте еще пульс. " +
                        "Может, его тоже нет");
                });
            }
        }

        public ICommand RandomMatrix
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    foreach(var row in Matrix)
                    {
                        for(int valueIndex = 0; valueIndex <= Matrix.Count; ++valueIndex)
                        {
                            row[valueIndex] = _randomExpert.GetRandomNumber();
                        }
                    }
                    OnPropertyChanged(nameof(Matrix));
                });                
            }
        }

        public IAsyncCommand CancelMethods
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    await CancelToken.Instance.Cancel();
                });
            }
        }

        public IAsyncCommand GetNumbers
        {
            get
            {
                return new AsyncCommand(async () =>
                {
                    bool anyOfMethodsIsActivated = Slaus.Any(x => x.IsActivated);

                    if (!anyOfMethodsIsActivated)
                    {
                        MessageBox.Show("Не выбраны методы СЛАУ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    SlauWholeReport newWholeReport = new SlauWholeReport(Slaus);
                    WholeReports.Insert(0, newWholeReport);
                    await newWholeReport.MakeReports(Matrix);

                    OnPropertyChanged(nameof(WholeReports));
                });
            }
        }
    }
}
