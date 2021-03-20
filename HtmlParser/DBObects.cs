using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System;

namespace HtmlParser
{
    public static class DBObects
    {
        public static List<string[]> GetObjects()
        {
            List<string[]> data = new List<string[]>();
            using (SqlConnection con = DB.GetConnection())
            {
                con.Open();

                string sqlExpression = "SELECT * FROM Object";

                SqlCommand command = new SqlCommand(sqlExpression, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new string[] { reader[0].ToString(), reader[1].ToString() });
                }
                con.Close();
            }
            return data;
        }

        public static bool SetObject(string[] str)
        {
            using (SqlConnection con = DB.GetConnection())
            {
                try
                {
                    con.Open();

                    string sqlExpression = $"UPDATE Object Set Object.Object = '{str[1]}' WHERE Object.Id = '{str[0]}'";

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

        public static bool AddObject(string[] str)
        {
            using (SqlConnection con = DB.GetConnection())
            {
                try
                {
                    con.Open();

                    string sqlExpression = $"INSERT INTO Object (Object.Object) VALUES  ('{str[0]}')";

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

        public static string[] GetObjectByName(string objectName)
        {
            using (SqlConnection con = DB.GetConnection())
            {
                try
                {
                    con.Open();

                    string sqlExpression = $"Select * FROM Object WHERE Object.Object = '{objectName}'";

                    SqlCommand command = new SqlCommand(sqlExpression, con);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string[] str = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            str[i] = reader[i].ToString();
                        }
                        con.Close();
                        return str;
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
