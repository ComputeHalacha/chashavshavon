using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class frmKavuahs : Form
    {
        public frmKavuahs()
        {
            InitializeComponent();
            this.kavuahBindingSource.DataSource = Kavuah.KavuahsList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAddKavuah f = new frmAddKavuah();
            f.ShowDialog(this);
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.kavuahBindingSource.DataSource = Kavuah.KavuahsList;
                this.kavuahBindingSource.ResetBindings(false);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmKavuahs_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void dgvKavuahList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.kavuahBindingSource.Remove(this.dgvKavuahList.Rows[e.RowIndex].DataBoundItem);                
            }            
        }
    }
}
