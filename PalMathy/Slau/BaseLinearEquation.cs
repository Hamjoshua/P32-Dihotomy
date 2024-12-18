using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Slau
{
    public abstract class BaseLinearEquation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public abstract ObservableCollection<int> GetNumbers(ObservableCollection<ObservableCollection<int>> matrix);
        public bool IsActivated { get; set; } = false;
    }

    class GaussEquation : BaseLinearEquation
    {
        public GaussEquation()
        {
            Name = "Метод Гаусса";
            Description = "Простой метод, в котором матрица приобретает нули по ступенькам";
        }
        public override ObservableCollection<int> GetNumbers(ObservableCollection<ObservableCollection<int>> matrix)
        {           
            // Базовые операции с матрицей
            for (int columnIndex = 0; columnIndex < matrix.Count; ++columnIndex)
            {
                ObservableCollection<int> zeroRow = matrix[columnIndex];
                int rowIndex = columnIndex + 1;

                while (rowIndex < matrix.Count)
                {
                    ObservableCollection<int> row = matrix[rowIndex];

                    int zeroCoeff = zeroRow[columnIndex];
                    int secondCoeff = row[columnIndex];

                    for (int index = 0; index < row.Count; ++index)
                    {
                        row[index] = (row[index] * zeroCoeff) - (zeroRow[index] * secondCoeff);
                    }
                    matrix[rowIndex] = row;
                    ++rowIndex;
                }
            }

            // Нахождение ответов
            ObservableCollection<int> decisions = new ObservableCollection<int>();            
            for (int rowIndex = matrix.Count - 1; rowIndex >= 0; --rowIndex)
            {
                int sum = 0;
                int lastElement = matrix[rowIndex][matrix.Count];
                int indexx = decisions.Count;

                while (indexx > 0)
                {
                    sum += matrix[rowIndex][rowIndex + indexx] * decisions[indexx - 1];
                    --indexx;
                }

                int x = (lastElement - sum) / matrix[rowIndex][rowIndex];
                decisions.Insert(0, x);
            }

            return decisions;
        }
    }
}
