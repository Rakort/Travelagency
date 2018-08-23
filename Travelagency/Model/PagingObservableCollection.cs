using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Travelagency.Model
{
    public class PagingObservableCollection<T> : ObservableCollection<T>
    {
        private readonly IList<T> _innerList;
        public IList<T> ItemsList => _innerList;
        private IList<T> InnerList => Filter != null ? _innerList.Where(w => Filter(w)).ToList() : _innerList;

        private int _itemsPerPage;

        
        private Func<object, bool> _filter;
        public Func<object, bool> Filter
        {
            get { return _filter; }
            set
            {
                if(value == null) return;
                if(value == _filter) return;
                _filter = value;
                Refresh();
            }
        }

        private int _currentPage = 1;
        
        public PagingObservableCollection(IList<T> innerList, int itemsPerPage = 10)
            : base(innerList)
        {
            _innerList = innerList;
            this._itemsPerPage = itemsPerPage;
        }
        
        public int CurrentPage
        {
            get { return this._currentPage; }
            set
            {
                int res;
                if (!int.TryParse(value.ToString(), out res)) return;
                if (res < 0) _currentPage = 0;
                if (res > PageCount) _currentPage = PageCount;
                this._currentPage = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));
            }
        }

        public int ItemsPerPage
        {
            get { return this._itemsPerPage; }
            set
            {
                int res;
                if (!int.TryParse(value.ToString(), out res)) return;
                if (res < 0) return;
                _itemsPerPage = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("ItemsPerPage"));
                CurrentPage = 1;
                Refresh();
            }
        }

        public ICommand SetItemsPerPage => new SimpleCommand((val) => ItemsPerPage = int.Parse(val.ToString()));

        public int PageCount => (this.InnerList.Count + this._itemsPerPage - 1)
                                / this._itemsPerPage;

        private int EndIndex
        {
            get
            {
                var end = this._currentPage * this._itemsPerPage - 1;
                return (end > this.InnerList.Count) ? this.InnerList.Count-1 : end;
            }
        }

        private int StartIndex => (this._currentPage - 1) * this._itemsPerPage;

        public ICommand MoveToNextPageCommand => new SimpleCommand(MoveToNextPage);

        public void MoveToNextPage()
        {
            if (this._currentPage < this.PageCount)
            {
                this.CurrentPage += 1;
            }
            this.Refresh();
        }

        public ICommand MoveToPreviousPageCommand => new SimpleCommand(MoveToPreviousPage);
        public void MoveToPreviousPage()
        {
            if (this._currentPage > 1)
            {
                this.CurrentPage -= 1;
            }
            this.Refresh();
        }
        public ICommand MoveToFirstPageCommand => new SimpleCommand(MoveToFirstPage);
        public void MoveToFirstPage()
        {
            this.CurrentPage = 1;
            this.Refresh();
        }
        public ICommand MoveToLastPageCommand => new SimpleCommand(MoveToLastPage);
        public void MoveToLastPage()
        {
            this.CurrentPage = PageCount;
            this.Refresh();
        }

        public void Refresh()
        {
            base.ClearItems();
            if (InnerList.Count == 0) return;
            
            for (int i = StartIndex; i <= EndIndex; i++)
                {
                    base.Add(InnerList[i]);
                }
        }
    }
}
