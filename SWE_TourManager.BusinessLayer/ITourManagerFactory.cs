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
        IEnumerable<TourItem> SearchTours(string tourName, bool caseSensitive = false);
        public TourItem CreateTour(string name, string description, double distance);
        public LogItem CreateLog(string report, TourItem tour);
    }
}
