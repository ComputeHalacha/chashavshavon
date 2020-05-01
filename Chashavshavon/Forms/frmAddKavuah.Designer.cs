namespace Chashavshavon
{
    partial class FrmAddKavuah
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddKavuah));
            this.lblNumber = new System.Windows.Forms.Label();
            this.cmbNumber = new System.Windows.Forms.ComboBox();
            this.rbDayOfMonth = new System.Windows.Forms.RadioButton();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbInterval = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMaayanPasuach = new System.Windows.Forms.CheckBox();
            this.rbdayOfWeek = new System.Windows.Forms.RadioButton();
            this.rbDilugDayOfMonth = new System.Windows.Forms.RadioButton();
            this.rbDilugHaflagah = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbDay = new System.Windows.Forms.RadioButton();
            this.rbNight = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.cbCancelsOnahBeinenis = new System.Windows.Forms.CheckBox();
            this.cmbSettingEntry = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumber
            // 
            this.lblNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumber.Location = new System.Drawing.Point(12, 170);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(113, 13);
            this.lblNumber.TabIndex = 0;
            this.lblNumber.Text = "מספר ימים";
            // 
            // cmbNumber
            // 
            this.cmbNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumber.Font = new System.Drawing.Font("Narkisim", 9F);
            this.cmbNumber.ForeColor = System.Drawing.Color.SaddleBrown;
            this.cmbNumber.FormattingEnabled = true;
            this.cmbNumber.Location = new System.Drawing.Point(13, 190);
            this.cmbNumber.Name = "cmbNumber";
            this.cmbNumber.Size = new System.Drawing.Size(112, 23);
            this.cmbNumber.TabIndex = 1;
            // 
            // rbDayOfMonth
            // 
            this.rbDayOfMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbDayOfMonth.AutoSize = true;
            this.rbDayOfMonth.Location = new System.Drawing.Point(417, 54);
            this.rbDayOfMonth.Name = "rbDayOfMonth";
            this.rbDayOfMonth.Size = new System.Drawing.Size(91, 20);
            this.rbDayOfMonth.TabIndex = 2;
            this.rbDayOfMonth.Text = "יום החדש";
            this.rbDayOfMonth.UseVisualStyleBackColor = true;
            this.rbDayOfMonth.CheckedChanged += new System.EventHandler(this.HaflagahTypeChanged);
            // 
            // tbNotes
            // 
            this.tbNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNotes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tbNotes.ForeColor = System.Drawing.Color.SaddleBrown;
            this.tbNotes.Location = new System.Drawing.Point(13, 295);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(519, 68);
            this.tbNotes.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "הערות";
            // 
            // rbInterval
            // 
            this.rbInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbInterval.AutoSize = true;
            this.rbInterval.Checked = true;
            this.rbInterval.Location = new System.Drawing.Point(439, 23);
            this.rbInterval.Name = "rbInterval";
            this.rbInterval.Size = new System.Drawing.Size(69, 20);
            this.rbInterval.TabIndex = 3;
            this.rbInterval.TabStop = true;
            this.rbInterval.Text = "הפלגה";
            this.rbInterval.UseVisualStyleBackColor = true;
            this.rbInterval.CheckedChanged += new System.EventHandler(this.HaflagahTypeChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbMaayanPasuach);
            this.groupBox1.Controls.Add(this.rbdayOfWeek);
            this.groupBox1.Controls.Add(this.rbDilugDayOfMonth);
            this.groupBox1.Controls.Add(this.rbDilugHaflagah);
            this.groupBox1.Controls.Add(this.rbDayOfMonth);
            this.groupBox1.Controls.Add(this.rbInterval);
            this.groupBox1.Location = new System.Drawing.Point(13, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(520, 108);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "סוג";
            // 
            // cbMaayanPasuach
            // 
            this.cbMaayanPasuach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMaayanPasuach.AutoSize = true;
            this.cbMaayanPasuach.Location = new System.Drawing.Point(387, 86);
            this.cbMaayanPasuach.Name = "cbMaayanPasuach";
            this.cbMaayanPasuach.Size = new System.Drawing.Size(134, 20);
            this.cbMaayanPasuach.TabIndex = 7;
            this.cbMaayanPasuach.Text = "ע\"פ מעיין פתוח?";
            this.cbMaayanPasuach.UseVisualStyleBackColor = true;
            // 
            // rbdayOfWeek
            // 
            this.rbdayOfWeek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbdayOfWeek.AutoSize = true;
            this.rbdayOfWeek.Location = new System.Drawing.Point(316, 23);
            this.rbdayOfWeek.Name = "rbdayOfWeek";
            this.rbdayOfWeek.Size = new System.Drawing.Size(95, 20);
            this.rbdayOfWeek.TabIndex = 6;
            this.rbdayOfWeek.Text = "יום השבוע";
            this.rbdayOfWeek.UseVisualStyleBackColor = true;
            this.rbdayOfWeek.CheckedChanged += new System.EventHandler(this.HaflagahTypeChanged);
            // 
            // rbDilugDayOfMonth
            // 
            this.rbDilugDayOfMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbDilugDayOfMonth.AutoSize = true;
            this.rbDilugDayOfMonth.Location = new System.Drawing.Point(141, 23);
            this.rbDilugDayOfMonth.Name = "rbDilugDayOfMonth";
            this.rbDilugDayOfMonth.Size = new System.Drawing.Size(152, 20);
            this.rbDilugDayOfMonth.TabIndex = 4;
            this.rbDilugDayOfMonth.Text = "דילוג של יום החדש";
            this.rbDilugDayOfMonth.UseVisualStyleBackColor = true;
            this.rbDilugDayOfMonth.CheckedChanged += new System.EventHandler(this.HaflagahTypeChanged);
            // 
            // rbDilugHaflagah
            // 
            this.rbDilugHaflagah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbDilugHaflagah.AutoSize = true;
            this.rbDilugHaflagah.Location = new System.Drawing.Point(281, 54);
            this.rbDilugHaflagah.Name = "rbDilugHaflagah";
            this.rbDilugHaflagah.Size = new System.Drawing.Size(130, 20);
            this.rbDilugHaflagah.TabIndex = 5;
            this.rbDilugHaflagah.Text = "דילוג של הפלגה";
            this.rbDilugHaflagah.UseVisualStyleBackColor = true;
            this.rbDilugHaflagah.CheckedChanged += new System.EventHandler(this.HaflagahTypeChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbDay);
            this.groupBox2.Controls.Add(this.rbNight);
            this.groupBox2.Location = new System.Drawing.Point(13, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 52);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "עונה";
            // 
            // rbDay
            // 
            this.rbDay.AutoSize = true;
            this.rbDay.Checked = true;
            this.rbDay.Location = new System.Drawing.Point(299, 18);
            this.rbDay.Name = "rbDay";
            this.rbDay.Size = new System.Drawing.Size(49, 20);
            this.rbDay.TabIndex = 7;
            this.rbDay.TabStop = true;
            this.rbDay.Text = "יום";
            this.rbDay.UseVisualStyleBackColor = true;
            // 
            // rbNight
            // 
            this.rbNight.AutoSize = true;
            this.rbNight.Location = new System.Drawing.Point(195, 18);
            this.rbNight.Name = "rbNight";
            this.rbNight.Size = new System.Drawing.Size(59, 20);
            this.rbNight.TabIndex = 6;
            this.rbNight.Text = "לילה";
            this.rbNight.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.BackColor = System.Drawing.Color.Lavender;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Narkisim", 11F);
            this.btnAdd.ForeColor = System.Drawing.Color.Lavender;
            this.btnAdd.Location = new System.Drawing.Point(11, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 34);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "&הוסף";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.BackColor = System.Drawing.Color.Lavender;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Narkisim", 11F);
            this.btnClose.ForeColor = System.Drawing.Color.Lavender;
            this.btnClose.Location = new System.Drawing.Point(113, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 34);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "&בטל";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Location = new System.Drawing.Point(13, 372);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(67, 20);
            this.cbActive.TabIndex = 12;
            this.cbActive.Text = "פעיל?";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // cbCancelsOnahBeinenis
            // 
            this.cbCancelsOnahBeinenis.AutoSize = true;
            this.cbCancelsOnahBeinenis.Location = new System.Drawing.Point(13, 395);
            this.cbCancelsOnahBeinenis.Name = "cbCancelsOnahBeinenis";
            this.cbCancelsOnahBeinenis.Size = new System.Drawing.Size(262, 20);
            this.cbCancelsOnahBeinenis.TabIndex = 13;
            this.cbCancelsOnahBeinenis.Text = "האם קבוע זאת מבטלת עונה בינונית?";
            this.cbCancelsOnahBeinenis.UseVisualStyleBackColor = true;
            // 
            // cmbSettingEntry
            // 
            this.cmbSettingEntry.DisplayMember = "DateTime";
            this.cmbSettingEntry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSettingEntry.Font = new System.Drawing.Font("Narkisim", 9F);
            this.cmbSettingEntry.FormatString = "dddd dd MMM yyyy";
            this.cmbSettingEntry.FormattingEnabled = true;
            this.cmbSettingEntry.Location = new System.Drawing.Point(146, 189);
            this.cmbSettingEntry.Name = "cmbSettingEntry";
            this.cmbSettingEntry.Size = new System.Drawing.Size(169, 23);
            this.cmbSettingEntry.TabIndex = 15;
            this.cmbSettingEntry.ValueMember = "DateTime";
            this.cmbSettingEntry.SelectedIndexChanged += new System.EventHandler(this.cmbSettingEntry_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "ראיה הקובע";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Narkisim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(329, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "הוסף וסת קבוע";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Chashavshavon.Properties.Resources.database_add;
            this.pictureBox1.Location = new System.Drawing.Point(496, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 426);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 56);
            this.panel2.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 52);
            this.panel1.TabIndex = 18;
            // 
            // frmAddKavuah
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(550, 482);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cmbSettingEntry);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCancelsOnahBeinenis);
            this.Controls.Add(this.tbNotes);
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.cmbNumber);
            this.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddKavuah";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "הוסף וסת קבוע";
            this.Load += new System.EventHandler(this.frmAddKavuah_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.ComboBox cmbNumber;
        private System.Windows.Forms.RadioButton rbDayOfMonth;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbInterval;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbDay;
        private System.Windows.Forms.RadioButton rbNight;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton rbDilugDayOfMonth;
        private System.Windows.Forms.RadioButton rbDilugHaflagah;
        private System.Windows.Forms.CheckBox cbActive;
        private System.Windows.Forms.CheckBox cbCancelsOnahBeinenis;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSettingEntry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbMaayanPasuach;
        private System.Windows.Forms.RadioButton rbdayOfWeek;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}