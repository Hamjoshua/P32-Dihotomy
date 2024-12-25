using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Windows;
using PalMathy.Extensions;
using System.IO.Packaging;
using System.ComponentModel;

namespace PalMathy.Helpers
{
    public class ImportHelper
    {
        public static ObservableCollection<ObservableCollection<T>> Get2DList<T>(int expectedRowsCount = -1)
        {
            ObservableCollection<ObservableCollection<T>> nums = new ObservableCollection<ObservableCollection<T>>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel таблица (*.xls)|*.xls";

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = excel.Workbooks.Open(openFileDialog.FileName);
            if (workbook.Worksheets.Count == 0)
            {
                throw new Exception("В Excel файле нет рабочих листов.");
            }
            Worksheet worksheet = (Worksheet)workbook.Worksheets[0];

            int rowCount = worksheet.Rows.Count;
            int colCount = worksheet.Columns.Count;

            if (expectedRowsCount != -1)
            {
                if (rowCount > expectedRowsCount)
                {
                    throw new ArgumentException("В матрице должно быть только две строки - X и Y!");
                }
            }

            for (int row = 0; row <= rowCount; row++)
            {
                var rowData = new ObservableCollection<T>();

                for (int col = 0; col <= colCount; col++)
                {
                    var cellValue = worksheet.Cells[row, col].ToString();

                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    try
                    {
                        if (converter != null && cellValue != null)
                        {
                            rowData.Add((T)converter.ConvertFromString(cellValue));
                        }
                    }
                    catch(Exception ex)
                    {
                        if(col != 0 && row != 0)
                        {
                            throw ex;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    
                }
                // Добавляем только не пустые строки
                if (rowData.Count > 0)
                {
                    nums.Add(rowData);
                }
            }

            return nums;
        }

        public static ObservableCollection<T> GetList<T>()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый файл (*.txt)|*.txt";
            ObservableCollection<T> nums = new ObservableCollection<T>();

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
                            nums.FromString<T>(text);
                        }
                        catch (NotSupportedException)
                        {
                            MessageBox.Show("Неверный формат списка! Нужно перечисление целых чисел через запятую");
                        }
                    }
                }

            }
            return nums;
        }
    }
}

