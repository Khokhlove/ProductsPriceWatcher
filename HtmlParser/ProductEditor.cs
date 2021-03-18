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

        }

        private void AddDataBase(string nameObject, string url)
        {
            
        }

        private void ConnectionDateBase()
        {
            SqlConnection db = new SqlConnection(@"C:\Users\alexe\source\repos\HtmlParser\HtmlParser\Database1.mdf";
            db.Open();

            SqlCommand command = new SqlCommand("INSERT * INTO Object (Object, UrlSite, SiteId) VALUES(@)");
        


    }
}
