using PalMathy.Slau;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PalMathy.ViewModels
{
    public class SlauViewModel : BaseViewModel
    {
        BaseLinearEquation _method = new GaussEquation();
        private List<List<int>> _matrix = new List<List<int>>() {
                        new List<int> { 3, 2, -5, -1 },
                        new List<int> { 2, -1, 3, 13 },
                        new List<int> { 1, 2, -1, 9 }};
        public List<List<int>> Matrix
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
        public ICommand GetNumbers
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _method.GetNumbers(Matrix);
                });
            }
        }
    }
}
