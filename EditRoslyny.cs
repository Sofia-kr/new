using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ОБЗД
{
    public partial class EditRoslyny : Form
    {
        private readonly Action _refreshCallback;
        public EditRoslyny(Action refreshCallback)
        {
            InitializeComponent();
            _refreshCallback = refreshCallback;
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
        public void ExecuteUpdateQuery(string query, Dictionary<string, object> parameters)
        {
            using (MySqlConnection con = new MySqlConnection(h.ConStr))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWhere.Text))
            {
                MessageBox.Show("Введіть умову зміни (WHERE)", "Попередження",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkBox1.Checked && (
                string.IsNullOrWhiteSpace(txtSetVik.Text) ||
                string.IsNullOrWhiteSpace(txtSetName.Text) ||
                string.IsNullOrWhiteSpace(txtSerVyd.Text) ||
                string.IsNullOrWhiteSpace(txtSetGeografia.Text)))
            {
                MessageBox.Show("Заповніть всі обов'язкові поля для зміни даних", "Попередження",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkBox2.Checked && (string.IsNullOrEmpty(h.pathToPhoto) || !File.Exists(h.pathToPhoto)))
            {
                MessageBox.Show("Виберіть коректне фото для заміни", "Попередження",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Ви впевнені, що хочете змінити дані?", "Підтвердження",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (checkBox1.Checked && !checkBox2.Checked)
                {
                    // Оновлення тільки даних (без фото)
                    string query = @"UPDATE рослинии SET 
                                `Vik roslyny` = @Vik,
                                `Vyd roslyny` = @Vyd,
                                `Nazva roslyny` = @Nazva,
                                `Geografia roslyny` = @Geografia
                                WHERE `ID roslyny` = @Id";

                    ExecuteUpdateQuery(query, new Dictionary<string, object>
                {
                    {"@Vik", txtSetVik.Text},
                    {"@Vyd", txtSerVyd.Text},
                    {"@Nazva", txtSetName.Text},
                    {"@Geografia", txtSetGeografia.Text},
                    {"@Id", txtWhere.Text}
                });
                }
                else if (!checkBox1.Checked && checkBox2.Checked)
                {
                    // Оновлення тільки фото
                    byte[] photoData = File.ReadAllBytes(h.pathToPhoto);
                    string query = "UPDATE рослинии SET photo = @File WHERE `ID roslyny` = @Id";

                    ExecuteUpdateQuery(query, new Dictionary<string, object>
                {
                    {"@File", photoData},
                    {"@Id", txtWhere.Text}
                });
                }
                else if (checkBox1.Checked && checkBox2.Checked)
                {
                    // Оновлення і даних, і фото
                    byte[] photoData = File.ReadAllBytes(h.pathToPhoto);
                    string query = @"UPDATE рослинии SET 
                                `Vik roslyny` = @Vik,
                                `Vyd roslyny` = @Vyd,
                                `Nazva roslyny` = @Nazva,
                                `Geografia roslyny` = @Geografia,
                                `photo` = @File
                                WHERE `ID roslyny` = @Id";

                    ExecuteUpdateQuery(query, new Dictionary<string, object>
                {
                    {"@Vik", txtSetVik.Text},
                    {"@Vyd", txtSerVyd.Text},
                    {"@Nazva", txtSetName.Text},
                    {"@Geografia", txtSetGeografia.Text},
                    {"@File", photoData},
                    {"@Id", txtWhere.Text}
                });
                }

                MessageBox.Show("Зміни успішно збережено");
                _refreshCallback?.Invoke();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні даних: {ex.Message}", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
               label1.Visible = true;
               txtSetVik.Visible = true;
               label5.Visible = true;
                txtSetGeografia.Visible = true;
                label3.Visible = true;
                txtSetName.Visible = true;
                label4.Visible = true;
                txtSerVyd.Visible = true;
                btnReplace.Visible = true;

            }
            else if (checkBox1.Checked == false)
            {
                label1.Visible = false;
                txtSetVik.Visible = false;
                label5.Visible = false;
                txtSetGeografia.Visible = false;
                label3.Visible = false;
                txtSetName.Visible = false;
                label4.Visible = false;
                txtSerVyd.Visible = false;
                btnReplace.Visible = false;
                if (checkBox2.Checked == false)
                {
                    btnReplace.Visible = false;
                }
            }
          
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OFD = new OpenFileDialog())
            {
                OFD.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                OFD.InitialDirectory = Application.StartupPath;

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    h.pathToPhoto = OFD.FileName;
                    pictureBox1.Image = Image.FromFile(h.pathToPhoto);
                }
            }
            
        }

        private void EditRoslyny_Load(object sender, EventArgs e)
        {
            h.pathToPhoto = @"C:\Users\s8774\Desktop\Знімок екрана 2025-05-19 223328.png";
            pictureBox1.Image = Image.FromFile(h.pathToPhoto);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                panel2.Visible = true;
                label6.Visible = true;
                button1.Visible = true;
                pictureBox1.Visible = true;
                btnReplace.Visible = true;
            }
            else if (checkBox2.Checked == false)
            {
                panel2.Visible = false;
                label6.Visible = false;
                button1.Visible = false;
                pictureBox1.Visible = false;
                if (checkBox1.Checked == false)
                {
                    btnReplace.Visible = false;
                }
            }
        }
    }
}
