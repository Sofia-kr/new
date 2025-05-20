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
    public partial class AddNewDoslidnyk : Form
    {
        public int IdDoslidnyka { get; private set; }
        public string NameDoslidnyka { get; private set; }
        public string LastName { get; private set; }
        public string PlaceOfWork { get; private set; }
        public AddNewDoslidnyk()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(h.ConStr))
            {
                string idDoslidnyka = txtIdDoslidnyka.Text;
                string nameDoslidnyka = txtNameDoslidnyka.Text;
                string lastName = txtPlaceOfWork.Text;
                string placeOfWork = txtPlaceOfWork.Text;
                string query = $"INSERT INTO дослідник (`ID doslidnyka`, `Name doslidnyka`, `Last name`, `place of work`) " +
                               $"VALUES (@IdDoslidnyka, @NameDoslidnyka, @LastName, @PlaceOfWork)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdDoslidnyka", idDoslidnyka);
                cmd.Parameters.AddWithValue("@NameDoslidnyka", nameDoslidnyka);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@PlaceOfWork", placeOfWork);
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
