using PalMathy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace PalMathy.Pages
{
    /// <summary>
    /// Логика взаимодействия для LeastSquaresPage.xaml
    /// </summary>
    public partial class LeastSquaresPage : Page
    {
        public LeastSquaresPage()
        {
            InitializeComponent();
        }

        private void UpdateDataGrid2D()
        {
            d2dGrid.ItemsSource2D = null;
            d2dGrid.Items.Refresh();
            LeastSquaresViewModel dataContext = (LeastSquaresViewModel)this.DataContext;
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
                if(row.GetIndex() == 0)
                {
                    row.Header = "X";
                }
                else
                {
                    row.Header = "Y";
                }                 
            }
        }
    }
}
