using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chashavshavon.Utils;

namespace Chashavshavon
{
    public partial class frmLuach : Form
    {
        private DateTime _monthToDisplay;


        public frmLuach(DateTime initialMonthToDisplay, List<Onah> problemOnahs)
        {
            InitializeComponent();
            this._monthToDisplay = initialMonthToDisplay;
        }

        private void frmLuach_Load(object sender, EventArgs e)
        {            
            this.DisplayMonth();
        }

        private void DisplayMonth()
        {       
            this.tableLayoutPanel1.Visible = false;
            this.tableLayoutPanel1.SuspendLayout();

            foreach (Control c in this.tableLayoutPanel1.Controls)
            {
                c.Dispose();
            }
            this.tableLayoutPanel1.Controls.Clear();
            int year = Program.HebrewCalendar.GetYear(this._monthToDisplay);
            MonthObject month = new MonthObject(year, Program.HebrewCalendar.GetMonth(this._monthToDisplay));
            int firstDayOfWeek = 1 + (int)this._monthToDisplay.AddDays(1 - Program.HebrewCalendar.GetDayOfMonth(this._monthToDisplay)).DayOfWeek;
            int currentRow = 1, currentColumn = firstDayOfWeek - 1;

            this.lblMonthName.Text = this._monthToDisplay.ToString("MMM yyyy", Program.CultureInfo);
            this.btnLastMonth.Text = "  " + this._monthToDisplay.AddMonths(-1).ToString("MMM") + "  ";
            this.btnNextMonth.Text = "  " + this._monthToDisplay.AddMonths(1).ToString("MMM") + "  ";

            for (int i = 0; i < 7; i++)
            {
                this.tableLayoutPanel1.Controls.Add(new Label()
                {
                    Text = Zmanim.DaysOfWeekHebrewFull[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                }, i, 0);
            }

            for (int i = 1; i < month.DaysInMonth + 1; i++)
            {
                DateTime date = new DateTime(Program.HebrewCalendar.GetYear(this._monthToDisplay),
                    Program.HebrewCalendar.GetMonth(this._monthToDisplay),
                    i,
                    Program.HebrewCalendar);
                Panel pnl = new Panel()
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    BorderStyle = Program.Today.IsSameday(date) ? BorderStyle.FixedSingle : BorderStyle.None,
                    Tag = date
                };

                pnl.Click += new EventHandler(AddNewEntry);
                pnl.Controls.Add(new Label()
                {
                    Dock = DockStyle.Top,
                    Font = new Font(Font.FontFamily, 15f, FontStyle.Bold),
                    ForeColor = Program.Today.IsSameday(date) ? Color.Blue : Color.SaddleBrown,
                    Text = Zmanim.DaysOfMonthHebrew[i],
                    TextAlign = ContentAlignment.MiddleCenter,
                    RightToLeft = System.Windows.Forms.RightToLeft.Yes
                });

                string daySpecialText = "";
                foreach (string holiday in JewishHolidays.GetHebrewHolidays(date, Program.CurrentPlace.IsInIsrael))
                {
                    daySpecialText += holiday + " ";
                }
                if (daySpecialText.Length > 0)
                {
                    pnl.Controls.Add(new Label()
                    {
                        Dock = DockStyle.Bottom,
                        Padding = new Padding(0, 7, 0, 2),
                        Text = daySpecialText,
                        Font = new Font(Font.FontFamily, 6f),
                        ForeColor = Color.DarkGreen,
                        TextAlign = ContentAlignment.MiddleCenter,
                        RightToLeft = System.Windows.Forms.RightToLeft.Yes
                    });
                }

                string onahText = "";

                if (Program.MainForm.ProblemOnas != null)
                {
                    var pOnahs = Program.MainForm.ProblemOnas.Where(o => o.DateTime == date);
                    if (pOnahs.Count() > 0)
                    {
                        foreach (var o in pOnahs)
                        {
                            onahText += (o.IsIgnored ? "[" : "") + o.HebrewDayNight + " - " + 
                                o.Name + (o.IsIgnored ? "]" : "") + Environment.NewLine;
                        }
                        this.toolTip1.SetToolTip(pnl, onahText);
                    }
                }

                Entry entry = Entry.EntryList.FirstOrDefault(en => 
                    !en.IsInvisible &&
                    en.DateTime == date);
                if (entry != null)
                {
                    pnl.BackColor = Color.Pink;
                    pnl.Controls.Add(new Label()
                    {
                        Dock = DockStyle.Bottom,
                        Text = "ראיה - עונת " + entry.HebrewDayNight + Environment.NewLine + " הפלגה: " + entry.Interval.ToString(),
                        ForeColor = Color.Red,
                        Font = new Font(Font.FontFamily, 6f),
                        TextAlign = ContentAlignment.TopCenter,
                        RightToLeft = System.Windows.Forms.RightToLeft.Yes
                    });
                }
                else if (!string.IsNullOrEmpty(onahText))
                {
                    pnl.BackColor = Color.Yellow;
                    pnl.Controls.Add(new Label()
                    {
                        Dock = DockStyle.Bottom,
                        Text = onahText.Substring(0, 17).PadRight(20, '.'),
                        TextAlign = ContentAlignment.TopCenter,
                        Font = new Font(Font.FontFamily, 6f),
                        ForeColor = Color.Black,
                        RightToLeft = System.Windows.Forms.RightToLeft.Yes
                    });
                }
                else if (currentColumn == tableLayoutPanel1.ColumnCount - 1)
                {
                    pnl.BackColor = Color.Tan;
                }

                foreach (Label lbl in pnl.Controls.OfType<Label>())
                {
                    lbl.Tag = date;
                    lbl.Click += new EventHandler(AddNewEntry);
                    if (!string.IsNullOrEmpty(onahText))
                    {
                        this.toolTip1.SetToolTip(lbl, onahText);
                    }
                }

                this.tableLayoutPanel1.Controls.Add(pnl, currentColumn, currentRow);

                if (currentColumn == tableLayoutPanel1.ColumnCount - 1)
                {
                    currentColumn = 0;
                    currentRow++;
                }
                else
                {
                    currentColumn++;
                }
            }
            this.tableLayoutPanel1.ResumeLayout();
            this.tableLayoutPanel1.Visible = true;
        }

        void AddNewEntry(object sender, EventArgs e)
        {
            frmAddNewEntry f = new frmAddNewEntry((DateTime)((Control)sender).Tag);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Refresh in case of change to current month
                this.DisplayMonth();
            }
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            this._monthToDisplay = this._monthToDisplay.AddMonths(1);
            this.DisplayMonth();
        }

        private void btnLastMonth_Click(object sender, EventArgs e)
        {
            this._monthToDisplay = this._monthToDisplay.AddMonths(-1);
            this.DisplayMonth();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLuach_ResizeBegin(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.Visible = false;
            this.tableLayoutPanel1.SuspendLayout();
        }

        
        private void frmLuach_ResizeEnd(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.ResumeLayout();
            this.tableLayoutPanel1.Visible = true;            
        }      
    }
}
