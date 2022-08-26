using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tahara;

namespace Chashavshavon
{
    public partial class FrmKavuahPrompt : Form
    {
        private readonly List<Kavuah> _listToAdd = new List<Kavuah>();

        public FrmKavuahPrompt(List<Kavuah> list)
        {
            InitializeComponent();
            _listToAdd.AddRange(list);
            bindingSource1.DataSource = _listToAdd;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bindingSource1.EndEdit();
            DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _listToAdd.Clear();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dgvKavuahList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                bindingSource1.Remove(dgvKavuahList.Rows[e.RowIndex].DataBoundItem);
            }
        }

        public List<Kavuah> ListToAdd => _listToAdd;
    }
}
