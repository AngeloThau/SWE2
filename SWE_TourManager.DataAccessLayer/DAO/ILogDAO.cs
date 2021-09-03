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
        LogItem AddNewLogItem(string report, TourItem logTourItem);
        IEnumerable<LogItem> GetLogForTourItem(TourItem tour);
    }
}
