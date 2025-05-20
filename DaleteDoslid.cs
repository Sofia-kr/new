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
    public partial class DaleteDoslid : Form
    {
        private readonly Action _refreshCallback;
        public DaleteDoslid(Action refreshCallback)
        {
            InitializeComponent();
            _refreshCallback = refreshCallback;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAge.Text, out int id))
            {
                MessageBox.Show("Введіть коректний id", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = $"DELETE FROM дослідження WHERE `ID Doslid` = {id}";
            DataTable result = h.myfunDt(query);

            _refreshCallback?.Invoke();
            MessageBox.Show("Видалення успішно виконано", "Результат",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
