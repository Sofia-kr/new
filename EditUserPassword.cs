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
    public partial class EditUserPassword : Form
    {
        DataTable dtusername;
        public EditUserPassword()
        {
            InitializeComponent();
        }

        private void EditUserType_Load(object sender, EventArgs e)
        {
            dtusername = h.myfunDt("SELECT * FROM userName");

            for (int i = 0; i < dtusername.Rows.Count; i++)
            {
                cmbNameUser.Items.Add(dtusername.Rows[i][1].ToString());
            }
            cmbNameUser.Text = dtusername.Rows[0][1].ToString();
        }
        

        private void btnEditPassword_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка совпадения паролей и их наличия
                if (string.IsNullOrWhiteSpace(txtPasswordUser.Text) ||
                    !txtPasswordUser.Text.Equals(txtPassword2User.Text))
                {
                    MessageBox.Show("Паролі не співпадають \n або не введені!",
                                  "Помилка!",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                    txtPasswordUser.Focus();
                    return;
                }

                // Обновление пароля в базе данных
                string sqlCommand = "UPDATE userName SET Password = @password WHERE UserName = @userName";

                using (MySqlConnection connection = new MySqlConnection(h.ConStr))
                using (MySqlCommand command = new MySqlCommand(sqlCommand, connection))
                {
                    // Добавляем параметры для безопасного выполнения запроса
                    command.Parameters.AddWithValue("@password", h.EncriptedPassword_SHA256(txtPasswordUser.Text));
                    command.Parameters.AddWithValue("@userName", cmbNameUser.Text);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Пароль користувача '{cmbNameUser.Text}'\n успішно змінено!",
                                      "",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Не вдалося змінити пароль. Користувач не знайдений.",
                                      "Помилка",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка при зміні пароля: {ex.Message}",
                              "Помилка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
