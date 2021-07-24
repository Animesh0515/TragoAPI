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
        [HttpPost]
        public List<FlightModel> getFlightDetails([FromBody]SearchFlightModel flt)
        {
            List<FlightModel> lst= new List<FlightModel>();
            
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT fd.flightdetail_id, f.airlinename, f.airlinecode, f.airlineimagepath, fd.destinationfrom, fd.destinationto, fd.departuretime, fd.arrivaltime, fd.duration, fd.cost FROM `flight` f inner join flightdetails fd on f.Flight_ID = fd.Flight_Id where fd.destinationfrom='"+ flt.From+ "' and fd.destinationto='" + flt.To+"'; ";
            try
            {
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {   
                    FlightModel flight = new FlightModel();
                    flight.FlightDetailsID = int.Parse(rdr["FlightDetail_ID"].ToString());
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

        [AuthenticationFilter]
        [Route("api/Flight/BookFlight")]
        [HttpPost]
        public bool BookFlight([FromBody]FlightBookingModel booking)
        {

            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into flightbooking (user_id, flightdetail_id, dateofbooking, bookedfordate, address, contactno, passportnumber, nationality) values ('" + WebApiApplication.UserID + "','" + booking.PackageDetailID + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + booking.BookedForDate + "','" + booking.Address + "','" + booking.ContactNo + "','" + booking.PassportNumber + "','" + booking.Nationality + "')";
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;



            }
            catch (Exception ex)
            {
                return false;
            }



        }
    }
}