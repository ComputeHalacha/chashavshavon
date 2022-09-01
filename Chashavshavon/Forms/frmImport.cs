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
        public List<TaharaEvent> TaharaEventList { get; private set; }

        public FrmImport(string fileText)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(fileText))
            {
                fileText = "{}";
            }

            if (fileText.TrimStart().StartsWith("<"))
            {
                (EntryList, KavuahList) =
               Program.LoadEntriesKavuahsFromXml(fileText);
            }
            else if (fileText.TrimStart().StartsWith("{"))
            {
                (EntryList, KavuahList, TaharaEventList) =
               Program.LoadEntriesKavuahsFromJson(fileText);
            }

            foreach (Entry e in EntryList)
            {
                lvEntries.Items.Add(new ListViewItem(e.ToString()) { Tag = e });
            }
            foreach (Kavuah k in KavuahList)
            {
                lvKavuahs.Items.Add(new ListViewItem(k.ToString()) { Tag = k });
            }
            foreach (TaharaEvent t in TaharaEventList)
            {
                lvTaharaEvents.Items.Add(new ListViewItem(t.ToString()) { Tag = t });
            }

            _loading = false;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            Hide();
            EntryList.Clear();
            foreach (ListViewItem i in lvEntries.Items)
            {
                if (i.Checked)
                {
                    EntryList.Add((Entry)i.Tag);
                }
            }
            foreach (ListViewItem i in lvKavuahs.Items)
            {
                if (i.Checked)
                {
                    KavuahList.Add((Kavuah)i.Tag);
                }
            }
            foreach (ListViewItem i in lvTaharaEvents.Items)
            {
                if (i.Checked)
                {
                    TaharaEventList.Add((TaharaEvent)i.Tag);
                }
            }
            DialogResult = DialogResult.OK;
        }

        private void cbAllEntries_CheckedChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }
            bool prev = _loading;
            _loading = true;
            if (cbAllEntries.CheckState != CheckState.Indeterminate)
            {
                foreach (ListViewItem i in lvEntries.Items)
                {
                    i.Checked = cbAllEntries.Checked;
                }
            }
            _loading = prev;
        }

        private void cbAllKavuahs_CheckedChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }
            bool prev = _loading;
            _loading = true;
            if (cbAllKavuahs.CheckState != CheckState.Indeterminate)
            {
                foreach (ListViewItem i in lvKavuahs.Items)
                {
                    i.Checked = cbAllKavuahs.Checked;
                }
            }
            _loading = prev;
        }

        private void cbAllTaharaEvents_CheckedChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }
            bool prev = _loading;
            _loading = true;
            if (cbAllTaharaEvents.CheckState != CheckState.Indeterminate)
            {
                foreach (ListViewItem i in lvTaharaEvents.Items)
                {
                    i.Checked = cbAllTaharaEvents.Checked;
                }
            }
            _loading = prev;
        }

        private void lvEntries_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_loading)
            {
                return;
            }
            bool prev = _loading;
            _loading = true;
            IEnumerable<ListViewItem> lv = lvEntries.Items.Cast<ListViewItem>();
            if (lv.All(lvi => lvi.Checked))
            {
                cbAllEntries.Checked = true;
                cbAllEntries.CheckState = CheckState.Checked;
            }
            else if (lv.All(lvi => !lvi.Checked))
            {
                cbAllEntries.Checked = false;
                cbAllEntries.CheckState = CheckState.Unchecked;
            }
            else
            {
                cbAllEntries.Checked = false;
                cbAllEntries.CheckState = CheckState.Indeterminate;
            }
            _loading = prev;
        }

        private void lvKavuahs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_loading)
            {
                return;
            }
            bool prev = _loading;
            _loading = true;
            IEnumerable<ListViewItem> lv = lvKavuahs.Items.Cast<ListViewItem>();
            if (lv.All(lvi => lvi.Checked))
            {
                cbAllKavuahs.Checked = true;
                cbAllKavuahs.CheckState = CheckState.Checked;
            }
            else if (lv.All(lvi => !lvi.Checked))
            {
                cbAllKavuahs.Checked = false;
                cbAllKavuahs.CheckState = CheckState.Unchecked;
            }
            else
            {
                cbAllKavuahs.Checked = false;
                cbAllKavuahs.CheckState = CheckState.Indeterminate;
            }
            _loading = prev;
        }

        private void lvTaharaEvents_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_loading)
            {
                return;
            }
            bool prev = _loading;
            _loading = true;
            IEnumerable<ListViewItem> lv = lvTaharaEvents.Items.Cast<ListViewItem>();
            if (lv.All(lvi => lvi.Checked))
            {
                cbAllTaharaEvents.Checked = true;
                cbAllTaharaEvents.CheckState = CheckState.Checked;
            }
            else if (lv.All(lvi => !lvi.Checked))
            {
                cbAllTaharaEvents.Checked = false;
                cbAllTaharaEvents.CheckState = CheckState.Unchecked;
            }
            else
            {
                cbAllTaharaEvents.Checked = false;
                cbAllTaharaEvents.CheckState = CheckState.Indeterminate;
            }
            _loading = prev;

        }

        private void lvEntries_ItemActivate(object sender, EventArgs e)
        {
            bool isc = lvEntries.SelectedItems[0].Checked;
            lvEntries.SelectedItems[0].Checked = !isc;
        }

        private void lvKavuahs_ItemActivate(object sender, EventArgs e)
        {

            bool isc = lvKavuahs.SelectedItems[0].Checked;
            lvKavuahs.SelectedItems[0].Checked = !isc;
        }

        private void lvTaharaEvents_ItemActivate(object sender, EventArgs e)
        {
            bool isc = lvTaharaEvents.SelectedItems[0].Checked;
            lvTaharaEvents.SelectedItems[0].Checked = !isc;
        }
    }
}
