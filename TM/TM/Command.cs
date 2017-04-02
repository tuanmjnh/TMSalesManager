using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TM
{
    public class RelayCommand : ICommand
    {
        private Action commandTask;
        private Action<object> p1;
        private Func<object, bool> p2;

        public RelayCommand(Action workToDo)
        {
            commandTask = workToDo;
        }

        public RelayCommand(Action<object> p1, Func<object, bool> p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
