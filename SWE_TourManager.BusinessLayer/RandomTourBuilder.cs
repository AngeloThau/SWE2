using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.BusinessLayer
{
    public class RandomTourBuilder
    {
        public string GenerateName(int length)
        {
            Random rand = new Random();

            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z" };
            string[] vowels = { "a", "e", "i", "o", "u",};

            string name = string.Empty;

            name += consonants[rand.Next(consonants.Length)].ToUpper();
            name += vowels[rand.Next(vowels.Length)];

            int charCount = 2;

            while (charCount < length)
            {
                name += consonants[rand.Next(consonants.Length)].ToUpper();
                charCount++;
                name += vowels[rand.Next(vowels.Length)];
                charCount++;
            }

            return name;
        }

        public int GenerateInt(int max)
        {
            Random rand = new Random();

            return rand.Next(max);
        }


        public string GetRandomLatLong()
        {
            Random rand = new Random();

            double lat = rand.Next(47, 49);
            double longi = rand.Next(9, 17);
            string location = lat.ToString() + "," + longi.ToString();
            return location;
        }

    }
}
