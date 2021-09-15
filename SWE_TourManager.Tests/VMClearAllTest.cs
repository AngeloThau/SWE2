using NUnit.Framework;
using SWE_TourManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.Tests
{
    class VMClearAllTest
    {
        CreateTourVM createTour = new CreateTourVM();

        [Test]
        public void ClearAllTest()
        {
            createTour.NewTourDescription = "noijo";
            createTour.NewTourDestination = "noijo";
            createTour.NewTourStart = "noijo";
            createTour.NewTourName = "noijo";
            createTour.ClearAll();

            Assert.That(createTour.NewTourDescription == "");
            Assert.That(createTour.NewTourDestination == "");
            Assert.That(createTour.NewTourStart == "");
            Assert.That(createTour.NewTourName == "");
        }
    }
}
