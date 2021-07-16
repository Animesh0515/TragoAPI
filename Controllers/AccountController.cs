using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TragoAPI.Models;

namespace TragoAPI.Controllers
{
    public class AccountController : ApiController
    {
        string constr = SqlConnection.getConnectionString();

        // GET: Login
        [Route("api/Account/Login")]
        [HttpPost]
        public LoginResponeModel Login([FromBody] LoginModel model)
        {
            LoginResponeModel loginResponeModel= new LoginResponeModel();   
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select * from users where email='" + model.Email + "' and  password='" + Base64Encode(model.Password) + "' and type='user'; ";
            try
            {
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                if(rdr.HasRows)
                {
                    rdr.Read();
                    loginResponeModel.valid = true;
                    loginResponeModel.token=TokenGenerator.GenerateToken(rdr["Name"].ToString());

                }
                else
                {
                    loginResponeModel.valid = false;
                    loginResponeModel.token ="";


                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                    loginResponeModel.valid = false;
                loginResponeModel.token = "";


            }


            return loginResponeModel;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        [Route("api/Account/Signup")]
        [HttpPost]
        public SignupResponseModel Signup([FromBody] SignupModel model)
        {
            SignupResponseModel response= new SignupResponseModel();
            string query="Select * from users where email='"+model.Email+"';";
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select * from users where email='" + model.Email + "';";
            try { 
            conn.Open();
                MySqlDataReader rdr= cmd.ExecuteReader();
                if(rdr.HasRows)
                {
                    response.StatusCode = 0;
                }
                else
                {
                    conn.Close();
                    cmd.CommandText = "INSERT INTO `users`(`Name`, `Email`, `Password`, `Type`) VALUES ('" + model.Name + "','" + model.Email + "','" + Base64Encode(model.Password) + "','user');";
                    try
                    {
                        conn.Open();    
                         cmd.ExecuteNonQuery();
                        response.StatusCode = 1;
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        response.StatusCode = 3;

                    }
                }
            }
            catch (MySqlException ex)
            {
                response.StatusCode = 3;
            }

            return response;
        }
    }

}