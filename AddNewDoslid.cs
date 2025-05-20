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
    public partial class AddNewDoslid : Form
    {
        public DateTime DataDoslid { get; private set; }
        public string StatusDoslid { get; private set; }
        public int IdRoslyny { get; private set; }
        public int IdDoslidnyka { get; private set; }
        public AddNewDoslid()
        {
            InitializeComponent();
            
        }
        //private void SetPlaceholder(TextBox textBox, string placeholder)
        //{
        //    textBox.Text = placeholder;
        //    textBox.ForeColor = SystemColors.GrayText;
        //    textBox.Enter += (s, e) => {
        //        if (textBox.Text == placeholder)
        //        {
        //            textBox.Text = "";
        //            textBox.ForeColor = SystemColors.WindowText;
        //        }
        //    };
        //    textBox.Leave += (s, e) => {
        //        if (string.IsNullOrWhiteSpace(textBox.Text))
        //        {
        //            textBox.Text = placeholder;
        //            textBox.ForeColor = SystemColors.GrayText;
        //        }
        //    };
        //}
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(h.ConStr))
            {
                string IdDoslida = txtIdDoslid.Text;
                string dateDoslid = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string statusDoslid = txtStatus.Text;
                string idRoslyny = txtIdRoslyny.Text;
                string idDoslidnyka = txtIdDoslidnyka.Text;

                string query = $"INSERT INTO дослідження (`ID Doslid`, `Data doslid`, `Status doslid`, `ID roslyly`,`ID doslidnyka`) " +
                               $"VALUES (@IdDoslida, @DataDoslid,@StatusDoslid,@IdRoslyny,@IdDoslidnyka)";
                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@IdDoslida", IdDoslida);
                cmd.Parameters.AddWithValue("@DataDoslid", dateDoslid);
                cmd.Parameters.AddWithValue("@StatusDoslid", statusDoslid);
                cmd.Parameters.AddWithValue("@IdRoslyny", idRoslyny);
                cmd.Parameters.AddWithValue("@IdDoslidnyka", idDoslidnyka);
                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Дослідження успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            this.Close();   
        }

 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
