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
using Excel = Microsoft.Office.Interop.Excel;

namespace ОБЗД
{
    public partial class Roslyny : Form
    {
        public Roslyny()
        {
            InitializeComponent();
        }
        void InitializeFilters()
        {
            // Завантаження діапазону віку
            DataTable dtBorder = h.myfunDt("SELECT MIN(`Vik roslyny`), MAX(`Vik roslyny`) FROM рослини");
            txtVikFrom.Text = dtBorder.Rows[0][0].ToString();
            txtVikTo.Text = dtBorder.Rows[0][1].ToString();
            // Завантаження унікальних ID рослин для фільтрації
            DataTable dtRoslynyID = h.myfunDt("SELECT DISTINCT `ID roslyny` FROM рослини ORDER BY `ID roslyny`");
            cmbGeografia.Items.Add(""); // Пусте значення для скидання фільтра
            foreach (DataRow row in dtRoslynyID.Rows)
            {
                cmbGeografia.Items.Add(row[0].ToString());
            }
            cmbGeografia.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void Roslyny_Load(object sender, EventArgs e)
        {
            if (h.typeUser == "Guest")
            {
                toolStripButton1.Visible = false;
                toolStripButton2.Visible = false;
                toolStripButton3.Visible = false;
                dataGridView1.ReadOnly = true; 
            }
           
                this.Height = 500;
            h.bs1 = new BindingSource();
            h.bs1.DataSource = h.myfunDt("SELECT * FROM sqlipz24_2_kss.рослинии");
            dataGridView1.DataSource = h.bs1;
            bindingNavigator1.BindingSource = h.bs1;


            h.bs1.Sort = "Nazva roslyny";

            DGWFormat();

            DataTable dtBorder = new DataTable();
            DataTable dtDistinct = new DataTable();
            dtBorder = h.myfunDt("select min(`Vik roslyny`), max(`Vik roslyny`) from рослинии;");
            dtDistinct = h.myfunDt("select distinct `ID roslyny` from рослинии;");

            // записуємо межі у відповідні елементи керування
            txtVikFrom.Text = dtBorder.Rows[0][0].ToString();
            txtVikTo.Text = dtBorder.Rows[0][1].ToString();


            // визначаємо перелік можливих значень текстового поля
            cmbGeografia.Items.Add("");
            for (int i = 0; i < dtDistinct.Rows.Count; i++)
            {
                cmbGeografia.Items.Add(dtDistinct.Rows[i][0].ToString());
            }
            cmbGeografia.DropDownStyle = ComboBoxStyle.DropDownList; //заборона редагування comboBox


        }
        void DGWFormat()
        {
            dataGridView1.Columns[0].HeaderText = "ID roslyny";
            dataGridView1.Columns[1].HeaderText = "Vik roslyny";
            dataGridView1.Columns[2].HeaderText = "Vyd roslyny";
            dataGridView1.Columns[3].HeaderText = "Nazva roslyny";
            dataGridView1.Columns[4].HeaderText = "Geografia roslyny";
        }

        private void btnFind_Click_1(object sender, EventArgs e)
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
                this.Height = 650;
                groupBox1.Visible = true;
            }
            else
            {
                this.Height = 370;
                groupBox1.Visible = false;
            }
        }

