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
    public partial class EditUserType : Form
    {
        DataTable dtusername; // Correctly declared and used  
        public EditUserType()
        {
            InitializeComponent();
        }

        private void EditUserType_Load(object sender, EventArgs e)
        {
            dtusername = h.myfunDt("SELECT * FROM userName");

            for (int i = 0; i < dtusername.Rows.Count; i++)
            {
                cmdNameUser.Items.Add(dtusername.Rows[i][1].ToString());
            }
            cmdNameUser.Text = dtusername.Rows[0][1].ToString();

            
            for (int i = 1; i <= 3; i++)
            {
                cmdTypeUser.Items.Add(i.ToString());
            }

            
            if (cmdTypeUser.Items.Count > 0)
            {
                cmdTypeUser.SelectedIndex = 0;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditTypeUser_Click(object sender, EventArgs e)
        {
            try
            {
               
                    string sqlCommand = "UPDATE userName SET Type = @userType WHERE UserName = @userName";

                    using (MySqlConnection connection = new MySqlConnection(h.ConStr))
                    using (MySqlCommand command = new MySqlCommand(sqlCommand, connection))
                    {
                        command.Parameters.AddWithValue("@userType", cmdTypeUser.Text);
                        command.Parameters.AddWithValue("@userName", cmdNameUser.Text);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        
                        
                            MessageBox.Show($"Тип користувача '{cmdNameUser.Text}'\n успішно змінено!",
                                          "Успіх",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);
                            this.Close();
                        
                    
                    }
            }
                
               
            
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка при зміні типу користувача: {ex.Message}",
                              "Помилка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }
    }
}
