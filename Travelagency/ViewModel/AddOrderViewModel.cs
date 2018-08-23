using System;
using System.Windows.Input;
using Travelagency.Model;

namespace Travelagency.ViewModel
{
    public class AddOrderViewModel : ValidationViewModelBase
    {
        public Order Order { get; set; }

        public string LastName
        {
            get { return Order.LastName; }
            set { Order.LastName = value; }
        }

        public string FirstName
        {
            get { return Order.FirstName; }
            set { Order.FirstName = value; }
        }

        private bool _isOrder;
        public AddOrderViewModel()
        {
            Order = new Order();
        }

        public AddOrderViewModel(Order order, bool isOrder)
        {
            _isOrder = isOrder;
            Load(order);
        }

        public void Load(Order order = null)
        {
            Order = order ?? new Order();
            Order.IsOrder = _isOrder;
            //Если дата пустая устанавливается текущая
            if (Order.Date == null) Order.Date = DateTime.Now;
            OnPropertyChanged(nameof(Order));

            AddRule(() => LastName, () =>
                !String.IsNullOrWhiteSpace(LastName), "Фамилия должна быть заполнена");
            AddRule(() => FirstName, () =>
                !String.IsNullOrWhiteSpace(FirstName), "Имя должно быть заполнено");
        }

        public ICommand SaveCommand => new SimpleCommand(() =>
        {
            Order.Save();
            ShowWin.ShowOrder(_isOrder);
        }, (_) => !HasErrors);
        public ICommand CloseCommand => new SimpleCommand(()=>ShowWin.ShowOrder(_isOrder));
    }
}
