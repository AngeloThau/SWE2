using NUnit.Framework;
using SWE_TourManager.DataAccessLayer.Common;

namespace SWE_TourManager.NUnitTests
{
    [TestFixture]
    class DALDAOTests
    {

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [Test]
        public void TourExistsTest()
        {
            DALFactory db = DALFactory.GetDatabase();
            Assert.IsTrue(db.NameExists("First Tour"));
        }
    }
}