        private void btnFilterOk_Click(object sender, EventArgs e)
        {
            try
            {
                string strFilter = "";

                // Фільтр по назві
                if (!string.IsNullOrEmpty(txtNameRosl.Text))
                {
                    strFilter += $"`Nazva roslyny` LIKE '%{txtNameRosl.Text}%'";
                }

                // Фільтр по віку
                if (int.TryParse(txtVikFrom.Text, out int vikFrom) && int.TryParse(txtVikTo.Text, out int vikTo))
                {
                    if (!string.IsNullOrEmpty(strFilter)) strFilter += " AND ";
                    strFilter += $"`Vik roslyny` >= {vikFrom} AND `Vik roslyny` <= {vikTo}";
                }
                else if (!string.IsNullOrEmpty(txtVikFrom.Text) || !string.IsNullOrEmpty(txtVikTo.Text))
                {
                    MessageBox.Show("Будь ласка, введіть коректні числові значення для віку",
                                  "Помилка введення",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }

                // Фільтр по ID рослини
                if (cmbGeografia.SelectedIndex > 0)
                {
                    if (!string.IsNullOrEmpty(strFilter)) strFilter += " AND ";
                    strFilter += $"`ID roslyny` = {cmbGeografia.SelectedItem}";
                }

                // Застосування фільтра
                h.bs1.Filter = strFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка фільтрації: {ex.Message}", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnFilterCancel_Click(object sender, EventArgs e)
        {
            ResetFilters(); 
        }
        private void ResetFilters()
        {
            txtNameRosl.Text = "";
            cmbGeografia.SelectedIndex = 0;

            if (h.bs1.DataSource is DataTable dataTable && dataTable.Rows.Count > 0)
            {
                var minMax = GetMinMaxVikFromExistingData(dataTable);
                txtVikFrom.Text = minMax.min.ToString();
                txtVikTo.Text = minMax.max.ToString();
            }

            // Скидаємо фільтр
            h.bs1.Filter = "";
        }
        private (int min, int max) GetMinMaxVikFromExistingData(DataTable dataTable)
        {
            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Vik roslyny"] != DBNull.Value && int.TryParse(row["Vik roslyny"].ToString(), out int vik))
                {
                    min = Math.Min(min, vik);
                    max = Math.Max(max, vik);
                }
            }

            return (min == int.MaxValue ? 0 : min, max == int.MinValue ? 0 : max);
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {
            AddNewRoslyny addNewRoslyny = new AddNewRoslyny();
            addNewRoslyny.ShowDialog();
            RefreshData();
        }
        public void RefreshData()
        {
            h.bs1.DataSource = h.myfunDt("SELECT * FROM sqlipz24_2_kss.рослинии");
            dataGridView1.DataSource = h.bs1;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (AddNewRoslyny addNewRoslyny = new AddNewRoslyny())
            {
                if (addNewRoslyny.ShowDialog() == DialogResult.OK) // Якщо запис був доданий
                {
                    RefreshData(); // Оновлюємо таблицю
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DeleteRoslyny daleteRoslyny = new DeleteRoslyny(RefreshData);
            daleteRoslyny.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
          
            EditRoslyny editRoslyny = new EditRoslyny(RefreshData);
            editRoslyny.ShowDialog();
           
        }

        public Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            try
            {
                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    return Image.FromStream(ms);
                }
            }
            catch
            {
                return null;
            }
        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var photoCell = dataGridView1.Rows[e.RowIndex].Cells["photo"]; // Ensure "photo" matches your column name    
                if (photoCell.Value != null && photoCell.Value != DBNull.Value)
                {
                    pictureBox1.Image = ByteArrayToImage((byte[])photoCell.Value); // Correctly converts byte[] to Image  
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(@"C:\Users\s8774\Desktop\Знімок екрана 2025-05-19 223328.png"); // Use Image.FromFile for file paths  
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private string path = @"C:\Users\s8774\Desktop\Нова папка"; // Define the 'path' variable with a default value

        private void btnStream_Click(object sender, EventArgs e)
        {
            var srcEcoding = Encoding.GetEncoding(1251);
            // Визначаємо розширення файлу  
            string extension;
            if (rdobtn_tsv.Checked)
                extension = "tsv";
            else if (rdobtn_doc.Checked)
                extension = "doc";
            else if (rdobtn_xls.Checked)
                extension = "xls";  // Виправлено "ils" на "xls"  
            else
                extension = "txt";

            // Формуємо шлях до файлу  
            string fileName = Path.Combine(path, $"exported_data.{extension}"); // Fixed incorrect usage of h.myfunDt  

            // Видаляємо існуючий файл, якщо він є  
            if (File.Exists(fileName))
                File.Delete(fileName);

            // Створюємо StreamWriter з кодуванням Windows-1251  
            StreamWriter wr = new StreamWriter(fileName, false, Encoding.GetEncoding(1251));

            try
            {
                // Записуємо заголовки стовпців  
                wr.Write("ID" + "\t" + "Вік" + "\t" + "Вид рослини" + "\t" + "Назва рослини" + "\t" + "Географія рослини" + "\t");
                wr.WriteLine();

                // Перевіряємо, чи DataSource є DataTable  
                if (h.bs1.DataSource is DataTable dataTable)
                {
                    // Записуємо назви стовпців з DataTable  
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        wr.Write(column.ColumnName + "\t");
                    }
                    wr.WriteLine();

                    // Записуємо дані з DataTable  
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            if (item != null && item != DBNull.Value)
                            {
                                // Обробка BLOB-даних (зображень)  
                                if (item is byte[])
                                    wr.Write("[BLOB DATA]" + "\t");

                                // Обробка дати  
                                else if (item is DateTime dateTime)
                                    wr.Write(dateTime.ToString("dd/MM/yyyy") + "\t");

                                // Обробка чисел  
                                else if (item is double number)
                                    wr.Write(number.ToString() + "\t");

                                // Обробка тексту  
                                else
                                    wr.Write(item.ToString() + "\t");
                            }
                            else
                            {
                                wr.Write("\t"); // Пусте значення  
                            }
                        }
                        wr.WriteLine();
                    }
                }
                else
                {
                    MessageBox.Show("DataSource is not a DataTable.", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при експорті: {ex.Message}", "Помилка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                wr.Close(); // Закриваємо файл  
            }

            MessageBox.Show("Експорт успішно завершено", "Успіх",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOLEDB_Click(object sender, EventArgs e)
        {
            if (!(h.bs1.DataSource is DataTable dataTable))
            {
                MessageBox.Show("DataSource is not a valid DataTable.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fileName = Path.Combine(path, "OLEDB.xls");

            // Remove existing file if it exists  
            if (File.Exists(fileName))
                File.Delete(fileName);

            // Connection string for Excel  
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName +
                                      ";Mode=ReadWrite;Extended Properties='Excel 8.0;HDR=YES'";

            // SQL command to create a table in Excel  
            string commandCreateOleDb = "CREATE TABLE [MySheet] ([" + dataTable.Columns[0].ColumnName + "] int";
            for (int i = 1; i < dataTable.Columns.Count; i++)
            {
                commandCreateOleDb += ", [" + dataTable.Columns[i].ColumnName + "] string";
            }
            commandCreateOleDb += ")";

            // Connect to Excel via OLEDB  
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                // Command to create the table  
                using (OleDbCommand cmd = new OleDbCommand(commandCreateOleDb, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery(); // Create the table in Excel  

                        // Add data to the table  
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            cmd.CommandText = "INSERT INTO [MySheet] VALUES(" + Convert.ToString(dataTable.Rows[i][0]);

                            for (int j = 1; j < dataTable.Columns.Count; j++)
                            {
                                if (dataTable.Rows[i][j] == null || dataTable.Rows[i][j] == DBNull.Value)
                                {
                                    cmd.CommandText += ", NULL";
                                }
                                else if (dataTable.Columns[j].DataType == typeof(string))
                                {
                                    cmd.CommandText += ", '" + Convert.ToString(dataTable.Rows[i][j]).Replace("'", "''") + "'";
                                }
                                else if (dataTable.Columns[j].DataType == typeof(int) ||
                                         dataTable.Columns[j].DataType == typeof(double) ||
                                         dataTable.Columns[j].DataType == typeof(decimal))
                                {
                                    cmd.CommandText += ", " + Convert.ToString(dataTable.Rows[i][j]);
                                }
                                else if (dataTable.Columns[j].DataType == typeof(DateTime))
                                {
                                    cmd.CommandText += ", '" + Convert.ToDateTime(dataTable.Rows[i][j]).ToString("yyyy-MM-dd") + "'";
                                }
                                else
                                {
                                    cmd.CommandText += ", '" + Convert.ToString(dataTable.Rows[i][j]) + "'";
                                }
                            }
                            cmd.CommandText += ")";

                            cmd.ExecuteNonQuery(); // Execute INSERT  
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при експортуванні: {ex.Message}", "Помилка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            MessageBox.Show("Дані експортовіні в Excel через  OLEDB", "Успіх",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnComObject_Click(object sender, EventArgs e)
        {

            string fileName = Path.Combine(path, "Roslyny_COM.xlsx");

            // Видаляємо існуючий файл
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // Створюємо новий екземпляр Excel
                excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false; // Вимкнути сповіщення

                // Додаємо нову книгу
                workbook = excelApp.Workbooks.Add(Type.Missing);
                worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "Рослини"; // Перейменовуємо аркуш

                // Отримуємо DataTable з BindingSource
                DataTable dataTable = h.bs1.DataSource as DataTable;
                if (dataTable == null)
                {
                    MessageBox.Show("Немає даних для експорту", "Помилка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Записуємо заголовки стовпців
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[1, j + 1] = dataTable.Columns[j].ColumnName;
                }

                // Записуємо дані
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        if (dataTable.Columns[j].DataType == typeof(byte[]))
                        {
                            worksheet.Cells[i + 2, j + 1] = "[ФОТО]";
                        }
                        else if (dataTable.Columns[j].DataType == typeof(DateTime))
                        {
                            worksheet.Cells[i + 2, j + 1] = Convert.ToDateTime(dataTable.Rows[i][j]);
                            ((Excel.Range)worksheet.Cells[i + 2, j + 1]).NumberFormat = "dd.MM.yyyy";
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
                        }
                    }
                }

                // Форматування таблиці
                FormatExcelWorksheet(worksheet, dataTable.Rows.Count, dataTable.Columns.Count);

                // Зберігаємо файл
                workbook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookDefault,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Excel.XlSaveAsAccessMode.xlNoChange);

                MessageBox.Show("Дані успішно експортовано в Excel через COM", "Успіх",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при експорті: {ex.Message}", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Закриваємо Excel
                if (workbook != null)
                {
                    workbook.Close(false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }

                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }

                // Очищаємо COM-об'єкти
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void FormatExcelWorksheet(Excel.Worksheet worksheet, int rowCount, int columnCount)
        {
            // Форматуємо заголовки
            Excel.Range headerRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, columnCount]];
            headerRange.Font.Bold = true;
            headerRange.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Форматуємо всі дані
            Excel.Range dataRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[rowCount + 1, columnCount]];
            dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

            // Автопідбір ширини стовпців
            dataRange.Columns.AutoFit();
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            try
            {
                // Отримуємо DataTable з BindingSource
                DataTable dataTable = h.bs1.DataSource as DataTable;
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Немає даних для експорту", "Попередження",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Створюємо шлях для XML-файлу
                string fileName = Path.Combine(path, "Roslyny_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");

                // Видаляємо існуючий файл, якщо він є
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Зберігаємо дані у XML-файл зі схемою
                dataTable.WriteXml(fileName, XmlWriteMode.WriteSchema);

                MessageBox.Show($"Дані успішно експортовано у XML-файл:\n{fileName}", "Успіх",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при експорті в XML: {ex.Message}", "Помилка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
