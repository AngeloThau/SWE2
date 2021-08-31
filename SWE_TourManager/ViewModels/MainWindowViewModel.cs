using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using SWE_TourManager.ViewModels.Commands;

namespace SWE_TourManager.ViewModels
{
    public class MainWindowViewModel :ViewModelBase
    {

        private ICommand searchCommand;
        private ICommand clearCommand;
        public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);
        public ICommand ClearCommand => clearCommand ??= new RelayCommand(Clear);

        public MainWindowViewModel()
        {

        }


        private void Search(object commandParameter)
        {
        }

        private void Clear(object commandParameter)
        {
        }

    }
}
