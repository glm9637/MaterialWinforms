using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialWinforms.Animations;
using System.Runtime.InteropServices;

namespace MaterialWinforms.Controls
{

    [DefaultEvent("TextChanged")]
    public class MaterialSearchButton : Control, IMaterialControl
    {

        #region  Variables

        SearchTextField MaterialTB = new SearchTextField();

        HorizontalAlignment ALNType;
        int maxchars = 32767;
        int StartX;
        AnimationManager AnimManager;

        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }

        private SizeF IconSize;
        private bool expanded;

        #endregion
        #region  Properties

        //Properties for managing the material design properties
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

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

        protected override void OnClick(System.EventArgs e)
        {
            if (!expanded)
            {
                BringToFront();
                AnimManager.SetProgress(0);
                AnimManager.StartNewAnimation(AnimationDirection.In);
            }
            else
            {
                if (new RectangleF(Width-IconSize.Width,0,IconSize.Width,Height).Contains(PointToClient(MousePosition)))
                {
                    AnimManager.SetProgress(1);
                    AnimManager.StartNewAnimation(AnimationDirection.Out);

                }
            }
            base.OnClick(e);
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
            MaterialTB.Width = Width - Convert.ToInt16(IconSize.Width);
        }

        #endregion
       
        public void AddTextBox()
        {
            MaterialTB.Text = Text;
            MaterialTB.Location = new Point(0, 1);
            MaterialTB.Size = new Size(Width - Convert.ToInt16(IconSize.Width), 20);
            MaterialTB.KeyDown += OnKeyDown;

        }

        public MaterialSearchButton()
        {
            expanded = false;
            DoubleBuffered = true;
            IconSize = new Size(25, 25);
            AddTextBox();
            Controls.Add(MaterialTB);
            Width = Convert.ToInt16(IconSize.Width);
            MaterialTB.TextChanged += (sender, args) => Text = MaterialTB.Text;
            base.TextChanged += (sender, args) => MaterialTB.Text = Text;
            AnimManager = new AnimationManager();
            AnimManager.AnimationType =  AnimationType.EaseOut;
            AnimManager.Increment = 0.03;
            AnimManager.OnAnimationProgress += AnimManager_OnAnimationProgress;
            MaterialTB.Text = String.Empty;
            MaterialTB.Hint = "Suchbegriff eingeben";
            StartX = -50;
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            if (StartX == -50)
            {
                StartX = Location.X;
            }
        }
        

