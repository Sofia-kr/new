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
    public partial class DeleteUser : Form
    {
        DataTable dtusername; // Corrected variable name to match the existing declaration  

        public DeleteUser()
        {
            InitializeComponent();
        }

        private void DeleteUser_Load(object sender, EventArgs e)
        {
            dtusername = h.myfunDt("SELECT * FROM userName");

            for (int i = 0; i < dtusername.Rows.Count; i++)
            {
                cmbNameUser.Items.Add(dtusername.Rows[i][1].ToString());
            }
            cmbNameUser.Text = dtusername.Rows[0][1].ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
               
                
                    string sqlCommand = "DELETE FROM userName WHERE UserName = @userName";

                    using (MySqlConnection connection = new MySqlConnection(h.ConStr))
                    using (MySqlCommand command = new MySqlCommand(sqlCommand, connection))
                    {
                        command.Parameters.AddWithValue("@userName", cmbNameUser.Text);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Користувача '{cmbNameUser.Text}'\n успішно видалено!");
                            this.Close();
                        }
                    else
                    {
                        MessageBox.Show($"Ви не можете видалити користувача '{cmbNameUser.Text}'",
                                      "Увага!",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                        cmbNameUser.Focus();
                    }
                    }
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка при видаленні користувача: {ex.Message}",
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
