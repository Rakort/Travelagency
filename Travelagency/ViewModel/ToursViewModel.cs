using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Travelagency.Annotations;
using Travelagency.Model;

namespace Travelagency.ViewModel
{
    public class ToursViewModel : ViewModelBase
    {
        public PagingObservableCollection<Tour> ObColl { get; set; }
        public Tour FindTour { get; set; }
        
        public bool CleanBool { get; set; }
        public Tour CurrentTour { get; set; }
        public ToursViewModel()
        {
            ObColl = new PagingObservableCollection<Tour>(Sql.GetTable<Tour>());
            FindTour = new Tour();
            ObColl.Filter = Filter;
        }

        public ICommand FindCommand => new SimpleCommand(() =>
        {
            ObColl.Refresh();
            
        });

        
        private bool Filter(object obj)
        {
            if (obj == null) return false;
            var t = obj as Tour;
            if (!String.IsNullOrEmpty(FindTour.Touroperator))
                if (!t.Touroperator.ToLower().Contains(FindTour.Touroperator.ToLower())) return false;
            if (!String.IsNullOrEmpty(FindTour.Country))
                if (!t.Country.ToLower().Contains(FindTour.Country.ToLower())) return false;
            if (!String.IsNullOrEmpty(FindTour.City))
                if (!t.City.ToLower().Contains(FindTour.City.ToLower())) return false;
            if (!String.IsNullOrEmpty(FindTour.Hotel))
                if (!t.Hotel.ToLower().Contains(FindTour.Hotel.ToLower())) return false;

            if (t.StartDate > FindTour.StartDate || t.EndDate < FindTour.StartDate) return false;
            if (!String.IsNullOrEmpty(FindTour.Food))
            {
                var str = FindTour.Food.Split(' ');
                foreach (var s in str)
                {
                    if(!t.Food.Contains(s)) return false;
                }
            }
            if (!String.IsNullOrEmpty(FindTour.Accommodation))
            {
                var str = FindTour.Accommodation.Split(' ');
                foreach (var s in str)
                {
                    if (!t.Accommodation.Contains(s)) return false;
                }
            }
            return true;
        }

        public ICommand AddCommand => new SimpleCommand(() => ShowWin.ShowAddTour(null));
        public ICommand EditCommand => new SimpleCommand(() => ShowWin.ShowAddTour(CurrentTour));
        public ICommand ClearFindCommand => new SimpleCommand(() =>
        {
            FindTour = new Tour();
            OnPropertyChanged(nameof(FindTour));
            OnPropertyChanged(nameof(CleanBool));
            ObColl.Refresh();
        });
        public ICommand DelCommand => new SimpleCommand(() =>
        {
            var res = MessageBox.Show("Вы действительно хотите удалить тур", "Удалить тур", MessageBoxButton.OKCancel,
                MessageBoxImage.Warning);

            if (res == MessageBoxResult.OK)
            {
                Sql.Delete(CurrentTour);
                ObColl = new PagingObservableCollection<Tour>(Sql.GetTable<Tour>());
                OnPropertyChanged(nameof(ObColl));
            }
        });
    }
}
