using System;

namespace SWE_TourManager.Models
{
    public class TourItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Distance { get; set; }

        public TourItem(int id, string name, string description, double distance)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Distance = distance;
        }
    }
}
