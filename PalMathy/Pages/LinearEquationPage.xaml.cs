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
        }

        private void ComboBox_MouseLeave(object sender, MouseEventArgs e)
        {
            d2dGrid.ItemsSource2D = null;
            d2dGrid.Items.Refresh();
            SlauViewModel dataContext = (SlauViewModel)this.DataContext;
            d2dGrid.ItemsSource2D = dataContext.Matrix;
            d2dGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_MouseLeave(sender, null);
        }
    }
}
