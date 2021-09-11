using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SWE_TourManager.BusinessLayer;
using SWE_TourManager.ViewModels.Commands;

namespace SWE_TourManager.ViewModels
{
    class ImportTourVM : ViewModelBase
    {
        private string importTourName;
        private ITourManagerFactory tourManagerFactory;

        public ImportTourVM()
        {
            tourManagerFactory = TourManagerFactory.GetInstance();
        }

        public string ImportTourName
        {
            get
            {
                return importTourName;
            }
            set
            {
                if (importTourName != value)
                {
                    importTourName = value;
                    RaisePropertyChangedEvent(nameof(ImportTourName));
                }
            }
        }

        private ICommand importTourCommand;
        public ICommand ImportTourCommand => importTourCommand ??= new RelayCommand(ImportTour);

        private void ImportTour(object commandParameter)
        {
            tourManagerFactory.ImportTour(importTourName);

            MessageBox.Show("Tour has been sucessfully Imported!");
        }
    }
}
