using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TragoAPI.Models
{
    public class SignupModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }

    public class SignupResponseModel
    {
        public int StatusCode { get; set; }  
        }
}