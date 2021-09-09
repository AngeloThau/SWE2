using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.DAO
{
    public interface ITourDAO
    {
        TourItem FindById(int itemId);

        TourItem AddNewItem(string name, string description, double distance, string start, string destination, string imgPath);
        IEnumerable<TourItem> GetTourItems();
    }
}
