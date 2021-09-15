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
        private string printPath;

        public TourFileHandler()
        {
            this.filePath = ConfigurationManager.AppSettings["ExportFolderPath"].ToString();
            this.printPath = ConfigurationManager.AppSettings["PrintFolderPath"].ToString();
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

        public int PrintTour(TourItem tour, List<LogItem> logItemList)
        {
            string filename = printPath + "\\" + tour.Name + "Print.txt";

            StreamWriter sw = new StreamWriter(filename);

            sw.WriteLine(tour.Name);
            sw.WriteLine(tour.Description);
            sw.WriteLine(tour.Distance.ToString());
            sw.WriteLine(tour.Start);
            sw.WriteLine(tour.Destination);
            sw.WriteLine(tour.ImgPath);
            sw.WriteLine("");

            sw.WriteLine(logItemList.Count + " Logs:");

            foreach (LogItem item in logItemList)
            {
                sw.WriteLine("Name: " + item.LogName);
                sw.WriteLine("Distance: " + item.LogDistance);
                sw.WriteLine("Time: " + item.LogTime);
                sw.WriteLine("Rating: " + item.LogRating);
                sw.WriteLine("Speed: " + item.LogSpeed);
                sw.WriteLine("VerUp: " + item.LogVerUp);
                sw.WriteLine("VerDown: " + item.LogVerDown);
                sw.WriteLine("Diff: " + item.LogDiff);
                sw.WriteLine("Date: " + item.LogDateTime);
                sw.WriteLine("Report: " + item.LogReport);

                sw.WriteLine("End of Log");
                sw.WriteLine("");
                                             
            }

            sw.Close();
            return 1;
        }
    }
}
