namespace Chashavshavon
{
    partial class frmMain
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
            if(disposing && (components != null))
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.dgEntries = new System.Windows.Forms.DataGridView();
            this.btnDeleteColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.DateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DayNightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IntervalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DayOfWeekColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceEntries = new System.Windows.Forms.BindingSource(this.components);
            this.gbCalendar = new System.Windows.Forms.GroupBox();
            this.btnAddEntry2 = new System.Windows.Forms.Button();
            this.btnViewTextEntryList = new System.Windows.Forms.Button();
            this.btnPrintEntryList = new System.Windows.Forms.Button();
            this.btnViewTextCalendar = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRecentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceTextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemPrintEntryList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEntryList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.OpenBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KavuahToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openKavuaListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.AddKavuahToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.SearchForKavuahsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnCheshbonKavuahs = new System.Windows.Forms.Button();
            this.btnOpenKavuahs = new System.Windows.Forms.Button();
            this.btnPrefs = new System.Windows.Forms.Button();
            this.btnAddEntry = new System.Windows.Forms.Button();
            this.btnAddKavuah = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.lblNextProblem = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbWeb = new System.Windows.Forms.PictureBox();
            this.pnlNextProblem = new System.Windows.Forms.Panel();
            this.btnLastMonth = new System.Windows.Forms.Button();
            this.btnNextMonth = new System.Windows.Forms.Button();
            this.lblMonthName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.luachTableLayout = new Chashavshavon.DoubleBufferedLayoutTable();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgEntries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEntries)).BeginInit();
            this.gbCalendar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWeb)).BeginInit();
            this.pnlNextProblem.SuspendLayout();
            this.panel2.SuspendLayout();
            this.luachTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.Size = new System.Drawing.Size(1015, 576);
            // 
            // dgEntries
            // 
            this.dgEntries.AllowUserToAddRows = false;
            this.dgEntries.AllowUserToDeleteRows = false;
            this.dgEntries.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgEntries.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEntries.AutoGenerateColumns = false;
            this.dgEntries.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgEntries.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgEntries.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Narkisim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgEntries.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEntries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnDeleteColumn,
            this.DateColumn,
            this.DayNightColumn,
            this.IntervalColumn,
            this.DayOfWeekColumn,
            this.NotesColumn});
            this.dgEntries.DataSource = this.bindingSourceEntries;
            this.dgEntries.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgEntries.Location = new System.Drawing.Point(52, 23);
            this.dgEntries.Name = "dgEntries";
            this.dgEntries.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgEntries.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Narkisim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgEntries.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgEntries.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dgEntries.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgEntries.Size = new System.Drawing.Size(951, 87);
            this.dgEntries.TabIndex = 12;
            this.dgEntries.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEntries_CellContentClick);
            this.dgEntries.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgEntries_CellFormatting);
            // 
            // btnDeleteColumn
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
            this.btnDeleteColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.btnDeleteColumn.HeaderText = "";
            this.btnDeleteColumn.LinkColor = System.Drawing.Color.SlateGray;
            this.btnDeleteColumn.Name = "btnDeleteColumn";
            this.btnDeleteColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnDeleteColumn.Text = "מחק";
            this.btnDeleteColumn.ToolTipText = "מחק שורה הזאת";
            this.btnDeleteColumn.UseColumnTextForLinkValue = true;
            this.btnDeleteColumn.Width = 40;
            // 
            // DateColumn
            // 
            this.DateColumn.DataPropertyName = "DateTime";
            dataGridViewCellStyle4.Format = "dd MMM yyyy";
            dataGridViewCellStyle4.NullValue = null;
            this.DateColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.DateColumn.HeaderText = "תאריך";
            this.DateColumn.Name = "DateColumn";
            this.DateColumn.ReadOnly = true;
            this.DateColumn.Width = 90;
            // 
            // DayNightColumn
            // 
            this.DayNightColumn.DataPropertyName = "HebrewDayNight";
            this.DayNightColumn.HeaderText = "יום/לילה";
            this.DayNightColumn.Name = "DayNightColumn";
            this.DayNightColumn.ReadOnly = true;
            this.DayNightColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DayNightColumn.Width = 67;
            // 
            // IntervalColumn
            // 
            this.IntervalColumn.DataPropertyName = "Interval";
            this.IntervalColumn.HeaderText = "הפלגה";
            this.IntervalColumn.Name = "IntervalColumn";
            this.IntervalColumn.ReadOnly = true;
            this.IntervalColumn.Width = 57;
            // 
            // DayOfWeekColumn
            // 
            this.DayOfWeekColumn.DataPropertyName = "HebrewDayOfWeek";
            this.DayOfWeekColumn.HeaderText = "יום בשבוע";
            this.DayOfWeekColumn.Name = "DayOfWeekColumn";
            this.DayOfWeekColumn.ReadOnly = true;
            this.DayOfWeekColumn.Width = 77;
            // 
            // NotesColumn
            // 
            this.NotesColumn.DataPropertyName = "Notes";
            this.NotesColumn.HeaderText = "הערות";
            this.NotesColumn.Name = "NotesColumn";
            this.NotesColumn.ReadOnly = true;
            this.NotesColumn.Width = 415;
            // 
            // gbCalendar
            // 
            this.gbCalendar.BackColor = System.Drawing.SystemColors.Control;
            this.gbCalendar.Controls.Add(this.btnAddEntry2);
            this.gbCalendar.Controls.Add(this.dgEntries);
            this.gbCalendar.Controls.Add(this.btnViewTextEntryList);
            this.gbCalendar.Controls.Add(this.btnPrintEntryList);
            this.gbCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCalendar.Location = new System.Drawing.Point(0, 0);
            this.gbCalendar.Name = "gbCalendar";
            this.gbCalendar.Size = new System.Drawing.Size(1006, 142);
            this.gbCalendar.TabIndex = 13;
            this.gbCalendar.TabStop = false;
            this.gbCalendar.Text = "רשומות";
            // 
            // btnAddEntry2
            // 
            this.btnAddEntry2.AutoSize = true;
            this.btnAddEntry2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAddEntry2.FlatAppearance.BorderSize = 0;
            this.btnAddEntry2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddEntry2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAddEntry2.ForeColor = System.Drawing.Color.Blue;
            this.btnAddEntry2.Image = global::Chashavshavon.Properties.Resources.add;
            this.btnAddEntry2.Location = new System.Drawing.Point(0, 116);
            this.btnAddEntry2.Name = "btnAddEntry2";
            this.btnAddEntry2.Size = new System.Drawing.Size(51, 48);
            this.btnAddEntry2.TabIndex = 34;
            this.btnAddEntry2.Text = "הוסף";
            this.btnAddEntry2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnAddEntry2, "הוסף רשומה חדשה");
            this.btnAddEntry2.UseVisualStyleBackColor = true;
            this.btnAddEntry2.Click += new System.EventHandler(this.btnAddEntry_Click);
            // 
            // btnViewTextEntryList
            // 
            this.btnViewTextEntryList.FlatAppearance.BorderSize = 0;
            this.btnViewTextEntryList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewTextEntryList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnViewTextEntryList.ForeColor = System.Drawing.Color.Blue;
            this.btnViewTextEntryList.Image = global::Chashavshavon.Properties.Resources.page_previous;
            this.btnViewTextEntryList.Location = new System.Drawing.Point(0, 8);
            this.btnViewTextEntryList.Name = "btnViewTextEntryList";
            this.btnViewTextEntryList.Size = new System.Drawing.Size(51, 48);
            this.btnViewTextEntryList.TabIndex = 21;
            this.btnViewTextEntryList.Text = "רשימה";
            this.btnViewTextEntryList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnViewTextEntryList, "הצג רשימת הראיות כדי לשמור או להדפיס");
            this.btnViewTextEntryList.UseVisualStyleBackColor = true;
            this.btnViewTextEntryList.Click += new System.EventHandler(this.btnViewTextEntryList_Click);
            // 
            // btnPrintEntryList
            // 
            this.btnPrintEntryList.FlatAppearance.BorderSize = 0;
            this.btnPrintEntryList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintEntryList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnPrintEntryList.ForeColor = System.Drawing.Color.Blue;
            this.btnPrintEntryList.Image = global::Chashavshavon.Properties.Resources.printer;
            this.btnPrintEntryList.Location = new System.Drawing.Point(0, 62);
            this.btnPrintEntryList.Name = "btnPrintEntryList";
            this.btnPrintEntryList.Size = new System.Drawing.Size(51, 48);
            this.btnPrintEntryList.TabIndex = 22;
            this.btnPrintEntryList.Text = "הדפס";
            this.btnPrintEntryList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnPrintEntryList, "הדפס רשימת הראיות");
            this.btnPrintEntryList.UseVisualStyleBackColor = true;
            this.btnPrintEntryList.Click += new System.EventHandler(this.btnPrintEntryList_Click);
            // 
            // btnViewTextCalendar
            // 
            this.btnViewTextCalendar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnViewTextCalendar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnViewTextCalendar.FlatAppearance.BorderSize = 0;
            this.btnViewTextCalendar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewTextCalendar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnViewTextCalendar.ForeColor = System.Drawing.Color.Blue;
            this.btnViewTextCalendar.Image = global::Chashavshavon.Properties.Resources.page_previous;
            this.btnViewTextCalendar.Location = new System.Drawing.Point(0, 100);
            this.btnViewTextCalendar.Margin = new System.Windows.Forms.Padding(0);
            this.btnViewTextCalendar.Name = "btnViewTextCalendar";
            this.btnViewTextCalendar.Size = new System.Drawing.Size(86, 50);
            this.btnViewTextCalendar.TabIndex = 19;
            this.btnViewTextCalendar.Text = "רשימת עונות";
            this.btnViewTextCalendar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnViewTextCalendar, "הצג הלוח כדי לשמור בקובץ או להדפיס");
            this.btnViewTextCalendar.UseVisualStyleBackColor = true;
            this.btnViewTextCalendar.Click += new System.EventHandler(this.btnViewTextCalendar_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.KavuahToolStripMenuItem,
            this.RemoteToolStripMenuItem,
            this.PreferencesToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem,
            this.SaveAsToolStripMenuItem,
            this.NewToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.toolStripSeparator7,
            this.recentFilesToolStripMenuItem,
            this.clearRecentFilesToolStripMenuItem,
            this.toolStripSeparator2,
            this.RefreshToolStripMenuItem,
            this.SourceToolStripMenuItem,
            this.SourceTextMenuItem,
            this.toolStripSeparator3,
            this.PrintToolStripMenuItem,
            this.TextListToolStripMenuItem,
            this.toolStripSeparator4,
            this.toolStripMenuItemPrintEntryList,
            this.toolStripMenuItemEntryList,
            this.toolStripSeparator8,
            this.OpenBackupToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.FileToolStripMenuItem.Text = "&קובץ";
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.SaveToolStripMenuItem.Text = "&שמור";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.SaveAsToolStripMenuItem.Text = "שמיר&ה בשם";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.NewToolStripMenuItem.Text = "&חדש";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.OpenToolStripMenuItem.Text = "&פתח";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(226, 6);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.recentFilesToolStripMenuItem.Text = "&קבצים אחרונים";
            this.recentFilesToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.recentFilesToolStripMenuItem_DropDownItemClicked);
            // 
            // clearRecentFilesToolStripMenuItem
            // 
            this.clearRecentFilesToolStripMenuItem.Name = "clearRecentFilesToolStripMenuItem";
            this.clearRecentFilesToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.clearRecentFilesToolStripMenuItem.Text = "&נקה רשימת קבצים אחרונים";
            this.clearRecentFilesToolStripMenuItem.Click += new System.EventHandler(this.clearRecentFilesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.RefreshToolStripMenuItem.Text = "&רענן";
            this.RefreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // SourceToolStripMenuItem
            // 
            this.SourceToolStripMenuItem.Name = "SourceToolStripMenuItem";
            this.SourceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.SourceToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.SourceToolStripMenuItem.Text = "&מקןר";
            this.SourceToolStripMenuItem.Click += new System.EventHandler(this.SourceToolStripMenuItem_Click);
            // 
            // SourceTextMenuItem
            // 
            this.SourceTextMenuItem.Name = "SourceTextMenuItem";
            this.SourceTextMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.SourceTextMenuItem.Size = new System.Drawing.Size(229, 22);
            this.SourceTextMenuItem.Text = "מקור &טקסט";
            this.SourceTextMenuItem.Click += new System.EventHandler(this.SourceTextMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(226, 6);
            // 
            // PrintToolStripMenuItem
            // 
            this.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem";
            this.PrintToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.PrintToolStripMenuItem.Text = "הדפס לוח שבועי";
            this.PrintToolStripMenuItem.Click += new System.EventHandler(this.PrintToolStripMenuItem_Click);
            // 
            // TextListToolStripMenuItem
            // 
            this.TextListToolStripMenuItem.Name = "TextListToolStripMenuItem";
            this.TextListToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.TextListToolStripMenuItem.Text = "רשימה - לוח שבועי";
            this.TextListToolStripMenuItem.Click += new System.EventHandler(this.TextListToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(226, 6);
            // 
            // toolStripMenuItemPrintEntryList
            // 
            this.toolStripMenuItemPrintEntryList.Name = "toolStripMenuItemPrintEntryList";
            this.toolStripMenuItemPrintEntryList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.toolStripMenuItemPrintEntryList.Size = new System.Drawing.Size(229, 22);
            this.toolStripMenuItemPrintEntryList.Text = "הדפס רשימת וסתות";
            this.toolStripMenuItemPrintEntryList.Click += new System.EventHandler(this.toolStripMenuItemPrintEntryList_Click);
            // 
            // toolStripMenuItemEntryList
            // 
            this.toolStripMenuItemEntryList.Name = "toolStripMenuItemEntryList";
            this.toolStripMenuItemEntryList.Size = new System.Drawing.Size(229, 22);
            this.toolStripMenuItemEntryList.Text = "רשימה - וסתות";
            this.toolStripMenuItemEntryList.Click += new System.EventHandler(this.toolStripMenuItemEntryList_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(226, 6);
            // 
            // OpenBackupToolStripMenuItem
            // 
            this.OpenBackupToolStripMenuItem.Name = "OpenBackupToolStripMenuItem";
            this.OpenBackupToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.OpenBackupToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.OpenBackupToolStripMenuItem.Text = "פתח תיקית &גיבויים";
            this.OpenBackupToolStripMenuItem.Click += new System.EventHandler(this.OpenBackupToolStripMenuItem_Click);
            // 
            // KavuahToolStripMenuItem
            // 
            this.KavuahToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openKavuaListToolStripMenuItem,
            this.toolStripSeparator6,
            this.AddKavuahToolStripMenuItem,
            this.toolStripSeparator5,
            this.SearchForKavuahsToolStripMenuItem,
            this.toolStripSeparator1,
            this.importToolStripMenuItem});
            this.KavuahToolStripMenuItem.Name = "KavuahToolStripMenuItem";
            this.KavuahToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.KavuahToolStripMenuItem.Text = "ק&בוע";
            // 
            // openKavuaListToolStripMenuItem
            // 
            this.openKavuaListToolStripMenuItem.Name = "openKavuaListToolStripMenuItem";
            this.openKavuaListToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.openKavuaListToolStripMenuItem.Text = "&פתח רשימת וסת קבוע";
            this.openKavuaListToolStripMenuItem.Click += new System.EventHandler(this.openKavuaListToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(260, 6);
            // 
            // AddKavuahToolStripMenuItem
            // 
            this.AddKavuahToolStripMenuItem.Name = "AddKavuahToolStripMenuItem";
            this.AddKavuahToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.AddKavuahToolStripMenuItem.Text = "&הוסף חדש";
            this.AddKavuahToolStripMenuItem.Click += new System.EventHandler(this.AddKavuahToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(260, 6);
            // 
            // SearchForKavuahsToolStripMenuItem
            // 
            this.SearchForKavuahsToolStripMenuItem.Name = "SearchForKavuahsToolStripMenuItem";
            this.SearchForKavuahsToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.SearchForKavuahsToolStripMenuItem.Text = "&חשבן וסתי קבוע";
            this.SearchForKavuahsToolStripMenuItem.Click += new System.EventHandler(this.SearchForKavuahsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(260, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.importToolStripMenuItem.Text = "ייבא רשימת וסת קבוע מקובץ אחר";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // RemoteToolStripMenuItem
            // 
            this.RemoteToolStripMenuItem.Name = "RemoteToolStripMenuItem";
            this.RemoteToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.RemoteToolStripMenuItem.Text = "&מקוון";
            this.RemoteToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemRemote_Click);
            // 
            // PreferencesToolStripMenuItem
            // 
            this.PreferencesToolStripMenuItem.Name = "PreferencesToolStripMenuItem";
            this.PreferencesToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.PreferencesToolStripMenuItem.Text = "&העדפות";
            this.PreferencesToolStripMenuItem.Click += new System.EventHandler(this.PreferencesToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.AboutToolStripMenuItem.Text = "&אודות";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AbouToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xml";
            this.openFileDialog1.Filter = "קבצי חשבשבון |*.pm|כל סוגי קובץ|*.*";
            this.openFileDialog1.InitialDirectory = global::Chashavshavon.Properties.Settings.Default.ChashFilesPath;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.InitialDirectory = global::Chashavshavon.Properties.Settings.Default.ChashFilesPath;
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.Empty;
            this.toolTip1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // btnCheshbonKavuahs
            // 
            this.btnCheshbonKavuahs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCheshbonKavuahs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnCheshbonKavuahs.FlatAppearance.BorderSize = 0;
            this.btnCheshbonKavuahs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheshbonKavuahs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnCheshbonKavuahs.ForeColor = System.Drawing.Color.Blue;
            this.btnCheshbonKavuahs.Image = global::Chashavshavon.Properties.Resources.database_process;
            this.btnCheshbonKavuahs.Location = new System.Drawing.Point(0, 200);
            this.btnCheshbonKavuahs.Margin = new System.Windows.Forms.Padding(0);
            this.btnCheshbonKavuahs.Name = "btnCheshbonKavuahs";
            this.btnCheshbonKavuahs.Size = new System.Drawing.Size(86, 50);
            this.btnCheshbonKavuahs.TabIndex = 26;
            this.btnCheshbonKavuahs.Text = "חשבן קבוע";
            this.btnCheshbonKavuahs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnCheshbonKavuahs, "חשבן וסתי קבוע ע\"י סריקת רשימת הראיות");
            this.btnCheshbonKavuahs.UseVisualStyleBackColor = true;
            this.btnCheshbonKavuahs.Click += new System.EventHandler(this.btnCheshbonKavuahs_Click);
            // 
            // btnOpenKavuahs
            // 
            this.btnOpenKavuahs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenKavuahs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnOpenKavuahs.FlatAppearance.BorderSize = 0;
            this.btnOpenKavuahs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenKavuahs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnOpenKavuahs.ForeColor = System.Drawing.Color.Blue;
            this.btnOpenKavuahs.Image = global::Chashavshavon.Properties.Resources.database_search;
            this.btnOpenKavuahs.Location = new System.Drawing.Point(0, 150);
            this.btnOpenKavuahs.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenKavuahs.Name = "btnOpenKavuahs";
            this.btnOpenKavuahs.Size = new System.Drawing.Size(86, 50);
            this.btnOpenKavuahs.TabIndex = 24;
            this.btnOpenKavuahs.Text = "רשימת קבוע";
            this.btnOpenKavuahs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnOpenKavuahs, "הצג רשימת וסת קבוע");
            this.btnOpenKavuahs.UseVisualStyleBackColor = true;
            this.btnOpenKavuahs.Click += new System.EventHandler(this.btnOpenKavuahs_Click);
            // 
            // btnPrefs
            // 
            this.btnPrefs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPrefs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnPrefs.FlatAppearance.BorderSize = 0;
            this.btnPrefs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrefs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnPrefs.ForeColor = System.Drawing.Color.Blue;
            this.btnPrefs.Image = global::Chashavshavon.Properties.Resources.process;
            this.btnPrefs.Location = new System.Drawing.Point(0, 300);
            this.btnPrefs.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrefs.Name = "btnPrefs";
            this.btnPrefs.Size = new System.Drawing.Size(86, 50);
            this.btnPrefs.TabIndex = 32;
            this.btnPrefs.Text = "העדפות";
            this.btnPrefs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnPrefs, "העדפות ואופציות כולל גדרים הלכתיים");
            this.btnPrefs.UseVisualStyleBackColor = true;
            this.btnPrefs.Click += new System.EventHandler(this.btnPrefs_Click);
            // 
            // btnAddEntry
            // 
            this.btnAddEntry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddEntry.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAddEntry.FlatAppearance.BorderSize = 0;
            this.btnAddEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddEntry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAddEntry.ForeColor = System.Drawing.Color.Blue;
            this.btnAddEntry.Image = global::Chashavshavon.Properties.Resources.add;
            this.btnAddEntry.Location = new System.Drawing.Point(0, 50);
            this.btnAddEntry.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddEntry.Name = "btnAddEntry";
            this.btnAddEntry.Size = new System.Drawing.Size(86, 50);
            this.btnAddEntry.TabIndex = 33;
            this.btnAddEntry.Text = "הוסף רשומה";
            this.btnAddEntry.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnAddEntry, "הוסף רשומה חדשה");
            this.btnAddEntry.UseVisualStyleBackColor = true;
            this.btnAddEntry.Click += new System.EventHandler(this.btnAddEntry_Click);
            // 
            // btnAddKavuah
            // 
            this.btnAddKavuah.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddKavuah.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAddKavuah.FlatAppearance.BorderSize = 0;
            this.btnAddKavuah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddKavuah.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAddKavuah.ForeColor = System.Drawing.Color.Blue;
            this.btnAddKavuah.Image = global::Chashavshavon.Properties.Resources.database_add_small;
            this.btnAddKavuah.Location = new System.Drawing.Point(0, 250);
            this.btnAddKavuah.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddKavuah.Name = "btnAddKavuah";
            this.btnAddKavuah.Size = new System.Drawing.Size(86, 50);
            this.btnAddKavuah.TabIndex = 34;
            this.btnAddKavuah.Text = "הוסף קבוע";
            this.btnAddKavuah.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnAddKavuah, "העדפות ואופציות כולל גדרים הלכתיים");
            this.btnAddKavuah.UseVisualStyleBackColor = true;
            this.btnAddKavuah.Click += new System.EventHandler(this.btnAddKavuah_Click);
            // 
            // btnToday
            // 
            this.btnToday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToday.BackColor = System.Drawing.Color.Transparent;
            this.btnToday.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.btnToday.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnToday.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToday.FlatAppearance.BorderSize = 0;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("Narkisim", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnToday.ForeColor = System.Drawing.Color.Lavender;
            this.btnToday.Image = global::Chashavshavon.Properties.Resources.down;
            this.btnToday.Location = new System.Drawing.Point(717, 1);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(77, 35);
            this.btnToday.TabIndex = 36;
            this.btnToday.Text = "היום";
            this.btnToday.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnToday.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnToday, "הצג בלוח החודש הנוכחי");
            this.btnToday.UseVisualStyleBackColor = false;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // lblNextProblem
            // 
            this.lblNextProblem.AutoEllipsis = true;
            this.lblNextProblem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNextProblem.Font = new System.Drawing.Font("Narkisim", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblNextProblem.ForeColor = System.Drawing.Color.White;
            this.lblNextProblem.Location = new System.Drawing.Point(0, 0);
            this.lblNextProblem.Name = "lblNextProblem";
            this.lblNextProblem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblNextProblem.Size = new System.Drawing.Size(1006, 25);
            this.lblNextProblem.TabIndex = 16;
            this.lblNextProblem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 24);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.AutoScroll = true;
            this.splitContainerMain.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainerMain.Panel1.Controls.Add(this.flowLayoutPanel3);
            this.splitContainerMain.Panel1.Controls.Add(this.panel1);
            this.splitContainerMain.Panel1.Controls.Add(this.pbWeb);
            this.splitContainerMain.Panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainerMain.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainerMain.Panel1MinSize = 355;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.AutoScroll = true;
            this.splitContainerMain.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainerMain.Panel2.Controls.Add(this.pnlNextProblem);
            this.splitContainerMain.Panel2.Controls.Add(this.gbCalendar);
            this.splitContainerMain.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainerMain.Panel2MinSize = 133;
            this.splitContainerMain.Size = new System.Drawing.Size(1008, 538);
            this.splitContainerMain.SplitterDistance = 391;
            this.splitContainerMain.SplitterWidth = 3;
            this.splitContainerMain.TabIndex = 23;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.btnRefresh);
            this.flowLayoutPanel3.Controls.Add(this.btnAddEntry);
            this.flowLayoutPanel3.Controls.Add(this.btnViewTextCalendar);
            this.flowLayoutPanel3.Controls.Add(this.btnOpenKavuahs);
            this.flowLayoutPanel3.Controls.Add(this.btnCheshbonKavuahs);
            this.flowLayoutPanel3.Controls.Add(this.btnAddKavuah);
            this.flowLayoutPanel3.Controls.Add(this.btnPrefs);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(-2, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(86, 350);
            this.flowLayoutPanel3.TabIndex = 38;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Blue;
            this.btnRefresh.Image = global::Chashavshavon.Properties.Resources.refresh1;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(86, 50);
            this.btnRefresh.TabIndex = 37;
            this.btnRefresh.Text = "רענן הכל";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.luachTableLayout);
            this.panel1.Location = new System.Drawing.Point(85, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 348);
            this.panel1.TabIndex = 35;
            // 
            // pbWeb
            // 
            this.pbWeb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWeb.Image = ((System.Drawing.Image)(resources.GetObject("pbWeb.Image")));
            this.pbWeb.Location = new System.Drawing.Point(-509, -3);
            this.pbWeb.Name = "pbWeb";
            this.pbWeb.Size = new System.Drawing.Size(53, 51);
            this.pbWeb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbWeb.TabIndex = 15;
            this.pbWeb.TabStop = false;
            this.pbWeb.Click += new System.EventHandler(this.pbWeb_Click);
            // 
            // pnlNextProblem
            // 
            this.pnlNextProblem.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleBar;
            this.pnlNextProblem.Controls.Add(this.lblNextProblem);
            this.pnlNextProblem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNextProblem.Location = new System.Drawing.Point(0, 117);
            this.pnlNextProblem.Name = "pnlNextProblem";
            this.pnlNextProblem.Size = new System.Drawing.Size(1006, 25);
            this.pnlNextProblem.TabIndex = 17;
            // 
            // btnLastMonth
            // 
            this.btnLastMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLastMonth.BackColor = System.Drawing.Color.Transparent;
            this.btnLastMonth.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.btnLastMonth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLastMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLastMonth.FlatAppearance.BorderSize = 0;
            this.btnLastMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastMonth.Font = new System.Drawing.Font("Narkisim", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnLastMonth.ForeColor = System.Drawing.Color.Lavender;
            this.btnLastMonth.Image = global::Chashavshavon.Properties.Resources.next;
            this.btnLastMonth.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLastMonth.Location = new System.Drawing.Point(795, 1);
            this.btnLastMonth.Name = "btnLastMonth";
            this.btnLastMonth.Size = new System.Drawing.Size(126, 35);
            this.btnLastMonth.TabIndex = 29;
            this.btnLastMonth.Text = "button2";
            this.btnLastMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLastMonth.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLastMonth.UseVisualStyleBackColor = false;
            this.btnLastMonth.Click += new System.EventHandler(this.btnLastMonth_Click);
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.BackColor = System.Drawing.Color.Transparent;
            this.btnNextMonth.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleTile;
            this.btnNextMonth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNextMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextMonth.FlatAppearance.BorderSize = 0;
            this.btnNextMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextMonth.Font = new System.Drawing.Font("Narkisim", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnNextMonth.ForeColor = System.Drawing.Color.Lavender;
            this.btnNextMonth.Image = global::Chashavshavon.Properties.Resources.previous;
            this.btnNextMonth.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNextMonth.Location = new System.Drawing.Point(1, 1);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(126, 35);
            this.btnNextMonth.TabIndex = 28;
            this.btnNextMonth.Text = "button1";
            this.btnNextMonth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNextMonth.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnNextMonth.UseVisualStyleBackColor = false;
            this.btnNextMonth.Click += new System.EventHandler(this.btnNextMonth_Click);
            // 
            // lblMonthName
            // 
            this.lblMonthName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMonthName.AutoSize = true;
            this.lblMonthName.BackColor = System.Drawing.Color.Transparent;
            this.lblMonthName.Font = new System.Drawing.Font("Narkisim", 25F);
            this.lblMonthName.ForeColor = System.Drawing.Color.Lavender;
            this.lblMonthName.Location = new System.Drawing.Point(391, 2);
            this.lblMonthName.Name = "lblMonthName";
            this.lblMonthName.Size = new System.Drawing.Size(141, 34);
            this.lblMonthName.TabIndex = 30;
            this.lblMonthName.Text = "חודש שנה";
            this.lblMonthName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkBlueMarbleBar;
            this.panel2.Controls.Add(this.btnNextMonth);
            this.panel2.Controls.Add(this.btnLastMonth);
            this.panel2.Controls.Add(this.btnToday);
            this.panel2.Controls.Add(this.lblMonthName);
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(922, 38);
            this.panel2.TabIndex = 37;
            // 
            // luachTableLayout
            // 
            this.luachTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.luachTableLayout.BackColor = System.Drawing.Color.Transparent;
            this.luachTableLayout.BackgroundImage = global::Chashavshavon.Properties.Resources.DarkMarble;
            this.luachTableLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.luachTableLayout.ColumnCount = 7;
            this.luachTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.luachTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.luachTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.luachTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.luachTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.luachTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.luachTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.luachTableLayout.Controls.Add(this.label7, 6, 0);
            this.luachTableLayout.Controls.Add(this.label6, 5, 0);
            this.luachTableLayout.Controls.Add(this.label5, 4, 0);
            this.luachTableLayout.Controls.Add(this.label9, 3, 0);
            this.luachTableLayout.Controls.Add(this.label10, 2, 0);
            this.luachTableLayout.Controls.Add(this.label11, 1, 0);
            this.luachTableLayout.Controls.Add(this.label12, 0, 0);
            this.luachTableLayout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.luachTableLayout.Location = new System.Drawing.Point(0, 0);
            this.luachTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.luachTableLayout.Name = "luachTableLayout";
            this.luachTableLayout.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.luachTableLayout.RowCount = 6;
            this.luachTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.luachTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.luachTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.luachTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.luachTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.12766F));
            this.luachTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.14894F));
            this.luachTableLayout.Size = new System.Drawing.Size(922, 350);
            this.luachTableLayout.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.LightSlateGray;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Narkisim", 12F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(2, 2);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "שבת קודש";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.LightSlateGray;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Narkisim", 12F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(136, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "שישי";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightSlateGray;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Narkisim", 12F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(267, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "חמישי";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.LightSlateGray;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Narkisim", 12F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(398, 2);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 20);
            this.label9.TabIndex = 3;
            this.label9.Text = "רביעי";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.LightSlateGray;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Narkisim", 12F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(529, 2);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 20);
            this.label10.TabIndex = 2;
            this.label10.Text = "שלישי";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.LightSlateGray;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Narkisim", 12F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(660, 2);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 20);
            this.label11.TabIndex = 1;
            this.label11.Text = "שני";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.LightSlateGray;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Narkisim", 12F);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(791, 2);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(129, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "ראשון";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Narkisim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "חשבשבון";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResizeBegin += new System.EventHandler(this.frmMain_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.frmMain_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.dgEntries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEntries)).EndInit();
            this.gbCalendar.ResumeLayout(false);
            this.gbCalendar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbWeb)).EndInit();
            this.pnlNextProblem.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.luachTableLayout.ResumeLayout(false);
            this.luachTableLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgEntries;
        private System.Windows.Forms.GroupBox gbCalendar;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PreferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;        
        private System.Windows.Forms.ToolStripMenuItem SourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.PictureBox pbWeb;
        private System.Windows.Forms.Label lblNextProblem;
        private System.Windows.Forms.ToolStripMenuItem KavuahToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem PrintToolStripMenuItem;
        private System.Windows.Forms.Button btnViewTextCalendar;
        private System.Windows.Forms.ToolStripMenuItem TextListToolStripMenuItem;
        private System.Windows.Forms.Button btnPrintEntryList;
        private System.Windows.Forms.Button btnViewTextEntryList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPrintEntryList;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEntryList;
        private System.Windows.Forms.BindingSource bindingSourceEntries;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ToolStripMenuItem openKavuaListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddKavuahToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SearchForKavuahsToolStripMenuItem;
        private System.Windows.Forms.Button btnOpenKavuahs;
        private System.Windows.Forms.Button btnCheshbonKavuahs;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridViewLinkColumn btnDeleteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayNightColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntervalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayOfWeekColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotesColumn;
        private System.Windows.Forms.Button btnPrefs;
        private System.Windows.Forms.Label lblMonthName;
        private System.Windows.Forms.Button btnLastMonth;
        private System.Windows.Forms.Button btnNextMonth;
        private Chashavshavon.DoubleBufferedLayoutTable luachTableLayout;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnAddEntry;
        private System.Windows.Forms.Button btnAddKavuah;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnAddEntry2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.ToolStripMenuItem SourceTextMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRecentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem OpenBackupToolStripMenuItem;
        private System.Windows.Forms.Panel pnlNextProblem;



    }
}

