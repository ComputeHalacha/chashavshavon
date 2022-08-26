using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
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
            InitializeProps();
        }

        public object ValueChoiceOne
        {
            get => _valueChoiceOne;
            set => _valueChoiceOne = value;
        }
        public object ValueChoiceTwo
        {
            get => _valueChoiceTwo;
            set => _valueChoiceTwo = value;
        }
        public Color BackColorSelected
        {
            get => _backColorSelected;
            set
            {
                if (_backColorSelected != value)
                {
                    _backColorSelected = value;
                    Invalidate();
                }
            }
        }
        public Color BackColorNotSelected
        {
            get => _backColorNotSelected;
            set
            {
                if (_backColorNotSelected != value)
                {
                    _backColorNotSelected = value;
                    Invalidate();
                }
            }
        }
        public Font FontSelected
        {
            get => _fontSelected;
            set
            {
                if (_fontSelected != value)
                {
                    _fontSelected = value;
                    Invalidate();
                }
            }
        }
        public Font FontNotSelected
        {
            get => _fontNotSelected;
            set
            {
                if (_fontNotSelected != value)
                {
                    _fontNotSelected = value;
                    Invalidate();
                }
            }
        }
        public Color ForeColorSelected
        {
            get => _foreColorSelected;
            set
            {
                if (_foreColorSelected != value)
                {
                    _foreColorSelected = value;
                    Invalidate();
                }
            }
        }
        public Color ForeColorNotSelected
        {
            get => _foreColorNotSelected;
            set
            {
                if (_foreColorNotSelected != value)
                {
                    _foreColorNotSelected = value;
                    Invalidate();
                }
            }
        }
        public float FontSize
        {
            get => _fontSize;
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    Font = new Font(Font.FontFamily, value, Font.Style);
                    _fontSelected = new Font(_fontSelected.FontFamily, value, _fontSelected.Style);
                    _fontNotSelected = new Font(_fontNotSelected.FontFamily, value, _fontNotSelected.Style);
                    Invalidate();
                }
            }
        }
        public object SelectedValue
        {
            get => _choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                    _valueChoiceOne : _valueChoiceTwo;
            set
            {
                if (value == null)
                {
                    return;
                }
                if (SelectedValue != value)
                {
                    if (value.Equals(_valueChoiceOne))
                    {
                        ChoiceChosen = ChoiceSwitcherChoices.ChoiceOne;
                    }
                    else if (value.Equals(_valueChoiceTwo))
                    {
                        ChoiceChosen = ChoiceSwitcherChoices.ChoiceTwo;
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
            get => _textChoiceOne;
            set
            {
                _textChoiceOne = value;
                Invalidate();
            }
        }
        public string TextChoiceTwo
        {
            get => _textChoiceTwo;
            set
            {
                _textChoiceTwo = value;
                Invalidate();
            }
        }
        public ChoiceSwitcherChoices ChoiceChosen
        {
            get => _choiceChosen;
            set
            {
                if (_choiceChosen != value)
                {
                    _choiceChosen = value;
                    ChoiceSwitched(this, new EventArgs());
                    Invalidate();
                }
            }
        }
        public bool ChoiceOneSelected
        {
            get => _choiceChosen == ChoiceSwitcherChoices.ChoiceOne;
            set
            {
                if (ChoiceOneSelected != value)
                {
                    ChoiceChosen = value ?
                        ChoiceSwitcherChoices.ChoiceOne : ChoiceSwitcherChoices.ChoiceTwo;
                }
            }
        }
        public bool ChoiceTwoSelected
        {
            get => _choiceChosen == ChoiceSwitcherChoices.ChoiceTwo;
            set
            {
                if (ChoiceTwoSelected != value)
                {
                    ChoiceChosen = value ?
                        ChoiceSwitcherChoices.ChoiceTwo : ChoiceSwitcherChoices.ChoiceOne;
                }
            }
        }
        public Color BackColorSlot
        {
            get => _backColorSlot;
            set
            {
                if (_backColorSlot != value)
                {
                    _backColorSlot = value;
                    Invalidate();
                }
            }
        }
        public Color BackColorSlotChoiceTwo
        {
            get => _backColorSlotChoiceTwo;
            set
            {
                if (_backColorSlotChoiceTwo != value)
                {
                    _backColorSlotChoiceTwo = value;
                    Invalidate();
                }
            }
        }
        public bool DisplayAsYesNo
        {
            get => (_textChoiceOne.ToLower() == "no" || _textChoiceOne.ToLower() == "true") &&
                    (_textChoiceTwo.ToLower() == "yes" || _textChoiceTwo.ToLower() == "true") &&
                    _valueChoiceOne == (object)false &&
                    _valueChoiceTwo == (object)true;
            set
            {
                if (DisplayAsYesNo != value)
                {
                    if (value)
                    {
                        _textChoiceOne = "No";
                        _valueChoiceOne = false;
                        _textChoiceTwo = "Yes";
                        _valueChoiceTwo = true;
                        _backColorSlot = Color.Gray;
                        _backColorSlotChoiceTwo = Color.Teal;
                    }
                    else
                    {
                        InitializeProps();
                    }
                    Invalidate();
                }
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            ChoiceChosen =
                _choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                ChoiceSwitcherChoices.ChoiceTwo : ChoiceSwitcherChoices.ChoiceOne;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            SuspendLayout();
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                try
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    SizeF textOneSize = TextRenderer.MeasureText(_textChoiceOne,
                            _fontSelected),
                        textTwoSize = TextRenderer.MeasureText(_textChoiceTwo,
                            _fontSelected);
                    float textWidth = textOneSize.Width + textTwoSize.Width + 2,
                        slotWidth = ((Width - textWidth) * 0.8f) - 2,
                        slotHeight = Height * 0.7f,
                        slotTop = (Height - slotHeight) / 2f,
                        slotLeft = textOneSize.Width + 5f;
                    Brush slotBrush = new SolidBrush(
                        _choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                            _backColorSlot : _backColorSlotChoiceTwo),
                        selectedBackBrush = new SolidBrush(_backColorSelected),
                        notSelectedBackBrush = new SolidBrush(_backColorNotSelected);

                    if (_choiceChosen == ChoiceSwitcherChoices.ChoiceOne &&
                        _backColorSelected != BackColor)
                    {
                        g.FillRectangle(
                            selectedBackBrush,
                            0,
                            0,
                            textOneSize.Width,
                            Height);

                    }
                    else if (_choiceChosen == ChoiceSwitcherChoices.ChoiceTwo &&
                        _backColorNotSelected != BackColor)
                    {
                        g.FillRectangle(
                            notSelectedBackBrush,
                            0,
                            0,
                            textOneSize.Width,
                            Height);

                    }
                    TextRenderer.DrawText(g,
                        _textChoiceOne,
                        _choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                            _fontSelected : _fontNotSelected,
                        new Point(0, (int)((Height / 2) - (textOneSize.Height / 2))),
                         _choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                            _foreColorSelected : _foreColorNotSelected);
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
                            (_choiceChosen == ChoiceSwitcherChoices.ChoiceOne ?
                                slotLeft - 2 : slotLeft + (slotWidth - slotHeight)) - 2,
                            0,
                            Height,
                            Height));
                    if (_backColorSelected != BackColor &&
                        _choiceChosen == ChoiceSwitcherChoices.ChoiceTwo)
                    {
                        g.FillRectangle(
                            selectedBackBrush,
                            (slotLeft + slotWidth) + 5,
                            0,
                            textTwoSize.Width,
                            Height);
                    }
                    else if (_backColorNotSelected != BackColor &&
                        _choiceChosen == ChoiceSwitcherChoices.ChoiceOne)
                    {
                        g.FillRectangle(
                            notSelectedBackBrush,
                            (slotLeft + slotWidth) + 5,
                            0,
                            textTwoSize.Width,
                            Height);
                    }
                    TextRenderer.DrawText(g,
                    _textChoiceTwo,
                     _choiceChosen == ChoiceSwitcherChoices.ChoiceTwo ?
                         _fontSelected : _fontNotSelected,
                     new Point((int)((slotLeft + slotWidth) + 5),
                        (int)((Height / 2) - (textOneSize.Height / 2))),
                      _choiceChosen == ChoiceSwitcherChoices.ChoiceTwo ?
                         _foreColorSelected : _foreColorNotSelected);

                    slotBrush.Dispose();
                    selectedBackBrush.Dispose();
                    notSelectedBackBrush.Dispose();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            ResumeLayout();
        }
        private void InitializeProps()
        {
            Width = 100;
            Height = 25;
            _textChoiceOne = "One";
            _textChoiceTwo = "Two";
            _valueChoiceOne = false;
            _valueChoiceTwo = true;
            _choiceChosen = ChoiceSwitcherChoices.ChoiceOne;
            _backColorSlot = SystemColors.ControlDark;
            _backColorSlotChoiceTwo = _backColorSlot;
            _fontSize = Font.Size;
            _fontSelected = Font;
            _fontNotSelected = _fontSelected;
            _backColorSelected = Color.Transparent;
            _backColorNotSelected = _backColorSelected;
            _foreColorSelected = ForeColor;
            _foreColorNotSelected = _foreColorSelected;
        }
    }
}