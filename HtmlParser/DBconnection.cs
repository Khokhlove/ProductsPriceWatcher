using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RepositoryVisualStudio\HtmlParser\Khokhlove\ProductsPriceWatcher\HtmlParser\Database1.mdf;Integrated Security=True";
            return connectionString;
        }
    }
}
