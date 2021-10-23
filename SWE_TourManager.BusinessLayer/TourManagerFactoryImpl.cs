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
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public IEnumerable<TourItem> GetTourItems()
        {
            logger.Info("BL: Getting Tour Items");
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.GetTourItems();
        }

        public IEnumerable<TourItem> SearchTours(string tourName, bool caseSensitive = false)
        {
            IEnumerable<TourItem> tourItem = GetTourItems();
            logger.Info("BL:Searching Tours");

            if (caseSensitive)
            {
                return tourItem.Where(x => x.Name.Contains(tourName));
            }
            return tourItem.Where(x => x.Name.ToLower().Contains(tourName.ToLower()));
        }

        public TourItem CreateTour(string name, string description, double distance, string start, string destination, string imgPath)
        {
            logger.Info("BL: Creating Tour");
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.AddNewItem(name, description, distance, start, destination, imgPath);
        }

        public LogItem CreateLog(TourItem logTourItem, string logName, double logDistance, int logTime, int logRating, int logSpeed, int logVerUp, int logVerDown, int logDiff, DateTime logDate, string logReport)
        {
            logger.Info("BL: Creating Log");
            ILogDAO logDAO = DALFactory.CreateLogDAO();
            return logDAO.AddNewLogItem(logTourItem, logName, logDistance, logTime, logRating, logSpeed, logVerUp, logVerDown, logDiff, logDate, logReport);
        }

        public IEnumerable<LogItem> GetLogForTourItem(TourItem tour)
        {
            logger.Info("BL: Getting Logs for Tour");
            ILogDAO logDAO = DALFactory.CreateLogDAO();
            return logDAO.GetLogForTourItem(tour);
        }

        public IEnumerable<LogItem> SearchLogs(TourItem tour, string logName, bool caseSensitive = false)
        {
            logger.Info("BL: Searching Logs");
            IEnumerable<LogItem> logItem = GetLogForTourItem(tour);

            if (caseSensitive)
            {
                return logItem.Where(x => x.LogName.Contains(logName));
            }
            return logItem.Where(x => x.LogName.ToLower().Contains(logName.ToLower()));
        }

        public TourItem ModifyTour(string name, string description, int id)
        {
            logger.Info("BL: Modifying Tour");
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.ModifyTourItem(name, description, id);
        }

        public int DeleteTour(TourItem tour)
        {
            logger.Info("BL: Deleting Tour");
            ILogDAO logDAO = DALFactory.CreateLogDAO();
            ITourDAO tourDAO = DALFactory.CreateTourDAO();

            logDAO.DeleteTourLogs(tour);
            return tourDAO.DeleteTourItem(tour.Id);
        }

        public TourItem ImportTour(string tourName)
        {
            logger.Info("BL: Importing Tour");
            TourFileHandler tourFileHandler = new TourFileHandler();
            string [] tourString = tourFileHandler.GetImportTourString(tourName);

            return CreateTour(tourString[0], tourString[1], Convert.ToDouble(tourString[2]), tourString[3], tourString[4], tourString[5]);
        }

        public void ExportTour(TourItem tour)
        {
            logger.Info("BL: Exporting Tour");
            TourFileHandler tourFileHandler = new TourFileHandler();
            tourFileHandler.ExportTour(tour);
        }

        public LogItem ModifyLog(LogItem logItem, string logName, double logDistance, int logTime, int logRating, int logSpeed, int logVerUp, int logVerDown, int logDiff, DateTime logDate, string logReport)
        {
            logger.Info("BL: Modifying Log");
            ILogDAO logDAO = DALFactory.CreateLogDAO();
            return logDAO.UpdateLogItem(logItem.Id, logName, logDistance, logTime, logRating, logSpeed, logVerUp, logVerDown, logDiff, logDate, logReport);
        }

        public int DeleteLog(LogItem log)
        {
            logger.Info("BL: Deleting Log");
            ILogDAO logDAO = DALFactory.CreateLogDAO();
            return logDAO.DeleteLogItem(log.Id);
        }

        public int PrintTour(TourItem tour)
        {
            logger.Info("BL: Print Tour");
            TourFileHandler fileHandler = new TourFileHandler();
            List<LogItem> logItems = GetLogForTourItem(tour).ToList();
            return fileHandler.PrintTour(tour, logItems);
        }

        public string summarizeReport()
        {
            logger.Info("BL: Summarize Report");

            string report;
            int totalTime = 0;
            double totalDistance = 0;

            foreach (TourItem item in GetTourItems())
            {
                foreach (LogItem logItem in GetLogForTourItem(item))
                {
                    totalTime = totalTime + logItem.LogTime;
                    totalDistance = totalDistance + logItem.LogDistance;
                }
            }

            report = "Summarize Report: \n Total Time over All Logs: " + totalTime.ToString() + "\n Total Distance over all Logs: " + totalDistance.ToString();
            return report;
        }
    }
}
