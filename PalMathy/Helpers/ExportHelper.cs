using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using PalMathy.Extensions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel;
using DocumentFormat.OpenXml;

namespace PalMathy.Helpers
{
    public class ImportHelper
    {
        public static ObservableCollection<ObservableCollection<T>> Get2DList<T>(int expectedRowsCount = -1, bool isExtendedMatrix = false)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel таблица (*.xls;*.xlsx)|*.xls;*.xlsx";
            openFileDialog.ShowDialog();

            ObservableCollection<ObservableCollection<T>> nums = new ObservableCollection<ObservableCollection<T>>();

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(openFileDialog.FileName, false))
            {

                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart ?? spreadsheetDocument.AddWorkbookPart();
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                string? text;

                int rowCount = sheetData.Elements<Row>().Count();                

                if (expectedRowsCount != -1)
                {
                    if (rowCount > expectedRowsCount)
                    {
                        throw new ArgumentException($"В матрице должно быть больше {expectedRowsCount} строк");
                    }
                }                

                foreach (Row r in sheetData.Elements<Row>())
                {
                    if (isExtendedMatrix)
                    {
                        if (r.Elements<Cell>().Count() != rowCount + 1)
                        {
                            throw new ArgumentException($"Неверный формат расширенной матрицы");
                        }
                    }

                    ObservableCollection<T> newRow = new ObservableCollection<T>();
                    foreach (Cell c in r.Elements<Cell>())
                    {
                        c.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                        var converter = TypeDescriptor.GetConverter(typeof(T));
                        if (converter != null)
                        {
                            var stringValue = c?.CellValue.InnerText;
                            newRow.Add((T)converter.ConvertFromString(stringValue));
                        }                        
                    }
                    nums.Add(newRow);
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