        void AnimManager_OnAnimationProgress(object sender)
        {
            
            if (!expanded)
            {
                if (AnimManager.GetProgress()*100 < 100)
                {
                    Location = new Point(StartX - Convert.ToInt32(300 * AnimManager.GetProgress()), Location.Y);
                    MaterialTB.Width =Convert.ToInt32(300*AnimManager.GetProgress());
                    Width = Convert.ToInt16(IconSize.Width) + MaterialTB.Width;
                    Refresh();
                }
                else
                {
                    Location = new Point(StartX - Convert.ToInt32(300 * AnimManager.GetProgress()), Location.Y);
                    MaterialTB.Width = 300;
                    Width = Convert.ToInt16(IconSize.Width) + MaterialTB.Width;
                    expanded = true;
                    Invalidate();
                }
            }
            else
            {
                if (AnimManager.GetProgress()  > 0)
                {
                    Location = new Point(StartX - Convert.ToInt32(300 * AnimManager.GetProgress()), Location.Y);
                    MaterialTB.Width = Convert.ToInt32(300 *AnimManager.GetProgress());
                    Width = Convert.ToInt16(IconSize.Width) + MaterialTB.Width;
                    Invalidate();
                }
                else
                {
                    Location = new Point(StartX, Location.Y);
                    MaterialTB.Width = 0;
                    Width = Convert.ToInt16(IconSize.Width) + MaterialTB.Width;
                    expanded = false;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.Clear(SkinManager.ColorScheme.PrimaryColor);
            if (expanded) 
            {
                Pen IconPen = new Pen(SkinManager.ColorScheme.TextColor, 2);
                G.DrawLine(IconPen, Width + 4 - IconSize.Width, Height - (Height - IconSize.Height) / 2, Width - 4,( Height - IconSize.Height) / 2);
                G.DrawLine(IconPen, Width - 4, Height - (Height - IconSize.Height)/2, Width + 4 - IconSize.Width, (Height - IconSize.Height) / 2);
            }
            else 
            { 
            Pen IconPen = new Pen(SkinManager.ColorScheme.TextColor, 2);
            RectangleF MagTopRect = new RectangleF(Width - IconSize.Width + 2, (Height - IconSize.Height) / 2, Convert.ToSingle(IconSize.Width * 0.66) - 4, Convert.ToSingle(IconSize.Height * 0.66) - 4);
            G.DrawArc(IconPen,MagTopRect , 60, 180);
            G.DrawArc(IconPen, MagTopRect, 240, 180);
            IconPen.Width = 4;
            G.DrawLine(IconPen, Width - 2, Height - (Height - IconSize.Height), Width - Convert.ToSingle(IconSize.Width * 0.5), (Height / 2)-2);
            }
            e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
            G.Dispose();
            B.Dispose();
        
        }

    }

   class SearchTextField : Control, IMaterialControl
   {

         //Properties for managing the material design properties
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public override string Text { get { return baseTextBox.Text; } set { baseTextBox.Text = value; } }
        public new object Tag { get { return baseTextBox.Tag; } set { baseTextBox.Tag = value; } }
        public new int MaxLength { get { return baseTextBox.MaxLength; } set { baseTextBox.MaxLength = value; } }
        
        public string SelectedText { get { return baseTextBox.SelectedText; } set { baseTextBox.SelectedText = value; } }
        public string Hint { get { return baseTextBox.Hint; } set { baseTextBox.Hint = value; } }

        public int SelectionStart { get { return baseTextBox.SelectionStart; } set { baseTextBox.SelectionStart = value; } }
        public int SelectionLength { get { return baseTextBox.SelectionLength; } set { baseTextBox.SelectionLength = value; } }
        public int TextLength { get { return baseTextBox.TextLength; } }
        public void SelectAll() { baseTextBox.SelectAll(); }
        public void Clear() { baseTextBox.Clear(); }

        public void Copy() { baseTextBox.Copy();}

        public void Cut(){ baseTextBox.Cut(); }

        public bool ReadOnly { get { return baseTextBox.ReadOnly; } set { baseTextBox.ReadOnly = value; } }

        public HorizontalAlignment TextAlign { get { return baseTextBox.TextAlign; } set { baseTextBox.TextAlign = value; } } 
        

        # region Forwarding events to baseTextBox
        public event EventHandler AcceptsTabChanged
        {
            add
            {
                
                baseTextBox.AcceptsTabChanged += value;
            }
            remove
            {
                baseTextBox.AcceptsTabChanged -= value;
            }
        }

        public new event EventHandler AutoSizeChanged
        {
            add
            {
                baseTextBox.AutoSizeChanged += value;
            }
            remove
            {
                baseTextBox.AutoSizeChanged -= value;
            }
        }

        public new event EventHandler BackgroundImageChanged
        {
            add
            {
                baseTextBox.BackgroundImageChanged += value;
            }
            remove
            {
                baseTextBox.BackgroundImageChanged -= value;
            }
        }

        public new event EventHandler BackgroundImageLayoutChanged
        {
            add
            {
                baseTextBox.BackgroundImageLayoutChanged += value;
            }
            remove
            {
                baseTextBox.BackgroundImageLayoutChanged -= value;
            }
        }

        public new event EventHandler BindingContextChanged
        {
            add
            {
                baseTextBox.BindingContextChanged += value;
            }
            remove
            {
                baseTextBox.BindingContextChanged -= value;
            }
        }

        public event EventHandler BorderStyleChanged
        {
            add
            {
                baseTextBox.BorderStyleChanged += value;
            }
            remove
            {
                baseTextBox.BorderStyleChanged -= value;
            }
        }

        public new event EventHandler CausesValidationChanged
        {
            add
            {
                baseTextBox.CausesValidationChanged += value;
            }
            remove
            {
                baseTextBox.CausesValidationChanged -= value;
            }
        }

        public new event UICuesEventHandler ChangeUICues
        {
            add
            {
                baseTextBox.ChangeUICues += value;
            }
            remove
            {
                baseTextBox.ChangeUICues -= value;
            }
        }

        public new event EventHandler Click
        {
            add
            {
                baseTextBox.Click += value;
            }
            remove
            {
                baseTextBox.Click -= value;
            }
        }

        public new event EventHandler ClientSizeChanged
        {
            add
            {
                baseTextBox.ClientSizeChanged += value;
            }
            remove
            {
                baseTextBox.ClientSizeChanged -= value;
            }
        }

        public new event EventHandler ContextMenuChanged
        {
            add
            {
                baseTextBox.ContextMenuChanged += value;
            }
            remove
            {
                baseTextBox.ContextMenuChanged -= value;
            }
        }

        public new event EventHandler ContextMenuStripChanged
        {
            add
            {
                baseTextBox.ContextMenuStripChanged += value;
            }
            remove
            {
                baseTextBox.ContextMenuStripChanged -= value;
            }
        }

        public new event ControlEventHandler ControlAdded
        {
            add
            {
                baseTextBox.ControlAdded += value;
            }
            remove
            {
                baseTextBox.ControlAdded -= value;
            }
        }

        public new event ControlEventHandler ControlRemoved
        {
            add
            {
                baseTextBox.ControlRemoved += value;
            }
            remove
            {
                baseTextBox.ControlRemoved -= value;
            }
        }

        public new event EventHandler CursorChanged
        {
            add
            {
                baseTextBox.CursorChanged += value;
            }
            remove
            {
                baseTextBox.CursorChanged -= value;
            }
        }

        public new event EventHandler Disposed
        {
            add
            {
                baseTextBox.Disposed += value;
            }
            remove
            {
                baseTextBox.Disposed -= value;
            }
        }

        public new event EventHandler DockChanged
        {
            add
            {
                baseTextBox.DockChanged += value;
            }
            remove
            {
                baseTextBox.DockChanged -= value;
            }
        }

        public new event EventHandler DoubleClick
        {
            add
            {
                baseTextBox.DoubleClick += value;
            }
            remove
            {
                baseTextBox.DoubleClick -= value;
            }
        }

        public new event DragEventHandler DragDrop
        {
            add
            {
                baseTextBox.DragDrop += value;
            }
            remove
            {
                baseTextBox.DragDrop -= value;
            }
        }

        public new event DragEventHandler DragEnter
        {
            add
            {
                baseTextBox.DragEnter += value;
            }
            remove
            {
                baseTextBox.DragEnter -= value;
            }
        }

        public new event EventHandler DragLeave
        {
            add
            {
                baseTextBox.DragLeave += value;
            }
            remove
            {
                baseTextBox.DragLeave -= value;
            }
        }

        public new event DragEventHandler DragOver
        {
            add
            {
                baseTextBox.DragOver += value;
            }
            remove
            {
                baseTextBox.DragOver -= value;
            }
        }

        public new event EventHandler EnabledChanged
        {
            add
            {
                baseTextBox.EnabledChanged += value;
            }
            remove
            {
                baseTextBox.EnabledChanged -= value;
            }
        }

        public new event EventHandler Enter
        {
            add
            {
                baseTextBox.Enter += value;
            }
            remove
            {
                baseTextBox.Enter -= value;
            }
        }

        public new event EventHandler FontChanged
        {
            add
            {
                baseTextBox.FontChanged += value;
            }
            remove
            {
                baseTextBox.FontChanged -= value;
            }
        }

        public new event EventHandler ForeColorChanged
        {
            add
            {
                baseTextBox.ForeColorChanged += value;
            }
            remove
            {
                baseTextBox.ForeColorChanged -= value;
            }
        }

        public new event GiveFeedbackEventHandler GiveFeedback
        {
            add
            {
                baseTextBox.GiveFeedback += value;
            }
            remove
            {
                baseTextBox.GiveFeedback -= value;
            }
        }

        public new event EventHandler GotFocus
        {
            add
            {
                baseTextBox.GotFocus += value;
            }
            remove
            {
                baseTextBox.GotFocus -= value;
            }
        }

        public new event EventHandler HandleCreated
        {
            add
            {
                baseTextBox.HandleCreated += value;
            }
            remove
            {
                baseTextBox.HandleCreated -= value;
            }
        }

        public new event EventHandler HandleDestroyed
        {
            add
            {
                baseTextBox.HandleDestroyed += value;
            }
            remove
            {
                baseTextBox.HandleDestroyed -= value;
            }
        }

        public new event HelpEventHandler HelpRequested
        {
            add
            {
                baseTextBox.HelpRequested += value;
            }
            remove
            {
                baseTextBox.HelpRequested -= value;
            }
        }

        public event EventHandler HideSelectionChanged
        {
            add
            {
                baseTextBox.HideSelectionChanged += value;
            }
            remove
            {
                baseTextBox.HideSelectionChanged -= value;
            }
        }

        public new event EventHandler ImeModeChanged
        {
            add
            {
                baseTextBox.ImeModeChanged += value;
            }
            remove
            {
                baseTextBox.ImeModeChanged -= value;
            }
        }

        public new event InvalidateEventHandler Invalidated
        {
            add
            {
                baseTextBox.Invalidated += value;
            }
            remove
            {
                baseTextBox.Invalidated -= value;
            }
        }

        public new event KeyEventHandler KeyDown
        {
            add
            {
                baseTextBox.KeyDown += value;
            }
            remove
            {
                baseTextBox.KeyDown -= value;
            }
        }

        public new event KeyPressEventHandler KeyPress
        {
            add
            {
                baseTextBox.KeyPress += value;
            }
            remove
            {
                baseTextBox.KeyPress -= value;
            }
        }

        public new event KeyEventHandler KeyUp
        {
            add
            {
                baseTextBox.KeyUp += value;
            }
            remove
            {
                baseTextBox.KeyUp -= value;
            }
        }

        public new event LayoutEventHandler Layout
        {
            add
            {
                baseTextBox.Layout += value;
            }
            remove
            {
                baseTextBox.Layout -= value;
            }
        }

        public new event EventHandler Leave
        {
            add
            {
                baseTextBox.Leave += value;
            }
            remove
            {
                baseTextBox.Leave -= value;
            }
        }

        public new event EventHandler LocationChanged
        {
            add
            {
                baseTextBox.LocationChanged += value;
            }
            remove
            {
                baseTextBox.LocationChanged -= value;
            }
        }

        public new event EventHandler LostFocus
        {
            add
            {
                baseTextBox.LostFocus += value;
            }
            remove
            {
                baseTextBox.LostFocus -= value;
            }
        }

        public new event EventHandler MarginChanged
        {
            add
            {
                baseTextBox.MarginChanged += value;
            }
            remove
            {
                baseTextBox.MarginChanged -= value;
            }
        }

        public event EventHandler ModifiedChanged
        {
            add
            {
                baseTextBox.ModifiedChanged += value;
            }
            remove
            {
                baseTextBox.ModifiedChanged -= value;
            }
        }

        public new event EventHandler MouseCaptureChanged
        {
            add
            {
                baseTextBox.MouseCaptureChanged += value;
            }
            remove
            {
                baseTextBox.MouseCaptureChanged -= value;
            }
        }

        public new event MouseEventHandler MouseClick
        {
            add
            {
                baseTextBox.MouseClick += value;
            }
            remove
            {
                baseTextBox.MouseClick -= value;
            }
        }

        public new event MouseEventHandler MouseDoubleClick
        {
            add
            {
                baseTextBox.MouseDoubleClick += value;
            }
            remove
            {
                baseTextBox.MouseDoubleClick -= value;
            }
        }

        public new event MouseEventHandler MouseDown
        {
            add
            {
                baseTextBox.MouseDown += value;
            }
            remove
            {
                baseTextBox.MouseDown -= value;
            }
        }

        public new event EventHandler MouseEnter
        {
            add
            {
                baseTextBox.MouseEnter += value;
            }
            remove
            {
                baseTextBox.MouseEnter -= value;
            }
        }

        public new event EventHandler MouseHover
        {
            add
            {
                baseTextBox.MouseHover += value;
            }
            remove
            {
                baseTextBox.MouseHover -= value;
            }
        }

        public new event EventHandler MouseLeave
        {
            add
            {
                baseTextBox.MouseLeave += value;
            }
            remove
            {
                baseTextBox.MouseLeave -= value;
            }
        }

        public new event MouseEventHandler MouseMove
        {
            add
            {
                baseTextBox.MouseMove += value;
            }
            remove
            {
                baseTextBox.MouseMove -= value;
            }
        }

        public new event MouseEventHandler MouseUp
        {
            add
            {
                baseTextBox.MouseUp += value;
            }
            remove
            {
                baseTextBox.MouseUp -= value;
            }
        }

        public new event MouseEventHandler MouseWheel
        {
            add
            {
                baseTextBox.MouseWheel += value;
            }
            remove
            {
                baseTextBox.MouseWheel -= value;
            }
        }

        public new event EventHandler Move
        {
            add
            {
                baseTextBox.Move += value;
            }
            remove
            {
                baseTextBox.Move -= value;
            }
        }

        public event EventHandler MultilineChanged
        {
            add
            {
                baseTextBox.MultilineChanged += value;
            }
            remove
            {
                baseTextBox.MultilineChanged -= value;
            }
        }

        public new event EventHandler PaddingChanged
        {
            add
            {
                baseTextBox.PaddingChanged += value;
            }
            remove
            {
                baseTextBox.PaddingChanged -= value;
            }
        }

        public new event PaintEventHandler Paint
        {
            add
            {
                baseTextBox.Paint += value;
            }
            remove
            {
                baseTextBox.Paint -= value;
            }
        }

        public new event EventHandler ParentChanged
        {
            add
            {
                baseTextBox.ParentChanged += value;
            }
            remove
            {
                baseTextBox.ParentChanged -= value;
            }
        }

        public new event PreviewKeyDownEventHandler PreviewKeyDown
        {
            add
            {
                baseTextBox.PreviewKeyDown += value;
            }
            remove
            {
                baseTextBox.PreviewKeyDown -= value;
            }
        }

        public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
        {
            add
            {
                baseTextBox.QueryAccessibilityHelp += value;
            }
            remove
            {
                baseTextBox.QueryAccessibilityHelp -= value;
            }
        }

        public new event QueryContinueDragEventHandler QueryContinueDrag
        {
            add
            {
                baseTextBox.QueryContinueDrag += value;
            }
            remove
            {
                baseTextBox.QueryContinueDrag -= value;
            }
        }

        public event EventHandler ReadOnlyChanged
        {
            add
            {
                baseTextBox.ReadOnlyChanged += value;
            }
            remove
            {
                baseTextBox.ReadOnlyChanged -= value;
            }
        }

        public new event EventHandler RegionChanged
        {
            add
            {
                baseTextBox.RegionChanged += value;
            }
            remove
            {
                baseTextBox.RegionChanged -= value;
            }
        }

        public new event EventHandler Resize
        {
            add
            {
                baseTextBox.Resize += value;
            }
            remove
            {
                baseTextBox.Resize -= value;
            }
        }

        public new event EventHandler RightToLeftChanged
        {
            add
            {
                baseTextBox.RightToLeftChanged += value;
            }
            remove
            {
                baseTextBox.RightToLeftChanged -= value;
            }
        }

        public new event EventHandler SizeChanged
        {
            add
            {
                baseTextBox.SizeChanged += value;
            }
            remove
            {
                baseTextBox.SizeChanged -= value;
            }
        }

        public new event EventHandler StyleChanged
        {
            add
            {
                baseTextBox.StyleChanged += value;
            }
            remove
            {
                baseTextBox.StyleChanged -= value;
            }
        }

        public new event EventHandler SystemColorsChanged
        {
            add
            {
                baseTextBox.SystemColorsChanged += value;
            }
            remove
            {
                baseTextBox.SystemColorsChanged -= value;
            }
        }

        public new event EventHandler TabIndexChanged
        {
            add
            {
                baseTextBox.TabIndexChanged += value;
            }
            remove
            {
                baseTextBox.TabIndexChanged -= value;
            }
        }

        public new event EventHandler TabStopChanged
        {
            add
            {
                baseTextBox.TabStopChanged += value;
            }
            remove
            {
                baseTextBox.TabStopChanged -= value;
            }
        }

        public event EventHandler TextAlignChanged
        {
            add
            {
                baseTextBox.TextAlignChanged += value;
            }
            remove
            {
                baseTextBox.TextAlignChanged -= value;
            }
        }

        public new event EventHandler TextChanged
        {
            add
            {
                baseTextBox.TextChanged += value;
            }
            remove
            {
                baseTextBox.TextChanged -= value;
            }
        }

        public new event EventHandler Validated
        {
            add
            {
                baseTextBox.Validated += value;
            }
            remove
            {
                baseTextBox.Validated -= value;
            }
        }

        public new event CancelEventHandler Validating
        {
            add
            {
                baseTextBox.Validating += value;
            }
            remove
            {
                baseTextBox.Validating -= value;
            }
        }

        public new event EventHandler VisibleChanged
        {
            add
            {
                baseTextBox.VisibleChanged += value;
            }
            remove
            {
                baseTextBox.VisibleChanged -= value;
            }
        }
        # endregion

        protected readonly BaseTextBox baseTextBox;
        public SearchTextField()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);


            baseTextBox = new BaseTextBox
            {
                BorderStyle = BorderStyle.None,
                Font = SkinManager.ROBOTO_REGULAR_11,
                ForeColor = SkinManager.GetPrimaryTextColor(),
                Location = new Point(0, 15),
                Width = Width,
                Height = Height -5
            };

            if (!Controls.Contains(baseTextBox) && !DesignMode)
            {
                Controls.Add(baseTextBox);
            }

            BackColorChanged += (sender, args) =>
            {
                baseTextBox.BackColor = BackColor;
                baseTextBox.ForeColor = SkinManager.ACTION_BAR_TEXT;
            };

            baseTextBox.TextChanged += new EventHandler(Redraw);

			//Fix for tabstop
			baseTextBox.TabStop = true;
			this.TabStop = false;
            BackColor = SkinManager.ColorScheme.PrimaryColor;
            Text = "";
            baseTextBox.Text = "";
        }


