using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;

namespace ОБЗД
{
    public partial class Doslid : Form
    {
        private int j;
        private object lable1;
        private string path = @"C:\Users\s8774\Desktop\Нова папка";
        public Doslid()
        {
            InitializeComponent();
        }

        private void Doslid_Load(object sender, EventArgs e)
        {
            if (h.typeUser == "Guest")    {
                toolStripButton1.Visible = false;
                toolStripButton2.Visible = false;
                toolStripButton3.Visible = false;
                dataGridView1.ReadOnly = true;
            }
            this.Height = 350;
            h.bs1 = new BindingSource();
            h.bs1.DataSource = h.myfunDt("SELECT * FROM sqlipz24_2_kss.дослідження");
            dataGridView1.DataSource = h.bs1;
            bindingNavigator1.BindingSource = h.bs1;
            h.bs1.Sort = "Data doslid";

            DGWFormat();

            DataTable dtBorder = new DataTable();
            DataTable dtDistinct = new DataTable();
            dtBorder = h.myfunDt("select min(`Data doslid`), max(`Data doslid`) from дослідження;");
            dtDistinct = h.myfunDt("select distinct `ID Doslid` from дослідження;");

            // записуємо межі у відповідні елементи керування
            txtDateFrom.Value = Convert.ToDateTime(dtBorder.Rows[0][0].ToString());
            txtDateTo.Value = Convert.ToDateTime(dtBorder.Rows[0][1].ToString());



            // визначаємо перелік можливих значень текстового поля
            cmbIdDosl.Items.Add("");
            for (int i = 0; i < dtDistinct.Rows.Count; i++)
            {
                cmbIdDosl.Items.Add(dtDistinct.Rows[i][0].ToString());
            }
            cmbIdDosl.DropDownStyle = ComboBoxStyle.DropDownList; //заборона редагування comboBox

        }
        void DGWFormat()
        {
            dataGridView1.Columns[0].HeaderText = "ID Doslid";
            dataGridView1.Columns[1].HeaderText = "Data doslid";
            dataGridView1.Columns[2].HeaderText = "Status doslid";
            dataGridView1.Columns[3].HeaderText = "ID roslyly";
            dataGridView1.Columns[4].HeaderText = "ID doslidnyka";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (btnFind.Checked)
            {
                label1.Visible = true;
                txtFind.Visible = true;
                txtFind.Focus();
            }
            else
            {
                ChancelFind();
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(txtFind.Text))
                    {
                        dataGridView1.Rows[i].Selected = true;
                        break;
                    }
                }
            }
        }



        private void txtFind_Leave(object sender, EventArgs e)
        {
            btnFind.Checked = false;
            ChancelFind();
        }


        private void ChancelFind()
        {
            label1.Visible = false;
            txtFind.Visible = false;
            txtFind.Text = "";
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
            }
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {

            Graphics gfx = e.Graphics;
            Pen p = new Pen(Color.DarkViolet, 1);
            gfx.DrawLine(p, 0, 5, 5, 5);  
            gfx.DrawLine(p, 35, 5, e.ClipRectangle.Width - 2, 5);
            gfx.DrawLine(p, 0, 5, 0, e.ClipRectangle.Height - 2);
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, 5, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2);
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2, 0, e.ClipRectangle.Height - 2);

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (btnFilter.Checked)
            {
                this.Height = 635;
                groupBox1.Visible = true;
            }
            else
            {
                this.Height = 350;
                groupBox1.Visible = false;
            }
        }

        private void btnFilterOk_Click(object sender, EventArgs e)
        {
            string strFilter = "";

            // Фільтр по назві
            if (!string.IsNullOrEmpty(txtStatusDosl.Text))
            {
                strFilter += $"`Status doslid` LIKE '%{txtStatusDosl.Text}%'";
            }

            if (!string.IsNullOrEmpty(cmbIdDosl.Text))
            {
                if (strFilter != "")
                {
                    strFilter += " AND ";
                }
                strFilter += $"`ID Doslid` = '{cmbIdDosl.Text}'";
            }

            if (txtDateFrom.Value != txtDateTo.Value)
            {
                if (strFilter != "")
                {
                    strFilter += " AND ";
                }
                strFilter += $"`Data doslid` >= '{txtDateFrom.Value.ToString("yyyy-MM-dd")}'";
            }

            h.bs1.Filter = strFilter;
        }
        private void ResetFilters()
        {
            txtStatusDosl.Text = "";
            cmbIdDosl.SelectedIndex = 0;

            if (h.bs1.DataSource is DataTable dataTable && dataTable.Rows.Count > 0)
            {
                var minMax = GetMinMaxDates(dataTable);
                txtDateFrom.Text = minMax.min.ToString("yyyy-MM-dd");
                txtDateTo.Text = minMax.max.ToString("yyyy-MM-dd");
            }

            // Скидаємо фільтр
            h.bs1.Filter = "";
        }
        private (DateTime min, DateTime max) GetMinMaxDates(DataTable dataTable)
        {
            DateTime min = DateTime.MaxValue;
            DateTime max = DateTime.MinValue;

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Data doslid"] != DBNull.Value && DateTime.TryParse(row["Data doslid"].ToString(), out DateTime date))
                {
                    min = (date < min) ? date : min;
                    max = (date > max) ? date : max;
                }
            }

            return (min == DateTime.MaxValue ? DateTime.MinValue : min,
                    max == DateTime.MinValue ? DateTime.MaxValue : max);
        }


        private void btnFilterCancel_Click(object sender, EventArgs e)
        {
            ResetFilters();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddNewDoslid addNewDoslid = new AddNewDoslid();
            addNewDoslid.ShowDialog();
            RefreshData();
        }
        public void RefreshData()
        {
            h.bs1.DataSource = h.myfunDt("SELECT * FROM дослідження");
            dataGridView1.DataSource = h.bs1;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DaleteDoslid daleteDoslid = new DaleteDoslid(RefreshData);
            daleteDoslid.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            EditDoslid editDoslid = new EditDoslid(RefreshData);
            editDoslid.ShowDialog();
        }

        private void btnStream_Click(object sender, EventArgs e)
        {
            var srcEncoding = Encoding.GetEncoding(1251);
            string extension = GetSelectedExtension();
            string fileName = Path.Combine(path, $"Doslidzhennya_Export_{DateTime.Now:yyyyMMddHHmmss}.{extension}");

            if (File.Exists(fileName)) File.Delete(fileName);

            using (StreamWriter wr = new StreamWriter(fileName, false, srcEncoding))
            {
                try
                {
                    DataTable dataTable = h.bs1.DataSource as DataTable;
                    if (dataTable == null)
                    {
                        MessageBox.Show("Немає даних для експорту", "Помилка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Запис заголовків
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        wr.Write(dataTable.Columns[i].ColumnName + (extension == "tsv" ? "\t" : "\t"));
                    }
                    wr.WriteLine();

                    // Запис даних
                    foreach (DataRow row in dataTable.Rows)
                    {
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            if (row[i] != null && row[i] != DBNull.Value)
                            {
                                if (dataTable.Columns[i].DataType == typeof(DateTime))
                                {
                                    wr.Write(Convert.ToDateTime(row[i]).ToString("yyyy-MM-dd") + (extension == "tsv" ? "\t" : "\t"));
                                }
                                else
                                {
                                    wr.Write(row[i].ToString() + (extension == "tsv" ? "\t" : "\t"));
                                }
                            }
                            else
                            {
                                wr.Write((extension == "tsv" ? "\t" : "\t"));
                            }
                        }
                        wr.WriteLine();
                    }

                    MessageBox.Show($"Дані експортовано у файл: {fileName}", "Успіх",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при експорті: {ex.Message}", "Помилка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOLEDB_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(path, $"Doslidzhennya_Export_{DateTime.Now:yyyyMMddHHmmss}.xls");

            if (File.Exists(fileName)) File.Delete(fileName);

            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={fileName};" +
                                    "Mode=ReadWrite;Extended Properties=\"Excel 8.0;HDR=YES\"";

            DataTable dataTable = h.bs1.DataSource as DataTable;
            if (dataTable == null)
            {
                MessageBox.Show("Немає даних для експорту", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    // Створення таблиці
                    string createTable = "CREATE TABLE [Дослідження] (";
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        string dataType = "VARCHAR(255)";
                        if (dataTable.Columns[i].DataType == typeof(DateTime))
                            dataType = "DATETIME";
                        else if (dataTable.Columns[i].DataType == typeof(int))
                            dataType = "INT";

                        createTable += $"[{dataTable.Columns[i].ColumnName}] {dataType}";
                        if (i < dataTable.Columns.Count - 1) createTable += ", ";
                    }
                    createTable += ")";

                    using (OleDbCommand cmd = new OleDbCommand(createTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Вставка даних
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string insert = "INSERT INTO [Дослідження] VALUES (";
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            if (row[i] == null || row[i] == DBNull.Value)
                            {
                                insert += "NULL";
                            }
                            else if (dataTable.Columns[i].DataType == typeof(DateTime))
                            {
                                insert += $"#{Convert.ToDateTime(row[i]).ToString("yyyy-MM-dd")}#";
                            }
                            else
                            {
                                insert += $"'{row[i].ToString().Replace("'", "''")}'";
                            }

                            if (i < dataTable.Columns.Count - 1) insert += ", ";
                        }
                        insert += ")";

                        using (OleDbCommand cmd = new OleDbCommand(insert, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show($"Дані експортовано у Excel файл: {fileName}", "Успіх",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при експорті: {ex.Message}", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnComObject_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(path, $"Doslidzhennya_Export_{DateTime.Now:yyyyMMddHHmmss}.xlsx");

            if (File.Exists(fileName)) File.Delete(fileName);

            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false;
                workbook = excelApp.Workbooks.Add();
                worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "Дослідження";

                DataTable dataTable = h.bs1.DataSource as DataTable;
                if (dataTable == null)
                {
                    MessageBox.Show("Немає даних для експорту", "Помилка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Запис заголовків
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
                }

                // Запис даних
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        if (dataTable.Columns[j].DataType == typeof(DateTime))
                        {
                            worksheet.Cells[i + 2, j + 1] = Convert.ToDateTime(dataTable.Rows[i][j]);
                            ((Excel.Range)worksheet.Cells[i + 2, j + 1]).NumberFormat = "yyyy-mm-dd";
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
                        }
                    }
                }

                // Форматування
                Excel.Range range = worksheet.Range[worksheet.Cells[1, 1],
                                                  worksheet.Cells[dataTable.Rows.Count + 1, dataTable.Columns.Count]];
                range.Font.Name = "Arial";
                range.Font.Size = 10;
                range.Columns.AutoFit();

                // Збереження
                workbook.SaveAs(fileName);
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show($"Дані експортовано у Excel файл: {fileName}", "Успіх",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при експорті: {ex.Message}", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (excelApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(path, $"Doslidzhennya_Export_{DateTime.Now:yyyyMMddHHmmss}.xml");

            if (File.Exists(fileName)) File.Delete(fileName);

            DataTable dataTable = h.bs1.DataSource as DataTable;
            if (dataTable == null)
            {
                MessageBox.Show("Немає даних для експорту", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                dataTable.WriteXml(fileName, XmlWriteMode.WriteSchema);
                MessageBox.Show($"Дані експортовано у XML файл: {fileName}", "Успіх",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при експорті: {ex.Message}", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetSelectedExtension()
        {
            if (rdobtn_tsv.Checked) return "tsv";
            if (rdobtn_doc.Checked) return "doc";
            if (rdobtn_xls.Checked) return "xls";
            return "txt";
        }
    }
}

