using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace HtmlParser
{
    public static class DB
    {

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            return connection;
        }

        public static string GetConnectionString()
        {
            string fullPath = Path.GetFullPath(@"..\..\");
            string solutionPath = $"{fullPath}Database1.mdf";
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={solutionPath};Integrated Security=True;";
            return connectionString;
        }
    }
}
