using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace HtmlParser
{
    public partial class HTMLParser : Form
    {
        HashSet<int> rowIds = new HashSet<int>();
        public HTMLParser()
        {
            InitializeComponent();
        }

        private void Pars_Click(object sender, EventArgs e)
        {
            //CQ citilinkDom = CQ.CreateFromUrl("https://www.citilink.ru/product/videokarta-msi-nvidia-geforce-gtx-1660-gtx-1660-ventus-xs-6g-oc-6gb-gd-1133566/");
            //CQ citilinPriceNode = citilinkDom[".ProductHeaderprice-default_current-price"];
            //if (citilinPriceNode != null)
            //{
            //    int price = Convert.ToInt32(citilinPriceNode.Text().Trim().Replace(" ", ""));
            //    label1.Text = price.ToString();
            //}
            CQ avelotDom = CQ.CreateFromUrl("https://avelot.ru/videokarty/262003-videokarta-gigabyte-geforce-gtx1650-4gb-gddr5128bit3hdmidp-gv-n1650gaming-oc-4gdret.html?search_query=gtx1650&results=26");
            CQ avelotPriceNode = avelotDom["#our_price_display"];
            if (avelotPriceNode != null)
            {
                int price = Convert.ToInt32(avelotPriceNode.Attr("content"));
                // label2.Text = price.ToString();
            }
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ДобавитьТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.CreateMenu();
        }

        private void ИзменитьТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.EditMenu();
        }

        private void HTMLParser_Load(object sender, EventArgs e)
        {

        }

        private void LoadBD_Click(object sender, EventArgs e)
        {
            InitPivotTable();
        }

        void InitPivotTable()
        {
            DataGridView data = dataGridView2;

            if (data.Rows.Count > 0)
                data.Rows.Clear();

            using (SqlConnection con = DB.GetConnection())
            {
                con.Open();

                string sqlExpression = "SELECT * FROM Object INNER JOIN Shop ON (Object.Id = Shop.NameId)";

                SqlCommand command = new SqlCommand(sqlExpression, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Rows.Add(new string[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString() });
                }
                con.Close();
            }
        }

        private void SaveBD_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (int r in rowIds)
                {
                    string[] Cells = ToArray(dataGridView2.Rows[r]);
                    DBObects.SetObject(Cells);
                    DBShop.SetShop(Cells);   
                }
                    MessageBox.Show("База обновлена!", "Выполнено");
                    rowIds.Clear();
            }
            catch
            {
                return;
            }

            string[] ToArray (DataGridViewRow row)
            {
                string[] rowCells = new string[row.Cells.Count];
                for(int i = 0; i < row.Cells.Count; i++)
                {
                    rowCells[i] = row.Cells[i].Value.ToString();
                }
                return rowCells;
            }
        }

        private void AddDB_Click(object sender, EventArgs e)
        {
            string tBO = tBObject.Text.Trim();
            string tBS = tBShop.Text.Trim();
            string tBL = tBLink.Text.Trim();

            if (tBO != "" && tBS != "" && tBL != "")
            {
                AvailibiltyCheck();
                tBShop.Text = "";
                tBLink.Text = "";
                InitPivotTable();
            }
            else
                MessageBox.Show("Заполните все поля дибилы!","Ошибка");

            void AvailibiltyCheck()
            {
                string[] str = DBObects.GetObjectByName(tBO);
                if (str == null)
                {
                    if(DBObects.AddObject(new string[] { tBO }))
                        str = DBObects.GetObjectByName(tBO);
                }

                if (DBShop.AddShop(new string[] { str[0], tBS, tBL }))
                    MessageBox.Show($"Товар '{tBO}' добавлен в базу данных", "Выполнено");
            }
        }
        private void DataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                rowIds.Add(e.RowIndex);
        }
    }
}
