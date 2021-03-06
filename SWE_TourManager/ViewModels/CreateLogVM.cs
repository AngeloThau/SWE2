using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SWE_TourManager.ViewModels.Commands;
using SWE_TourManager.BusinessLayer;
using System.Windows;

namespace SWE_TourManager.ViewModels
{
    public class CreateLogVM : ViewModelBase
    {
        private ITourManagerFactory tourManagerFactory;
        private TourItem currentTour;
        private string newLogName;
        private double newLogDistance;
        private int newLogTime;
        private int newLogRating;
        private int newLogSpeed;
        private int newLogVerUp;
        private int newLogVerDown;
        private int newLogDiff;
        private DateTime newLogDate = DateTime.Now;
        private string newLogReport;
        private RandomTourBuilder randi;
      
        private ICommand createLogCommand;
        public ICommand CreateLogCommand => createLogCommand ??= new RelayCommand(CreateLog);

        public CreateLogVM(TourItem tour)
        {
            this.currentTour = tour;
            this.tourManagerFactory = TourManagerFactory.GetInstance();
            randi = new RandomTourBuilder();
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
        public DateTime NewLogDate
        {
            get
            {
                return newLogDate;
            }
            set
            {
                if (newLogDate != value)
                {
                    newLogDate = value;
                    RaisePropertyChangedEvent(nameof(NewLogDate));
                }
            }
        }

        public string NewLogName
        {
            get
            {
                return newLogName;
            }
            set
            {
                if (newLogName != value)
                {
                    newLogName = value;
                    RaisePropertyChangedEvent(nameof(NewLogName));
                }
            }
        }

        public double NewLogDistance
        {
            get
            {
                return newLogDistance;
            }
            set
            {
                if (newLogDistance != value)
                {
                    newLogDistance = value;
                    RaisePropertyChangedEvent(nameof(NewLogDistance));
                }
            }
        }
        public int NewLogTime
        {
            get
            {
                return newLogTime;
            }
            set
            {
                if (newLogTime != value)
                {
                    newLogTime = value;
                    RaisePropertyChangedEvent(nameof(NewLogTime));
                }
            }
        }

        public int NewLogRating
        {
            get
            {
                return newLogRating;
            }
            set
            {
                if (newLogRating != value)
                {
                    newLogRating = value;
                    RaisePropertyChangedEvent(nameof(NewLogRating));
                }
            }
        }

        public int NewLogSpeed
        {
            get
            {
                return newLogSpeed;
            }
            set
            {
                if (newLogSpeed != value)
                {
                    newLogSpeed = value;
                    RaisePropertyChangedEvent(nameof(NewLogSpeed));
                }
            }
        }

        public int NewLogVerUp
        {
            get
            {
                return newLogVerUp;
            }
            set
            {
                if (newLogVerUp != value)
                {
                    newLogVerUp = value;
                    RaisePropertyChangedEvent(nameof(NewLogVerUp));
                }
            }
        }

        public int NewLogDiff
        {
            get
            {
                return newLogDiff;
            }
            set
            {
                if (newLogDiff != value)
                {
                    newLogDiff = value;
                    RaisePropertyChangedEvent(nameof(NewLogDiff));
                }
            }
        }
        public int NewLogVerDown
        {
            get
            {
                return newLogVerDown;
            }
            set
            {
                if (newLogVerDown != value)
                {
                    newLogVerDown = value;
                    RaisePropertyChangedEvent(nameof(NewLogVerDown));
                }
            }
        }

        public string NewLogReport
        {
            get
            {
                return newLogReport;
            }
            set
            {
                if (newLogReport != value)
                {
                    newLogReport = value;
                    RaisePropertyChangedEvent(nameof(NewLogReport));
                }
            }
        }

       
        private void CreateLog(object commandParameter)
        {

            Validator validator = new Validator();

            if (NewLogName == null || NewLogReport == null || NewLogName == "" || NewLogReport == "")
            {
                MessageBox.Show("Parameter Missing, Report and Name Cannot be empty");
                return;
            }
            else if (!validator.IsAllowedInput(NewLogName))
            {
                MessageBox.Show("Input at Log Name not allowed");
                return;
            }
            else if (!validator.IsNumber(NewLogTime.ToString()))
            {
                MessageBox.Show("Input at Time not allowed");
                return;
            }
            else if (!validator.IsNumber(NewLogSpeed.ToString()))
            {
                MessageBox.Show("Input at Speed not allowed");
                return;
            }
            else if (!validator.IsNumber(NewLogVerUp.ToString()))
            {
                MessageBox.Show("Input at Ver Up not allowed");
                return;
            }
            else if (!validator.IsNumber(NewLogVerDown.ToString()))
            {
                MessageBox.Show("Input at Ver Down not allowed");
                return;
            }
            else if (!validator.IsAllowedInput(NewLogReport))
            {
                MessageBox.Show("Input at Report not allowed");
                return;
            }
            LogItem generatedLog = tourManagerFactory.CreateLog(CurrentTour, NewLogName, NewLogDistance, NewLogTime, NewLogRating, NewLogSpeed, NewLogVerUp, NewLogVerDown, NewLogDiff, NewLogDate, NewLogReport);
            MessageBox.Show("Successfully created a new Log with Name " + NewLogName);
            ClearLogWindow();
        }

        public void ClearLogWindow()
        {
            NewLogDistance = 0;
            NewLogTime = 0;
            NewLogName = "";
            NewLogReport = "";
            NewLogRating = 1;
            NewLogDate = DateTime.Now;
            NewLogDiff = 1;
            NewLogSpeed = 0;
            NewLogVerDown = 0;
            NewLogVerUp = 0;           
        }

        private ICommand random;
        public ICommand Random => random ??= new RelayCommand(PerformRandom);

        private void PerformRandom(object commandParameter)
        {
            NewLogDistance = randi.GenerateInt(Convert.ToInt32(currentTour.Distance));
            NewLogTime = randi.GenerateInt(1000);
            NewLogName = randi.GenerateName(10);
            NewLogReport = randi.GenerateName(40);
            NewLogRating = randi.GenerateInt(10);
            NewLogDate = DateTime.Now;
            NewLogDiff = randi.GenerateInt(10);
            NewLogSpeed = randi.GenerateInt(20);
            NewLogVerDown = randi.GenerateInt(100);
            NewLogVerUp = randi.GenerateInt(100);
        }
    }
}
