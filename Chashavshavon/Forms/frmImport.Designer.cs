namespace Chashavshavon
{
    partial class FrmImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvEntries = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvKavuahs = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbAllEntries = new System.Windows.Forms.CheckBox();
            this.cbAllKavuahs = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lvTaharaEvents = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbAllTaharaEvents = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnter.BackColor = System.Drawing.Color.Lavender;
            this.btnEnter.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.btnEnter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnter.Font = new System.Drawing.Font("Narkisim", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnEnter.ForeColor = System.Drawing.Color.Lavender;
            this.btnEnter.Location = new System.Drawing.Point(10, 11);
            this.btnEnter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(134, 35);
            this.btnEnter.TabIndex = 23;
            this.btnEnter.Text = "המשך";
            this.btnEnter.UseVisualStyleBackColor = false;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Lavender;
            this.btnClose.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Narkisim", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnClose.ForeColor = System.Drawing.Color.Lavender;
            this.btnClose.Location = new System.Drawing.Point(1164, 11);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 35);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "סגור";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1259, 70);
            this.panel2.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleBar;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(612, 23);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 42);
            this.panel1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.ForeColor = System.Drawing.Color.Lavender;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(631, 42);
            this.label2.TabIndex = 11;
            this.label2.Text = "ייבוא רשומות ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleBar;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(302, 23);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(306, 42);
            this.panel5.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.ForeColor = System.Drawing.Color.Lavender;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 42);
            this.label1.TabIndex = 11;
            this.label1.Text = "ייבוא רשומות קבוע ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnClose);
            this.panel4.Controls.Add(this.btnEnter);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 629);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1259, 56);
            this.panel4.TabIndex = 31;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(15, 76);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvEntries);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvTaharaEvents);
            this.splitContainer1.Panel2.Controls.Add(this.lvKavuahs);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer1.Size = new System.Drawing.Size(1233, 519);
            this.splitContainer1.SplitterDistance = 631;
            this.splitContainer1.TabIndex = 32;
            // 
            // lvEntries
            // 
            this.lvEntries.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvEntries.CheckBoxes = true;
            this.lvEntries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEntries.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lvEntries.ForeColor = System.Drawing.Color.DimGray;
            this.lvEntries.FullRowSelect = true;
            this.lvEntries.GridLines = true;
            this.lvEntries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvEntries.HideSelection = false;
            this.lvEntries.HotTracking = true;
            this.lvEntries.HoverSelection = true;
            this.lvEntries.Location = new System.Drawing.Point(0, 0);
            this.lvEntries.Name = "lvEntries";
            this.lvEntries.RightToLeftLayout = true;
            this.lvEntries.Size = new System.Drawing.Size(631, 519);
            this.lvEntries.TabIndex = 0;
            this.lvEntries.UseCompatibleStateImageBehavior = false;
            this.lvEntries.View = System.Windows.Forms.View.Details;
            this.lvEntries.ItemActivate += new System.EventHandler(this.lvEntries_ItemActivate);
            this.lvEntries.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvEntries_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 500;
            // 
            // lvKavuahs
            // 
            this.lvKavuahs.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvKavuahs.CheckBoxes = true;
            this.lvKavuahs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvKavuahs.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lvKavuahs.ForeColor = System.Drawing.Color.DimGray;
            this.lvKavuahs.FullRowSelect = true;
            this.lvKavuahs.GridLines = true;
            this.lvKavuahs.HideSelection = false;
            this.lvKavuahs.HotTracking = true;
            this.lvKavuahs.HoverSelection = true;
            this.lvKavuahs.Location = new System.Drawing.Point(292, 0);
            this.lvKavuahs.Name = "lvKavuahs";
            this.lvKavuahs.RightToLeftLayout = true;
            this.lvKavuahs.Size = new System.Drawing.Size(306, 519);
            this.lvKavuahs.TabIndex = 1;
            this.lvKavuahs.UseCompatibleStateImageBehavior = false;
            this.lvKavuahs.View = System.Windows.Forms.View.Details;
            this.lvKavuahs.ItemActivate += new System.EventHandler(this.lvKavuahs_ItemActivate);
            this.lvKavuahs.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvKavuahs_ItemChecked);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 500;
            // 
            // cbAllEntries
            // 
            this.cbAllEntries.AutoSize = true;
            this.cbAllEntries.BackColor = System.Drawing.Color.Transparent;
            this.cbAllEntries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAllEntries.Location = new System.Drawing.Point(15, 601);
            this.cbAllEntries.Name = "cbAllEntries";
            this.cbAllEntries.Size = new System.Drawing.Size(109, 22);
            this.cbAllEntries.TabIndex = 28;
            this.cbAllEntries.Text = "כל הרשומות";
            this.cbAllEntries.UseVisualStyleBackColor = false;
            this.cbAllEntries.CheckedChanged += new System.EventHandler(this.cbAllEntries_CheckedChanged);
            // 
            // cbAllKavuahs
            // 
            this.cbAllKavuahs.AutoSize = true;
            this.cbAllKavuahs.BackColor = System.Drawing.Color.Transparent;
            this.cbAllKavuahs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAllKavuahs.Location = new System.Drawing.Point(650, 601);
            this.cbAllKavuahs.Name = "cbAllKavuahs";
            this.cbAllKavuahs.Size = new System.Drawing.Size(109, 22);
            this.cbAllKavuahs.TabIndex = 28;
            this.cbAllKavuahs.Text = "כל הרשומות";
            this.cbAllKavuahs.UseVisualStyleBackColor = false;
            this.cbAllKavuahs.CheckedChanged += new System.EventHandler(this.cbAllKavuahs_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleBar;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(10, 23);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(288, 42);
            this.panel3.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.ForeColor = System.Drawing.Color.Lavender;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 42);
            this.label3.TabIndex = 11;
            this.label3.Text = "ייבוא אירועי טהרה";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvTaharaEvents
            // 
            this.lvTaharaEvents.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvTaharaEvents.CheckBoxes = true;
            this.lvTaharaEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lvTaharaEvents.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lvTaharaEvents.ForeColor = System.Drawing.Color.DimGray;
            this.lvTaharaEvents.FullRowSelect = true;
            this.lvTaharaEvents.GridLines = true;
            this.lvTaharaEvents.HideSelection = false;
            this.lvTaharaEvents.HotTracking = true;
            this.lvTaharaEvents.HoverSelection = true;
            this.lvTaharaEvents.Location = new System.Drawing.Point(0, 0);
            this.lvTaharaEvents.Name = "lvTaharaEvents";
            this.lvTaharaEvents.RightToLeftLayout = true;
            this.lvTaharaEvents.Size = new System.Drawing.Size(288, 519);
            this.lvTaharaEvents.TabIndex = 2;
            this.lvTaharaEvents.UseCompatibleStateImageBehavior = false;
            this.lvTaharaEvents.View = System.Windows.Forms.View.Details;
            this.lvTaharaEvents.ItemActivate += new System.EventHandler(this.lvTaharaEvents_ItemActivate);
            this.lvTaharaEvents.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTaharaEvents_ItemChecked);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 500;
            // 
            // cbAllTaharaEvents
            // 
            this.cbAllTaharaEvents.AutoSize = true;
            this.cbAllTaharaEvents.BackColor = System.Drawing.Color.Transparent;
            this.cbAllTaharaEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAllTaharaEvents.Location = new System.Drawing.Point(960, 601);
            this.cbAllTaharaEvents.Name = "cbAllTaharaEvents";
            this.cbAllTaharaEvents.Size = new System.Drawing.Size(109, 22);
            this.cbAllTaharaEvents.TabIndex = 33;
            this.cbAllTaharaEvents.Text = "כל הרשומות";
            this.cbAllTaharaEvents.UseVisualStyleBackColor = false;
            this.cbAllTaharaEvents.CheckedChanged += new System.EventHandler(this.cbAllTaharaEvents_CheckedChanged);
            // 
            // FrmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1259, 685);
            this.Controls.Add(this.cbAllTaharaEvents);
            this.Controls.Add(this.cbAllKavuahs);
            this.Controls.Add(this.cbAllEntries);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ייבוא רשומות";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvEntries;
        private System.Windows.Forms.ListView lvKavuahs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox cbAllEntries;
        private System.Windows.Forms.CheckBox cbAllKavuahs;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvTaharaEvents;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.CheckBox cbAllTaharaEvents;
    }
}