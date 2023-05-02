using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IntelligentControl.Base
{
    internal class CommandBase : ICommand
    {
        
        public CommandBase() { }

        public CommandBase(Action<object?> action) { 
            this.ExecuteDelegate = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ExecuteDelegate?.Invoke(parameter);
        }

        public Action<object?>? ExecuteDelegate { set; get; }

    }
}
