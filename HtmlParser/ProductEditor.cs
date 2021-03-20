using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HtmlParser
{
    public partial class ProductEditor : Form
    {

        public ProductEditor()
        {
            InitializeComponent();
        }

        private void Quantity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            ConnectionDateBase(textBox1.Text, textBox2.Text, "10");
        }

        private void AddDataBase(string nameObject, string url)
        {
            
        }

        private void ConnectionDateBase(string o, string u, string i)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RepositoryVisualStudio\ProductsPriceWatcher\HtmlParser\Database1.mdf;Integrated Security=True";
            string sqlExpression = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

            }

        }
    }
}
