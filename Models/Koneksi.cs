using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    public class Koneksi
    {
      public static string connString = ConfigurationManager.ConnectionStrings["Set"].ConnectionString;
    }
}