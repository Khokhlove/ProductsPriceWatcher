using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System;

namespace HtmlParser
{
    public static class DBInformation
    {
        //public static List<string[]> GetShopById(int id)
        //{
        //    List<string[]> data = new List<string[]>();
        //    using (SqlConnection con = DB.GetConnection())
        //    {
        //        try
        //        {
        //            con.Open();

        //            string sqlExpression = $"SELECT * FROM Shop WHERE NameId = '{id}'";

        //            SqlCommand command = new SqlCommand(sqlExpression, con);

        //            SqlDataReader reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                data.Add(new string[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString() });
        //            }
        //            con.Close();

        //            return data;
        //        }
        //        catch(Exception e)
        //        {
        //            MessageBox.Show(e.Message, "Ошибка!");
        //            return null;
        //        }
        //    }
        //}

        //    public static bool SetShop(string[] str)
        //{
        //    using (SqlConnection con = DB.GetConnection())
        //    {
        //        try
        //        {
        //            con.Open();

        //            string sqlExpression = $"UPDATE Shop Set NameId = '{str[3]}', NameShop = '{str[4]}', Url = '{str[5]}' WHERE Shop.Id = '{str[2]}'";

        //            SqlCommand command = new SqlCommand(sqlExpression, con);

        //            SqlDataReader reader = command.ExecuteReader();
        //            con.Close();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show(e.Message, "Ошибка");
        //            return false;
        //        }
        //    }
        //}

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
        //public static bool DeleteShopByName(string shopName)
        //{
        //    using (SqlConnection con = DB.GetConnection())
        //    {
        //        try
        //        {
        //            con.Open();

        //            string sqlExpression = $"DELETE FROM Shop WHERE NameShop = '{shopName}'";

        //            SqlCommand command = new SqlCommand(sqlExpression, con);

        //            SqlDataReader reader = command.ExecuteReader();
        //            con.Close();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show(e.Message, "Ошибка");
        //            return false;
        //        }
        //    }
        //}
    }
}
