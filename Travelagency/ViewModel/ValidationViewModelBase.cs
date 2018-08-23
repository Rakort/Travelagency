using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Travelagency.Annotations;

namespace Travelagency.ViewModel
{
    public class ValidationViewModelBase : ViewModelBase, IDataErrorInfo 
    {
        private readonly Dictionary<string, Binder> _ruleMap = new Dictionary<string, Binder>();

        public void AddRule<T>(Expression<Func<T>> expression, Func<bool> ruleDelegate, string errorMessage)
        {
            var name = GetPropertyName(expression);

            _ruleMap.Add(name, new Binder(ruleDelegate, errorMessage));
        }

        protected virtual void Set<T>(Expression<Func<T>> path, T value, bool forceUpdate = false)
        {
            _ruleMap[GetPropertyName(path)].IsDirty = true;
            var oldValue = Get(path);
            var propertyName = GetPropertyName(path);

            if (!object.Equals(value, oldValue) || forceUpdate)
            {
                _propertyValueMap[propertyName] = value;
                OnPropertyChanged(GetPropertyName(path));
            }
        }
        public bool HasErrors
        {
            get
            {
                var values = _ruleMap.Values.ToList();
                values.ForEach(b => b.Update());

                return values.Any(b => b.HasError);
            }
        }

        public string Error
        {
            get
            {
                var errors = from b in _ruleMap.Values where b.HasError select b.Error;

                return string.Join("\n", errors);
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (_ruleMap.ContainsKey(columnName))
                {
                    _ruleMap[columnName].Update();
                    return _ruleMap[columnName].Error;
                }
                return null;
            }
        }
        private Dictionary<string, object> _propertyValueMap = new Dictionary<string, object>();
        protected static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            Expression body = expression.Body;
            MemberExpression memberExpression = body as MemberExpression ?? (MemberExpression)((UnaryExpression)body).Operand;
            return memberExpression.Member.Name;
        }
        protected virtual T Get<T>(Expression<Func<T>> path, T defaultValue = default(T))
        {
            var propertyName = GetPropertyName(path);
            if (_propertyValueMap.ContainsKey(propertyName))
            {
                return (T)_propertyValueMap[propertyName];
            }
            else
            {
                _propertyValueMap.Add(propertyName, defaultValue);
                return defaultValue;
            }
        }


        

        private class Binder
        {
            private readonly Func<bool> _ruleDelegate;
            private readonly string _message;

            internal Binder(Func<bool> ruleDelegate, string message)
            {
                this._ruleDelegate = ruleDelegate;
                this._message = message;

                IsDirty = true;
            }

            internal string Error { get; set; }
            internal bool HasError { get; set; }

            internal bool IsDirty { get; set; }

            internal void Update()
            {
                if (!IsDirty)
                    return;

                Error = null;
                HasError = false;
                try
                {
                    if (!_ruleDelegate())
                    {
                        Error = _message;
                        HasError = true;
                    }
                }
                catch (Exception e)
                {
                    Error = e.Message;
                    HasError = true;
                }
            }
        }
    }
}
