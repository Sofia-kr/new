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
    public partial class EditDoslid : Form
    {
        private readonly Action _refreshCallback;
        public EditDoslid(Action refreshCallback)
        {
            InitializeComponent();
            _refreshCallback = refreshCallback;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSetStatus.Text))
            {
                MessageBox.Show("Введіть значення для зміни (Статус)", "Попередження",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtWhere.Text))
            {
                MessageBox.Show("Введіть умову зміни (ID дослідження)", "Попередження",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSetData.Text))
            {
                MessageBox.Show("Введіть умову зміни (Дата)", "Попередження",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = $"UPDATE дослідження SET " +
                        $"`Data doslid` = '{txtSetData.Text.Replace("'", "''")}', " +
                        $"`Status doslid` = '{txtSetStatus.Text.Replace("'", "''")}' " +
                        $"WHERE `ID Doslid` = {txtWhere.Text}";

            h.myfunDt(query);
            _refreshCallback?.Invoke();

            MessageBox.Show("Дані успішно оновлено", "Успіх",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditDoslid_Load(object sender, EventArgs e)
        {

        }
    }
}
