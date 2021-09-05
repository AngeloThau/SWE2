using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.Models
{
    public class LogItem
    {
        public int Id { get; set; }
        public TourItem LogTourItem { get; set; }
        public string LogName { get; set; }
        public double LogDistance { get; set; }
        public int LogTime { get; set; }
        public int LogRating { get; set; }
        public int LogSpeed { get; set; }
        public int LogVerUp { get; set; }
        public int LogVerDown { get; set; }
        public int LogDiff { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogReport { get; set; }

        public LogItem(TourItem logTourItem, int id, string logName, double logDistance, int logTime, int logRating, int logSpeed, int logVerUp, int logVerDown, int logDiff, DateTime logDate, string logReport)
        {
            this.LogTourItem = logTourItem;
            this.Id = id;
            this.LogName = logName;
            this.LogDistance = logDistance;
            this.LogTime = logTime;
            this.LogRating = logRating;
            this.LogSpeed = logSpeed;
            this.LogVerUp = logVerUp;           
            this.LogVerDown = logVerDown;
            this.LogDiff = logDiff;
            this.LogDateTime = logDate;
            this.LogReport = logReport;
        }
    }
}
