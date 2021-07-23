using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TragoAPI.Models
{
    public class FlightModel
    {
        
        

        public int FlightID { get; set; }
        public string AirlineName { get; set; }
        public int AirlineCode { get; set; }
        public string AirlineImage { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public string Duration { get; set; }
        public decimal  Cost { get; set; }
    }
}