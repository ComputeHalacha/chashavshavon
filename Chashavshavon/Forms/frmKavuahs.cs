﻿using System;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class FrmKavuahs : Form
    {
        public FrmKavuahs()
        {
            this.InitializeComponent();
            this.kavuahBindingSource.DataSource = Program.KavuahList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Program.MainForm.AddNewKavuah(this))
            {
                this.kavuahBindingSource.DataSource = Program.KavuahList;
                this.kavuahBindingSource.ResetBindings(false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmKavuahs_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.kavuahBindingSource.EndEdit();
        }

        private void frmKavuahs_FormClosed(object sender, FormClosedEventArgs e)
        {
            //The owner form does a refresh when this closes, so this is how we tell it that we are closed
            this.DialogResult = DialogResult.OK;
        }

        private void dgvKavuahList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.kavuahBindingSource.Remove(this.dgvKavuahList.Rows[e.RowIndex].DataBoundItem);
                this.kavuahBindingSource.EndEdit();
            }
        }

        private void dgvKavuahList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is DateTime dt)
            {
                if (Program.HebrewCalendar.MinSupportedDateTime <= dt)
                {
                    e.Value = dt.ToString("dddd dd MMM yyyy", Program.CultureInfo);
                }
            }
        }
    }
}
