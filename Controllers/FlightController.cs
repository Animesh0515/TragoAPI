using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TragoAPI.Models;

namespace TragoAPI.Controllers
{
    public class FlightController : ApiController
    {
        string constr = SqlConnection.getConnectionString();

        [AuthenticationFilter]
        [Route("api/Flight/getFlightDetails")]
        [HttpGet]
        public List<FlightModel> getFlightDetails()
        {
            List<FlightModel> lst= new List<FlightModel>();
            
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT f.flight_id, f.airlinename, f.airlinecode, f.airlineimagepath, fd.destinationfrom, fd.destinationto, fd.departuretime, fd.arrivaltime, fd.duration, fd.cost FROM `flight` f inner join flightdetails fd on f.Flight_ID = fd.Flight_Id;";
            try
            {
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    FlightModel flight = new FlightModel();
                    flight.FlightID = int.Parse(rdr["Flight_ID"].ToString());
                    flight.AirlineName = rdr["AirlineName"].ToString();
                    flight.AirlineCode=int.Parse(rdr["AirlineCode"].ToString());
                    flight.AirlineImage=rdr["AirlineImagePath"].ToString();
                    flight.From=rdr["DestinationFrom"].ToString();
                    flight.To=rdr["DestinationTo"].ToString();
                    flight.DepartureTime=rdr["departureTime"].ToString();
                    flight.ArrivalTime=rdr["ArrivalTime"].ToString();
                    flight.Duration=rdr["Duration"].ToString();
                    flight.Cost=decimal.Parse(rdr["Cost"].ToString());
                    lst.Add(flight);
                }
                conn.Close();
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
           



            
        }
    }
}