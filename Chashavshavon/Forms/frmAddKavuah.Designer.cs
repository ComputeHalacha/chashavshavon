﻿namespace Chashavshavon
{
    partial class frmAddKavuah
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
            this.lblNumber = new System.Windows.Forms.Label();
            this.cmbNumber = new System.Windows.Forms.ComboBox();
            this.rbDayOfMonth = new System.Windows.Forms.RadioButton();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbInterval = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDilugDayOfMonth = new System.Windows.Forms.RadioButton();
            this.rbDilugHaflagah = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbDay = new System.Windows.Forms.RadioButton();
            this.rbNight = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(418, 138);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(71, 16);
            this.lblNumber.TabIndex = 0;
            this.lblNumber.Text = "מספר ימים";
            // 
            // cmbNumber
            // 
            this.cmbNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumber.ForeColor = System.Drawing.Color.SaddleBrown;
            this.cmbNumber.FormattingEnabled = true;
            this.cmbNumber.Location = new System.Drawing.Point(362, 158);
            this.cmbNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbNumber.Name = "cmbNumber";
            this.cmbNumber.Size = new System.Drawing.Size(127, 24);
            this.cmbNumber.TabIndex = 1;
            // 
            // rbDayOfMonth
            // 
            this.rbDayOfMonth.AutoSize = true;
            this.rbDayOfMonth.Location = new System.Drawing.Point(310, 70);
            this.rbDayOfMonth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbDayOfMonth.Name = "rbDayOfMonth";
            this.rbDayOfMonth.Size = new System.Drawing.Size(75, 17);
            this.rbDayOfMonth.TabIndex = 2;
            this.rbDayOfMonth.Text = "יום בחדש";
            this.rbDayOfMonth.UseVisualStyleBackColor = true;
            this.rbDayOfMonth.CheckedChanged += new System.EventHandler(this.HaflagahTypeChanged);
            // 
            // tbNotes
            // 
            this.tbNotes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tbNotes.ForeColor = System.Drawing.Color.SaddleBrown;
            this.tbNotes.Location = new System.Drawing.Point(62, 295);
            this.tbNotes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(427, 69);
            this.tbNotes.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(444, 276);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "הערות";
            // 
            // rbInterval
            // 
            this.rbInterval.AutoSize = true;
            this.rbInterval.Checked = true;
            this.rbInterval.Location = new System.Drawing.Point(329, 33);
            this.rbInterval.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbInterval.Name = "rbInterval";
            this.rbInterval.Size = new System.Drawing.Size(59, 17);
            this.rbInterval.TabIndex = 3;
            this.rbInterval.TabStop = true;
            this.rbInterval.Text = "הפלגה";
            this.rbInterval.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDilugDayOfMonth);
            this.groupBox1.Controls.Add(this.rbDilugHaflagah);
            this.groupBox1.Controls.Add(this.rbDayOfMonth);
            this.groupBox1.Controls.Add(this.rbInterval);
            this.groupBox1.Location = new System.Drawing.Point(54, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(435, 108);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "סוג";
            // 
            // rbDilugDayOfMonth
            // 
            this.rbDilugDayOfMonth.AutoSize = true;
            this.rbDilugDayOfMonth.Location = new System.Drawing.Point(129, 70);
            this.rbDilugDayOfMonth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbDilugDayOfMonth.Name = "rbDilugDayOfMonth";
            this.rbDilugDayOfMonth.Size = new System.Drawing.Size(127, 17);
            this.rbDilugDayOfMonth.TabIndex = 4;
            this.rbDilugDayOfMonth.Text = "דילוג של יום החדש";
            this.rbDilugDayOfMonth.UseVisualStyleBackColor = true;
            this.rbDilugDayOfMonth.CheckedChanged += new System.EventHandler(this.HaflagahTypeChanged);
            // 
            // rbDilugHaflagah
            // 
            this.rbDilugHaflagah.AutoSize = true;
            this.rbDilugHaflagah.Location = new System.Drawing.Point(150, 33);
            this.rbDilugHaflagah.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbDilugHaflagah.Name = "rbDilugHaflagah";
            this.rbDilugHaflagah.Size = new System.Drawing.Size(111, 17);
            this.rbDilugHaflagah.TabIndex = 5;
            this.rbDilugHaflagah.Text = "דילוג של הפלגה";
            this.rbDilugHaflagah.UseVisualStyleBackColor = true;
            this.rbDilugHaflagah.CheckedChanged += new System.EventHandler(this.HaflagahTypeChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbDay);
            this.groupBox2.Controls.Add(this.rbNight);
            this.groupBox2.Location = new System.Drawing.Point(60, 199);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(429, 64);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "עונה";
            // 
            // rbDay
            // 
            this.rbDay.AutoSize = true;
            this.rbDay.Checked = true;
            this.rbDay.Location = new System.Drawing.Point(342, 22);
            this.rbDay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbDay.Name = "rbDay";
            this.rbDay.Size = new System.Drawing.Size(42, 17);
            this.rbDay.TabIndex = 7;
            this.rbDay.TabStop = true;
            this.rbDay.Text = "יום";
            this.rbDay.UseVisualStyleBackColor = true;
            // 
            // rbNight
            // 
            this.rbNight.AutoSize = true;
            this.rbNight.Location = new System.Drawing.Point(223, 22);
            this.rbNight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbNight.Name = "rbNight";
            this.rbNight.Size = new System.Drawing.Size(51, 17);
            this.rbNight.TabIndex = 6;
            this.rbNight.Text = "לילה";
            this.rbNight.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.BurlyWood;
            this.btnAdd.Location = new System.Drawing.Point(346, 538);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(99, 28);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "&הוסף";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.BurlyWood;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(237, 538);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 28);
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
            this.cbActive.Location = new System.Drawing.Point(428, 384);
            this.cbActive.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(61, 20);
            this.cbActive.TabIndex = 12;
            this.cbActive.Text = "פעיל?";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(249, 412);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(240, 20);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "האם קבוע זאת מבטלת עונה בינונית?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.tbNotes);
            this.panel1.Controls.Add(this.cbActive);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblNumber);
            this.panel1.Controls.Add(this.cmbNumber);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(-32, 56);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(544, 451);
            this.panel1.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Narkisim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(15, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "הוסף וסת קבוע";
            // 
            // frmAddKavuah
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(480, 579);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Font = new System.Drawing.Font("Narkisim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmAddKavuah";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "הוסף וסת קבוע";
            this.Load += new System.EventHandler(this.frmAddKavuah_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
    }
}