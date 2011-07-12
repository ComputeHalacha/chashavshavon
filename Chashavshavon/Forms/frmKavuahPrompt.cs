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
    public partial class frmKavuahPrompt : Form
    {
        private List<Kavuah> _listToAdd = new List<Kavuah>();

        public frmKavuahPrompt(List<Kavuah> list)
        {
            InitializeComponent();
            this._listToAdd.AddRange(list);
            this.bindingSource1.DataSource = this._listToAdd;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.bindingSource1.EndEdit();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this._listToAdd.Clear();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void dgvKavuahList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.bindingSource1.Remove(this.dgvKavuahList.Rows[e.RowIndex].DataBoundItem);
            }
        }

        public List<Kavuah> ListToAdd
        {
            get
            {
                return this._listToAdd;
            }
        }                                
    }
}
