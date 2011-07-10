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
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.dataRepeater1 = new Microsoft.VisualBasic.PowerPacks.DataRepeater();
            this.kavuahDescriptionHebrewLabel1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.kavuahBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataRepeater1.ItemTemplate.SuspendLayout();
            this.dataRepeater1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kavuahBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.BurlyWood;
            this.button2.Location = new System.Drawing.Point(548, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "חדש";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Narkisim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(12, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "רשימת וסת קבוע";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.BurlyWood;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(455, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "בטל";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataRepeater1
            // 
            this.dataRepeater1.AllowUserToAddItems = false;
            this.dataRepeater1.AllowUserToDeleteItems = false;
            this.dataRepeater1.BackColor = System.Drawing.Color.White;
            this.dataRepeater1.DataSource = this.kavuahBindingSource;
            this.dataRepeater1.ItemHeaderVisible = false;
            // 
            // dataRepeater1.ItemTemplate
            // 
            this.dataRepeater1.ItemTemplate.BackColor = System.Drawing.Color.White;
            this.dataRepeater1.ItemTemplate.Controls.Add(this.kavuahDescriptionHebrewLabel1);
            this.dataRepeater1.ItemTemplate.Controls.Add(this.btnDelete);
            this.dataRepeater1.ItemTemplate.Size = new System.Drawing.Size(627, 34);
            this.dataRepeater1.Location = new System.Drawing.Point(6, 51);
            this.dataRepeater1.Name = "dataRepeater1";
            this.dataRepeater1.SelectionColor = System.Drawing.Color.Wheat;
            this.dataRepeater1.Size = new System.Drawing.Size(635, 211);
            this.dataRepeater1.TabIndex = 3;
            this.dataRepeater1.Text = "dataRepeater1";
            // 
            // kavuahDescriptionHebrewLabel1
            // 
            this.kavuahDescriptionHebrewLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kavuahBindingSource, "KavuahDescriptionHebrew", true));
            this.kavuahDescriptionHebrewLabel1.Font = new System.Drawing.Font("Narkisim", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.kavuahDescriptionHebrewLabel1.ForeColor = System.Drawing.Color.Black;
            this.kavuahDescriptionHebrewLabel1.Location = new System.Drawing.Point(3, 3);
            this.kavuahDescriptionHebrewLabel1.Name = "kavuahDescriptionHebrewLabel1";
            this.kavuahDescriptionHebrewLabel1.Size = new System.Drawing.Size(574, 25);
            this.kavuahDescriptionHebrewLabel1.TabIndex = 8;
            this.kavuahDescriptionHebrewLabel1.Text = "label1";
            this.kavuahDescriptionHebrewLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::Chashavshavon.Properties.Resources.delete;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Location = new System.Drawing.Point(597, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(20, 20);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // kavuahBindingSource
            // 
            this.kavuahBindingSource.DataSource = typeof(Chashavshavon.Kavuah);
            // 
            // frmKavuahs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Wheat;
            this.CancelButton = this.button3;
            this.ClientSize = new System.Drawing.Size(647, 274);
            this.Controls.Add(this.dataRepeater1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmKavuahs";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "רשימת וסת קבוע";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKavuahs_FormClosed);
            this.dataRepeater1.ItemTemplate.ResumeLayout(false);
            this.dataRepeater1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kavuahBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource kavuahBindingSource;
        private System.Windows.Forms.Button button3;
        private Microsoft.VisualBasic.PowerPacks.DataRepeater dataRepeater1;
        private System.Windows.Forms.Label kavuahDescriptionHebrewLabel1;
        private System.Windows.Forms.Button btnDelete;
    }
}