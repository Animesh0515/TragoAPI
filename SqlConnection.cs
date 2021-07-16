using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TragoAPI
{
    public static class SqlConnection
    {
        public static string getConnectionString()
        {
            string Connectionstring = "Server=localhost;Port=3306;Database= trago ;Uid=root;Pwd=database;";
            return Connectionstring;
        }
    }
}