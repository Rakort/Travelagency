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
using Travelagency.Model;
using Travelagency.ViewModel;

namespace Travelagency
{
    /// <summary>
    /// Логика взаимодействия для AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : UserControl
    {
        public AddOrderView()
        {
            InitializeComponent();
        }

        private AddOrderViewModel orderVM => (DataContext as AddOrderViewModel);
        private Order order => orderVM.Order;


        private void CheckBoxFood_Checked(object sender, RoutedEventArgs e)
        {
            var t = (sender as CheckBox).Content.ToString();
            if ((sender as CheckBox).IsChecked.Value)
            {
                order.Food += $" {t};";
            }
            else
            {
                order.Food = order.Food.Replace($" {t};", "");
                order.Food = order.Food.Replace($"{t};", "");
            }
            order.Food = order.Food.TrimStart(' ');
            
        }

        private void CheckBoxAccommodation_Checked(object sender, RoutedEventArgs e)
        {
            var t = (sender as CheckBox).Content.ToString();
            if ((sender as CheckBox).IsChecked.Value)
            {
                order.Accommodation += $" {t};";
            }
            else
            {
                order.Accommodation = order.Accommodation.Replace($" {t};", "");
                order.Accommodation = order.Accommodation.Replace($"{t};", "");
            }
            order.Accommodation = order.Accommodation.TrimStart(' ');
        }

        private void CheckBoxFood_Initialized(object sender, RoutedEventArgs e)
        {
            var t = (sender as CheckBox).Content.ToString();
            if(order.Food != null)
                (sender as CheckBox).IsChecked = order.Food.Contains($"{t};");
        }
        private void CheckBoxAccommodation_Initialized(object sender, RoutedEventArgs e)
        {
            var t = (sender as CheckBox).Content.ToString();
            if (order.Accommodation != null)
                (sender as CheckBox).IsChecked = order.Accommodation.Contains($"{t};");
        }

    }
}
