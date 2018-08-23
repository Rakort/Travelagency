using System;
using System.Windows.Input;
using Travelagency.Model;

namespace Travelagency.ViewModel
{
    public class AddTourViewModel : ValidationViewModelBase
    {
        public Tour Tour { get; set; }

        public string Country
        {
            get { return Tour.Country; }
            set { Tour.Country = value; }
        }

        public string Touroperator
        {
            get { return Tour.Touroperator; }
            set { Tour.Touroperator = value; }
        }

        public AddTourViewModel()
        {
            Load();
        }

        public AddTourViewModel(Tour tour)
        {
            Load(tour);
        }

        public void Load(Tour tour = null)
        {
            Tour = tour ?? new Tour();
            //Если дата пустая устанавливается текущая
            if (Tour.StartDate == null) Tour.StartDate = DateTime.Now;
            if (Tour.EndDate == null) Tour.EndDate = DateTime.Now;
            AddRule(() => Touroperator, () =>
                !String.IsNullOrWhiteSpace(Touroperator), "Турооператор должн быть заполнен");
            AddRule(() => Country, () =>
                !String.IsNullOrWhiteSpace(Country), "Страна должна быть заполнена");
        }
        public ICommand SaveCommand => new SimpleCommand(() =>
        {
            Tour.Save();
            ShowWin.ShowTour();
        });
        public ICommand CloseCommand => new SimpleCommand(ShowWin.ShowTour);

    }
}
