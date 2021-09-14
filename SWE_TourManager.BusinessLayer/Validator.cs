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
        private ITourManagerFactory tourManagerFactory;
        public Validator()
        {
            this.tourManagerFactory = TourManagerFactory.GetInstance();
        }

        public bool TourNameDoesNotExist(string tourName)
        {
            if (tourManagerFactory.SearchTours(tourName).Count() == 0)
            {
                return true;
            }
            return false;
        }

        public bool IsAllowedInput(string input)
        {
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
            Regex r = new Regex("^[a-zA-Z0-9äöüÄÖÜ ]+$");
            if (r.IsMatch(input))
            {
                return true;
            }
            return false;
        }
        public bool IsNumber(string input)
        {
            Regex r = new Regex("^[0-9]+$");
            if (r.IsMatch(input))
            {
                return true;
            }
            return false;
        }
    }
}
