using System.Windows.Input;

namespace BounceBall.Command
{
    public class RelayCommand : ICommand
    {

        public event EventHandler? CanExecuteChanged;

        private Action<object> _execute { get; set; }
        private Predicate<object> _canExecute { get; set; }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
