using NUnit.Framework;
using SWE_TourManager.DataAccessLayer.DAO;
using SWE_TourManager.DataAccessLayer.PostgresSqlServer;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.Tests
{
    class DALTests
    {
        LogItemPostgresDAO logDAO = new LogItemPostgresDAO();
        TourItemPostgresDAO tourDAO = new TourItemPostgresDAO();
        TourItem tour = new TourItem(12, "a", "b", 2.3, "d", "g", "hieristBildhaha");
        TourItem tour1 = new TourItem(1, "a", "b", 2.3, "d", "g", "hieristBildhaha");

        [Test]
        public void LogItemIdTest()
        {
                 
            LogItem logItem = logDAO.FindById(13);
            Assert.IsNotNull(logItem);
            Assert.That(logItem.Id == 13);
        }
        [Test]
        public void TourItemIdTest()
        {

            TourItem tourItem = tourDAO.FindById(11);
            Assert.IsNotNull(tourItem);
            Assert.That(tourItem.Id == 11);
        }
        [Test]
        public void LogItemByTourIdTest()
        {
            IEnumerable<LogItem> foundlogs = logDAO.GetLogForTourItem(tour);
            Assert.That(foundlogs.Count() != 0);
        }
        [Test]
        public void LogItemByTourIdTestFalse()
        {
            IEnumerable<LogItem> foundlogs = logDAO.GetLogForTourItem(tour1);
            Assert.That(foundlogs.Count() == 0);
        }
        [Test]
        public void GetToursTest()
        {
            IEnumerable<TourItem> touritems = tourDAO.GetTourItems();
            Assert.That(touritems.Count() != 0);
        }
        
    }
}
