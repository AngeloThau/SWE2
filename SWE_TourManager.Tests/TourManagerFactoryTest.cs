using NUnit.Framework;
using SWE_TourManager.BusinessLayer;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.Tests
{
    class TourManagerFactoryTest
    {

        ITourManagerFactory tourManagerFactory = TourManagerFactory.GetInstance();
        ITourManagerFactory tourManagerFactory1 = TourManagerFactory.GetInstance();
        TourItem tourItem = new TourItem(1, "a", "b", 2.3, "d", "g", "hieristBildhaha");

        [Test]
        public void TourManagerFactoryInstanceTest()
        {           
            Assert.AreEqual(tourManagerFactory1, tourManagerFactory);
        }
        

    }
}
