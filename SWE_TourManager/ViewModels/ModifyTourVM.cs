using SWE_TourManager.BusinessLayer;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SWE_TourManager.ViewModels.Commands;
using System.Windows;

namespace SWE_TourManager.ViewModels
{
    public class ModifyTourVM : ViewModelBase
    {

        private string modifyTourName;       
        private string modifyTourDescription;               
        private ITourManagerFactory tourManagerFactory;
        private TourItem currentTour;

        public ModifyTourVM(TourItem tour)
        {
            tourManagerFactory = TourManagerFactory.GetInstance();           
            currentTour = tour;
            modifyTourDescription = tour.Description;
            modifyTourName = tour.Name;
        }

        public TourItem CurrentTour
        {
            get
            {
                return currentTour;
            }
            set
            {
                if (currentTour != value)
                {
                    currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                }
            }
        }

        public string ModifyTourName
        {
            get
            {
                return modifyTourName;
            }
            set
            {
                if (modifyTourName != value)
                {
                    modifyTourName = value;
                    RaisePropertyChangedEvent(nameof(ModifyTourName));
                }
            }
        }

        public string ModifyTourDescription
        {
            get
            {
                return modifyTourDescription;
            }
            set
            {
                if (modifyTourDescription != value)
                {
                    modifyTourDescription = value;
                    RaisePropertyChangedEvent(nameof(ModifyTourDescription));
                }
            }
        }

        private ICommand modifyTourCommand;
        public ICommand ModifyTourCommand => modifyTourCommand ??= new RelayCommand(ModifyTour);

        private void ModifyTour(object commandParameter)
        {
            Validator validator = new Validator();

            if (!validator.IsAllowedInput(ModifyTourName) || !validator.TourNameDoesNotExist(ModifyTourName))
            {
                MessageBox.Show("Input at Tour Name not allowed (maybe Name is already used)");
                return;
            }
            else if (!validator.IsNumber(ModifyTourDescription))
            {
                MessageBox.Show("Input at Description not allowed");
                return;
            }
            
            tourManagerFactory.ModifyTour(ModifyTourName, ModifyTourDescription, currentTour.Id);
            MessageBox.Show("Tour has been Modified");
        }

    }
}
