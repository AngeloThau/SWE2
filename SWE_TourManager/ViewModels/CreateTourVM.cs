using SWE_TourManager.BusinessLayer;
using SWE_TourManager.DataAccessLayer;
using SWE_TourManager.DataAccessLayer.Common;
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
    public class CreateTourVM : ViewModelBase  
    {
        private string newTourName;
        private string newTourStart;
        private string newTourDestination;
        private string newTourDescription;
        private double newTourDistance;
        public HTTPRequest httpRequest;
        public HTTPResponseHandler httpResponse;
        public TourImgHandler imageHandler;
        private ITourManagerFactory tourManagerFactory;

        private ICommand createTourCommand;
        public ICommand CreateTourCommand => createTourCommand ??= new RelayCommand(CreateTour);

        public CreateTourVM()
        {
            tourManagerFactory = TourManagerFactory.GetInstance();
            httpRequest = new HTTPRequest();
            httpResponse = new HTTPResponseHandler();
            imageHandler = new TourImgHandler();
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

        private void CreateTour(object commandParameter)
        {
            if (NewTourStart == null || NewTourStart == "" || NewTourDestination == null || NewTourDestination == "" || NewTourDescription == null || NewTourDescription == "" || NewTourName == null || NewTourName == "")
            {
                MessageBox.Show("Missing Parameters - please Fill out everything");               
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
    }
}
