using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.BusinessLayer
{
    public interface ITourItemFactory
    {
        IEnumerable<TourItem> GetTourItems();
        IEnumerable<TourItem> SearchTours(string tourName, bool caseSensitive = false);
    }
}
