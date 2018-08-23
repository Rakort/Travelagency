using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Travelagency.Annotations;
using Travelagency.Model;

namespace Travelagency.ViewModel
{
    public class OrdersViewModel : ViewModelBase
    {
        public CollectionView Collection { get; set; }
        private readonly bool _isOrder;
        public string AddText { get; set; }

        public OrdersViewModel(){}
        public OrdersViewModel(bool isOrder)
        {
            _isOrder = isOrder;
            AddText = _isOrder ? "+ Добавить заявку" : "+ Добавить обращение";
            Collection = new CollectionView(Sql.GetTable<Order>().Where(w=>w.IsOrder == isOrder));
        }

        public ICommand FindCommand => new SimpleCommand(() =>
        {
            Collection.Filter = null;
            Collection.Filter = o =>
            {
                if (o == null) return false;
                var i = o as Order;
                return false;
            };
        });

        public ICommand AddCommand => new SimpleCommand(()=>ShowWin.ShowAddOrder(null, _isOrder));
        public ICommand EditCommand => new SimpleCommand(() => ShowWin.ShowAddOrder(Collection.CurrentItem as Order, _isOrder));
        public ICommand DelCommand => new SimpleCommand(() =>
        {
            var s = _isOrder ? "заявку" : "обращение";
            var res = MessageBox.Show($"Вы действительно хотите удалить {s}", $"Удалить {s}", MessageBoxButton.OKCancel,
                MessageBoxImage.Warning);

            if (res == MessageBoxResult.OK)
            {
                Sql.Delete(Collection.CurrentItem as Order);
                Collection = new CollectionView(Sql.GetTable<Order>().Where(w => w.IsOrder == _isOrder));
                OnPropertyChanged(nameof(Collection));
            }
        });


    } 
}
