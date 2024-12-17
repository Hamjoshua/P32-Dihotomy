using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Slau
{
    abstract class BaseLinearEquation
    {
        abstract public List<int> GetNumbers(List<List<int>> matrix);
    }

    class GaussEquation : BaseLinearEquation
    {
        public override List<int> GetNumbers(List<List<int>> matrix)
        {           
            // Базовые операции с матрицей
            for (int columnIndex = 0; columnIndex < matrix.Count; ++columnIndex)
            {
                List<int> zeroRow = matrix[columnIndex];
                int rowIndex = columnIndex + 1;

                while (rowIndex < matrix.Count)
                {
                    List<int> row = matrix[rowIndex];

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
            List<int> decisions = new List<int>();            
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
