using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
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
        private string imgPath;
        

        public TourFileHandler()
        {
            this.filePath = ConfigurationManager.AppSettings["ExportFolderPath"].ToString();
            this.printPath = ConfigurationManager.AppSettings["PrintFolderPath"].ToString();
            this.imgPath = ConfigurationManager.AppSettings["ImgFolderPath"].ToString();
           
            
        }

        public void ExportTour(TourItem tour)
        {
            
            string filename = filePath + "\\" + tour.Name + ".txt";

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
            string filename = printPath + "\\" + tour.Name + "Print.pdf";

            TourImgHandler imgHandler = new TourImgHandler();
            
            PdfWriter writer = new PdfWriter(filename);
            PdfDocument pdf = new PdfDocument(writer);
            Document doc = new Document(pdf);
            Paragraph header = new Paragraph(tour.Name);
            doc.Add(header);
            LineSeparator ls = new LineSeparator(new SolidLine());
            doc.Add(ls);
            Image img = imgHandler.GetTourImage(tour.Name);
            doc.Add(img);
            Paragraph tourDesc = new Paragraph("Description: " + tour.Description );
            Paragraph tourDis = new Paragraph("Distance: " + tour.Distance.ToString());
            Paragraph tourStart = new Paragraph("Start: " + tour.Start );
            Paragraph tourDest = new Paragraph("Destination: " + tour.Description);
            doc.Add(tourDesc);
            doc.Add(tourDis);
            doc.Add(tourStart);
            doc.Add(tourDest);

            Paragraph logHeader = new Paragraph("LOGS:")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(15);
            doc.Add(logHeader);
            doc.Add(ls);
            foreach (LogItem item in logItemList)
            {
                Paragraph logName = new Paragraph(item.LogName);
                doc.Add(logName);
                Paragraph logDistance = new Paragraph("Distance: " + item.LogDistance.ToString());
                doc.Add(logDistance);
                Paragraph logTime = new Paragraph("DateTime: " + item.LogDateTime.ToString());
                doc.Add(logTime);
                Paragraph logRating = new Paragraph("Rating: " + item.LogRating);
                doc.Add(logRating);
                Paragraph logSpeed = new Paragraph("Average Speed: " + item.LogSpeed.ToString());
                doc.Add(logSpeed);
                Paragraph logVerUp = new Paragraph("Ver up: " + item.LogVerUp.ToString());
                doc.Add(logVerUp);
                Paragraph logVerDown = new Paragraph("Ver down: " + item.LogVerDown.ToString());
                doc.Add(logVerDown);
                Paragraph logDiff = new Paragraph("Difficulty: " + item.LogDiff.ToString());
                doc.Add(logDiff);
                Paragraph logReport = new Paragraph("Report: " + item.LogReport);
                doc.Add(logReport);
                doc.Add(ls);
            }



            doc.Close();          
            return 1;
        }


        public void SummarizeReport(int totalTime, double totalDis)
        {
            string filename = printPath + "\\" + "SummarizeReport.pdf";




            PdfWriter writer = new PdfWriter(filename);
            PdfDocument pdf = new PdfDocument(writer);
            Document doc = new Document(pdf);
            Paragraph header = new Paragraph("Summarize Report");
            doc.Add(header);
            LineSeparator ls = new LineSeparator(new SolidLine());
            doc.Add(ls);

            Paragraph tTime = new Paragraph("Total Time over all Logs: " + totalTime.ToString() + " min");
            Paragraph tDis = new Paragraph("Total Distance over all Logs: " + totalDis.ToString() + " km");
            doc.Add(tTime);
            doc.Add(tDis);

            doc.Close();
        }

    }
}
