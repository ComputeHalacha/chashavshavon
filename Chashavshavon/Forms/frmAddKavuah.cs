using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Chashavshavon.Utils;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class frmAddKavuah : Form
    {
        public frmAddKavuah()
        {
            InitializeComponent();
        }

        private void frmAddKavuah_Load(object sender, EventArgs e)
        {
            LoadIntervalNumbers();
        }        

        private void rbDayOfMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbDayOfMonth.Checked)
            {
                LoadHebrewNumbers();
                this.lblNumber.Text = "תבחר יום בחדש";
            }
            else
            {
                LoadIntervalNumbers();
                this.lblNumber.Text = "תבחר מספר ימים של ההפלגה";
            }
        }

        private void LoadHebrewNumbers()
        {
            this.cmbNumber.DataSource = null;
            this.cmbNumber.Items.Clear();
            string[] a = Zmanim.DaysOfMonthHebrew;
            for (int i = 1; i < a.Length; i++)
            {
                this.cmbNumber.Items.Add(a[i]);
            }
            this.cmbNumber.SelectedIndex = 0;
        }

        private void LoadIntervalNumbers()
        {
            this.cmbNumber.Items.Clear();
            int[] a = new int[300];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = i + 1;
            }
            this.cmbNumber.DataSource = a;
            this.cmbNumber.SelectedIndex = 28;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var kv = new Kavuah();
            kv.DayNight = this.rbDay.Checked ? DayNight.Day : DayNight.Night;
            kv.ProblemOnahType = this.rbInterval.Checked ? ProblemOnahType.Haflagah : ProblemOnahType.DayOfMonth;
            kv.Number = this.cmbNumber.SelectedIndex + 1;
            kv.Notes = this.tbNotes.Text;
            Kavuah.KavuahsList.Add(kv);
            Properties.Settings.Default.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
