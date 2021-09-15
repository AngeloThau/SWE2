using NUnit.Framework;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.Tests
{
    class LogItemTest
    {
        

        [Test]
        public void TourNameTest()
        {
            TourItem tour = new TourItem(1, "a", "b", 2.3, "d", "g", "hieristBildhaha");
            LogItem logItem = new LogItem(tour, 1, "sdf", 3.3, 23, 4, 5, 6, 7, 2, DateTime.Now, "nnn");
            Assert.NotNull(logItem);
        }
    }
}