        private void Redraw(object sencer, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.Clear(SkinManager.ColorScheme.PrimaryColor);

            int lineY = baseTextBox.Bottom + 3;
            baseTextBox.BackColor = BackColor;
            g.FillRectangle(SkinManager.ColorScheme.AccentBrush , baseTextBox.Location.X, lineY, baseTextBox.Width, 2 );
            if (!String.IsNullOrWhiteSpace(Hint) && (!String.IsNullOrWhiteSpace(Text) || Focused()))
            {
               g.DrawString(
               Hint,
               SkinManager.ROBOTO_MEDIUM_10,
               SkinManager.ColorScheme.AccentBrush,
               new Rectangle(ClientRectangle.X , 0, ClientRectangle.Width, ClientRectangle.Height),
               new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
            }
        }

        public bool Focused()
        {
            return baseTextBox.Focused;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            baseTextBox.Location = new Point(0, 15);
            baseTextBox.Width = Width;

            Height = baseTextBox.Height + 20;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            baseTextBox.BackColor = Parent.BackColor;
            baseTextBox.ForeColor = SkinManager.GetPrimaryTextColor();
        }

        protected class BaseTextBox : TextBox
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

            private const int EM_SETCUEBANNER = 0x1501;
            private const char EmptyChar = (char)0;
            private const char VisualStylePasswordChar = '\u25CF';
            private const char NonVisualStylePasswordChar = '\u002A';

            private string hint = string.Empty;
            public string Hint
            {
                get { return hint; }
                set
                {
                    hint = value;
                    SendMessage(Handle, EM_SETCUEBANNER, (int)IntPtr.Zero, Hint);
                }
            }

            private char passwordChar = EmptyChar;
            public new char PasswordChar
            {
                get { return passwordChar; }
                set
                {
                    passwordChar = value;
                    SetBasePasswordChar();
                }
            }

            public new void SelectAll()
            {
                BeginInvoke((MethodInvoker) delegate()
                {
                    base.Focus();
                    base.SelectAll();
                });
            }


            private char useSystemPasswordChar = EmptyChar;
            public new bool UseSystemPasswordChar
            {
                get { return useSystemPasswordChar != EmptyChar; }
                set
                {
                    if (value)
                    {
                        useSystemPasswordChar = Application.RenderWithVisualStyles ? VisualStylePasswordChar : NonVisualStylePasswordChar;
                    }
                    else
                    {
                        useSystemPasswordChar = EmptyChar;
                    }

                    SetBasePasswordChar();
                }
            }

            private void SetBasePasswordChar()
            {
                base.PasswordChar = UseSystemPasswordChar ? useSystemPasswordChar : passwordChar;
            }

            public BaseTextBox()
            {
                MaterialContextMenuStrip cms = new TextBoxContextMenuStrip();
                cms.Opening += ContextMenuStripOnOpening;
                cms.OnItemClickStart += ContextMenuStripOnItemClickStart;
                BackColor = Color.White;
                ContextMenuStrip = cms;
            }

            private void ContextMenuStripOnItemClickStart(object sender, ToolStripItemClickedEventArgs toolStripItemClickedEventArgs)
            {
                switch (toolStripItemClickedEventArgs.ClickedItem.Text)
                {
                    case "Undo":
                        Undo();
                        break;
                    case "Cut":
                        Cut();
                        break;
                    case "Copy":
                        Copy();
                        break;
                    case "Paste":
                        Paste();
                        break;
                    case "Delete":
                        SelectedText = string.Empty;
                        break;
                    case "Select All":
                        SelectAll();
                        break;
                }
            }

            private void ContextMenuStripOnOpening(object sender, CancelEventArgs cancelEventArgs)
            {
                var strip = sender as TextBoxContextMenuStrip;
                if (strip != null)
                {
                    strip.undo.Enabled = CanUndo;
                    strip.cut.Enabled = !string.IsNullOrEmpty(SelectedText);
                    strip.copy.Enabled = !string.IsNullOrEmpty(SelectedText);
                    strip.paste.Enabled = Clipboard.ContainsText();
                    strip.delete.Enabled = !string.IsNullOrEmpty(SelectedText);
                    strip.selectAll.Enabled = !string.IsNullOrEmpty(Text);
                }
            }
        }

        private class TextBoxContextMenuStrip : MaterialContextMenuStrip
        {
            public readonly ToolStripItem undo = new MaterialToolStripMenuItem { Text = "Undo" };
            public readonly ToolStripItem seperator1 = new ToolStripSeparator();
            public readonly ToolStripItem cut = new MaterialToolStripMenuItem { Text = "Cut" };
            public readonly ToolStripItem copy = new MaterialToolStripMenuItem { Text = "Copy" };
            public readonly ToolStripItem paste = new MaterialToolStripMenuItem { Text = "Paste" };
            public readonly ToolStripItem delete = new MaterialToolStripMenuItem { Text = "Delete" };
            public readonly ToolStripItem seperator2 = new ToolStripSeparator();
            public readonly ToolStripItem selectAll = new MaterialToolStripMenuItem { Text = "Select All" };

            public TextBoxContextMenuStrip()
            {
                Items.AddRange(new[]
                {
                    undo,
                    seperator1,
                    cut,
                    copy,
                    paste,
                    delete,
                    seperator2,
                    selectAll
                });
            }
        }
   }
}


