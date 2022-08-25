using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
namespace Chashavshavon
{
    public enum ChoiceSwitcherChoices
    {
        ChoiceOne,
        ChoiceTwo
    }

    [DefaultEvent("ChoiceSwitched")]
    public class ChoiceSwitcher : Control
    {
        private string _textChoiceOne;
        private string _textChoiceTwo;
        private object _valueChoiceOne;
        private object _valueChoiceTwo;
        private ChoiceSwitcherChoices _choiceChosen;
        private Color _backColorSlot;
        private Color _backColorSlotChoiceTwo;
        private Color _foreColorSelected;
        private Color _foreColorNotSelected;
        private Color _backColorSelected;
        private Color _backColorNotSelected;
        private Font _fontSelected;
        private Font _fontNotSelected;
        private float _fontSize;

        [Category("Action")]
        [Description("Fires when the choice is toggled")]
        public event EventHandler ChoiceSwitched = delegate (object sender, EventArgs e) { };

        public ChoiceSwitcher()
        {
            this.InitializeProps();
        }

        public object ValueChoiceOne
        {
            get { return this._valueChoiceOne; }
            set { this._valueChoiceOne = value; }
        }
        public object ValueChoiceTwo
        {
            get { return this._valueChoiceTwo; }
            set { this._valueChoiceTwo = value; }
        }
        public Color BackColorSelected
        {
            get { return this._backColorSelected; }
            set
            {
                if (this._backColorSelected != value)
                {
                    this._backColorSelected = value;
                    this.Invalidate();
                }
            }
        }
        public Color BackColorNotSelected
        {
            get { return this._backColorNotSelected; }
            set
            {
                if (this._backColorNotSelected != value)
                {
                    this._backColorNotSelected = value;
                    this.Invalidate();
                }
            }
        }
        public Font FontSelected
        {
            get { return this._fontSelected; }
            set
            {
                if (this._fontSelected != value)
                {
                    this._fontSelected = value;
                    this.Invalidate();
                }
            }
        }
        public Font FontNotSelected
        {
            get { return this._fontNotSelected; }
            set
            {
                if (this._fontNotSelected != value)
                {
                    this._fontNotSelected = value;
                    this.Invalidate();
                }
            }
        }
        public Color ForeColorSelected
        {
            get { return this._foreColorSelected; }
            set
            {
                if (this._foreColorSelected != value)
                {
                    this._foreColorSelected = value;
                    this.Invalidate();
                }
            }
        }
        public Color ForeColorNotSelected
        {
            get { return this._foreColorNotSelected; }
            set
            {
                if (this._foreColorNotSelected != value)
                {
                    this._foreColorNotSelected = value;
                    this.Invalidate();
                }
            }
        }
        public float FontSize
        {
            get { return this._fontSize; }
            set
            {
                if (this._fontSize != value)
                {
                    this._fontSize = value;
                    this.Font = new Font(this.Font.FontFamily, value, this.Font.Style);
                    this._fontSelected = new Font(this._fontSelected.FontFamily, value, this._fontSelected.Style);
                    this._fontNotSelected = new Font(this._fontNotSelected.FontFamily, value, this._fontNotSelected.Style);
                    this.Invalidate();
                }
            }
        }
        public object SelectedValue
        {
            get
            {
                return this._choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                    this._valueChoiceOne : this._valueChoiceTwo;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                if (this.SelectedValue != value)
                {
                    if (value.Equals(this._valueChoiceOne))
                    {
                        this.ChoiceChosen = ChoiceSwitcherChoices.ChoiceOne;
                    }
                    else if (value.Equals(this._valueChoiceTwo))
                    {
                        this.ChoiceChosen = ChoiceSwitcherChoices.ChoiceTwo;
                    }
                    else
                    {
                        throw new ArgumentException("The supplied object does not equal the value of either choice");
                    }
                }
            }
        }
        public string TextChoiceOne
        {
            get
            {
                return this._textChoiceOne;
            }
            set
            {
                this._textChoiceOne = value;
                this.Invalidate();
            }
        }
        public string TextChoiceTwo
        {
            get
            {
                return this._textChoiceTwo;
            }
            set
            {
                this._textChoiceTwo = value;
                this.Invalidate();
            }
        }
        public ChoiceSwitcherChoices ChoiceChosen
        {
            get
            {
                return this._choiceChosen;
            }
            set
            {
                if (this._choiceChosen != value)
                {
                    this._choiceChosen = value;
                    ChoiceSwitched(this, new EventArgs());
                    this.Invalidate();
                }
            }
        }
        public bool ChoiceOneSelected
        {
            get
            {
                return this._choiceChosen == ChoiceSwitcherChoices.ChoiceOne;
            }
            set
            {
                if (this.ChoiceOneSelected != value)
                {
                    this.ChoiceChosen = value ?
                        ChoiceSwitcherChoices.ChoiceOne : ChoiceSwitcherChoices.ChoiceTwo;
                }
            }
        }
        public bool ChoiceTwoSelected
        {
            get
            {
                return this._choiceChosen == ChoiceSwitcherChoices.ChoiceTwo;
            }
            set
            {
                if (this.ChoiceTwoSelected != value)
                {
                    this.ChoiceChosen = value ?
                        ChoiceSwitcherChoices.ChoiceTwo : ChoiceSwitcherChoices.ChoiceOne;
                }
            }
        }
        public Color BackColorSlot
        {
            get
            {
                return this._backColorSlot;
            }
            set
            {
                if (this._backColorSlot != value)
                {
                    this._backColorSlot = value;
                    this.Invalidate();
                }
            }
        }
        public Color BackColorSlotChoiceTwo
        {
            get
            {
                return this._backColorSlotChoiceTwo;
            }
            set
            {
                if (this._backColorSlotChoiceTwo != value)
                {
                    this._backColorSlotChoiceTwo = value;
                    this.Invalidate();
                }
            }
        }
        public bool DisplayAsYesNo
        {
            get
            {
                return
                    (this._textChoiceOne.ToLower() == "no" || this._textChoiceOne.ToLower() == "true") &&
                    (this._textChoiceTwo.ToLower() == "yes" || this._textChoiceTwo.ToLower() == "true") &&
                    this._valueChoiceOne == (object)false &&
                    this._valueChoiceTwo == (object)true;
            }
            set
            {
                if (this.DisplayAsYesNo != value)
                {
                    if (value)
                    {
                        this._textChoiceOne = "No";
                        this._valueChoiceOne = false;
                        this._textChoiceTwo = "Yes";
                        this._valueChoiceTwo = true;
                        this._backColorSlot = Color.Gray;
                        this._backColorSlotChoiceTwo = Color.Teal;
                    }
                    else
                    {
                        this.InitializeProps();
                    }
                    this.Invalidate();
                }
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            this.ChoiceChosen =
                this._choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                ChoiceSwitcherChoices.ChoiceTwo : ChoiceSwitcherChoices.ChoiceOne;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            this.SuspendLayout();
            base.OnPaint(e);
            using (var g = e.Graphics)
            {
                try
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    SizeF textOneSize = TextRenderer.MeasureText(this._textChoiceOne,
                            this._fontSelected),
                        textTwoSize = TextRenderer.MeasureText(this._textChoiceTwo,
                            this._fontSelected);
                    float textWidth = textOneSize.Width + textTwoSize.Width + 2,
                        slotWidth = ((this.Width - textWidth) * 0.8f) - 2,
                        slotHeight = this.Height * 0.7f,
                        slotTop = (this.Height - slotHeight) / 2f,
                        slotLeft = textOneSize.Width + 5f;
                    Brush slotBrush = new SolidBrush(
                        this._choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                            this._backColorSlot : this._backColorSlotChoiceTwo),
                        selectedBackBrush = new SolidBrush(this._backColorSelected),
                        notSelectedBackBrush = new SolidBrush(this._backColorNotSelected);

                    if (this._choiceChosen == ChoiceSwitcherChoices.ChoiceOne &&
                        this._backColorSelected != this.BackColor)
                    {
                        g.FillRectangle(
                            selectedBackBrush,
                            0,
                            0,
                            textOneSize.Width,
                            this.Height);

                    }
                    else if (this._choiceChosen == ChoiceSwitcherChoices.ChoiceTwo &&
                        this._backColorNotSelected != this.BackColor)
                    {
                        g.FillRectangle(
                            notSelectedBackBrush,
                            0,
                            0,
                            textOneSize.Width,
                            this.Height);

                    }
                    TextRenderer.DrawText(g,
                        this._textChoiceOne,
                        this._choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                            this._fontSelected : this._fontNotSelected,
                        new Point(0, (int)((this.Height / 2) - (textOneSize.Height / 2))),
                         this._choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                            this._foreColorSelected : this._foreColorNotSelected);
                    GraphicsPath graphPath = new GraphicsPath
                    {
                        FillMode = FillMode.Winding
                    };
                    graphPath.AddEllipse(slotLeft, slotTop, slotHeight, slotHeight);
                    graphPath.AddRectangle(
                        new RectangleF(
                            slotLeft + (slotHeight / 2),
                            slotTop,
                            (slotWidth - slotHeight),
                            slotHeight));
                    graphPath.AddEllipse(
                        (slotLeft + slotWidth) - slotHeight,
                        slotTop,
                        slotHeight,
                        slotHeight);
                    g.FillPath(slotBrush, graphPath);
                    g.DrawImage(
                        Properties.Resources.SwitchHead,
                        new RectangleF(
                            (this._choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                                slotLeft - 2 : slotLeft + (slotWidth - slotHeight)) - 2,
                            0,
                            this.Height,
                            this.Height));
                    if (this._backColorSelected != this.BackColor &&
                        this._choiceChosen == ChoiceSwitcherChoices.ChoiceTwo)
                    {
                        g.FillRectangle(
                            selectedBackBrush,
                            (slotLeft + slotWidth) + 5,
                            0,
                            textTwoSize.Width,
                            this.Height);
                    }
                    else if (this._backColorNotSelected != this.BackColor &&
                        this._choiceChosen == ChoiceSwitcherChoices.ChoiceOne)
                    {
                        g.FillRectangle(
                            notSelectedBackBrush,
                            (slotLeft + slotWidth) + 5,
                            0,
                            textTwoSize.Width,
                            this.Height);
                    }
                    TextRenderer.DrawText(g,
                    this._textChoiceTwo,
                     this._choiceChosen == ChoiceSwitcherChoices.ChoiceTwo ?
                         this._fontSelected : this._fontNotSelected,
                     new Point((int)((slotLeft + slotWidth) + 5),
                        (int)((this.Height / 2) - (textOneSize.Height / 2))),
                      this._choiceChosen == ChoiceSwitcherChoices.ChoiceTwo ?
                         this._foreColorSelected : this._foreColorNotSelected);

                    slotBrush.Dispose();
                    selectedBackBrush.Dispose();
                    notSelectedBackBrush.Dispose();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            this.ResumeLayout();
        }
        private void InitializeProps()
        {
            this.Width = 100;
            this.Height = 25;
            this._textChoiceOne = "One";
            this._textChoiceTwo = "Two";
            this._valueChoiceOne = false;
            this._valueChoiceTwo = true;
            this._choiceChosen = ChoiceSwitcherChoices.ChoiceOne;
            this._backColorSlot = SystemColors.ControlDark;
            this._backColorSlotChoiceTwo = this._backColorSlot;
            this._fontSize = this.Font.Size;
            this._fontSelected = this.Font;
            this._fontNotSelected = this._fontSelected;
            this._backColorSelected = Color.Transparent;
            this._backColorNotSelected = this._backColorSelected;
            this._foreColorSelected = this.ForeColor;
            this._foreColorNotSelected = this._foreColorSelected;
        }
    }
}