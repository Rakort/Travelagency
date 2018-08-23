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
    /// Логика взаимодействия для AddTourView.xaml
    /// </summary>
    public partial class AddTourView : UserControl
    {
        public AddTourView()
        {
            InitializeComponent();
        }
        private AddTourViewModel tourVM => (DataContext as AddTourViewModel);
        private Tour tour => tourVM.Tour;

        private void CheckBoxFood_Checked(object sender, RoutedEventArgs e)
        {
            var t = (sender as CheckBox).Content.ToString();
            if ((sender as CheckBox).IsChecked.Value)
            {
                tour.Food += $" {t};";
            }
            else
            {
                tour.Food = tour.Food.Replace($" {t};", "");
                tour.Food = tour.Food.Replace($"{t};", "");
            }
            tour.Food = tour.Food.TrimStart(' ');

        }

        private void CheckBoxAccommodation_Checked(object sender, RoutedEventArgs e)
        {
            var t = (sender as CheckBox).Content.ToString();
            if ((sender as CheckBox).IsChecked.Value)
            {
                tour.Accommodation += $" {t};";
            }
            else
            {
                tour.Accommodation = tour.Accommodation.Replace($" {t};", "");
                tour.Accommodation = tour.Accommodation.Replace($"{t};", "");
            }
            tour.Accommodation = tour.Accommodation.TrimStart(' ');
        }

        private void CheckBoxFood_Initialized(object sender, RoutedEventArgs e)
        {
            var t = (sender as CheckBox).Content.ToString();
            if (tour.Food != null)
                (sender as CheckBox).IsChecked = tour.Food.Contains($"{t};");
        }
        private void CheckBoxAccommodation_Initialized(object sender, RoutedEventArgs e)
        {
            var t = (sender as CheckBox).Content.ToString();
            if (tour.Accommodation != null)
                (sender as CheckBox).IsChecked = tour.Accommodation.Contains($"{t};");
        }
    }
}
