using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TragoAPI.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
    }

    public class UserData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}