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
    public partial class AddNewUser : Form
    {
        DataTable dtuserName;
        bool nuser;
        public AddNewUser()
        {
            InitializeComponent();
        }

        private void AddNewUser_Load(object sender, EventArgs e)
        {
            dtuserName = h.myfunDt("SELECT * FROM userName");
        }

        private void txtNameUser_Leave(object sender, EventArgs e)
        {
            nuser = true;
            if (btnExit.Focused)
            {
                this.Close();
            }
            else
            {
                for (int i = 0; i < dtuserName.Rows.Count; i++)
                {
                    if (String.Equals(txtNameUser.Text.Trim(), dtuserName.Rows[i][1].ToString()) || String.Equals(txtNameUser.Text, ""))
                    {
                        nuser = false;
                        break;
                    }
                }
            }
            if (!nuser)
            {
                MessageBox.Show("Ім'я користувача не заповнено або вже існує!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNameUser.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTypeUser_Leave(object sender, EventArgs e)
        {
            if (btnExit.Focused)
            {
                this.Close();
                return;
            }

            // Перевірка введеного типу користувача
            if (!int.TryParse(txtTypeUser.Text, out int userType))
            {
                ShowTypeUserError("Не вірний тип користувача! Введіть число.");
                return;
            }

            if (userType < 0 || userType > 3)
            {
                ShowTypeUserError("Тип користувача повинен бути від 0 до 3.");
                return;
            }

            nuser = true;
     
        }
        private void ShowTypeUserError(string message)
        {
            nuser = false;
            MessageBox.Show(message, "УВАГА!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTypeUser.Focus();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            // Перевірка заповнення поля імені
            if (string.IsNullOrWhiteSpace(txtNameUser.Text))
            {
                ShowError("Поле 'Користувач' не заповнено!\nВиправте будь ласка...", txtNameUser);
                return;
            }

            // Перевірка заповнення поля типу
            if (string.IsNullOrWhiteSpace(txtTypeUser.Text) || !nuser)
            {
                ShowError("Тип доступу не заповнено або невірний!\nВиправте тип доступу...", txtTypeUser);
                return;
            }

            // Перевірка збігу паролів
            if (!string.Equals(txtPasswordUser.Text, txtPassword2User.Text))
            {
                ShowError("Паролі не співпадають!\nВиправте паролі...", txtPasswordUser);
                return;
            }

            // Додавання нового користувача до БД
            try
            {
                string sqlcmd = "INSERT INTO userName (UserName, Type, Password) VALUES (@P1, @P2, @P3)";

                using (MySqlConnection con = new MySqlConnection(h.ConStr))
                {
                    con.Open();
                    using (MySqlCommand cmdAdd = new MySqlCommand(sqlcmd, con))
                    {
                        cmdAdd.Parameters.AddWithValue("@P1", txtNameUser.Text);
                        cmdAdd.Parameters.AddWithValue("@P2", txtTypeUser.Text);
                        cmdAdd.Parameters.AddWithValue("@P3", h.EncriptedPassword_SHA256(txtPasswordUser.Text));
                        cmdAdd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Нового користувача '{txtNameUser.Text}'\nуспішно додано!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при додаванні користувача:\n{ex.Message}", "Помилка!",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ShowError(string message, Control controlToFocus)
        {
            MessageBox.Show(message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            controlToFocus.Focus();
        }

        private void txtTypeUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
