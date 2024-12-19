using PalMathy.Sortings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace PalMathy.Slau
{
    public abstract class BaseLinearEquation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public abstract ObservableCollection<int> GetNumbers(List<List<int>> matrix);
        public bool IsActivated { get; set; } = false;
    }

    class GaussEquation : BaseLinearEquation
    {
        public GaussEquation()
        {
            Name = "Метод Гаусса";
            Description = "Система уравнений преобразуется к треугольному виду путем последовательного исключения неизвестных." +
                " Затем решение находится методом обратного хода (подстановкой).\n\n" +
                "Гаусса-Жордана эффективен для систем среднего и большого размера. Он немного быстрее, чем метод Гаусса," +
                " и часто предпочтительнее для решения систем на компьютере.";
        }
        public List<List<int>> MakeForwardGaussOperation(List<List<int>> matrix)
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

            return matrix;
        }
        public override ObservableCollection<int> GetNumbers(List<List<int>> matrix)
        {
            matrix = MakeForwardGaussOperation(matrix);

            // Нахождение ответов
            ObservableCollection<int> decisions = new ObservableCollection<int>();            
            for (int rowIndex = matrix.Count - 1; rowIndex >= 0; --rowIndex)
            {
                int sum = 0;
                int lastElement = matrix[rowIndex][matrix.Count];
                int indexx = decisions.Count;

                while (indexx > 0)
                {
                    if (CancelToken.Instance.cancellationTokenSource.IsCancellationRequested)
                    {
                        return null;
                    }
                    sum += matrix[rowIndex][rowIndex + indexx] * decisions[indexx - 1];
                    --indexx;
                }

                if (matrix[rowIndex][rowIndex] == 0)
                {
                    throw new ArgumentException("В матрице не должны быть нули");
                }
                int x = (lastElement - sum) / matrix[rowIndex][rowIndex];
                decisions.Insert(0, x);
            }

            return decisions;
        }
    }

    class GaussJordanaEquation : GaussEquation
    {
        public GaussJordanaEquation()
        {
            Name = "Метод Гаусса-Жордана";
            Description = "Расширение метода Гаусса. В отличие от метода Гаусса, он приводит матрицу к диагональному виду (единицы на главной диагонали и нули вне её). " +
                "Решение находится непосредственно из диагональной матрицы.\n\n" +
                "квадратных систем линейных алгебраических уравнений, нахождения обратной матрицы, нахождения координат вектора в" +
                " заданном базисе или отыскания ранга матрицы. Метод является модификацией метода Гаусса.";
        }

        public override ObservableCollection<int> GetNumbers(List<List<int>> matrix)
        {
            int n = matrix.Count();
            int m = matrix[0].Count();

            if (m <= n) throw new ArgumentException("Матрица должна быть расширенной (больше столбцов, чем строк)");


            matrix = MakeForwardGaussOperation(matrix);

            //Обратный ход            
            for (int columnIndex = n - 1; columnIndex > 0; --columnIndex)
            {
                List<int> zeroRow = matrix[columnIndex];
                int rowIndex = columnIndex - 1;

                while (rowIndex >= 0)
                {
                    if (CancelToken.Instance.cancellationTokenSource.IsCancellationRequested)
                    {
                        return null;
                    }

                    List<int> row = matrix[rowIndex];

                    int zeroCoeff = zeroRow[columnIndex];
                    int secondCoeff = row[columnIndex];

                    for (int index = 0; index < row.Count; ++index)
                    {
                        row[index] = (row[index] * zeroCoeff) - (zeroRow[index] * secondCoeff);
                    }
                    matrix[rowIndex] = row;
                    --rowIndex;
                }
            }

            //Извлечение решений
            ObservableCollection<int> solutions = new ObservableCollection<int>();
            for (int i = 0; i < n; i++)
            {
                if (matrix[i][i] == 0)
                {
                    throw new ArgumentException("В матрице не должны быть нули");
                }
                solutions.Add(matrix[i][n] / matrix[i][i]);
            }
            return solutions;
        }

        private static void SwapRows(List<List<int>> matrix, int row1, int row2)
        {
            int cols = matrix[0].Count();
            for (int i = 0; i < cols; i++)
            {
                int temp = matrix[row1][i];
                matrix[row1][i] = matrix[row2][i];
                matrix[row2][i] = temp;
            }
        }
    }

    class KramerEquation : BaseLinearEquation
    {
        public KramerEquation()
        {
            Name = "Метод Крамера";
            Description = "Решение системы находится путем вычисления определителей. Для каждой неизвестной вычисляется определитель матрицы," +
                " где столбец свободных членов замещается столбцом коэффициентов данной неизвестной. Решение для неизвестной равно " +
                "отношению этого определителя к определителю основной матрицы коэффициентов.\n\n" +
                "Метод Крамера Метод Крамера прост для понимания и реализации, но очень неэффективен для больших систем уравнений (более 3-4 неизвестных). " +
                "Его преимущество — простота и наглядность для маленьких систем.";

        }
        public override ObservableCollection<int> GetNumbers(List<List<int>> matrix)
        {
            int n = matrix.Count;
            if (n == 0) return new ObservableCollection<int>(); //Обработка пустой матрицы

            // Проверка на квадратность матрицы
            if (!matrix.All(row => row.Count == n + 1))
            {
                throw new ArgumentException("Матрица должна быть расширенной (n строк, n+1 столбец) и квадратной");
            }


            double detMain = Determinant(matrix.Select(row => row.Take(n).ToArray()).ToArray());

            if (Math.Abs(detMain) < 1e-9)
            {
                throw new Exception("Определитель главной матрицы равен нулю. Метод Крамера неприменим.");
            }

            ObservableCollection<int> solutions = new ObservableCollection<int>();
            for (int i = 0; i < n; i++)
            {
                List<List<int>> tempMatrix = new List<List<int>>();
                for (int j = 0; j < n; j++)
                {
                    tempMatrix.Add(matrix[j].ToList());
                    tempMatrix[j].RemoveAt(n);
                }

                if (CancelToken.Instance.cancellationTokenSource.IsCancellationRequested)
                {
                    return null;
                }

                tempMatrix = ReplaceColumnWithConstants(tempMatrix, matrix, i);
                double det = Determinant(tempMatrix.Select(row => row.ToArray()).ToArray());
                solutions.Add((int)(det / detMain)); //Приведение к int; возможна потеря точности
            }

            return solutions;
        }

        private static double Determinant(int[][] matrix)
        {
            int n = matrix.Length;
            if (n == 1) return matrix[0][0];
            if (n == 2) return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];

            double det = 0;
            for (int i = 0; i < n; i++)
            {
                int[][] submatrix = new int[n - 1][];
                for (int j = 1; j < n; j++)
                {
                    submatrix[j - 1] = matrix[j].Where((x, index) => index != i).ToArray();
                }
                det += Math.Pow(-1, i) * matrix[0][i] * Determinant(submatrix);
            }
            return det;
        }

        private static List<List<int>> ReplaceColumnWithConstants(List<List<int>> matrix, List<List<int>> originalMatrix, int columnIndex)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                matrix[i][columnIndex] = originalMatrix[i].Last();
            }
            return matrix;
        }
    }
}
