using System;

namespace SWE_TourManager.Models
{
    public class TourItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Distance { get; set; }
        public string ImgPath { get; set; }

        public string Start { get; set; }
        public string Destination { get; set; }

        public TourItem(int id, string name, string description, double distance, string start, string destination,  string imgPath)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Distance = distance;
            this.Start = start;
            this.Destination = destination;           
            this.ImgPath = imgPath;
        }
    }
}
