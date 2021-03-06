namespace Chashavshavon
{
    partial class FrmPreferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPreferences));
            this.rbPlacesInDiaspora = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbPlaces = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPlacesInIsrael = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.cbKeepLongerHaflagah = new System.Windows.Forms.CheckBox();
            this.ShowOhrZeruah = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbDontOpenFile = new System.Windows.Forms.RadioButton();
            this.rbOpenFileDialog = new System.Windows.Forms.RadioButton();
            this.rbOpenNewFile = new System.Windows.Forms.RadioButton();
            this.rbOpenLastFile = new System.Windows.Forms.RadioButton();
            this.pbShowPassword = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numberMonthsAheadToWarn = new System.Windows.Forms.NumericUpDown();
            this.cbRequirePassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnResetAll = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberMonthsAheadToWarn)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPlacesInDiaspora
            // 
            this.rbPlacesInDiaspora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbPlacesInDiaspora.AutoSize = true;
            this.rbPlacesInDiaspora.Location = new System.Drawing.Point(516, 28);
            this.rbPlacesInDiaspora.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbPlacesInDiaspora.Name = "rbPlacesInDiaspora";
            this.rbPlacesInDiaspora.Size = new System.Drawing.Size(52, 17);
            this.rbPlacesInDiaspora.TabIndex = 2;
            this.rbPlacesInDiaspora.TabStop = true;
            this.rbPlacesInDiaspora.Text = "בחו\"ל";
            this.rbPlacesInDiaspora.UseVisualStyleBackColor = true;
            this.rbPlacesInDiaspora.CheckedChanged += new System.EventHandler(this.rbPlacesInDiaspora_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Lavender;
            this.button1.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Narkisim", 11F);
            this.button1.ForeColor = System.Drawing.Color.Lavender;
            this.button1.Location = new System.Drawing.Point(12, 23);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "שמור וסגור";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Lavender;
            this.button2.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Narkisim", 11F);
            this.button2.ForeColor = System.Drawing.Color.Lavender;
            this.button2.Location = new System.Drawing.Point(122, 23);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 27);
            this.button2.TabIndex = 4;
            this.button2.Text = "בטל וסגור";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbPlaces
            // 
            this.cbPlaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPlaces.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbPlaces.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPlaces.DisplayMember = "InnerText";
            this.cbPlaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlaces.ForeColor = System.Drawing.Color.SaddleBrown;
            this.cbPlaces.FormattingEnabled = true;
            this.cbPlaces.Location = new System.Drawing.Point(252, 54);
            this.cbPlaces.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbPlaces.Name = "cbPlaces";
            this.cbPlaces.Size = new System.Drawing.Size(386, 21);
            this.cbPlaces.TabIndex = 0;
            this.cbPlaces.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cbPlacess_Format);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cbPlaces);
            this.groupBox1.Controls.Add(this.rbPlacesInIsrael);
            this.groupBox1.Controls.Add(this.rbPlacesInDiaspora);
            this.groupBox1.Location = new System.Drawing.Point(13, 57);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(672, 98);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "בחר מקום";
            // 
            // rbPlacesInIsrael
            // 
            this.rbPlacesInIsrael.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbPlacesInIsrael.AutoSize = true;
            this.rbPlacesInIsrael.Location = new System.Drawing.Point(588, 28);
            this.rbPlacesInIsrael.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbPlacesInIsrael.Name = "rbPlacesInIsrael";
            this.rbPlacesInIsrael.Size = new System.Drawing.Size(50, 17);
            this.rbPlacesInIsrael.TabIndex = 1;
            this.rbPlacesInIsrael.TabStop = true;
            this.rbPlacesInIsrael.Text = "בארץ";
            this.rbPlacesInIsrael.UseVisualStyleBackColor = true;
            this.rbPlacesInIsrael.CheckedChanged += new System.EventHandler(this.rbPlacesInIsrael_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.cbKeepLongerHaflagah);
            this.groupBox2.Controls.Add(this.ShowOhrZeruah);
            this.groupBox2.Location = new System.Drawing.Point(13, 170);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(672, 175);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "העדפות הלכיות";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.Checked = global::Chashavshavon.Properties.Settings.Default.DilugChodeshPastEnds;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "DilugChodeshPastEnds", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(159, 136);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(497, 17);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "להמשיך קבוע של דילוג יום החודש אחר שהגיע לסוף או תחילת החודש";
            this.toolTip1.SetToolTip(this.checkBox1, "להמשיך קבוע של דילוג יום החודש אחר שהגיע לסוף או תחילת החודש");
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox3.Checked = global::Chashavshavon.Properties.Settings.Default.OnahBenIs24Hours;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "OnahBenIs24Hours", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox3.Location = new System.Drawing.Point(267, 100);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(389, 17);
            this.checkBox3.TabIndex = 11;
            this.checkBox3.Text = "עונה בינונית מעת לעת";
            this.toolTip1.SetToolTip(this.checkBox3, "האם להתריע על עונה בינונית (30 ו31) מעת לעת, או רק עונת הראייה?");
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // cbKeepLongerHaflagah
            // 
            this.cbKeepLongerHaflagah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbKeepLongerHaflagah.Checked = global::Chashavshavon.Properties.Settings.Default.KeepLongerHaflagah;
            this.cbKeepLongerHaflagah.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbKeepLongerHaflagah.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "keepLongerHaflagah", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbKeepLongerHaflagah.Location = new System.Drawing.Point(231, 68);
            this.cbKeepLongerHaflagah.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbKeepLongerHaflagah.Name = "cbKeepLongerHaflagah";
            this.cbKeepLongerHaflagah.Size = new System.Drawing.Size(425, 17);
            this.cbKeepLongerHaflagah.TabIndex = 9;
            this.cbKeepLongerHaflagah.Text = "הפלגה קצרה אינו מבטל ארוכה (הט\"ז ושו\"ע הרב)";
            this.toolTip1.SetToolTip(this.cbKeepLongerHaflagah, "באין וסת קבוע, האם להתריע על יום הפלגה גם אחרי ראייה נוספת כל זמן שלא היתה הפלגה " +
        "ארוכה יותר?");
            this.cbKeepLongerHaflagah.UseVisualStyleBackColor = true;
            // 
            // ShowOhrZeruah
            // 
            this.ShowOhrZeruah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowOhrZeruah.Checked = global::Chashavshavon.Properties.Settings.Default.ShowOhrZeruah;
            this.ShowOhrZeruah.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowOhrZeruah.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Chashavshavon.Properties.Settings.Default, "ShowOhrZeruah", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ShowOhrZeruah.Location = new System.Drawing.Point(235, 36);
            this.ShowOhrZeruah.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ShowOhrZeruah.Name = "ShowOhrZeruah";
            this.ShowOhrZeruah.Size = new System.Drawing.Size(421, 17);
            this.ShowOhrZeruah.TabIndex = 6;
            this.ShowOhrZeruah.Text = "הצג זמני אור זרוע";
            this.toolTip1.SetToolTip(this.ShowOhrZeruah, "האם להתריע על העונה שלפני עונת הראייה או הוסת קבוע?");
            this.ShowOhrZeruah.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.pbShowPassword);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numberMonthsAheadToWarn);
            this.groupBox3.Controls.Add(this.cbRequirePassword);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Location = new System.Drawing.Point(13, 360);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(672, 209);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "העדפות כלליות";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbDontOpenFile);
            this.groupBox4.Controls.Add(this.rbOpenFileDialog);
            this.groupBox4.Controls.Add(this.rbOpenNewFile);
            this.groupBox4.Controls.Add(this.rbOpenLastFile);
            this.groupBox4.Location = new System.Drawing.Point(24, 121);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(628, 82);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "בפתיחת התוכנה";
            // 
            // rbDontOpenFile
            // 
            this.rbDontOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbDontOpenFile.AutoSize = true;
            this.rbDontOpenFile.Location = new System.Drawing.Point(145, 54);
            this.rbDontOpenFile.Name = "rbDontOpenFile";
            this.rbDontOpenFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbDontOpenFile.Size = new System.Drawing.Size(98, 17);
            this.rbDontOpenFile.TabIndex = 1;
            this.rbDontOpenFile.Text = "אל תפתח קובץ";
            this.rbDontOpenFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbDontOpenFile.UseVisualStyleBackColor = true;
            this.rbDontOpenFile.CheckedChanged += new System.EventHandler(this.rbOpenLastFile_CheckedChanged);
            // 
            // rbOpenFileDialog
            // 
            this.rbOpenFileDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOpenFileDialog.AutoSize = true;
            this.rbOpenFileDialog.Location = new System.Drawing.Point(484, 56);
            this.rbOpenFileDialog.Name = "rbOpenFileDialog";
            this.rbOpenFileDialog.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOpenFileDialog.Size = new System.Drawing.Size(111, 17);
            this.rbOpenFileDialog.TabIndex = 0;
            this.rbOpenFileDialog.Text = "תן לי לבחור קובץ";
            this.rbOpenFileDialog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOpenFileDialog.UseVisualStyleBackColor = true;
            this.rbOpenFileDialog.CheckedChanged += new System.EventHandler(this.rbOpenLastFile_CheckedChanged);
            // 
            // rbOpenNewFile
            // 
            this.rbOpenNewFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOpenNewFile.AutoSize = true;
            this.rbOpenNewFile.Location = new System.Drawing.Point(145, 28);
            this.rbOpenNewFile.Name = "rbOpenNewFile";
            this.rbOpenNewFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOpenNewFile.Size = new System.Drawing.Size(98, 17);
            this.rbOpenNewFile.TabIndex = 0;
            this.rbOpenNewFile.Text = "פתח קובץ חדש";
            this.rbOpenNewFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOpenNewFile.UseVisualStyleBackColor = true;
            this.rbOpenNewFile.CheckedChanged += new System.EventHandler(this.rbOpenLastFile_CheckedChanged);
            // 
            // rbOpenLastFile
            // 
            this.rbOpenLastFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOpenLastFile.AutoSize = true;
            this.rbOpenLastFile.Checked = true;
            this.rbOpenLastFile.Location = new System.Drawing.Point(429, 28);
            this.rbOpenLastFile.Name = "rbOpenLastFile";
            this.rbOpenLastFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbOpenLastFile.Size = new System.Drawing.Size(166, 17);
            this.rbOpenLastFile.TabIndex = 0;
            this.rbOpenLastFile.TabStop = true;
            this.rbOpenLastFile.Text = "פתח קובץ אחרון - במחשב זה";
            this.rbOpenLastFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbOpenLastFile.UseVisualStyleBackColor = true;
            this.rbOpenLastFile.CheckedChanged += new System.EventHandler(this.rbOpenLastFile_CheckedChanged);
            // 
            // pbShowPassword
            // 
            this.pbShowPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbShowPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbShowPassword.Image = ((System.Drawing.Image)(resources.GetObject("pbShowPassword.Image")));
            this.pbShowPassword.Location = new System.Drawing.Point(267, 78);
            this.pbShowPassword.Name = "pbShowPassword";
            this.pbShowPassword.Size = new System.Drawing.Size(24, 24);
            this.pbShowPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbShowPassword.TabIndex = 11;
            this.pbShowPassword.TabStop = false;
            this.pbShowPassword.Click += new System.EventHandler(this.pbShowPassword_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(382, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "מספר חודשים קדימה לחשב התראות:";
            // 
            // numberMonthsAheadToWarn
            // 
            this.numberMonthsAheadToWarn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numberMonthsAheadToWarn.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Chashavshavon.Properties.Settings.Default, "numberMonthsAheadToWarn", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numberMonthsAheadToWarn.Location = new System.Drawing.Point(335, 22);
            this.numberMonthsAheadToWarn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberMonthsAheadToWarn.Name = "numberMonthsAheadToWarn";
            this.numberMonthsAheadToWarn.Size = new System.Drawing.Size(41, 20);
            this.numberMonthsAheadToWarn.TabIndex = 9;
            this.numberMonthsAheadToWarn.Value = global::Chashavshavon.Properties.Settings.Default.NumberMonthsAheadToWarn;
            // 
            // cbRequirePassword
            // 
            this.cbRequirePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRequirePassword.Checked = true;
            this.cbRequirePassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRequirePassword.Location = new System.Drawing.Point(267, 54);
            this.cbRequirePassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbRequirePassword.Name = "cbRequirePassword";
            this.cbRequirePassword.Size = new System.Drawing.Size(357, 17);
            this.cbRequirePassword.TabIndex = 7;
            this.cbRequirePassword.Text = "תחייב הקשת הסיסמה הבאה בפתיחת התוכנית";
            this.cbRequirePassword.UseVisualStyleBackColor = true;
            this.cbRequirePassword.CheckedChanged += new System.EventHandler(this.cbRequirePassword_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(298, 73);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(326, 29);
            this.txtPassword.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Narkisim", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.SlateGray;
            this.label4.Location = new System.Drawing.Point(584, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 19);
            this.label4.TabIndex = 11;
            this.label4.Text = "העדפות";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnReload);
            this.panel1.Controls.Add(this.btnResetAll);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 590);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(698, 74);
            this.panel1.TabIndex = 12;
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.BackColor = System.Drawing.Color.Lavender;
            this.btnReload.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.btnReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReload.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Narkisim", 11F);
            this.btnReload.ForeColor = System.Drawing.Color.Lavender;
            this.btnReload.Location = new System.Drawing.Point(394, 23);
            this.btnReload.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(123, 27);
            this.btnReload.TabIndex = 4;
            this.btnReload.Text = "ביטול שינויים";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnResetAll
            // 
            this.btnResetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetAll.BackColor = System.Drawing.Color.Lavender;
            this.btnResetAll.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.btnResetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResetAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnResetAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetAll.Font = new System.Drawing.Font("Narkisim", 11F);
            this.btnResetAll.ForeColor = System.Drawing.Color.Lavender;
            this.btnResetAll.Location = new System.Drawing.Point(525, 23);
            this.btnResetAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnResetAll.Name = "btnResetAll";
            this.btnResetAll.Size = new System.Drawing.Size(159, 27);
            this.btnResetAll.TabIndex = 4;
            this.btnResetAll.Text = "שחזר ברירות מחדל";
            this.btnResetAll.UseVisualStyleBackColor = false;
            this.btnResetAll.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(698, 49);
            this.panel2.TabIndex = 13;
            // 
            // frmPreferences
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(698, 664);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPreferences";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "העדפות";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPreferences_FormClosing);
            this.Load += new System.EventHandler(this.frmPreferences_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberMonthsAheadToWarn)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPlaces;
        private System.Windows.Forms.RadioButton rbPlacesInIsrael;
        private System.Windows.Forms.RadioButton rbPlacesInDiaspora;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ShowOhrZeruah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox cbRequirePassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numberMonthsAheadToWarn;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox cbKeepLongerHaflagah;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pbShowPassword;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbOpenFileDialog;
        private System.Windows.Forms.RadioButton rbOpenNewFile;
        private System.Windows.Forms.RadioButton rbOpenLastFile;
        private System.Windows.Forms.RadioButton rbDontOpenFile;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnResetAll;
    }
}