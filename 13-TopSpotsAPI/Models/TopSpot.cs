using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopSpotsAPI.Models
{
    public class TopSpot
    {
        public int Id { get; set; }             // Assuming the json file or database has an ID for each item.
        public string Name { get; set; }
        public string Description { get; set; }
        public double[] Location { get; set; }

    }
}


