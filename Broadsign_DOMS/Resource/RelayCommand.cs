using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Broadsign_DOMS.Resource
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        public RelayCommand(Action<object> _execute)
        {
            if (_execute == null)
                throw new ArgumentNullException("execute");
            execute = _execute;
        }
        public RelayCommand(Action<object> _execute, Predicate<object> _canExecute)
        {
            if (_execute == null)
                throw new ArgumentNullException("");
            this.execute = _execute;
            this.canExecute = _canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if(canExecute == null)
                return true;
            return CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
