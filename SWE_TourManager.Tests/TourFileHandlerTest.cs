using NUnit.Framework;
using SWE_TourManager.DataAccessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.Tests
{
    class TourFileHandlerTest
    {
        TourFileHandler tourFileHandler = new TourFileHandler();
        string tourname = "First Tour";

        [Test]
        public void TourImportTest()
        {
            string[] tourfilestring = tourFileHandler.GetImportTourString(tourname);
            Assert.That(tourfilestring[0] == "First Tour");
        }
    }
}
