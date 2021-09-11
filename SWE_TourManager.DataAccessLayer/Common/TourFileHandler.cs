using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.Common
{
    public class TourFileHandler
    {
        private string filePath;


        public TourFileHandler()
        {
            this.filePath = ConfigurationManager.AppSettings["ExportFolderPath"].ToString();
        }

        public void ExportTour(TourItem tour)
        {
            
            string filename = filePath+ "\\" + tour.Name + ".txt";

            StreamWriter sw = new StreamWriter(filename);
            
            sw.WriteLine(tour.Name);
            sw.WriteLine(tour.Description);
            sw.WriteLine(tour.Distance.ToString());
            sw.WriteLine(tour.Start);
            sw.WriteLine(tour.Destination);
            sw.WriteLine(tour.ImgPath);

            sw.Close();
           
        }

        public string[] GetImportTourString(string tourname)
        {
            try
            {
                string filename = filePath + "\\" + tourname + ".txt";;

                string[] tourString = File.ReadAllLines(filename);
                return tourString;
            }
            catch (Exception)
            {
                Console.WriteLine("could not open/find File on GetImportTourString at DAL");
                return null;
            }
            

        }
    }
}
