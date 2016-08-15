
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialWinforms.Controls
{

[DefaultEvent("TextChanged")]
public class MaterialFileInput : Control, IMaterialControl
{

    #region  Variables

    Button InPutBTN = new Button();
    MaterialWinforms.Controls.MaterialSingleLineTextField MaterialTB = new MaterialWinforms.Controls.MaterialSingleLineTextField();

    HorizontalAlignment ALNType;
    int maxchars = 32767;
    bool readOnly;
    bool previousReadOnly;
    bool isPasswordMasked = false;
    bool Enable = true;
       
    Timer AnimationTimer = new Timer { Interval = 1 };
    FontManager font = new FontManager();
    
    public OpenFileDialog Dialog;
    string filter = @"All Files (*.*)|*.*";

    bool Focus = false;
    bool mouseOver = false;

    float SizeAnimation = 0;
    float SizeInc_Dec;

    float PointAnimation;
    float PointInc_Dec;

    string fontColor = "#999999";
    string focusColor = "#508ef5";

    Color EnabledFocusedColor;
    Color EnabledStringColor;

    Color EnabledInPutColor = ColorTranslator.FromHtml("#acacac");
    Color EnabledUnFocusedColor = ColorTranslator.FromHtml("#dbdbdb");

    Color DisabledInputColor = ColorTranslator.FromHtml("#d1d2d4");
    Color DisabledUnFocusedColor = ColorTranslator.FromHtml("#e9ecee");
    Color DisabledStringColor = ColorTranslator.FromHtml("#babbbd");

    [Browsable(false)]
    public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }

    #endregion
    #region  Properties

    //Properties for managing the material design properties
    [Browsable(false)]
    public int Depth { get; set; }
    [Browsable(false)]
    public MouseState MouseState { get; set; }
    public Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : typeof(IShadowedMaterialControl).IsAssignableFrom(Parent.GetType()) ? ((IMaterialControl)Parent).BackColor : Parent.BackColor; } }

    public HorizontalAlignment TextAlignment
    {
        get
        {
            return ALNType;
        }
        set
        {
            ALNType = value;
            Invalidate();
        }
    }

    [Category("Behavior")]
    public int MaxLength
    {
        get
        {
            return maxchars;
        }
        set
        {
            maxchars = value;
            MaterialTB.MaxLength = MaxLength;
            Invalidate();
        }
    }

    [Category("Behavior")]
    public bool UseSystemPasswordChar
    {
        get
        {
            return isPasswordMasked;
        }
        set
        {
            MaterialTB.UseSystemPasswordChar = UseSystemPasswordChar;
            isPasswordMasked = value;
            Invalidate();
        }
    }

    [Category("Behavior")]
    public bool ReadOnly
    {
        get
        {
            return readOnly;
        }
        set
        {
            readOnly = value;
            if (MaterialTB != null)
            {
                MaterialTB.ReadOnly = value;
            }
        }
    }

    [Category("Behavior")]
    public bool IsEnabled
    {
        get { return Enable; }
        set
        {
            Enable = value;

            if (IsEnabled)
            {
                readOnly = previousReadOnly;
                MaterialTB.ReadOnly = previousReadOnly;
                MaterialTB.ForeColor = EnabledStringColor;
                InPutBTN.Enabled = true;
            }
            else
            {
                previousReadOnly = ReadOnly;
                ReadOnly = true;
                MaterialTB.ForeColor = DisabledStringColor;
                InPutBTN.Enabled = false;
            }

            Invalidate();
        }
    }

    [Category("Behavior")]
    public string Filter
    {
        get { return filter; }
        set
        {
            filter = value;
            Invalidate();
        }
    }

    [Category("Appearance")]
    public string FocusedColor
    {
        get { return focusColor; }
        set
        {
            focusColor = value;
            Invalidate();
        }
    }

    [Category("Appearance")]
    public string FontColor
    {
        get { return fontColor; }
        set
        {
            fontColor = value;
            Invalidate();
        }
    }

    [Category("Appearance")]
    public string Hint
    {
        get { return MaterialTB.Hint; }
        set
        {
            MaterialTB.Hint = value;
            Invalidate();
        }
    }

    [Browsable(false)]
    public bool Enabled
    {
        get { return base.Enabled; }
        set { base.Enabled = value; }
    }

    [Browsable(false)]
    public Font Font
    {
        get { return base.Font; }
        set { base.Font = value; }
    }

    [Browsable(false)]
    public Color ForeColor
    {
        get { return base.ForeColor; }
        set { base.ForeColor = value; }
    }

    #endregion
    #region  Events

    protected void OnKeyDown(object Obj, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.A)
        {
            MaterialTB.SelectAll();
            e.SuppressKeyPress = true;
        }
        if (e.Control && e.KeyCode == Keys.C)
        {
            MaterialTB.Copy();
            e.SuppressKeyPress = true;
        }
        if (e.Control && e.KeyCode == Keys.X)
        {
            MaterialTB.Cut();
            e.SuppressKeyPress = true;
        }
    }
    private void BrowseDown(object Obj, EventArgs e)
    {      
        Dialog = new OpenFileDialog();
        Dialog.Filter = filter;
        DialogResult result = Dialog.ShowDialog();

        if (result == DialogResult.OK && Dialog.SafeFileName != null)
        {
            Text = Dialog.FileName;
        }
        Focus();
    }
    protected override void OnTextChanged(System.EventArgs e)
    {
        base.OnTextChanged(e);
        Invalidate();
    }

    protected override void OnGotFocus(System.EventArgs e)
    {
        base.OnGotFocus(e);
        MaterialTB.Focus();
        MaterialTB.SelectionLength = 0;
    }
    protected override void OnResize(System.EventArgs e)
    {
        base.OnResize(e);

        Height = MaterialTB.Height;

        PointAnimation = Width / 2;
        SizeInc_Dec = Width / 18;
        PointInc_Dec = Width / 36;

        MaterialTB.Width = Width - 21;
        InPutBTN.Location = new Point(Width - 21, 1);
        InPutBTN.Size = new Size(21, this.Height - 2);
    }  

    #endregion
    public void AddButton()
    {
        InPutBTN.Location = new Point(Width - 21, 1);
        InPutBTN.Size = new Size(21, this.Height - 2);

        InPutBTN.ForeColor = Color.FromArgb(255, 255, 255);
        InPutBTN.TextAlign = ContentAlignment.MiddleCenter;
        InPutBTN.BackColor = Color.Transparent;

        InPutBTN.TabStop = false;
        InPutBTN.FlatStyle = FlatStyle.Flat;
        InPutBTN.FlatAppearance.MouseOverBackColor = Color.Transparent;
        InPutBTN.FlatAppearance.MouseDownBackColor = Color.Transparent;
        InPutBTN.FlatAppearance.BorderSize = 0;

        InPutBTN.Click += new EventHandler(BrowseDown);
        InPutBTN.MouseEnter += (sender, args) => mouseOver = true;
        InPutBTN.MouseLeave += (sender, args) => mouseOver = false;
    }
    public void AddTextBox()
    {
        MaterialTB.Text = Text;
        MaterialTB.Location = new Point(0, 1);
        MaterialTB.Size = new Size(Width - 21, 20);

        MaterialTB.Font = font.Roboto_Regular10;
        MaterialTB.UseSystemPasswordChar = UseSystemPasswordChar;

        MaterialTB.KeyDown += OnKeyDown;

        MaterialTB.GotFocus += (sender, args) => Focus = true; AnimationTimer.Start();
        MaterialTB.LostFocus += (sender, args) => Focus = false; AnimationTimer.Start();
    }
    public MaterialFileInput()
    {
        Width = 300;
        DoubleBuffered = true;       
        previousReadOnly = ReadOnly;

        AddTextBox();
        AddButton();
        Controls.Add(MaterialTB);
        Controls.Add(InPutBTN);

        MaterialTB.TextChanged += (sender, args) => Text = MaterialTB.Text;
        base.TextChanged += (sender, args) => MaterialTB.Text = Text;

        AnimationTimer.Tick += new EventHandler(AnimationTick);
    }

    protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
    {
        base.OnPaint(e);
        Bitmap B = new Bitmap(Width, Height);
        Graphics G = Graphics.FromImage(B);
        G.Clear(BackColor);

        EnabledStringColor = ColorTranslator.FromHtml(fontColor);
        EnabledFocusedColor = ColorTranslator.FromHtml(focusColor);

        MaterialTB.TextAlign = TextAlignment;
        MaterialTB.ForeColor = IsEnabled ? EnabledStringColor : DisabledStringColor;
        MaterialTB.UseSystemPasswordChar = UseSystemPasswordChar;

        G.DrawLine(new Pen(new SolidBrush(IsEnabled ? SkinManager.GetDividersColor() : DisabledUnFocusedColor)), new Point(0, Height - 1), new Point(Width, Height - 1));
        if (IsEnabled)
        { G.FillRectangle(MaterialTB.Focused() ? SkinManager.ColorScheme.PrimaryBrush : SkinManager.GetDividersBrush(), PointAnimation, (float)Height - 1, SizeAnimation, MaterialTB.Focused() ? 2 : 1); }


        G.SmoothingMode = SmoothingMode.AntiAlias;
        G.FillEllipse(new SolidBrush(IsEnabled ? mouseOver ? SkinManager.ColorScheme.AccentColor : SkinManager.GetDividersColor() : DisabledInputColor), Width - 5, 24, 4, 4);
        G.FillEllipse(new SolidBrush(IsEnabled ? mouseOver ? SkinManager.ColorScheme.AccentColor : SkinManager.GetDividersColor() : DisabledInputColor), Width - 11, 24, 4, 4);
        G.FillEllipse(new SolidBrush(IsEnabled ? mouseOver ? SkinManager.ColorScheme.AccentColor : SkinManager.GetDividersColor() : DisabledInputColor), Width - 17, 24, 4, 4);

        e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
        G.Dispose();
        B.Dispose();
    }


    protected void AnimationTick(object sender, EventArgs e)
    {
        if (Focus)
        {
            if (SizeAnimation < Width)
            {
                SizeAnimation += SizeInc_Dec;
                this.Invalidate();
            }

            if (PointAnimation > 0)
            {
                PointAnimation -= PointInc_Dec;
                this.Invalidate();
            }
        }
        else
        {
            if (SizeAnimation > 0)
            {
                SizeAnimation -= SizeInc_Dec;
                this.Invalidate();
            }

            if (PointAnimation < Width / 2)
            {
                PointAnimation += PointInc_Dec;
                this.Invalidate();
            }
        }
    }

}
}


