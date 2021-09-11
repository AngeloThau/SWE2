﻿using System;
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
using System.Windows;

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
        }

        private ICommand createLogCommand;
        public ICommand CreateLogCommand => createLogCommand ??= new RelayCommand(CreateLog);

        private void CreateLog(object commandParameter)
        {
            CreateLog window = new CreateLog(CurrentTour);
            window.Show();           
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

        private ICommand deleteTourCommand;
        public ICommand DeleteTourCommand => deleteTourCommand ??= new RelayCommand(DeleteTour);

        private void DeleteTour(object commandParameter)
        {
            tourManagerFactory.DeleteTour(currentTour);
            MessageBox.Show("Tour has been Deleted");
            LogItems.Clear();
            TourItems.Clear();
            TourListFill();
        }

        private ICommand modifyTourCommand;
        public ICommand ModifyTourCommand => modifyTourCommand ??= new RelayCommand(ModifyTour);

        private void ModifyTour(object commandParameter)
        {
            ModifyTour window = new ModifyTour(CurrentTour);
            window.Show();
        }

        private ICommand copyTourCommand;
        public ICommand CopyTourCommand => copyTourCommand ??= new RelayCommand(CopyTour);

        private void CopyTour(object commandParameter)
        {
            CopyTour window = new CopyTour(CurrentTour);
            window.Show();
        }

        private ICommand exportTourCommand;
        public ICommand ExportTourCommand => exportTourCommand ??= new RelayCommand(ExportTour);

        private void ExportTour(object commandParameter)
        {
            tourManagerFactory.ExportTour(CurrentTour);
            MessageBox.Show("Tour has been Exported");
        }

        private ICommand importTourCommand;
        public ICommand ImportTourCommand => importTourCommand ??= new RelayCommand(ImportTour);

        private void ImportTour(object commandParameter)
        {
            ImportTour window = new ImportTour();
            window.Show();
        }
    }
}
