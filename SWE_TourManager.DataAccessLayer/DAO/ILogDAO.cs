using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.DAO
{
    public interface ILogDAO
    {
        LogItem FindById(int logId);
        LogItem AddNewLogItem(TourItem logTourItem, string logName, double logDistance, int logTime, int logRating, int logSpeed, int logVerUp, int logVerDown, int logDiff, DateTime logDate, string logReport);
        int DeleteTourLogs(TourItem tour);
        IEnumerable<LogItem> GetLogForTourItem(TourItem tour);
    }
}
