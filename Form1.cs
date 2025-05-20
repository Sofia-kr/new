using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ОБЗД
{
    public partial class Form1 : Form
    {

        private readonly string _connectionString;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
        }
        public bool CreateBackup(out string message)
        {
            string backupSuffix = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                using (var command = new MySqlCommand("backup_database", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@backup_suffix", backupSuffix);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        message = reader.GetString(0);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = $"Помилка при створенні резервної копії: {ex.Message}";
                return false;
            }

            message = "Резервну копію створено, але не вдалося отримати підтвердження від сервера";
            return false;
        }
    
        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void проПрограмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Калькулятор calc = new Калькулятор();
            calc.ShowDialog();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void таблиціБДToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void OpenTableDoslid_Click(object sender, EventArgs e)
        {
            Doslid doslid = new Doslid();
            doslid.ShowDialog();
        }

        private void OpenTableDoslidnyk_Click(object sender, EventArgs e)
        {
            Doslidnyk doslidnyk = new Doslidnyk();
            doslidnyk.ShowDialog();
        }

        private void OpenTableRoslyny_Click(object sender, EventArgs e)
        {
            Roslyny roslyny = new Roslyny();
            roslyny.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show($"Поточний тип користувача: {h.typeUser ?? "null"}");
            CheckAdminAccess();
        }
        private void CheckAdminAccess()
        {
            try
            {
                // Перевіряємо, чи тип користувача встановлений і чи це адміністратор (наприклад, тип 1)
                bool isAdmin = !string.IsNullOrEmpty(h.typeUser) && h.typeUser == "Admin";

                // Встановлюємо видимість та доступність меню
                адмініструванняToolStripMenuItem.Enabled = isAdmin;
                адмініструванняToolStripMenuItem.Visible = isAdmin;

                // Додатково можна логувати для діагностики
                Console.WriteLine($"Користувач тип: {h.typeUser}, Доступ адміністратора: {isAdmin}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка перевірки прав доступу: {ex.Message}");
                адмініструванняToolStripMenuItem.Enabled = false;
                адмініструванняToolStripMenuItem.Visible = false;
            }
        }
        private bool IsAdmin()
        {
            return !string.IsNullOrEmpty(h.typeUser) && h.typeUser == "Admin";
        }
        private void адмініструванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsAdmin())
            {
                MessageBox.Show("Доступ заборонено. Необхідні права адміністратора.",
                              "Помилка доступу",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }
        }

        private void додатиКористувачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser addNewUser = new AddNewUser();
            addNewUser.ShowDialog();
        }

        private void видалитиКористувачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser();
            deleteUser.ShowDialog();
        }

        private void змінитиПарольКористувачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditUserPassword editUserPassword = new EditUserPassword();
            editUserPassword.ShowDialog();
        }

        private void змінитиТипДоступуКористувачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditUserType editUserType = new EditUserType();
            editUserType.ShowDialog();
        }

       
        private void резервнеКопіюванняБДToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(h.ConStr);
            MySqlCommand cmd = new MySqlCommand("call copyTablesBD", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Резервне копіювання успішно завершено!");
            }
            catch
            {
              MessageBox.Show("Помилка резервного копіювання БД!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void резервнеВідновленняБДToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MySqlConnection con = new MySqlConnection(h.ConStr);
            MySqlCommand cmd = new MySqlCommand("call restoreTablesBD", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Резервне відновлення успішно завершено!");
            }
            catch
            {
                MessageBox.Show("Помилка резервного відновлення БД!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }

}
