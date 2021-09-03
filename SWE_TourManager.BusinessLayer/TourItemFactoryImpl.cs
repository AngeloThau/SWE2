using SWE_TourManager.DataAccessLayer;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.BusinessLayer
{
    internal class TourItemFactoryImpl : ITourItemFactory
    {
        private TourItemDAO tourItemDAO = new TourItemDAO();
        
        public IEnumerable<TourItem> GetTourItems()
        {
            return tourItemDAO.GetTourItems();
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
    }
}
