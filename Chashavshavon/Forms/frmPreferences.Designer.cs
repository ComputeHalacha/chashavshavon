namespace Chashavshavon
{
    partial class frmPreferences
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
            this.rbLocsInDiaspora = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbLocations = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSummerTime = new System.Windows.Forms.CheckBox();
            this.rbLocsInIsrael = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ShowOhrZeruah = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbRequirePassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbLocsInDiaspora
            // 
            this.rbLocsInDiaspora.AutoSize = true;
            this.rbLocsInDiaspora.Location = new System.Drawing.Point(296, 53);
            this.rbLocsInDiaspora.Name = "rbLocsInDiaspora";
            this.rbLocsInDiaspora.Size = new System.Drawing.Size(56, 17);
            this.rbLocsInDiaspora.TabIndex = 2;
            this.rbLocsInDiaspora.TabStop = true;
            this.rbLocsInDiaspora.Text = "בחו\"ל";
            this.rbLocsInDiaspora.UseVisualStyleBackColor = true;
            this.rbLocsInDiaspora.CheckedChanged += new System.EventHandler(this.rbLocsInDiaspora_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(198, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "שמור";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(117, 374);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "בטל";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbLocations
            // 
            this.cbLocations.DisplayMember = "InnerText";
            this.cbLocations.FormattingEnabled = true;
            this.cbLocations.Location = new System.Drawing.Point(20, 76);
            this.cbLocations.Name = "cbLocations";
            this.cbLocations.Size = new System.Drawing.Size(332, 21);
            this.cbLocations.TabIndex = 0;
            this.cbLocations.Text = "קרית ספר";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbLocations);
            this.groupBox1.Controls.Add(this.cbSummerTime);
            this.groupBox1.Controls.Add(this.rbLocsInIsrael);
            this.groupBox1.Controls.Add(this.rbLocsInDiaspora);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 145);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "בחר מקום";
            // 
            // cbSummerTime
            // 
            this.cbSummerTime.Checked = global::Chashavshavon.Properties.Settings.Default.IsSummerTime;
            this.cbSummerTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSummerTime.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "IsSummerTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbSummerTime.Location = new System.Drawing.Point(194, 110);
            this.cbSummerTime.Name = "cbSummerTime";
            this.cbSummerTime.Size = new System.Drawing.Size(158, 17);
            this.cbSummerTime.TabIndex = 5;
            this.cbSummerTime.Text = "שעון קיץ";
            this.cbSummerTime.UseVisualStyleBackColor = true;
            // 
            // rbLocsInIsrael
            // 
            this.rbLocsInIsrael.AutoSize = true;
            this.rbLocsInIsrael.Checked = global::Chashavshavon.Properties.Settings.Default.UserInIsrael;
            this.rbLocsInIsrael.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "UserInIsrael", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rbLocsInIsrael.Location = new System.Drawing.Point(298, 30);
            this.rbLocsInIsrael.Name = "rbLocsInIsrael";
            this.rbLocsInIsrael.Size = new System.Drawing.Size(54, 17);
            this.rbLocsInIsrael.TabIndex = 1;
            this.rbLocsInIsrael.TabStop = true;
            this.rbLocsInIsrael.Text = "בארץ";
            this.rbLocsInIsrael.UseVisualStyleBackColor = true;
            this.rbLocsInIsrael.CheckedChanged += new System.EventHandler(this.rbLocsInIsrael_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ShowOhrZeruah);
            this.groupBox2.Location = new System.Drawing.Point(12, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 74);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "העדפות הלכיות";
            // 
            // ShowOhrZeruah
            // 
            this.ShowOhrZeruah.Checked = global::Chashavshavon.Properties.Settings.Default.ShowOhrZeruah;
            this.ShowOhrZeruah.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowOhrZeruah.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "ShowOhrZeruah", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ShowOhrZeruah.Location = new System.Drawing.Point(194, 36);
            this.ShowOhrZeruah.Name = "ShowOhrZeruah";
            this.ShowOhrZeruah.Size = new System.Drawing.Size(158, 17);
            this.ShowOhrZeruah.TabIndex = 6;
            this.ShowOhrZeruah.Text = "הצג זמני אור זרוע";
            this.ShowOhrZeruah.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbRequirePassword);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Location = new System.Drawing.Point(12, 254);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(374, 85);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "העדפות כלליות";
            // 
            // cbRequirePassword
            // 
            this.cbRequirePassword.Checked = true;
            this.cbRequirePassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRequirePassword.Location = new System.Drawing.Point(20, 30);
            this.cbRequirePassword.Name = "cbRequirePassword";
            this.cbRequirePassword.Size = new System.Drawing.Size(332, 17);
            this.cbRequirePassword.TabIndex = 7;
            this.cbRequirePassword.Text = "תחייב הקשת הסיסמה הבאה בפתיחת התוכנית";
            this.cbRequirePassword.UseVisualStyleBackColor = true;
            this.cbRequirePassword.CheckedChanged += new System.EventHandler(this.cbRequirePassword_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(20, 53);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(332, 20);
            this.txtPassword.TabIndex = 0;
            // 
            // frmPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 409);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPreferences";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "העדפות";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Preferences_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPreferences_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLocations;
        private System.Windows.Forms.RadioButton rbLocsInIsrael;
        private System.Windows.Forms.RadioButton rbLocsInDiaspora;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbSummerTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ShowOhrZeruah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox cbRequirePassword;

    }
}