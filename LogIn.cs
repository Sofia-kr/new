using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace ОБЗД
{
    public partial class LogIn : Form
    {
        public string[,] matrix;
        DataTable dt;
        private DataGridView dataGridView1; 
        

        public LogIn()
        {
            InitializeComponent();
            h.ConStr = "server = 194.44.236.9; database = sqlipz24_2_kss; user = sqlipz24_2_kss; password = ipz24_kss; charset=cp1251;";
            dt = h.myfunDt("SELECT * FROM userName");

            int kilkz = dt.Rows.Count;
            matrix = new string[kilkz, 4];

            for (int i = 0; i < kilkz; i++)
            {
                matrix[i, 0] = dt.Rows[i].Field<int>("Id").ToString();
                matrix[i, 1] = dt.Rows[i].Field<string>("UserName");
                matrix[i, 2] = dt.Rows[i].Field<int>("Type").ToString();
                matrix[i, 3] = dt.Rows[i].Field<string>("Password");
                cmbUser.Items.Add(matrix[i, 1]);
            }
            cmbUser.Text = matrix[0, 1];
            txtPassword.UseSystemPasswordChar = true;
            cmbUser.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Avtorization();
        }
        private void Avtorization()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (String.Equals(cmbUser.Text.ToUpper(), matrix[i, 1].ToUpper()))
                {
                    if (String.Equals(h.EncriptedPassword_SHA256(txtPassword.Text), matrix[i, 3]))
                    {
                        // Перевірка типу користувача  
                        if (matrix[i, 2] == "1") // Припустимо, що "1" означає адміністратор  
                        {
                            h.typeUser = "Admin";
                        }
                        else if (matrix[i, 2] == "2")
                        {
                            h.typeUser = "User";
                        }
                        else
                        {
                            h.typeUser = "Guest";
                        }

                        h.nameUser = matrix[i, 1]; // Зберігаємо ім'я користувача  
                        this.Hide();
                        Form1 fi = new Form1();
                        fi.ShowDialog();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Невірний пароль!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            MessageBox.Show("Користувача не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
           

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Avtorization();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }
    }
    static class h
    {
        public static string ConStr { get; set; }
        public static string typeUser { get; set; }
        public static string nameUser { get; set; }

        public static string curValO { get; set; }
        public static string keyName { get; set; }
        public static BindingSource bs1 { get; set; }

        public static string pathToPhoto { get; set; }


        public static DataTable myfunDt(string commandString)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(h.ConStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(commandString, con))
                    try
                    {
                        con.Open();
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {

                                dt.Load(dr);
                            }
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Неможливо з'єднатися з SQL-сервером! \nПеревірте наявність інтернету... ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
            return dt;
        }
        //public static string EncriptedPassword_MD5(string s)
        //{
        //    if (string.Compare(s, "null", true) == 0)
        //        return "NULL";
        //    byte[] bytes = Encoding.Unicode.GetBytes(s);
        //    MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
        //    byte[] byteHasch = CSP.ComputeHash(bytes);
        //    string hasch = string.Empty;
        //    foreach (byte b in byteHasch)
        //        hasch += string.Format("{0:x2}", b);
        //    return hasch;
        //}
        public static string EncriptedPassword_SHA256(string s)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(s));
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }        
    }
}