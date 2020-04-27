namespace Chashavshavon
{
    partial class frmKavuahs
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKavuahs));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvKavuahList = new System.Windows.Forms.DataGridView();
            this.kavuahBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettingEntryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayNightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kavuahTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKavuahList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kavuahBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Lavender;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Narkisim", 11F);
            this.button2.ForeColor = System.Drawing.Color.Lavender;
            this.button2.Location = new System.Drawing.Point(139, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "חדש";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Narkisim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.SlateGray;
            this.label4.Location = new System.Drawing.Point(722, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 24);
            this.label4.TabIndex = 4;
            this.label4.Text = "רשימת וסת קבוע";
            // 
            // dgvKavuahList
            // 
            this.dgvKavuahList.AllowUserToAddRows = false;
            this.dgvKavuahList.AllowUserToOrderColumns = true;
            this.dgvKavuahList.AutoGenerateColumns = false;
            this.dgvKavuahList.BackgroundColor = System.Drawing.Color.White;
            this.dgvKavuahList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvKavuahList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.SaddleBrown;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKavuahList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKavuahList.ColumnHeadersHeight = 25;
            this.dgvKavuahList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnDelete,
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn,
            this.SettingEntryDate,
            this.activeDataGridViewCheckBoxColumn,
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn,
            this.notesDataGridViewTextBoxColumn,
            this.dayNightDataGridViewTextBoxColumn,
            this.kavuahTypeDataGridViewTextBoxColumn,
            this.numberDataGridViewTextBoxColumn});
            this.dgvKavuahList.DataSource = this.kavuahBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvKavuahList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvKavuahList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvKavuahList.Location = new System.Drawing.Point(22, 58);
            this.dgvKavuahList.Name = "dgvKavuahList";
            this.dgvKavuahList.RowHeadersVisible = false;
            this.dgvKavuahList.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.dgvKavuahList.RowTemplate.Height = 40;
            this.dgvKavuahList.Size = new System.Drawing.Size(930, 335);
            this.dgvKavuahList.TabIndex = 6;
            this.dgvKavuahList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKavuahList_CellContentClick);
            this.dgvKavuahList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvKavuahList_CellFormatting);
            // 
            // kavuahBindingSource
            // 
            this.kavuahBindingSource.DataSource = typeof(Tahara.Kavuah);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Lavender;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Narkisim", 11F);
            this.button1.ForeColor = System.Drawing.Color.Lavender;
            this.button1.Location = new System.Drawing.Point(22, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 33);
            this.button1.TabIndex = 7;
            this.button1.Text = "סגור";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(915, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.DataPropertyName = "SettingEntry";
            this.dataGridViewComboBoxColumn1.DisplayStyleForCurrentCellOnly = true;
            this.dataGridViewComboBoxColumn1.HeaderText = "ראיה הקובע";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Width = 180;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.DataPropertyName = "SettingEntry";
            this.dataGridViewComboBoxColumn2.DisplayStyleForCurrentCellOnly = true;
            this.dataGridViewComboBoxColumn2.HeaderText = "ראיה הקובע";
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.Width = 180;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.dgvKavuahList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(975, 413);
            this.panel1.TabIndex = 18;
            // 
            // btnDelete
            // 
            this.btnDelete.Frozen = true;
            this.btnDelete.HeaderText = "";
            this.btnDelete.Image = global::Chashavshavon.Properties.Resources.delete;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ReadOnly = true;
            this.btnDelete.ToolTipText = "מחק רשומה הזאת";
            this.btnDelete.Width = 40;
            // 
            // kavuahDescriptionHebrewDataGridViewTextBoxColumn
            // 
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.DataPropertyName = "KavuahDescriptionHebrew";
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.Frozen = true;
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.HeaderText = "סוג";
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.Name = "kavuahDescriptionHebrewDataGridViewTextBoxColumn";
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.ReadOnly = true;
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.Width = 57;
            // 
            // SettingEntryDate
            // 
            this.SettingEntryDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SettingEntryDate.DataPropertyName = "SettingEntryDate";
            this.SettingEntryDate.HeaderText = "ראיה הקובע";
            this.SettingEntryDate.Name = "SettingEntryDate";
            this.SettingEntryDate.ReadOnly = true;
            this.SettingEntryDate.Width = 112;
            // 
            // activeDataGridViewCheckBoxColumn
            // 
            this.activeDataGridViewCheckBoxColumn.DataPropertyName = "Active";
            this.activeDataGridViewCheckBoxColumn.HeaderText = "פעיל?";
            this.activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            this.activeDataGridViewCheckBoxColumn.Width = 50;
            // 
            // cancelsOnahBeinanisDataGridViewCheckBoxColumn
            // 
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.DataPropertyName = "CancelsOnahBeinanis";
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.HeaderText = "מבטל עונה בינונית?";
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.Name = "cancelsOnahBeinanisDataGridViewCheckBoxColumn";
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.Width = 139;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "הערות";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            this.notesDataGridViewTextBoxColumn.Width = 77;
            // 
            // dayNightDataGridViewTextBoxColumn
            // 
            this.dayNightDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dayNightDataGridViewTextBoxColumn.DataPropertyName = "DayNight";
            this.dayNightDataGridViewTextBoxColumn.HeaderText = "DayNight";
            this.dayNightDataGridViewTextBoxColumn.Name = "dayNightDataGridViewTextBoxColumn";
            this.dayNightDataGridViewTextBoxColumn.Visible = false;
            this.dayNightDataGridViewTextBoxColumn.Width = 99;
            // 
            // kavuahTypeDataGridViewTextBoxColumn
            // 
            this.kavuahTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.kavuahTypeDataGridViewTextBoxColumn.DataPropertyName = "KavuahType";
            this.kavuahTypeDataGridViewTextBoxColumn.HeaderText = "KavuahType";
            this.kavuahTypeDataGridViewTextBoxColumn.Name = "kavuahTypeDataGridViewTextBoxColumn";
            this.kavuahTypeDataGridViewTextBoxColumn.Visible = false;
            this.kavuahTypeDataGridViewTextBoxColumn.Width = 118;
            // 
            // numberDataGridViewTextBoxColumn
            // 
            this.numberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.numberDataGridViewTextBoxColumn.DataPropertyName = "Number";
            this.numberDataGridViewTextBoxColumn.HeaderText = "Number";
            this.numberDataGridViewTextBoxColumn.Name = "numberDataGridViewTextBoxColumn";
            this.numberDataGridViewTextBoxColumn.Visible = false;
            this.numberDataGridViewTextBoxColumn.Width = 91;
            // 
            // frmKavuahs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(975, 413);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKavuahs";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "רשימת וסת קבוע";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmKavuahs_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKavuahs_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKavuahList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kavuahBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource kavuahBindingSource;
        private System.Windows.Forms.DataGridView dgvKavuahList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewImageColumn btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn kavuahDescriptionHebrewDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettingEntryDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cancelsOnahBeinanisDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayNightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kavuahTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
    }
}