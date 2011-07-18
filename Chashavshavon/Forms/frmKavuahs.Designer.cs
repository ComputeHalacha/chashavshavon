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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvKavuahList = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.entryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kavuahBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.StartingEntryDate = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayNightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kavuahTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKavuahList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kavuahBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.BurlyWood;
            this.button2.Font = new System.Drawing.Font("Narkisim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button2.Location = new System.Drawing.Point(672, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 27);
            this.button2.TabIndex = 3;
            this.button2.Text = "חדש";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Narkisim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(12, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "רשימת וסת קבוע";
            // 
            // dgvKavuahList
            // 
            this.dgvKavuahList.AllowUserToAddRows = false;
            this.dgvKavuahList.AllowUserToOrderColumns = true;
            this.dgvKavuahList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.activeDataGridViewCheckBoxColumn,
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn,
            this.StartingEntryDate,
            this.notesDataGridViewTextBoxColumn,
            this.dayNightDataGridViewTextBoxColumn,
            this.kavuahTypeDataGridViewTextBoxColumn,
            this.numberDataGridViewTextBoxColumn});
            this.dgvKavuahList.DataSource = this.kavuahBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvKavuahList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvKavuahList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvKavuahList.Location = new System.Drawing.Point(12, 55);
            this.dgvKavuahList.Name = "dgvKavuahList";
            this.dgvKavuahList.RowHeadersVisible = false;
            this.dgvKavuahList.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.dgvKavuahList.RowTemplate.Height = 40;
            this.dgvKavuahList.Size = new System.Drawing.Size(857, 346);
            this.dgvKavuahList.TabIndex = 6;
            this.dgvKavuahList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKavuahList_CellContentClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.BurlyWood;
            this.button1.Font = new System.Drawing.Font("Narkisim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button1.Location = new System.Drawing.Point(773, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 27);
            this.button1.TabIndex = 7;
            this.button1.Text = "סגור";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // entryBindingSource
            // 
            this.entryBindingSource.DataSource = typeof(Chashavshavon.Entry);
            // 
            // kavuahBindingSource
            // 
            this.kavuahBindingSource.DataSource = typeof(Chashavshavon.Kavuah);
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
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.DataPropertyName = "KavuahDescriptionHebrew";
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.Frozen = true;
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.HeaderText = "סוג";
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.Name = "kavuahDescriptionHebrewDataGridViewTextBoxColumn";
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.ReadOnly = true;
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.Width = 250;
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
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.DataPropertyName = "CancelsOnahBeinanis";
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.HeaderText = "מבטל עונה בינונית?";
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.Name = "cancelsOnahBeinanisDataGridViewCheckBoxColumn";
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.Width = 135;
            // 
            // StartingEntryDate
            // 
            this.StartingEntryDate.DataPropertyName = "StartingEntryDate";
            this.StartingEntryDate.DataSource = this.entryBindingSource;
            dataGridViewCellStyle2.Format = "dddd dd MMM yyyy";
            this.StartingEntryDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.StartingEntryDate.DisplayMember = "DateTime";
            this.StartingEntryDate.HeaderText = "ראיה הקובע";
            this.StartingEntryDate.Name = "StartingEntryDate";
            this.StartingEntryDate.ValueMember = "DateTime";
            this.StartingEntryDate.Width = 180;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "הערות";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            this.notesDataGridViewTextBoxColumn.Width = 198;
            // 
            // dayNightDataGridViewTextBoxColumn
            // 
            this.dayNightDataGridViewTextBoxColumn.DataPropertyName = "DayNight";
            this.dayNightDataGridViewTextBoxColumn.HeaderText = "DayNight";
            this.dayNightDataGridViewTextBoxColumn.Name = "dayNightDataGridViewTextBoxColumn";
            this.dayNightDataGridViewTextBoxColumn.Visible = false;
            // 
            // kavuahTypeDataGridViewTextBoxColumn
            // 
            this.kavuahTypeDataGridViewTextBoxColumn.DataPropertyName = "KavuahType";
            this.kavuahTypeDataGridViewTextBoxColumn.HeaderText = "KavuahType";
            this.kavuahTypeDataGridViewTextBoxColumn.Name = "kavuahTypeDataGridViewTextBoxColumn";
            this.kavuahTypeDataGridViewTextBoxColumn.Visible = false;
            // 
            // numberDataGridViewTextBoxColumn
            // 
            this.numberDataGridViewTextBoxColumn.DataPropertyName = "Number";
            this.numberDataGridViewTextBoxColumn.HeaderText = "Number";
            this.numberDataGridViewTextBoxColumn.Name = "numberDataGridViewTextBoxColumn";
            this.numberDataGridViewTextBoxColumn.Visible = false;
            // 
            // frmKavuahs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(880, 413);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvKavuahList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmKavuahs";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "רשימת וסת קבוע";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmKavuahs_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKavuahs_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKavuahList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kavuahBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource kavuahBindingSource;
        private System.Windows.Forms.DataGridView dgvKavuahList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource entryBindingSource;
        private System.Windows.Forms.DataGridViewImageColumn btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn kavuahDescriptionHebrewDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cancelsOnahBeinanisDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn StartingEntryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayNightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kavuahTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
    }
}