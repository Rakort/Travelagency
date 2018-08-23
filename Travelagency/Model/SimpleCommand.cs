using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Travelagency
{
    public class SimpleCommand : ICommand
    {
        public Boolean CommandSucceeded { get; set; }

        public Predicate<object> CanExecuteDelegate { get; set; }

        public Action<object> ExecuteDelegate { get; set; }

        public bool IsExecute { get; set; }

        public SimpleCommand(Action p_action, Predicate<object> canExecute = null)
        {
            ExecuteDelegate = o => p_action();
            IsExecute = true;
            CanExecuteDelegate = canExecute;
        }
        public SimpleCommand(Action<object> p_action, Predicate<object> canExecute = null)
        {
            ExecuteDelegate = p_action;
            IsExecute = true;
            CanExecuteDelegate = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate?.Invoke(parameter) ?? true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        [DebuggerStepThrough]
        public void Execute(object parameter)
        {
            //if (!IsExecute) return;
            if (ExecuteDelegate != null)
            {
                ExecuteDelegate(parameter);
                CommandSucceeded = true;
            }
        }

    }
}
