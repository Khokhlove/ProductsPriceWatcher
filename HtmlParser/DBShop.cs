using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System;

namespace HtmlParser
{
    public static class DBShop
    {
        //public static List<string[]> GetShop()
        //{
        //    List<string[]> data = new List<string[]>();
        //    using (SqlConnection con = DB.GetConnection())
        //    {
        //        con.Open();

        //        string sqlExpression = "SELECT * FROM Shop";

        //        SqlCommand command = new SqlCommand(sqlExpression, con);

        //        SqlDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            data.Add(new string[] { reader[0].ToString(), reader[1].ToString() });
        //        }
        //        con.Close();
        //    }

        public static bool SetShop(string[] str)
        {
            using (SqlConnection con = DB.GetConnection())
            {
                try
                {
                    con.Open();

                    string sqlExpression = $"UPDATE Shop Set NameId = '{str[3]}', NameShop = '{str[4]}', Url = '{str[5]}' WHERE Shop.Id = '{str[2]}'";

                    SqlCommand command = new SqlCommand(sqlExpression, con);

                    SqlDataReader reader = command.ExecuteReader();
                    con.Close();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                    return false;
                }
            }
        }

        public static bool AddShop(string[] str)
        {
            using (SqlConnection con = DB.GetConnection())
            {
                try
                {
                    con.Open();

                    string sqlExpression = $"INSERT INTO Shop (NameId,NameShop,Url) VALUES ('{str[0]}', '{str[1]}','{str[2]}')";

                    SqlCommand command = new SqlCommand(sqlExpression, con);

                    SqlDataReader reader = command.ExecuteReader();
                    con.Close();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                    return false;
                }
            }
        }
    }
}
