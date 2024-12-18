using PalMathy.Async;
using PalMathy.Slau;
using PalMathy.Sortings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PalMathy.ViewModels
{

    public class SlauViewModel : BaseViewModel
    {
        private string _sizeOfMatrix = "3x3";
        private ObservableCollection<SlauWholeReport> _wholeReports = new ObservableCollection<SlauWholeReport>();
        private ObservableCollection<ObservableCollection<int>> _matrix = new ObservableCollection<ObservableCollection<int>>() {
                        new ObservableCollection<int> { 3, 2, -5, -1 },
                        new ObservableCollection<int> { 2, -1, 3, 13 },
                        new ObservableCollection<int> { 1, 2, -1, 9 }};

        public List<BaseLinearEquation> Slaus { get; set; } = new List<BaseLinearEquation>()
        {
            new GaussEquation(),
            new GaussJordanaEquation()
        };
        public List<string> Sizes { get; set; } = new List<string>()
        {
            "2x2", "3x3", "4x4", "5x5"
        };

        public ObservableCollection<int> Ints { get; set; } = new ObservableCollection<int> { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4 };

        public ObservableCollection<SlauWholeReport> WholeReports
        {
            get { return _wholeReports; }
            set
            {
                // OnPropertyChanged(nameof(WholeReports));
                Set(ref _wholeReports, value);
            }
        }
        public ObservableCollection<ObservableCollection<int>> Matrix
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
                    else
                    {
                        ObservableCollection<int> row = new ObservableCollection<int>();
                        for (int _ = 0; _ < size + 1; ++_)
                        {
                            row.Add(0);
                        }
                        Matrix.Add(row);
                    }
                }

                // Регулируем отделньные ячейки
                foreach (var row in Matrix)
                {
                    if(row.Count != size + 1)
                    {
                        if (row.Count > size)
                        {
                            for(int _ = size; _ < row.Count + 1; ++_)
                            {
                                row.RemoveAt(0);
                            }
                        }
                        else
                        {
                            for (int _ = row.Count; _ < size + 1; ++_)
                            {
                                row.Insert(0, 0);
                            }
                        }
                    }                    
                }

                OnPropertyChanged(nameof(Matrix));
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
                    SlauWholeReport newWholeReport = new SlauWholeReport(Slaus);
                    WholeReports.Insert(0, newWholeReport);
                    await newWholeReport.MakeReports(Matrix);

                    OnPropertyChanged(nameof(WholeReports));
                });
            }
        }
    }
}
