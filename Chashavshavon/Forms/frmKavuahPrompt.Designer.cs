namespace Chashavshavon
{
    partial class frmKavuahPrompt
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
            this.dgvKavuahList = new System.Windows.Forms.DataGridView();
            this.deleteColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isMaayanPauachDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayNightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kavuahTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKavuahList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvKavuahList
            // 
            this.dgvKavuahList.AllowUserToAddRows = false;
            this.dgvKavuahList.AutoGenerateColumns = false;
            this.dgvKavuahList.BackgroundColor = System.Drawing.Color.White;
            this.dgvKavuahList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvKavuahList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKavuahList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deleteColumn,
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn,
            this.activeDataGridViewCheckBoxColumn,
            this.isMaayanPauachDataGridViewCheckBoxColumn,
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn,
            this.notesDataGridViewTextBoxColumn,
            this.dayNightDataGridViewTextBoxColumn,
            this.kavuahTypeDataGridViewTextBoxColumn,
            this.numberDataGridViewTextBoxColumn});
            this.dgvKavuahList.DataSource = this.bindingSource1;
            this.dgvKavuahList.Location = new System.Drawing.Point(8, 76);
            this.dgvKavuahList.Name = "dgvKavuahList";
            this.dgvKavuahList.RowHeadersVisible = false;
            this.dgvKavuahList.Size = new System.Drawing.Size(768, 171);
            this.dgvKavuahList.TabIndex = 0;
            this.dgvKavuahList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKavuahList_CellContentClick);
            // 
            // deleteColumn
            // 
            this.deleteColumn.Frozen = true;
            this.deleteColumn.HeaderText = "";
            this.deleteColumn.Image = global::Chashavshavon.Properties.Resources.delete;
            this.deleteColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.deleteColumn.Name = "deleteColumn";
            this.deleteColumn.ReadOnly = true;
            this.deleteColumn.Width = 30;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Tan;
            this.btnAdd.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(368, 271);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(199, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "הוסף לרשימת וסתות הקבוע וסגור";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // kavuahDescriptionHebrewDataGridViewTextBoxColumn
            // 
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.DataPropertyName = "KavuahDescriptionHebrew";
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.Frozen = true;
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.HeaderText = "הסבר";
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.Name = "kavuahDescriptionHebrewDataGridViewTextBoxColumn";
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.ReadOnly = true;
            this.kavuahDescriptionHebrewDataGridViewTextBoxColumn.Width = 230;
            // 
            // activeDataGridViewCheckBoxColumn
            // 
            this.activeDataGridViewCheckBoxColumn.DataPropertyName = "Active";
            this.activeDataGridViewCheckBoxColumn.HeaderText = "פעיל?";
            this.activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            this.activeDataGridViewCheckBoxColumn.Width = 75;
            // 
            // isMaayanPauachDataGridViewCheckBoxColumn
            // 
            this.isMaayanPauachDataGridViewCheckBoxColumn.DataPropertyName = "IsMaayanPauach";
            this.isMaayanPauachDataGridViewCheckBoxColumn.HeaderText = "ע\"פ מעיין פתוח?";
            this.isMaayanPauachDataGridViewCheckBoxColumn.Name = "isMaayanPauachDataGridViewCheckBoxColumn";
            // 
            // cancelsOnahBeinanisDataGridViewCheckBoxColumn
            // 
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.DataPropertyName = "CancelsOnahBeinanis";
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.HeaderText = "מבטל עונה בינונות?";
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.Name = "cancelsOnahBeinanisDataGridViewCheckBoxColumn";
            this.cancelsOnahBeinanisDataGridViewCheckBoxColumn.Width = 130;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "הערות";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            this.notesDataGridViewTextBoxColumn.Width = 192;
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
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Chashavshavon.Kavuah);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Tan;
            this.btnClose.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(573, 271);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(199, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "סגור בלי להוסיף כלום";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Narkisim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "נמצאו וסת קבוע";
            // 
            // frmKavuahPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Wheat;
            this.ClientSize = new System.Drawing.Size(784, 308);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvKavuahList);
            this.Font = new System.Drawing.Font("Narkisim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmKavuahPrompt";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "נמצאו וסת קבוע";
            ((System.ComponentModel.ISupportInitialize)(this.dgvKavuahList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridView dgvKavuahList;
        private System.Windows.Forms.DataGridViewImageColumn deleteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kavuahDescriptionHebrewDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isMaayanPauachDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cancelsOnahBeinanisDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayNightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kavuahTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label4;
    }
}