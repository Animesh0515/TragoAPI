using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TragoAPI.Models
{
    public class PackageModel
    {
        public int PackageID { get; set; }
        public string Destination { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public bool Flight { get; set; }
        public bool Hotel { get; set; }
        public string MaxPerson { get; set; }
        public int Price  { get; set; }

        public string Image { get; set; }
    }



   
}