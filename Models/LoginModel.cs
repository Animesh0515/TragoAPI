using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TragoAPI.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponeModel
    {
        public bool valid { get; set; }
        public string token { get; set; }
    }
}