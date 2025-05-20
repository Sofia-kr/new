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

namespace ОБЗД
{
    public partial class AddNewRoslyny : Form
    {
        public int IdRoslyny { get; private set; }
        public string VikRoslyny { get; private set; }
        public string VydRoslyny { get; private set; }
        public string NazvaRoslyny { get; private set; }
        public string GeografiaRoslyny { get; private set; }
        public AddNewRoslyny()
        {
            InitializeComponent();
        }
        public static Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                // Return a default empty image or null  
                return Properties.Resources.Roslyny1; // Use an existing resource as a fallback  
            }

            try
            {
                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    return Image.FromStream(ms);
                }
            }
            catch
            {
                return Properties.Resources.Roslyny1; // Use an existing resource as a fallback  
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {// Add input validation
            if (string.IsNullOrWhiteSpace(txtNazvaRoslyny.Text))
            {
                MessageBox.Show("Будь ласка, введіть назву рослини");
                return;
            }

            // Convert image to bytes for storage
            byte[] imageBytes = null;
            if (pictureBox1.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    imageBytes = ms.ToArray();
                }
            }

            using (MySqlConnection con = new MySqlConnection(h.ConStr))
            {
                string query = @"INSERT INTO рослинии 
                        (`Vik roslyny`, `Vyd roslyny`, `Nazva roslyny`, `Geografia roslyny`, `photo`) 
                        VALUES (@VikRoslyny, @VydRoslyny, @NazvaRoslyny, @GeografiaRoslyny, @photo)";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@VikRoslyny", txtVikRoslyny.Text);
                cmd.Parameters.AddWithValue("@VydRoslyny", txtVydRoslyny.Text);
                cmd.Parameters.AddWithValue("@NazvaRoslyny", txtNazvaRoslyny.Text);
                cmd.Parameters.AddWithValue("@GeografiaRoslyny", txtGeografiaRoslyny.Text);
                cmd.Parameters.AddWithValue("@photo", imageBytes);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Дослідження успішно додано!", "Успіх",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Помилка: {ex.Message}", "Помилка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //using (MySqlConnection con = new MySqlConnection(h.ConStr))
            //{
            //    string idRoslyny = txtIdRoslyny.Text;
            //    string vikRoslyny = txtVikRoslyny.Text;
            //    string vydRoslyny = txtVydRoslyny.Text;
            //    string nazvaRoslyny = txtNazvaRoslyny.Text;
            //    string geografiaRoslyny = txtGeografiaRoslyny.Text;
            //    string pathToPhoto = h.pathToPhoto;
            //    string query = $"INSERT INTO рослинии (`ID roslyny`, `Vik roslyny`, `Vyd roslyny`, `Nazva roslyny`, `Geografia roslyny`,`photo`) " +
            //                   $"VALUES (@IdRoslyny, @VikRoslyny, @VydRoslyny, @NazvaRoslyny, @GeografiaRoslyny,@pathToPhoto)";
            //    MySqlCommand cmd = new MySqlCommand(query, con);
            //    cmd.Parameters.AddWithValue("@IdRoslyny", idRoslyny);
            //    cmd.Parameters.AddWithValue("@VikRoslyny", vikRoslyny);
            //    cmd.Parameters.AddWithValue("@VydRoslyny", vydRoslyny);
            //    cmd.Parameters.AddWithValue("@NazvaRoslyny", nazvaRoslyny);
            //    cmd.Parameters.AddWithValue("@GeografiaRoslyny", geografiaRoslyny);
            //    cmd.Parameters.AddWithValue("@pathToPhoto", pathToPhoto);
            //    con.Open();
            //    try
            //    {
            //        cmd.ExecuteNonQuery();
            //        MessageBox.Show("Дослідження успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    catch (MySqlException ex)
            //    {
            //        MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    con.Close();
            //}
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void AddNewRoslyny_Load(object sender, EventArgs e)
        {
            h.pathToPhoto = @"C:\Users\s8774\Desktop\Знімок екрана 2025-05-19 223328.png";
            pictureBox1.Image = Image.FromFile(h.pathToPhoto);
        }
    }
}

