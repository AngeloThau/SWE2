using SWE_TourManager.DataAccessLayer;
using SWE_TourManager.DataAccessLayer.Common;
using SWE_TourManager.DataAccessLayer.DAO;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.BusinessLayer
{
    internal class TourManagerFactoryImpl : ITourManagerFactory
    {
      
        
        public IEnumerable<TourItem> GetTourItems()
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.GetTourItems();
        }

        public IEnumerable<TourItem> SearchTours(string tourName, bool caseSensitive = false)
        {
            IEnumerable<TourItem> tourItem = GetTourItems();

            if (caseSensitive)
            {
                return tourItem.Where(x => x.Name.Contains(tourName));
            }
            return tourItem.Where(x => x.Name.ToLower().Contains(tourName.ToLower()));
        }

        public TourItem CreateTour(string name, string description, double distance, string start, string destination, string imgPath)
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.AddNewItem(name, description, distance, start, destination, imgPath);
        }

        public LogItem CreateLog(TourItem logTourItem, string logName, double logDistance, int logTime, int logRating, int logSpeed, int logVerUp, int logVerDown, int logDiff, DateTime logDate, string logReport)
        {
            ILogDAO logDAO = DALFactory.CreateLogDAO();
            return logDAO.AddNewLogItem(logTourItem, logName, logDistance, logTime, logRating, logSpeed, logVerUp, logVerDown, logDiff, logDate, logReport);
        }

        public IEnumerable<LogItem> GetLogForTourItem(TourItem tour)
        {
            ILogDAO logDAO = DALFactory.CreateLogDAO();
            return logDAO.GetLogForTourItem(tour);
        }

        public IEnumerable<LogItem> SearchLogs(TourItem tour, string logName, bool caseSensitive = false)
        {
            IEnumerable<LogItem> logItem = GetLogForTourItem(tour);

            if (caseSensitive)
            {
                return logItem.Where(x => x.LogName.Contains(logName));
            }
            return logItem.Where(x => x.LogName.ToLower().Contains(logName.ToLower()));
        }

        public TourItem ModifyTour(string name, string description, int id)
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.ModifyTourItem(name, description, id);
        }

        public void DeleteTour(TourItem tour)
        {
            ILogDAO logDAO = DALFactory.CreateLogDAO();
            ITourDAO tourDAO = DALFactory.CreateTourDAO();

            logDAO.DeleteTourLogs(tour);
            tourDAO.DeleteTourItem(tour.Id);
        }
    }
}
