using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWE_TourManager.BusinessLayer
{
    public class Validator
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ITourManagerFactory tourManagerFactory;
        public Validator()
        {
            this.tourManagerFactory = TourManagerFactory.GetInstance();
        }

        public bool TourNameDoesNotExist(string tourName)
        {
            IEnumerable<TourItem> tourList = tourManagerFactory.GetTourItems();
            logger.Info("BL: Validating if TourName already exists");
            foreach (var tour in tourList)
            {
                if (tour.Name == tourName)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsAllowedInput(string input)
        {
            logger.Info("BL: Validating if input is Valid");
            {
                Regex r = new Regex("^[a-zA-Z0-9äöüÄÖÜ _.:,!?=-]+$");
                if (r.IsMatch(input))
                {
                    return true;
                }
                return false;
            }
        }
        public bool IsAlphabetOrNumber(string input)
        {
            logger.Info("BL: Validating if input is Alphanumerical");
            Regex r = new Regex("^[a-zA-Z0-9äöüÄÖÜ ]+$");
            if (r.IsMatch(input))
            {
                return true;
            }
            return false;
        }
        public bool IsNumber(string input)
        {
            logger.Info("BL: Validating if input is a Number");
            Regex r = new Regex("^[0-9]+$");
            if (r.IsMatch(input))
            {
                return true;
            }
            return false;
        }
    }
}
