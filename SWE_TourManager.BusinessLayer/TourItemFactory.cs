using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.BusinessLayer
{
    public static class TourItemFactory
    {
        private static ITourItemFactory instance;

        public static ITourItemFactory GetInstance()
        {
            if(instance == null)
            {
                instance = new TourItemFactoryImpl();
            }
            return instance;
        }
    }
}
