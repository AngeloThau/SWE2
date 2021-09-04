using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.BusinessLayer
{
    public static class TourManagerFactory
    {
        private static ITourManagerFactory instance;

        public static ITourManagerFactory GetInstance()
        {
            if(instance == null)
            {
                instance = new TourManagerFactoryImpl();
            }
            return instance;
        }
    }
}
