using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SWE_TourManager.ViewModels.Commands;
using SWE_TourManager.BusinessLayer;

namespace SWE_TourManager.ViewModels
{
    public class CreateLogVM : ViewModelBase
    {
        private ITourManagerFactory tourManagerFactory;
        private TourItem currentTour;
        private string NewLogName;
        private double NewLogDistance;
        private int NewLogTime;
        private int NewLogRating;
        private int NewLogSpeed;
        private int NewLogVerUp;
        private int NewLogVerDown;
        private int NewLogDiff;
        private DateTime NewLogDate;
        private string NewLogReport;

        private ICommand cancelCommand;       
        private ICommand createLogCommand;
        public ICommand CreateLogCommand => createLogCommand ??= new RelayCommand(CreateLog);
        public ICommand CancelCommand => cancelCommand ??= new RelayCommand(Cancel);
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
        public CreateLogVM(TourItem tour)
        {
            CurrentTour = tour;
        }

       

        private void CreateLog(object commandParameter)
        {
            LogItem generatedLog = tourManagerFactory.CreateLog(CurrentTour, NewLogName, NewLogDistance, NewLogTime, NewLogRating, NewLogSpeed, NewLogVerUp, NewLogVerDown, NewLogDiff, NewLogDate, NewLogReport );
            
        }


    }
}
