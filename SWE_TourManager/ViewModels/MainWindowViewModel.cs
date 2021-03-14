using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;

namespace SWE_TourManager.ViewModels
{
    class MainWindowViewModel 
    {

        private ICommand textboxUpdate;
        private ICommand button;

        private class Command : ICommand
        {
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
}
