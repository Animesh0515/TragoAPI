using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TragoAPI.Models
{
    public class FlightModel
    {



        public int FlightDetailsID { get; set; }
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

    public class SearchFlightModel
    {
        public string From { get; set; }
        public string  To { get; set; }

    }

    public class FlightBookingModel
    {
        
        public int PackageDetailID { get; set; }
        
        public string BookedForDate { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public int PassportNumber { get; set; }
        public string Nationality { get; set; }

    }
}