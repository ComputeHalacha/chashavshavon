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

        private void HaflagahTypeChanged(object sender, EventArgs e)
        {
            if (this.rbDayOfMonth.Checked)
            {
                LoadHebrewNumbers();
                this.lblNumber.Text = "תבחר יום בחדש";
            }
            else if (this.rbInterval.Checked)
            {
                LoadIntervalNumbers();
                this.lblNumber.Text = "תבחר מספר ימים של ההפלגה";
            }
            else if (rbDilugHaflagah.Checked)
            {
                LoadDilugNumbers();
                this.lblNumber.Text = "תבחר מספר ימי דילוג של ההפלגה";
            }
            else if (this.rbDilugDayOfMonth.Checked)
            {
                LoadDilugNumbers();
                this.lblNumber.Text = "תבחר מספר ימי דילוג של יום החודש";
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
            this.cmbNumber.DataSource = null;
            this.cmbNumber.Items.Clear();
            int[] a = new int[300];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = i + 1;
            }
            this.cmbNumber.DataSource = a;
            this.cmbNumber.SelectedIndex = 28;
        }

        private void LoadDilugNumbers()
        {
            this.cmbNumber.DataSource = null;
            this.cmbNumber.Items.Clear();
            int[] a = new int[60];
            for (int i = -29, j = 0; i < 29; i++, j++)
            {
                a[j] = i;
            }
            this.cmbNumber.DataSource = a;
            this.cmbNumber.SelectedIndex = 32;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.AddedKavuah = new Kavuah();
            this.AddedKavuah.DayNight = this.rbDay.Checked ? DayNight.Day : DayNight.Night;
            if (this.rbInterval.Checked)
            {
                this.AddedKavuah.KavuahType = KavuahType.Haflagah;
            }
            else if (this.rbDayOfMonth.Checked)
            {
                this.AddedKavuah.KavuahType = KavuahType.DayOfMonth;
            }
            else if (rbDilugHaflagah.Checked)
            {
                this.AddedKavuah.KavuahType = KavuahType.DilugHaflaga;
            }
            else if (this.rbDilugDayOfMonth.Checked)
            {
                this.AddedKavuah.KavuahType = KavuahType.DilugDayOfMonth;
            }
            this.AddedKavuah.Number = this.cmbNumber.SelectedIndex + 1;
            this.AddedKavuah.Active = this.cbActive.Checked;
            this.AddedKavuah.Notes = this.tbNotes.Text;

            Kavuah.KavuahsList.Add(this.AddedKavuah);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public Kavuah AddedKavuah { get; set; }
    }
}
