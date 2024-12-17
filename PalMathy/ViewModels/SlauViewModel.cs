﻿using PalMathy.Slau;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PalMathy.ViewModels
{

    public class SlauViewModel : BaseViewModel
    {
        private string _sizeOfMatrix = "3x3";

        BaseLinearEquation _method = new GaussEquation();
        private ObservableCollection<ObservableCollection<int>> _matrix = new ObservableCollection<ObservableCollection<int>>() {
                        new ObservableCollection<int> { 3, 2, -5, -1 },
                        new ObservableCollection<int> { 2, -1, 3, 13 },
                        new ObservableCollection<int> { 1, 2, -1, 9 }};

        //private ObservableCollection<ObservableCollection<int>> GetMatrixFromUI
        //{
        //    throw NotImplementedException;
        //}
        public ObservableCollection<int> Ints { get; set; } = new ObservableCollection<int> { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4 };
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
                _sizeOfMatrix = value;
                OnPropertyChanged(SizeOfMatrix);
            }
        }

        public ICommand GetNumbers
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    // _method.GetNumbers(Matrix);
                });
            }
        }
    }
}
