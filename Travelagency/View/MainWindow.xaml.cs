using System.Windows;
using Travelagency.Model;

namespace Travelagency.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowWin.Init(this);
            Sql.CreateTable();
            ShowWin.ShowOrder(true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowWin.ShowOrder(true);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowWin.ShowTour();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShowWin.ShowOrder(false);
        }
    }
}
