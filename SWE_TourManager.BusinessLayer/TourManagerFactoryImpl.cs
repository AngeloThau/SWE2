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

        public TourItem CreateTour(string name, string description, double distance)
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.AddNewItem(name, description, distance);
        }

        public LogItem CreateLog(string report, TourItem tour)
        {
            ILogDAO logDAO = DALFactory.CreateLogDAO();
            return logDAO.AddNewLogItem(report, tour);
        }

    }
}
