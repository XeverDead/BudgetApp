using System;
using System.Windows.Input;

namespace UI
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _condition;

        public RelayCommand(Action<object> action, Func<object, bool> condition)
        {
            _action = action;
            _condition = condition;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _condition == null || _condition(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
