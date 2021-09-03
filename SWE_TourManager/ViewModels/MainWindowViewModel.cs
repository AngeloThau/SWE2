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

namespace SWE_TourManager.ViewModels
{
    public class MainWindowViewModel :ViewModelBase
    {

        private ITourItemFactory tourItemFactory;
        private TourItem currentTour;
        private string searchTourName;
        private ICommand searchTourCommand;
        private ICommand clearTourCommand;
        public ICommand SearchTourCommand => searchTourCommand ??= new RelayCommand(SearchTour);
        public ICommand ClearTourCommand => clearTourCommand ??= new RelayCommand(ClearTour);
        public ObservableCollection<TourItem> TourItems { get; set; }
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
            this.tourItemFactory = TourItemFactory.GetInstance();
            InitTourList();
            TourListFill();
        }

        private void InitTourList()
        {
            TourItems = new ObservableCollection<TourItem>();
        }

        private void TourListFill()
        {
            foreach (TourItem tour in this.tourItemFactory.GetTourItems())
            {
                TourItems.Add(tour);
            }
        }

        private void SearchTour(object commandParameter)
        {
            IEnumerable foundTourItems = this.tourItemFactory.SearchTours(SearchTourName);
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

        
    }
}
