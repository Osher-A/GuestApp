using System;
using System.Windows.Input;

namespace GuestApp.Utility
{
    public class CustomCommand : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;
        private object canSelecteEvent;
        private ICommand selectedEventCommand;

        public CustomCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public CustomCommand(object canSelecteEvent, ICommand selectedEventCommand)
        {
            this.canSelecteEvent = canSelecteEvent;
            this.selectedEventCommand = selectedEventCommand;
        }

        public CustomCommand(Action<object> canSelecteEvent, ICommand selectedEventCommand)
        {
            this.canSelecteEvent = canSelecteEvent;
            this.selectedEventCommand = selectedEventCommand;
        }

        public bool CanExecute(object parameter)
        {
            bool b = canExecute == null ? true : canExecute(parameter);
            return b;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}