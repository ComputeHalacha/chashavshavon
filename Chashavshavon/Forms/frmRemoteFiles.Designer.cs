namespace Chashavshavon
{
    partial class FrmRemoteFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemoteFiles));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnNewUser = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvFileNames = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSaveCurrent = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurrentFileName = new System.Windows.Forms.TextBox();
            this.pbWeb = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.llSite = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.switcherAlwaysUpdateRemote = new Chashavshavon.ChoiceSwitcher();
            this.btnImport = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWeb)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "שם משתמש";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(339, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "סיסמה";
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
            this.btnClose.Location = new System.Drawing.Point(18, 16);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "סגור";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnter.BackColor = System.Drawing.Color.Lavender;
            this.btnEnter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEnter.BackgroundImage")));
            this.btnEnter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnter.Font = new System.Drawing.Font("Narkisim", 10F);
            this.btnEnter.ForeColor = System.Drawing.Color.Lavender;
            this.btnEnter.Location = new System.Drawing.Point(207, 157);
            this.btnEnter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(112, 28);
            this.btnEnter.TabIndex = 2;
            this.btnEnter.Text = "כנס לחשבון";
            this.btnEnter.UseVisualStyleBackColor = false;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnNewUser
            // 
            this.btnNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewUser.BackColor = System.Drawing.Color.Lavender;
            this.btnNewUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewUser.BackgroundImage")));
            this.btnNewUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewUser.Font = new System.Drawing.Font("Narkisim", 10F);
            this.btnNewUser.ForeColor = System.Drawing.Color.Lavender;
            this.btnNewUser.Location = new System.Drawing.Point(87, 157);
            this.btnNewUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(112, 28);
            this.btnNewUser.TabIndex = 3;
            this.btnNewUser.Text = "חשבון חדש";
            this.btnNewUser.UseVisualStyleBackColor = false;
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.ForeColor = System.Drawing.Color.SaddleBrown;
            this.txtUserName.Location = new System.Drawing.Point(38, 46);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(350, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.ForeColor = System.Drawing.Color.SaddleBrown;
            this.txtPassword.Location = new System.Drawing.Point(38, 97);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(350, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.lvFileNames);
            this.groupBox2.Controls.Add(this.btnPreview);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Location = new System.Drawing.Point(453, 94);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(398, 323);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "קבצי רשת בחשבון";
            // 
            // lvFileNames
            // 
            this.lvFileNames.BackColor = System.Drawing.Color.Lavender;
            this.lvFileNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvFileNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvFileNames.FullRowSelect = true;
            this.lvFileNames.GridLines = true;
            this.lvFileNames.HideSelection = false;
            this.lvFileNames.Location = new System.Drawing.Point(19, 30);
            this.lvFileNames.Name = "lvFileNames";
            this.lvFileNames.RightToLeftLayout = true;
            this.lvFileNames.Size = new System.Drawing.Size(361, 235);
            this.lvFileNames.TabIndex = 4;
            this.lvFileNames.UseCompatibleStateImageBehavior = false;
            this.lvFileNames.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "שם";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "תאריך שינוי";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 161;
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.BackColor = System.Drawing.Color.Lavender;
            this.btnPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreview.BackgroundImage")));
            this.btnPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Narkisim", 10F);
            this.btnPreview.ForeColor = System.Drawing.Color.Lavender;
            this.btnPreview.Location = new System.Drawing.Point(311, 275);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(69, 25);
            this.btnPreview.TabIndex = 3;
            this.btnPreview.Text = "הצג";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.Lavender;
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Narkisim", 10F);
            this.btnDelete.ForeColor = System.Drawing.Color.Lavender;
            this.btnDelete.Location = new System.Drawing.Point(19, 275);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 25);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "מחק";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.BackColor = System.Drawing.Color.Lavender;
            this.btnOpen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpen.BackgroundImage")));
            this.btnOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Narkisim", 10F);
            this.btnOpen.ForeColor = System.Drawing.Color.Lavender;
            this.btnOpen.Location = new System.Drawing.Point(234, 275);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(69, 25);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "הורדה";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteUser);
            this.groupBox1.Controls.Add(this.btnEnter);
            this.groupBox1.Controls.Add(this.btnNewUser);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Location = new System.Drawing.Point(24, 94);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(406, 255);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "חשבון הזדהות";
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteUser.BackColor = System.Drawing.Color.Lavender;
            this.btnDeleteUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteUser.BackgroundImage")));
            this.btnDeleteUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteUser.Font = new System.Drawing.Font("Narkisim", 10F);
            this.btnDeleteUser.ForeColor = System.Drawing.Color.Lavender;
            this.btnDeleteUser.Location = new System.Drawing.Point(147, 190);
            this.btnDeleteUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(112, 28);
            this.btnDeleteUser.TabIndex = 4;
            this.btnDeleteUser.Text = "מחק חשבון";
            this.btnDeleteUser.UseVisualStyleBackColor = false;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSaveCurrent);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtCurrentFileName);
            this.groupBox3.Location = new System.Drawing.Point(26, 387);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(405, 90);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "גיבוי קובץ הנוכחי ברשת";
            // 
            // btnSaveCurrent
            // 
            this.btnSaveCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCurrent.BackColor = System.Drawing.Color.Lavender;
            this.btnSaveCurrent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSaveCurrent.BackgroundImage")));
            this.btnSaveCurrent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveCurrent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveCurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCurrent.Font = new System.Drawing.Font("Narkisim", 10F);
            this.btnSaveCurrent.ForeColor = System.Drawing.Color.Lavender;
            this.btnSaveCurrent.Location = new System.Drawing.Point(21, 44);
            this.btnSaveCurrent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSaveCurrent.Name = "btnSaveCurrent";
            this.btnSaveCurrent.Size = new System.Drawing.Size(64, 24);
            this.btnSaveCurrent.TabIndex = 1;
            this.btnSaveCurrent.Text = "המשך";
            this.btnSaveCurrent.UseVisualStyleBackColor = false;
            this.btnSaveCurrent.Click += new System.EventHandler(this.btnSaveCurrent_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "שם קובץ";
            // 
            // txtCurrentFileName
            // 
            this.txtCurrentFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentFileName.ForeColor = System.Drawing.Color.SaddleBrown;
            this.txtCurrentFileName.Location = new System.Drawing.Point(92, 45);
            this.txtCurrentFileName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCurrentFileName.Name = "txtCurrentFileName";
            this.txtCurrentFileName.Size = new System.Drawing.Size(294, 20);
            this.txtCurrentFileName.TabIndex = 0;
            // 
            // pbWeb
            // 
            this.pbWeb.BackColor = System.Drawing.Color.Transparent;
            this.pbWeb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWeb.Image = ((System.Drawing.Image)(resources.GetObject("pbWeb.Image")));
            this.pbWeb.Location = new System.Drawing.Point(794, 10);
            this.pbWeb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbWeb.Name = "pbWeb";
            this.pbWeb.Size = new System.Drawing.Size(62, 55);
            this.pbWeb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbWeb.TabIndex = 16;
            this.pbWeb.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Narkisim", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label4.Location = new System.Drawing.Point(468, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(249, 35);
            this.label4.TabIndex = 17;
            this.label4.Text = "גיבוי קבצים ברשת";
            // 
            // llSite
            // 
            this.llSite.AutoSize = true;
            this.llSite.BackColor = System.Drawing.Color.Transparent;
            this.llSite.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llSite.Location = new System.Drawing.Point(753, 23);
            this.llSite.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llSite.Name = "llSite";
            this.llSite.Size = new System.Drawing.Size(95, 14);
            this.llSite.TabIndex = 18;
            this.llSite.TabStop = true;
            this.llSite.Text = "פתח אתר חשבשבון";
            this.llSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSite_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pbWeb);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 76);
            this.panel1.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Chashavshavon.Properties.Resources.BlueMarble;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.llSite);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Location = new System.Drawing.Point(-1, 501);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(871, 62);
            this.panel2.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Narkisim", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(468, 450);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "שמור שינויים ברשת אוטומתית ";
            // 
            // switcherAlwaysUpdateRemote
            // 
            this.switcherAlwaysUpdateRemote.BackColorNotSelected = System.Drawing.Color.Transparent;
            this.switcherAlwaysUpdateRemote.BackColorSelected = System.Drawing.Color.Green;
            this.switcherAlwaysUpdateRemote.BackColorSlot = System.Drawing.Color.MediumSeaGreen;
            this.switcherAlwaysUpdateRemote.BackColorSlotChoiceTwo = System.Drawing.SystemColors.ControlDark;
            this.switcherAlwaysUpdateRemote.ChoiceChosen = Chashavshavon.ChoiceSwitcherChoices.ChoiceOne;
            this.switcherAlwaysUpdateRemote.ChoiceOneSelected = global::Chashavshavon.Properties.Settings.Default.AlwaysUpdateRemote;
            this.switcherAlwaysUpdateRemote.ChoiceTwoSelected = false;
            this.switcherAlwaysUpdateRemote.DataBindings.Add(new System.Windows.Forms.Binding("ChoiceOneSelected", global::Chashavshavon.Properties.Settings.Default, "AlwaysUpdateRemote", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.switcherAlwaysUpdateRemote.DisplayAsYesNo = false;
            this.switcherAlwaysUpdateRemote.FontNotSelected = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.switcherAlwaysUpdateRemote.FontSelected = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.switcherAlwaysUpdateRemote.FontSize = 8.25F;
            this.switcherAlwaysUpdateRemote.ForeColorNotSelected = System.Drawing.SystemColors.ControlText;
            this.switcherAlwaysUpdateRemote.ForeColorSelected = System.Drawing.SystemColors.ControlText;
            this.switcherAlwaysUpdateRemote.Location = new System.Drawing.Point(644, 436);
            this.switcherAlwaysUpdateRemote.Name = "switcherAlwaysUpdateRemote";
            this.switcherAlwaysUpdateRemote.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.switcherAlwaysUpdateRemote.SelectedValue = false;
            this.switcherAlwaysUpdateRemote.Size = new System.Drawing.Size(112, 42);
            this.switcherAlwaysUpdateRemote.TabIndex = 21;
            this.switcherAlwaysUpdateRemote.Text = "choiceSwitcher1";
            this.switcherAlwaysUpdateRemote.TextChoiceOne = "";
            this.switcherAlwaysUpdateRemote.TextChoiceTwo = "";
            this.switcherAlwaysUpdateRemote.ValueChoiceOne = false;
            this.switcherAlwaysUpdateRemote.ValueChoiceTwo = true;
            this.switcherAlwaysUpdateRemote.ChoiceSwitched += new System.EventHandler(this.switcherAlwaysUpdateRemote_ChoiceSwitched);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.BackColor = System.Drawing.Color.Lavender;
            this.btnImport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImport.BackgroundImage")));
            this.btnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Narkisim", 10F);
            this.btnImport.ForeColor = System.Drawing.Color.Lavender;
            this.btnImport.Location = new System.Drawing.Point(157, 275);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(69, 25);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "ייבוא";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // FrmRemoteFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(870, 560);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.switcherAlwaysUpdateRemote);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRemoteFiles";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "גיבוי קבצים ברשת";
            this.Load += new System.EventHandler(this.frmRemoteFiles_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWeb)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnNewUser;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSaveCurrent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurrentFileName;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.PictureBox pbWeb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel llSite;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lvFileNames;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private ChoiceSwitcher switcherAlwaysUpdateRemote;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnImport;
    }
}