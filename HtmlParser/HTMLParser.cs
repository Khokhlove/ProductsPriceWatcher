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
        List<string[]> shops = new List<string[]>();
        public Console cs;

        public HTMLParser()
        {
            InitializeComponent();
            InitConsole();
            InitSearchTab();
        }

        void InitConsole()
        {
            cs = new Console();
            new CConsole(cs);
            CConsole.GetInstance().Log("Введите help, чтобы получить информацию о доступных командах!");
            cs.Hide();
        }

        private void Parse_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0) dataGridView1.Rows.Clear();
            List<Shop> shops = new List<Shop>();
            this.shops.ForEach(s =>
            {
                Shop shop = Shop.GetShop(s);
                if (shop != null)
                {
                    shops.Add(shop);
                }
            });

            shops.ForEach(s =>
            {
                DateTime date = DateTime.Now;
                int price = s.GetPrice();
                if (price != -1)
                {
                    CConsole.GetInstance().LogSuccess($"Цена на {s.shopName} найдена!");
                    dataGridView1.Rows.Add(new string[] { s.shopName, price.ToString(), date.ToString("G") });
                    DBInformation.AddInformation(new string[] { comboBox1.Text, s.shopName, price.ToString(), date.ToString("yyyy-MM-dd HH:mm:ss"), s.link.ToString() });
                }
            });
            SearchBestPrice();
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HTMLParser_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.Object". При необходимости она может быть перемещена или удалена.
            this.objectTableAdapter.Fill(this.database1DataSet.Object);
        }

        private void LoadBD_Click(object sender, EventArgs e)
        {
            SetPivotTable();
        }

        void SetPivotTable()
        {
            DataGridView data = dataGridView2;

            if (data.Rows.Count > 0)
            {
                data.Rows.Clear();
                CConsole.GetInstance().Log("Грид очищен!");
            }

            CConsole.GetInstance().Log("Делаю запрос к БД");
            List<string[]> objects = GetObjectsPivot();

            if (objects.Count > 0)
            {
                CConsole.GetInstance().LogSuccess("Данные получены !");
                CConsole.GetInstance().Log($"Количество объектов: {objects.Count}");
                objects.ForEach(o => data.Rows.Add(o));
            }

            List<string[]> GetObjectsPivot()
            {
                List<string[]> objs = new List<string[]>();

                using (SqlConnection con = DB.GetConnection())
                {
                    con.Open();

                    string sqlExpression = "SELECT * FROM Object INNER JOIN Shop ON (Object.Id = Shop.NameId)";

                    SqlCommand command = new SqlCommand(sqlExpression, con);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        objs.Add(new string[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString() });
                        
                    }
                    con.Close();
                }

                return objs;
            }            
        }

        private void SaveBD_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (int r in rowIds)
                {
                    string[] Cells = ToArray(dataGridView2.Rows[r]);
                    DBObject.SetObject(Cells);
                    DBShop.SetShop(Cells);
                }
                string msg = "База обновлена!";
                MessageBox.Show(msg, "Выполнено");
                CConsole.GetInstance().LogSuccess(msg);
                rowIds.Clear();
            }
            catch(Exception ex)
            {
                CConsole.GetInstance().LogError($"{ex.Message.ToString()}");
                return;
            }

            string[] ToArray(DataGridViewRow row)
            {
                string[] rowCells = new string[row.Cells.Count];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    rowCells[i] = row.Cells[i].Value.ToString();
                }
                return rowCells;
            }
        }

        private void AddDB_Click(object sender, EventArgs e)
        {
            string tBO = tBObject.Text.Trim();
            string tBS = comboBox2.Text.Trim();
            string tBL = tBLink.Text.Trim();

            if (tBO != "" && tBS != "" && tBL != "")
            {
                AvailibiltyCheck();
                comboBox2.Text = "";
                tBLink.Text = "";
                SetPivotTable();
            }
            else
            {
                string msg = "Заполните все поля дибилы!";
                CConsole.GetInstance().LogError(msg);
                MessageBox.Show(msg, "Ошибка");
            }

            void AvailibiltyCheck()
            {
                string[] str = DBObject.GetObjectByName(tBO);
                if (str == null)
                {
                    if (DBObject.AddObject(new string[] { tBO }))
                        str = DBObject.GetObjectByName(tBO);
                }

                if (DBShop.AddShop(new string[] { str[0], tBS, tBL }))
                {
                    string msg = $"Товар '{tBO}' добавлен в базу данных";
                    MessageBox.Show(msg, "Выполнено");
                    CConsole.GetInstance().LogSuccess(msg);
                }
            }
        }
        private void DataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                rowIds.Add(e.RowIndex);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            dataGridView1.Rows.Clear();
            panel2.Visible = false;
            string name = comboBox1.Text;
            string[] obj = DBObject.GetObjectByName(name);

            if (obj != null)
            {
                int id = Convert.ToInt32(obj[0]);
                shops = DBShop.GetShopById(id);
                if (shops.Count != 0)
                {
                    shops.ForEach(el => listBox1.Items.Add(el[2]));
                }
                else
                {
                    string msg = "Перечень магазинов для данного товара не найден!";
                    CConsole.GetInstance().LogError(msg);
                    MessageBox.Show(msg, "Ошибка!");
                }
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var rows = dataGridView2.SelectedRows;
            if (rows.Count > 0)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    string shopName = rows[i].Cells[4].Value.ToString();
                    DBShop.DeleteShopByName(shopName);
                }
                SetPivotTable();
                string msg = "Магазин(ы)  удален(ы)!";
                CConsole.GetInstance().LogSuccess(msg);
                MessageBox.Show(msg, "Выполнено");
            }
            else
            {
                string msg = "Не ожидал ошибки? Выбери строку для удаления, придурок.";
                CConsole.GetInstance().LogError(msg);
                MessageBox.Show(msg, "Ошибка!");
            }
        }

        void InitSearchTab()
        {
            SetProductsComboBox();

            void SetProductsComboBox()
            {
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.Items.Clear();
                    CConsole.GetInstance().Log("Очищен список товаров!");
                }
                List<string[]> products = DBObject.GetObjects();
                if (products.Count > 0)
                {
                    CConsole.GetInstance().LogSuccess("Список продуктов получен!");
                    CConsole.GetInstance().Log($"Перечень товаров составляет: {products.Count} шт.");
                    products.ForEach(p => comboBox1.Items.Add(p[1]));
                }
            }
        }

        void InitUpdateDBTab()
        {
            SetPivotTable();
        }

        void InitHistoryTab()
        {
            if (dataGridView3.Rows.Count > 0)
            {
                dataGridView3.Rows.Clear();
                CConsole.GetInstance().Log("Грид очищен!");
            }

            List<string[]> history = DBInformation.GetHistory();

            if (history.Count > 0)
            {
                CConsole.GetInstance().LogSuccess("Данные получены!");
                CConsole.GetInstance().Log($"Количество строк: {history.Count}");
                history.ForEach(h => dataGridView3.Rows.Add(h));
            }             
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Action[] tabs = new Action[]
            {
                InitSearchTab,
                InitUpdateDBTab,
                InitHistoryTab
            };

            int i = tabControl1.SelectedIndex;

            if (i < tabs.Length) tabs[i].Invoke();
        }

        private void КонсольToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cs == null)
            {
                cs = new Console();
                cs.Show();
            }
            else
            {
                cs.Show();
            }
        }

        private void SearchBestPrice()
        {
            string[] str = DBInformation.GetBestPrice(comboBox1.Text);
            if (str != null)
            {
                labelShop.Text = str[0];
                labelPrice.Text = str[1];
                labelDataRequest.Text = str[2];
                panel2.Visible = true;
                CConsole.GetInstance().LogSuccess($"Выведена наилучшая цена для товара '{comboBox1.Text}'");
            }
            else
            {
                CConsole.GetInstance().LogWarning("Наилучшая цена не была найдена!");
            }
        }

    }
}
