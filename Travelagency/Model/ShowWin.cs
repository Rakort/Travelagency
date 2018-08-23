using Travelagency.View;
using Travelagency.ViewModel;

namespace Travelagency.Model
{
    public static class ShowWin
    {
        private static MainWindow _window;

        public static void Init(MainWindow window)
        {
            _window = window;
        }

        public static void ShowOrder(bool isOrder)
        {
            _window.Border.Child = new OrdersView() { DataContext = new OrdersViewModel(isOrder) };
        }
        public static void ShowTour()
        {
            _window.Border.Child = new ToursView() { DataContext = new ToursViewModel() };
        }
        public static void ShowAddOrder(Order order, bool isOrder)
        {
            _window.Border.Child = new AddOrderView() { DataContext = new AddOrderViewModel(order, isOrder) };
        }
        
        public static void ShowAddTour(Tour tour)
        {
            _window.Border.Child = new AddTourView() { DataContext = new AddTourViewModel(tour) };
        }
        
        public static void ShowRecourse()
        {
            _window.Border.Child = new OrdersView() { DataContext = new OrdersViewModel(false) };
        }
    }
}
