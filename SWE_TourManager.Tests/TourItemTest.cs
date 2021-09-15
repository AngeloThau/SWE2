using NUnit.Framework;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.Tests
{
    class TourItemTest
    {
        TourItem tourItem = new TourItem(1, "a", "b", 2.3, "d", "g", "hieristBildhaha");

        [Test]
        public void TourNameTest()
        {
            Assert.NotNull(tourItem);
        }
    }
}
