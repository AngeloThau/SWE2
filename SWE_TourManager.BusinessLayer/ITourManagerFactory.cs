using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.BusinessLayer
{
    public interface ITourManagerFactory
    {
        IEnumerable<TourItem> GetTourItems();
        IEnumerable<LogItem> GetLogForTourItem(TourItem tour);
        IEnumerable<TourItem> SearchTours(string tourName, bool caseSensitive = false);
        IEnumerable<LogItem> SearchLogs(TourItem tour, string logName, bool caseSensitive = false);
        public TourItem CreateTour(string name, string description, double distance, string start, string destination, string imgPath);
        public LogItem CreateLog(TourItem logTourItem, string logName, double logDistance, int logTime, int logRating, int logSpeed, int logVerUp, int logVerDown, int logDiff, DateTime logDate, string logReport);
        public LogItem ModifyLog(LogItem logItem, string logName, double logDistance, int logTime, int logRating, int logSpeed, int logVerUp, int logVerDown, int logDiff, DateTime logDate, string logReport);
        public TourItem ModifyTour(string name, string description, int id);
        public TourItem ImportTour(string tourName);
        public void ExportTour(TourItem tour);
        public int DeleteTour(TourItem tour);
        public int DeleteLog(LogItem log);
        public int PrintTour(TourItem tour);
        public string summarizeReport();
    }
}
