using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System;

namespace HtmlParser
{
    public static class DBInformation
    {
        public static List<string[]> GetHistory()
        {
            List<string[]> data = new List<string[]>();
            using (SqlConnection con = DB.GetConnection())
            {
                con.Open();

                string sqlExpression = "SELECT * FROM Information";

                SqlCommand command = new SqlCommand(sqlExpression, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new string[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()});
                }
                con.Close();
            }
            return data;
        }

        public static bool AddInformation(string[] str)
        {
            using (SqlConnection con = DB.GetConnection())
            {
                try
                {
                    con.Open();

                    string sqlExpression = $"INSERT INTO Information (Object, NameSite, Price, DateRequest, Site) " +
                    $"VALUES ('{str[0]}', '{str[1]}','{str[2]}', '{str[3]}', '{str[4]}')";

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

        public static string[] GetBestPrice(string str)
        {
            using (SqlConnection con = DB.GetConnection())
            {
                try
                {
                    con.Open();

                    string sqlExpression = $"SELECT TOP 1 * FROM INFORMATION WHERE OBJECT = '{str}' order by Price asc";
                    SqlCommand command = new SqlCommand(sqlExpression, con);

                    SqlDataReader reader = command.ExecuteReader();
                    List<string[]> l = new List<string[]>();
                    if (reader.Read())
                    {
                        return new string[]{reader[2].ToString(), reader[3].ToString(), reader[4].ToString() };
                    }
                    con.Close();
                    return null;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                    return null;
                }
            }
        }
    }
}
