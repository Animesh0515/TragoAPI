using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TragoAPI.Models;

namespace TragoAPI.Controllers
{
    public class PackageController : ApiController
    {
        string constr = SqlConnection.getConnectionString();
        static List<PackageModel> packagelst;


        [AuthenticationFilter]
        [Route("api/Package/GetPackage")]
        [HttpGet]
        public List<PackageModel> GetPackage()
        {
            packagelst = new List<PackageModel>();
            MySqlConnection conn= new MySqlConnection(constr);
            MySqlCommand cmd= conn.CreateCommand();
            cmd.CommandText = "SELECT p.Package_ID, p.Destination, p.Description, p.Type, p.MaxCustomer, p.NoOfDays, p.DestinationImage,  pd.PackageDetail_ID, pd.FlightDetail_ID, pd.HotelDetail_ID, pd.Price FROM package p left join packagedetails pd on p.Package_ID=pd.Package_ID;";
            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PackageModel package = new PackageModel();  
                    package.PackageID = int.Parse(reader["Package_ID"].ToString());
                    package.Destination=reader["Destination"].ToString();
                    package.Description = reader["Description"].ToString();
                    package.Duration=reader["NoOfDays"].ToString();
                    package.Type=reader["Type"].ToString(); 
                    package.MaxPerson=reader["MaxCustomer"].ToString();
                    package.Price=int.Parse(reader["Price"].ToString());
                    package.Image=reader["DestinationImage"].ToString();

                    if (reader["FlightDetail_ID"].ToString() == null || reader["FlightDetail_ID"].ToString() == "")
                    {
                        package.Flight = false;
                    }
                    else
                        {
                        package.Flight = true ;

                    }
                    if(reader["Hoteldetail_ID"].ToString() == null || reader["Hoteldetail_ID"].ToString()=="")
                    {
                        package.Hotel = false;

                    }
                    else
                    {
                        package.Hotel = true;

                    }
                    packagelst.Add(package);    

                }
                conn.Close();

                return packagelst;

            }
            catch (Exception ex)
            {
                return null;
            }
            

        }


        [AuthenticationFilter]
        [Route("api/Package/SearchPackage")]
        [HttpPost]

        public List<PackageModel> SearchPackage([FromBody] string Destination)
        {
            List<PackageModel> spackagelst = new List<PackageModel>();

            spackagelst = packagelst.Where(x => x.Destination == Destination).ToList();
            return spackagelst;


        }

        [AuthenticationFilter]
        [Route("api/Package/bookPackage")]
        [HttpPost]

        public bool bookPackage([FromBody] int PackageId)

        {
            
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into packagebooking (package_id, user_id) values ('"+PackageId+"','"+WebApiApplication.UserID+"')";
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