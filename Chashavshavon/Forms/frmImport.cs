using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Tahara;

namespace Chashavshavon
{
    public partial class FrmImport : Form
    {
        private bool _loading = true;

        public List<Entry> EntryList { get; private set; }
        public List<Kavuah> KavuahList { get; private set; }

        public FrmImport(string path)
        {
            this.InitializeComponent();

            if (File.Exists(path))
            {
                string fileText = File.ReadAllText(path);
                if (string.IsNullOrEmpty(fileText))
                {
                    fileText = "<Entries />";
                }
                var lists = Program.LoadFromText(fileText);
                if(lists != default)
                {
                    (this.EntryList, this.KavuahList) = lists;
                }
                foreach (Entry e in this.EntryList)
                {
                    this.lvEntries.Items.Add(new ListViewItem(e.ToString()) { Tag = e });
                }
                foreach (Kavuah k in this.KavuahList)
                {
                    this.lvKavuahs.Items.Add(new ListViewItem(k.ToString()) { Tag = k });
                }
            }
            this._loading = false;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.EntryList.Clear();
            foreach (ListViewItem i in this.lvEntries.Items)
            {
                if (i.Checked)
                {
                    this.EntryList.Add((Entry)i.Tag);
                }
            }
            foreach (ListViewItem i in this.lvKavuahs.Items)
            {
                if (i.Checked)
                {
                    this.KavuahList.Add((Kavuah)i.Tag);
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void cbAllEntries_CheckedChanged(object sender, EventArgs e)
        {
            if (this._loading)
            {
                return;
            }
            bool prev = this._loading;
            this._loading = true;
            if (this.cbAllEntries.CheckState != CheckState.Indeterminate)
            {
                foreach (ListViewItem i in this.lvEntries.Items)
                {
                    i.Checked = this.cbAllEntries.Checked;
                }
            }
            this._loading = prev;
        }

        private void cbAllKavuahs_CheckedChanged(object sender, EventArgs e)
        {
            if (this._loading)
            {
                return;
            }
            bool prev = this._loading;
            this._loading = true;
            if (this.cbAllKavuahs.CheckState != CheckState.Indeterminate)
            {
                foreach (ListViewItem i in this.lvKavuahs.Items)
                {
                    i.Checked = this.cbAllKavuahs.Checked;
                }
            }
            this._loading = prev;
        }

        private void lvEntries_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this._loading)
            {
                return;
            }
            bool prev = this._loading;
            this._loading = true;
            IEnumerable<ListViewItem> lv = this.lvEntries.Items.Cast<ListViewItem>();
            if (lv.All(lvi => lvi.Checked))
            {
                this.cbAllEntries.Checked = true;
                this.cbAllEntries.CheckState = CheckState.Checked;
            }
            else if (lv.All(lvi => !lvi.Checked))
            {
                this.cbAllEntries.Checked = false;
                this.cbAllEntries.CheckState = CheckState.Unchecked;
            }
            else
            {
                this.cbAllEntries.Checked = false;
                this.cbAllEntries.CheckState = CheckState.Indeterminate;
            }
            this._loading = prev;
        }

        private void lvKavuahs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this._loading)
            {
                return;
            }
            bool prev = this._loading;
            this._loading = true;
            IEnumerable<ListViewItem> lv = this.lvKavuahs.Items.Cast<ListViewItem>();
            if (lv.All(lvi => lvi.Checked))
            {
                this.cbAllKavuahs.Checked = true;
                this.cbAllKavuahs.CheckState = CheckState.Checked;
            }
            else if (lv.All(lvi => !lvi.Checked))
            {
                this.cbAllKavuahs.Checked = false;
                this.cbAllKavuahs.CheckState = CheckState.Unchecked;
            }
            else
            {
                this.cbAllKavuahs.Checked = false;
                this.cbAllKavuahs.CheckState = CheckState.Indeterminate;
            }
            this._loading = prev;
        }

        private void lvEntries_ItemActivate(object sender, EventArgs e)
        {
            bool isc = this.lvEntries.SelectedItems[0].Checked;
            this.lvEntries.SelectedItems[0].Checked = !isc;
        }

        private void lvKavuahs_ItemActivate(object sender, EventArgs e)
        {

            bool isc = this.lvKavuahs.SelectedItems[0].Checked;
            this.lvKavuahs.SelectedItems[0].Checked = !isc;
        }
    }
}
