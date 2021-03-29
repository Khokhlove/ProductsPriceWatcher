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
        List<string[]> failedParsings = new List<string[]>();
        public Console console;
        public CConsole cc;

        public HTMLParser()
        {
            InitializeComponent();
            InitConsole();
            InitSearchTab();
        }

        void InitConsole()
        {
            console = new Console();
            new CConsole(console);
            cc = CConsole.GetInstance();
            cc.Log("Введите help, чтобы получить информацию о доступных командах!")
                .Log("Стрелки ↑ и ↓ для выбора ранее вызванных команд!")
                .Log(" ");
            console.Hide();
        }

        private void Parse_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0) dataGridView1.Rows.Clear();

            ParsePrices(this.shops, s => { });
            SearchBestPrice();
        }

        void ParsePrices(List<string[]> shopsRaw, Action<Shop> callback)
        {
            List<Shop> shops = new List<Shop>();
            shopsRaw.ForEach(s =>
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
                    cc.LogSuccess($"Цена на {s.shopName} найдена!");
                    dataGridView1.Rows.Add(new string[] { s.shopName, price.ToString(), date.ToString("G") });
                    DBInformation.AddInformation(new string[] { comboBox1.Text, s.shopName, price.ToString(), date.ToString("yyyy-MM-dd HH:mm:ss"), s.link.ToString() });
                    callback(s);
                }
                else
                {
                    string[] shopRaw = shopsRaw.Find(raw => raw[3] == s.link);
                    if (shopsRaw != null)
                    {
                        if (!failedParsings.Contains(shopRaw))
                        {
                            failedParsings.Add(shopRaw);
                            cc.LogError($"Произошла ошибка при парсинге '{s.shopName}!'");
                            cc.LogError($"Информация о данном магазине добавлена во вкладку 'Фейлы'");
                        }
                    }
                }
            });
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
                cc.Log("Грид очищен!");
            }

            cc.Log("Делаю запрос к БД");
            List<string[]> objects = GetObjectsPivot();

            if (objects.Count > 0)
            {
                cc.LogSuccess("Данные получены !")
                    .Log($"Количество объектов: {objects.Count}");
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
                cc.LogSuccess(msg);
                rowIds.Clear();
            }
            catch(Exception ex)
            {
                cc.LogError($"{ex.Message.ToString()}");
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
                cc.LogError(msg);
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
                    cc.LogSuccess(msg);
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
                    cc.LogError(msg);
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
                cc.LogSuccess(msg);
                MessageBox.Show(msg, "Выполнено");
            }
            else
            {
                string msg = "Не ожидал ошибки? Выбери строку для удаления, придурок.";
                cc.LogError(msg);
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
                    cc.Log("Очищен список товаров!");
                }
                List<string[]> products = DBObject.GetObjects();
                if (products.Count > 0)
                {
                    cc.LogSuccess("Список продуктов получен!")
                        .Log($"Перечень товаров составляет: {products.Count} шт.");
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
                cc.Log("Грид очищен!");
            }

            List<string[]> history = DBInformation.GetHistory();

            if (history.Count > 0)
            {
                cc.LogSuccess("Данные получены!")
                    .Log($"Количество строк: {history.Count}");
                history.ForEach(h => dataGridView3.Rows.Add(h));
            }             
        }

        void InitFailedTab()
        {
            if (dataGridView4.Rows.Count > 0) dataGridView4.Rows.Clear();

            this.failedParsings.ForEach(f =>
            {
                dataGridView4.Rows.Add(new string[] { f[0], f[1], f[2], f[3] });
            });
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Action[] tabs = new Action[]
            {
                InitSearchTab,
                InitUpdateDBTab,
                InitHistoryTab,
                InitFailedTab
            };

            int i = tabControl1.SelectedIndex;

            if (i < tabs.Length) tabs[i].Invoke();
        }

        private void КонсольToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (console == null)
            {
                console = new Console();
                console.Show();
            }
            else
            {
                console.Show();
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
                cc.LogSuccess($"Выведена наилучшая цена для товара '{comboBox1.Text}'");
            }
            else
            {
                cc.LogWarning("Наилучшая цена не была найдена!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Shop> shops = new List<Shop>();
            ParsePrices(this.failedParsings, s => shops.Add(s));
            shops.ForEach(s =>
            {
                int i = this.failedParsings.FindIndex(f => f[3] == s.link);
                this.failedParsings.RemoveAt(i);
            });
        }
    }
}
