using SWE_TourManager.BusinessLayer;
using SWE_TourManager.DataAccessLayer;
using SWE_TourManager.DataAccessLayer.Common;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SWE_TourManager.ViewModels.Commands;

namespace SWE_TourManager.ViewModels
{
    public class CopyTourVM : ViewModelBase
    {
        private Validator validator = new Validator();
        private string newTourName;
        private string newTourStart;
        private string newTourDestination;
        private string newTourDescription;
        private double newTourDistance;
        public HTTPRequest httpRequest;
        public HTTPResponseHandler httpResponse;
        public TourImgHandler imageHandler;
        private ITourManagerFactory tourManagerFactory;        

        public CopyTourVM(TourItem tour)
        {
            tourManagerFactory = TourManagerFactory.GetInstance();
            httpRequest = new HTTPRequest();
            httpResponse = new HTTPResponseHandler();
            imageHandler = new TourImgHandler();           
            newTourName = tour.Name;
            newTourStart = tour.Start;
            newTourDestination = tour.Destination;
            newTourDescription = tour.Description;
            newTourDistance = tour.Distance;
        }


        public string NewTourName
        {
            get
            {
                return newTourName;
            }
            set
            {
                if (newTourName != value)
                {
                    newTourName = value;
                    RaisePropertyChangedEvent(nameof(NewTourName));
                }
            }
        }

        public string NewTourDescription
        {
            get
            {
                return newTourDescription;
            }
            set
            {
                if (newTourDescription != value)
                {
                    newTourDescription = value;
                    RaisePropertyChangedEvent(nameof(NewTourDescription));
                }
            }
        }

        public string NewTourStart
        {
            get
            {
                return newTourStart;
            }
            set
            {
                if (newTourStart != value)
                {
                    newTourStart = value;
                    RaisePropertyChangedEvent(nameof(NewTourStart));
                }
            }
        }
        public string NewTourDestination
        {
            get
            {
                return newTourDestination;
            }
            set
            {
                if (newTourDestination != value)
                {
                    newTourDestination = value;
                    RaisePropertyChangedEvent(nameof(NewTourDestination));
                }
            }
        }

        public double NewTourDistance
        {
            get
            {
                return newTourDistance;
            }
            set
            {
                if (newTourDistance != value)
                {
                    newTourDistance = value;
                    RaisePropertyChangedEvent(nameof(NewTourDistance));
                }
            }
        }

        private void CopyTour(object commandParameter)
        {
            if (NewTourStart == null || NewTourStart == "" || NewTourDestination == null || NewTourDestination == "" || NewTourDescription == null || NewTourDescription == "" || NewTourName == null || NewTourName == "")
            {
                MessageBox.Show("Missing Parameters - please Fill out everything");
                return;
            }

            else if (!validator.IsAllowedInput(NewTourName) || !validator.TourNameDoesNotExist(NewTourName))
            {
                MessageBox.Show("Please Use a different Tour Name");
                return;
            }
            else if (!validator.IsAlphabetOrNumber(NewTourStart))
            {
                MessageBox.Show("Input at Start not allowed");
                return;
            }
            else if (!validator.IsAlphabetOrNumber(NewTourDestination))
            {
                MessageBox.Show("Input at Destination not allowed");
                return;
            }
            else if (!validator.IsAllowedInput(NewTourDescription))
            {
                MessageBox.Show("Input at Description not allowed");
                return;
            }

            string response = httpRequest.GetJsonResponse(NewTourStart, NewTourDestination);
            httpResponse.setJson(response);
            string imgPath = imageHandler.GetImgPath(httpResponse.GetImgData(), NewTourName);
            NewTourDistance = double.Parse(httpResponse.GetImgData().distance);

            tourManagerFactory.CreateTour(NewTourName, NewTourDescription, NewTourDistance, NewTourStart, NewTourDestination, imgPath);
            MessageBox.Show("Tour Created!");
            ClearAll();

        }

        public void ClearAll()
        {
            NewTourName = "";
            NewTourDescription = "";
            NewTourDistance = 0;
            NewTourStart = "";
            NewTourDestination = "";
        }

        private ICommand createTourCommand;
        public ICommand CreateTourCommand => createTourCommand ??= new RelayCommand(CopyTour);
       
    }
}
