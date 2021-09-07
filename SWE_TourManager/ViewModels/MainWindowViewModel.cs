using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using SWE_TourManager.ViewModels.Commands;
using System.Collections.ObjectModel;
using SWE_TourManager.Models;
using SWE_TourManager.BusinessLayer;
using System.Collections;
using SWE_TourManager.Views;

namespace SWE_TourManager.ViewModels
{
    public class MainWindowViewModel :ViewModelBase
    {

        private ITourManagerFactory tourManagerFactory;
        private TourItem currentTour;
        private LogItem currentLog;
        private string searchTourName;
        private string searchLogName;
        private ICommand searchTourCommand;
        private ICommand clearTourCommand;
       
        public ObservableCollection<TourItem> TourItems { get; set; }
        public ObservableCollection<LogItem> LogItems { get; set; }
        public ICommand SearchTourCommand => searchTourCommand ??= new RelayCommand(SearchTour);
        public ICommand ClearTourCommand => clearTourCommand ??= new RelayCommand(ClearTour);

        private ICommand searchLogCommand;
        public ICommand SearchLogCommand => searchLogCommand ??= new RelayCommand(SearchLog);
        private ICommand clearLogCommand;
        public ICommand ClearLogCommand => clearLogCommand ??= new RelayCommand(ClearLog);
        public TourItem CurrentTour
        {
            get
            {
                return currentTour;
            }
            set
            {
                if((currentTour != value)&& (value != null))
                {
                    currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                    LogItems.Clear();
                    LogListFill();
                }
            }
        }

        public string SearchTourName
        {
            get { return searchTourName; }
            set
            {
                if((searchTourName != value) && (value != null))
                {
                    searchTourName = value;
                    RaisePropertyChangedEvent(nameof(SearchTourName));
                }
            }
        }

        public MainWindowViewModel()
        {
            this.tourManagerFactory = TourManagerFactory.GetInstance();
            InitTourList();
            TourListFill();
            InitLogList();           
        }

        private void InitTourList()
        {
            TourItems = new ObservableCollection<TourItem>();
        }

        private void TourListFill()
        {
            foreach (TourItem tour in this.tourManagerFactory.GetTourItems())
            {
                TourItems.Add(tour);
            }
        }

        private void SearchTour(object commandParameter)
        {
            IEnumerable foundTourItems = this.tourManagerFactory.SearchTours(SearchTourName);
            TourItems.Clear();
            foreach(TourItem tourItem in foundTourItems)
            {
                TourItems.Add(tourItem);
            }
        }

      
        private void ClearTour(object commandParameter)
        {
            TourItems.Clear();
            SearchTourName = "";

            TourListFill();
        }

        private ICommand newTourCommand;
        public ICommand NewTourCommand => newTourCommand ??= new RelayCommand(NewTour);

        private void NewTour(object commandParameter)
        {
            CreateTour window = new CreateTour();
            window.Show();
            //TourItem generatedItem = tourManagerFactory.CreateTour("GeneratedTour", "Generated Description", 5.0);
            //TourItems.Add(generatedItem);
        }

        private ICommand createLogCommand;
        public ICommand CreateLogCommand => createLogCommand ??= new RelayCommand(CreateLog);

        private void CreateLog(object commandParameter)
        {
            CreateLog window = new CreateLog(CurrentTour);
            window.Show();
            //LogItem generatedLog = tourManagerFactory.CreateLog(CurrentTour, "generatedName", 2.1, 34, 2, 21, 24, 50, 4, DateTime.Now, "Generated Report" );
        }

        public LogItem CurrentLog
        {
            get
            {
                return currentLog;
            }
            set
            {
                if ((currentLog != value) && (value != null))
                {
                    currentLog = value;
                    RaisePropertyChangedEvent(nameof(CurrentLog));
                }
            }
        }

        private void InitLogList()
        {
            LogItems = new ObservableCollection<LogItem>();
        }

        private void LogListFill()
        {
            foreach (LogItem log in this.tourManagerFactory.GetLogForTourItem(CurrentTour))
            {
                LogItems.Add(log);
            }
        }
        public string SearchLogName
        {
            get { return searchLogName; }
            set
            {
                if ((searchLogName != value) && (value != null))
                {
                    searchLogName = value;
                    RaisePropertyChangedEvent(nameof(SearchLogName));
                }
            }
        }
        private void SearchLog(object commandParameter)
        {
            IEnumerable foundLogItems = this.tourManagerFactory.SearchLogs(CurrentTour, SearchLogName);
            LogItems.Clear();
            foreach (LogItem logItem in foundLogItems)
            {
                LogItems.Add(logItem);
            }
        }


        private void ClearLog(object commandParameter)
        {
            LogItems.Clear();
            SearchLogName = "";

            LogListFill();
        }

    }
}
