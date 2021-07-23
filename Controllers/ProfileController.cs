using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TragoAPI.Models;

namespace TragoAPI.Controllers
{
    public class ProfileController : ApiController
    {
        static string ps;

        string constr = SqlConnection.getConnectionString();

        // GET: Profile
        [AuthenticationFilter]
        [Route("api/Profile/getProfile")]
        [HttpGet]
        public UserModel getProfile()
        {
            UserModel user = new UserModel();
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd=conn.CreateCommand();
            cmd.CommandText = "Select Name, Email, Password, Image from users where User_ID='" + WebApiApplication.UserID + "';";
            try
            {
                conn.Open();
                MySqlDataReader rdr= cmd.ExecuteReader();
                while(rdr.Read())
                {
                    user.Name = rdr["Name"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.Image= rdr["Image"].ToString();
                    user.Password = Base64Decode(rdr["Password"].ToString());
                    ps = Base64Decode(rdr["Password"].ToString());
                }
                return user;
            }
            catch (Exception ex)
            {
               return null;
            }
            return null;


        }
      

        [AuthenticationFilter]
        [Route("api/Profile/updateProfile")]
        [HttpPost]
        public int updateProfile(UserData data)
        {
            
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = conn.CreateCommand();
            if(data.OldPassword=="" && data.NewPassword=="")
            {
                cmd.CommandText = "Update users set Name='"+data.Name+"', email='"+data.Email+"' where user_id='" + WebApiApplication.UserID + "';";
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }

            }
            else
            {
                if(data.OldPassword!=ps)
                {
                    return 0;
                }
                else
                {
                    cmd.CommandText = "Update users set Name='" + data.Name + "', email='" + data.Email + "', password='"+ Base64Encode(data.NewPassword)+"' where user_id='" + WebApiApplication.UserID + "';";
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        return 2;
                    }

                }
            }
            
           


        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}