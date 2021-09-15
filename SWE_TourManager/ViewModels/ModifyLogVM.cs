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
    public class ModifyLogVM : ViewModelBase   
    {
        private ITourManagerFactory tourManagerFactory;
        private LogItem currentLog;
        private string modifyLogName;
        private double modifyLogDistance;
        private int modifyLogTime;
        private int modifyLogRating;
        private int modifyLogSpeed;
        private int modifyLogVerUp;
        private int modifyLogVerDown;
        private int modifyLogDiff;
        private DateTime modifyLogDate = DateTime.Now;
        private string modifyLogReport;
       

        public ModifyLogVM(LogItem log)
        {
            this.currentLog = log;
            this.tourManagerFactory = TourManagerFactory.GetInstance();
            this.modifyLogName = log.LogName;
            this.modifyLogDistance = log.LogDistance;
            this.modifyLogTime = log.LogTime;
            this.modifyLogRating = log.LogRating;
            this.modifyLogSpeed = log.LogSpeed;
            this.modifyLogVerUp = log.LogVerUp;
            this.modifyLogVerDown = log.LogVerDown;
            this.modifyLogDiff = log.LogDiff;
            this.modifyLogReport = log.LogReport;
        }

        public LogItem CurrentLog
        {
            get
            {
                return currentLog;
            }
            set
            {
                if (currentLog != value)
                {
                    currentLog = value;
                    RaisePropertyChangedEvent(nameof(CurrentLog));
                }
            }
        }
        public DateTime ModifyLogDate
        {
            get
            {
                return modifyLogDate;
            }
            set
            {
                if (modifyLogDate != value)
                {
                    modifyLogDate = value;
                    RaisePropertyChangedEvent(nameof(ModifyLogDate));
                }
            }
        }

        public string ModifyLogName
        {
            get
            {
                return modifyLogName;
            }
            set
            {
                if (modifyLogName != value)
                {
                    modifyLogName = value;
                    RaisePropertyChangedEvent(nameof(ModifyLogName));
                }
            }
        }

        public double ModifyLogDistance
        {
            get
            {
                return modifyLogDistance;
            }
            set
            {
                if (modifyLogDistance != value)
                {
                    modifyLogDistance = value;
                    RaisePropertyChangedEvent(nameof(ModifyLogDistance));
                }
            }
        }
        public int ModifyLogTime
        {
            get
            {
                return modifyLogTime;
            }
            set
            {
                if (modifyLogTime != value)
                {
                    modifyLogTime = value;
                    RaisePropertyChangedEvent(nameof(ModifyLogTime));
                }
            }
        }

        public int ModifyLogRating
        {
            get
            {
                return modifyLogRating;
            }
            set
            {
                if (modifyLogRating != value)
                {
                    modifyLogRating = value;
                    RaisePropertyChangedEvent(nameof(ModifyLogRating));
                }
            }
        }

        public int ModifyLogSpeed
        {
            get
            {
                return modifyLogSpeed;
            }
            set
            {
                if (modifyLogSpeed != value)
                {
                    modifyLogSpeed = value;
                    RaisePropertyChangedEvent(nameof(ModifyLogSpeed));
                }
            }
        }

        public int ModifyLogVerUp
        {
            get
            {
                return modifyLogVerUp;
            }
            set
            {
                if (modifyLogVerUp != value)
                {
                    modifyLogVerUp = value;
                    RaisePropertyChangedEvent(nameof(ModifyLogVerUp));
                }
            }
        }

        public int ModifyLogDiff
        {
            get
            {
                return modifyLogDiff;
            }
            set
            {
                if (modifyLogDiff != value)
                {
                    modifyLogDiff = value;
                    RaisePropertyChangedEvent(nameof(ModifyLogDiff));
                }
            }
        }
        public int ModifyLogVerDown
        {
            get
            {
                return modifyLogVerDown;
            }
            set
            {
                if (modifyLogVerDown != value)
                {
                    modifyLogVerDown = value;
                    RaisePropertyChangedEvent(nameof(ModifyLogVerDown));
                }
            }
        }

        public string ModifyLogReport
        {
            get
            {
                return modifyLogReport;
            }
            set
            {
                if (modifyLogReport != value)
                {
                    modifyLogReport = value;
                    RaisePropertyChangedEvent(nameof(ModifyLogReport));
                }
            }
        }

        private ICommand modifyLogCommand;
        public ICommand ModifyLogCommand => modifyLogCommand ??= new RelayCommand(ModifyLog);

        private void ModifyLog(object commandParameter)
        {
            Validator validator = new Validator();

            if (!validator.IsAllowedInput(ModifyLogName))
            {
                MessageBox.Show("Input at Log Name not allowed");
                return;
            }
            else if (!validator.IsNumber(ModifyLogTime.ToString()))
            {
                MessageBox.Show("Input at Time not allowed");
                return;
            }
            else if (!validator.IsNumber(ModifyLogSpeed.ToString()))
            {
                MessageBox.Show("Input at Speed not allowed");
                return;
            }
            else if (!validator.IsNumber(ModifyLogVerUp.ToString()))
            {
                MessageBox.Show("Input at Ver Up not allowed");
                return;
            }
            else if (!validator.IsNumber(ModifyLogVerDown.ToString()))
            {
                MessageBox.Show("Input at Ver Down not allowed");
                return;
            }
            else if (!validator.IsAllowedInput(ModifyLogReport))
            {
                MessageBox.Show("Input at Report not allowed");
                return;
            }
            tourManagerFactory.ModifyLog(CurrentLog, ModifyLogName, ModifyLogDistance, ModifyLogTime, ModifyLogRating, ModifyLogSpeed, ModifyLogVerUp, ModifyLogVerDown, ModifyLogDiff, ModifyLogDate, ModifyLogReport);
            MessageBox.Show("Log sucessfully Modified");
        }

    }
}

