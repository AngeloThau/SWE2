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
        public string Report { get; set; }
        public TourItem LogTourItem { get; set; }

        public LogItem(int id, string report, TourItem logTourItem)
        {
            this.Id = id;
            this.Report = report;
            this.LogTourItem = logTourItem;
        }
    }
}
