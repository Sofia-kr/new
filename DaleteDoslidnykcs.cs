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
    public partial class DaleteDoslidnykcs : Form
    {
        private readonly Action _refreshCallback;
        public DaleteDoslidnykcs(Action refreshCallback)
        {
            InitializeComponent();
            _refreshCallback = refreshCallback;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAge.Text, out int id))
            {
                MessageBox.Show("Введіть коректний вік", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = $"DELETE FROM дослідник WHERE `ID doslidnyka` = {id}";
            DataTable result = h.myfunDt(query);

            _refreshCallback?.Invoke();
            MessageBox.Show("Видалення успішно виконано", "Результат",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
