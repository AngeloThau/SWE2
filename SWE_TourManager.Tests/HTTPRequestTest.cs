using NUnit.Framework;
using SWE_TourManager.DataAccessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.Tests
{
    class HTTPRequestTest
    {
        HTTPRequest http = new HTTPRequest();

        [Test]
        public void RequestBuilderTest()       
        {
            Assert.That(http.RequestBuilder("Wien", "Wienerwald")== new Uri("http://www.mapquestapi.com/directions/v2/route?key=HpWCNiZeZm5zvY1ASIhSKLoJAms1I3Fx&from=Wien&to=Wienerwald"  ));
        }

       
    }
}
