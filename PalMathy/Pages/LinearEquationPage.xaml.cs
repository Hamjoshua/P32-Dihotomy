using DataGrid2DLibrary;
using PalMathy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PalMathy.Pages
{
    /// <summary>
    /// Логика взаимодействия для LinearEquationPage.xaml
    /// </summary>
    public partial class LinearEquationPage : Page
    {
        public LinearEquationPage()
        {
            InitializeComponent();
            UpdateDataGrid2D();
        }

        private void UpdateDataGrid2D()
        {
            d2dGrid.ItemsSource2D = null;
            d2dGrid.Items.Refresh();
            SlauViewModel dataContext = (SlauViewModel)this.DataContext;
            d2dGrid.ItemsSource2D = dataContext.Matrix;
            d2dGrid.Items.Refresh();
        }



        private void ComboBox_MouseLeave(object sender, MouseEventArgs e)
        {
            UpdateDataGrid2D();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid2D();
        }

        private void d2dGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row is DataGridRow row)
            {
                row.Header = $" Уравнение {row.GetIndex() + 1} ";
            }

            for (int index = 0; index < d2dGrid.Columns.Count - 1; ++index)
            {
                var column = d2dGrid.Columns[index];
                column.Header = $"x{index + 1}";
            }
            if (d2dGrid.Columns.Count() > 0)
            {
                d2dGrid.Columns.Last().Header = "Ответ";
            }
        }
    }
}
