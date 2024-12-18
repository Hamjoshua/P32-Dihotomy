using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

    class GaussJordanaEquation : BaseLinearEquation
    {
        public GaussJordanaEquation()
        {
            Name = "Метод Гаусса-Жордана";
            Description = "Метод Гаусса — Жордана (метод полного исключения неизвестных) — метод, который используется для решения " +
                "квадратных систем линейных алгебраических уравнений, нахождения обратной матрицы, нахождения координат вектора в" +
                " заданном базисе или отыскания ранга матрицы. Метод является модификацией метода Гаусса.";
        }

        public override ObservableCollection<int> GetNumbers(ObservableCollection<ObservableCollection<int>> matrix)
        {
            int n = matrix.Count; //Размерность начальной матрицы

            int[,] xirtaM = new int[n, n]; //Единичная матрица (искомая обратная матрица)
            for (int i = 0; i < n; i++)
                xirtaM[i, i] = 1;

            int[,] matrix_Big = new int[n, 2 * n]; //Общая матрица, получаемая скреплением Начальной матрицы и единичной
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    matrix_Big[i, j] = matrix[i][j];
                    matrix_Big[i, j + n] = xirtaM[i, j];
                }

            //Прямой ход (Зануление нижнего левого угла)
            for (int k = 0; k < n; k++) //k-номер строки
            {
                for (int i = 0; i < 2 * n; i++) //i-номер столбца
                    matrix_Big[k, i] = matrix_Big[k, i] / matrix[k][k]; //Деление k-строки на первый член !=0 для преобразования его в единицу
                for (int i = k + 1; i < n; i++) //i-номер следующей строки после k
                {
                    int K = matrix_Big[i, k] / matrix_Big[k, k]; //Коэффициент
                    for (int j = 0; j < 2 * n; j++) //j-номер столбца следующей строки после k
                        matrix_Big[i, j] = matrix_Big[i, j] - matrix_Big[k, j] * K; //Зануление элементов матрицы ниже первого члена, преобразованного в единицу
                }
                for (int i = 0; i < n; i++) //Обновление, внесение изменений в начальную матрицу
                    for (int j = 0; j < n; j++)
                        matrix[i][j] = matrix_Big[i, j];
            }

            //Обратный ход (Зануление верхнего правого угла)
            for (int k = n - 1; k > -1; k--) //k-номер строки
            {
                for (int i = 2 * n - 1; i > -1; i--) //i-номер столбца
                    matrix_Big[k, i] = matrix_Big[k, i] / matrix[k][k];
                for (int i = k - 1; i > -1; i--) //i-номер следующей строки после k
                {
                    int K = matrix_Big[i, k] / matrix_Big[k, k];
                    for (int j = 2 * n - 1; j > -1; j--) //j-номер столбца следующей строки после k
                        matrix_Big[i, j] = matrix_Big[i, j] - matrix_Big[k, j] * K;
                }
            }

            //Отделяем от общей матрицы
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    xirtaM[i, j] = matrix_Big[i, j + n];

            return new ObservableCollection<int>(xirtaM.Cast<int>().ToList());
        }
    }
}
