using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Commands
{
    public abstract class AsyncBaseCommand : BaseCommand
    {
        private bool _isRunning;
        private bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                CanExecute(null);
            }
        }
        public override void Execute(object parameter)
        {
            IsRunning = true;
            ExecuteAsync(parameter);
            IsRunning = false;
        }
        public abstract Task ExecuteAsync(object parameter);
        public override bool CanExecute(object parameter)
        {
            return IsRunning && base.CanExecute(parameter);
        }
    }
}
