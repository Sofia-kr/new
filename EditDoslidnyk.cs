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
    public partial class EditDoslidnyk : Form
    {
        private readonly Action _refreshCallback;
        public EditDoslidnyk(Action refreshCallback)
        {
            InitializeComponent();
            _refreshCallback = refreshCallback;
        }

       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSetName.Text))
            {
                MessageBox.Show("Введіть значення для зміни (Місця роботи)", "Попередження",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtWhere.Text))
            {
                MessageBox.Show("Введіть умову ID дослідника", "Попередження",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSerVyd.Text))
            {
                MessageBox.Show("Введіть умову зміни (Прізвище)", "Попередження",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSetVik.Text))
            {
                MessageBox.Show("Введіть умову зміни (Ім'я)", "Попередження",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"UPDATE дослідник SET " +
                $"`Name doslidnyka` = '{txtSetVik.Text.Replace("'", "''")}', " +
                $"`Last name` = '{txtSerVyd.Text.Replace("'", "''")}', " +
                $"`place of work` = '{txtSetName.Text.Replace("'", "''")}' " +
                $"WHERE `ID doslidnyka` = {txtWhere.Text}";

            h.myfunDt(query);
            _refreshCallback?.Invoke();

            MessageBox.Show("Дані оновлено", "Успіх",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

           
        }
    }
}
