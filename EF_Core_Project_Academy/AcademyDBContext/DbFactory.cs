using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.AcademyDBContext
{
    public static class DbFactory
    {
        private static readonly string _cs =
            ConfigurationManager.ConnectionStrings["AcademyDb"].ConnectionString;

        public static IDbConnection CreateConn()
        {
            var conn = new SqlConnection(_cs);
            conn.Open();
            return conn;
        }
    }
}
