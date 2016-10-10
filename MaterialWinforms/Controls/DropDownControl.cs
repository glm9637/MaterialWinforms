using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MaterialWinforms.Controls
{
    public partial class DropDownControl : UserControl,IMaterialControl
    {
        public enum eDockSide
        {
            Left,
            Right
        }

        public enum eDropState
        {
            Closed,
            Closing,
            Dropping,
            Dropped
        }

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }


        DropDownContainer dropContainer;
        Control _dropDownItem;
        bool closedWhileInControl;
        private Size storedSize;

        private eDropState _dropState;
        protected eDropState DropState
        {
            get { return _dropState; }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                this.Invalidate();
            }
        }

        public DropDownControl()
        {
            InitializeComponent();
            this.storedSize = this.Size;
            this.BackColor = Color.White;
            this.Text = this.Name;
            DoubleBuffered = true;
        }

        public void InitializeDropDown(Control dropDownItem)
        {
            if (_dropDownItem != null)
                throw new Exception("The drop down item has already been implemented!");
            _DesignView = false;
            _dropState = eDropState.Closed;
            this.Size = _AnchorSize;
            this._AnchorClientBounds = new Rectangle(2, 2, _AnchorSize.Width - 21, _AnchorSize.Height - 4);
            //removes the dropDown item from the controls list so it 
            //won't be seen until the drop-down window is active
            if (this.Controls.Contains(dropDownItem))
                this.Controls.Remove(dropDownItem);
            _dropDownItem = dropDownItem;
        }

        private Size _AnchorSize = new Size(121, 21);
        public Size AnchorSize
        {
            get { return _AnchorSize; }
            set
            {
                _AnchorSize = value;
                this.Invalidate();
            }
        }

        private eDockSide _DockSide;
        public eDockSide DockSide
        {
            get { return _DockSide; }
            set { _DockSide = value; }
        }


        private bool _DesignView = true;
        [DefaultValue(false)]
        protected bool DesignView
        {
            get { return _DesignView; }
            set
            {
                if (_DesignView == value) return;

                _DesignView = value;
                if (_DesignView)
                {
                    this.Size = storedSize;
                }
                else
                {
                    storedSize = this.Size;
                    this.Size = _AnchorSize;
                }

            }
        }

        public event EventHandler PropertyChanged;
        protected void OnPropertyChanged()
        {
            if (PropertyChanged != null)
                PropertyChanged(null, null);
        }

        private Rectangle _AnchorClientBounds;
        public Rectangle AnchorClientBounds
        {
            get { return _AnchorClientBounds; }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_DesignView)
                storedSize = this.Size;
            _AnchorSize.Width = this.Width;
            if (!_DesignView)
            {
                _AnchorSize.Height = this.Height;
                this._AnchorClientBounds = new Rectangle(2, 2, _AnchorSize.Width - 21, _AnchorSize.Height - 4);
            }
        }

        protected bool mousePressed;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mousePressed = true;
            OpenDropDown();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mousePressed = false;
            this.Invalidate();
        }

        protected virtual bool CanDrop
        {
            get
            {
                if (dropContainer != null)
                    return false;

                if (dropContainer == null && closedWhileInControl)
                {
                    closedWhileInControl = false;
                    return false;
                }

                return !closedWhileInControl;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;

            MouseState = MouseState.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                Invalidate();
            };
           
        }

        protected void OpenDropDown()
        {
            if (_dropDownItem == null)
                throw new NotImplementedException("The drop down item has not been initialized!  Use the InitializeDropDown() method to do so.");

            if (!CanDrop) return;

            dropContainer = new DropDownContainer(_dropDownItem);
            dropContainer.DropDownForm.Bounds = GetDropDownBounds();
            dropContainer.DropDownForm.DropStateChange += new DropDownForm.DropWindowArgs(dropContainer_DropStateChange);
            dropContainer.DropDownForm.FormClosed += new FormClosedEventHandler(dropContainer_Closed);
            this.ParentForm.Move += new EventHandler(ParentForm_Move);
            _dropState = eDropState.Dropping;
            dropContainer.Show();
            _dropState = eDropState.Dropped;
            this.Invalidate();
        }

        void ParentForm_Move(object sender, EventArgs e)
        {
            dropContainer.DropDownForm.Bounds = GetDropDownBounds();
        }


        public void CloseDropDown()
        {

            if (dropContainer != null)
            {
                _dropState = eDropState.Closing;
                dropContainer.DropDownForm.Freeze = false;
                dropContainer.DropDownForm.Close();
            }
        }

        void dropContainer_DropStateChange(DropDownControl.eDropState state)
        {
            _dropState = state;
        }
        void dropContainer_Closed(object sender, FormClosedEventArgs e)
        {
            if (!dropContainer.DropDownForm.IsDisposed)
            {
                dropContainer.DropDownForm.DropStateChange -= dropContainer_DropStateChange;
                dropContainer.DropDownForm.FormClosed -= dropContainer_Closed;
                this.ParentForm.Move -= ParentForm_Move;
                dropContainer.DropDownForm.Dispose();
            }
            dropContainer = null;
            closedWhileInControl = (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position));
            _dropState = eDropState.Closed;
            this.Invalidate();
        }

        protected virtual Rectangle GetDropDownBounds()
        {
            Size inflatedDropSize = new Size(_dropDownItem.Width + 2, _dropDownItem.Height + 2);
            Rectangle screenBounds = _DockSide == eDockSide.Left ?
                new Rectangle(this.Parent.PointToScreen(new Point(this.Bounds.X, this.Bounds.Bottom)), inflatedDropSize)
                : new Rectangle(this.Parent.PointToScreen(new Point(this.Bounds.Right - _dropDownItem.Width, this.Bounds.Bottom)), inflatedDropSize);
            Rectangle workingArea = Screen.GetWorkingArea(screenBounds);
            //make sure we're completely in the top-left working area
            if (screenBounds.X < workingArea.X) screenBounds.X = workingArea.X;
            if (screenBounds.Y < workingArea.Y) screenBounds.Y = workingArea.Y;

            //make sure we're not extended past the working area's right /bottom edge
            if (screenBounds.Right > workingArea.Right && workingArea.Width > screenBounds.Width)
                screenBounds.X = workingArea.Right - screenBounds.Width;
            if (screenBounds.Bottom > workingArea.Bottom && workingArea.Height > screenBounds.Height)
                screenBounds.Y = workingArea.Bottom - screenBounds.Height;

            return screenBounds;
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SkinManager.GetApplicationBackgroundColor());

            Brush LineBrush = (MouseState == MouseState.HOVER ? SkinManager.ColorScheme.PrimaryBrush : SkinManager.GetDividersBrush());

            using (Brush b = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), ClientRectangle);
            }

            e.Graphics.DrawLine(new Pen(LineBrush), new Point(0, ClientRectangle.Bottom-1), new Point(ClientRectangle.Right, ClientRectangle.Bottom-1));

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (Enabled) { 
            PointF p1 = new Point(ClientRectangle.Right - 5, (int)(ClientRectangle.Height * 0.33));
            PointF p2 = new Point(ClientRectangle.Right - 15, (int)(ClientRectangle.Height * 0.33));
            PointF p3 = new Point(ClientRectangle.Right - 10, (int)(ClientRectangle.Height * 0.66));
            PointF[] curvePoints = {
                                  p1,p2,p3
                              };
            e.Graphics.FillPolygon(LineBrush, curvePoints, FillMode.Winding);
        }
            TextRenderer.DrawText(e.Graphics, _Text, SkinManager.ROBOTO_MEDIUM_11, this.AnchorClientBounds, SkinManager.ColorScheme.TextColor, TextFormatFlags.WordEllipsis);
        }

        private System.Windows.Forms.VisualStyles.ComboBoxState getState()
        {
            if (mousePressed || dropContainer != null)
                return System.Windows.Forms.VisualStyles.ComboBoxState.Pressed;
            else
                return System.Windows.Forms.VisualStyles.ComboBoxState.Normal;
        }

        public void FreezeDropDown(bool remainVisible)
        {
            if (dropContainer != null)
            {
                dropContainer.DropDownForm.Freeze = true;
                if (!remainVisible)
                    dropContainer.DropDownForm.Visible = false;
            }
        }

        public void UnFreezeDropDown()
        {
            if (dropContainer != null)
            {
                dropContainer.DropDownForm.Freeze = false;
                if (!dropContainer.DropDownForm.Visible)
                    dropContainer.DropDownForm.Visible = true;
            }
        }

        internal sealed class DropDownContainer
        {
            private Timer _timer;
            public DropDownForm DropDownForm;


            public DropDownContainer(Control dropDownItem)
            {
                DropDownForm = new DropDownForm(dropDownItem);
                DropDownForm.Height = 0;
            }

            public void Show()
            {

                _timer = new Timer();
                Size formSize = DropDownForm.Size;

                DropDownForm.Size = new Size(formSize.Width, 0);
                DropDownForm.Visible = true;
                _timer.Interval = 1;
                _timer.Tag = new AnimateMsgBox(formSize);


                _timer.Tick += timer_Tick;
                _timer.Start();

            }

            private void timer_Tick(object sender, EventArgs e)
            {
                Timer timer = (Timer)sender;
                AnimateMsgBox animate = (AnimateMsgBox)timer.Tag;

                if (DropDownForm.Height < animate.FormSize.Height)
                {
                    DropDownForm.Height += animate.FormSize.Height /20;
                    if (DropDownForm.Height > animate.FormSize.Height) { DropDownForm.Height = animate.FormSize.Height; }
                    DropDownForm.Invalidate();
                }
                else
                {
                    _timer.Stop();
                    _timer.Dispose();
                }

            }
        }



        internal sealed class DropDownForm : Form, IMessageFilter
        {
            public bool Freeze;


            public DropDownForm(Control dropDownItem)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                dropDownItem.Location = new Point(0, 0);
                this.Controls.Add(dropDownItem);
                this.StartPosition = FormStartPosition.Manual;
                this.ShowInTaskbar = false;
                this.Size = dropDownItem.Size;
                Application.AddMessageFilter(this);
            }

            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                if (Controls.Count > 0)
                {
                    if (Width > Controls[0].Width) Width = Controls[0].Width;
                    if (Height > Controls[0].Width) Height = Controls[0].Height;
                }
            }

            public bool PreFilterMessage(ref Message m)
            {
                if (!Freeze && this.Visible && (Form.ActiveForm == null || !Form.ActiveForm.Equals(this)))
                {
                    OnDropStateChange(eDropState.Closing);
                    this.Close();
                }


                return false;
            }

            public delegate void DropWindowArgs(eDropState state);
            public event DropWindowArgs DropStateChange;
            protected void OnDropStateChange(eDropState state)
            {
                if (DropStateChange != null)
                    DropStateChange(state);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
               
            }

            protected override void OnClosing(CancelEventArgs e)
            {
                Application.RemoveMessageFilter(this);
                this.Controls.RemoveAt(0); //prevent the control from being disposed
                base.OnClosing(e);
            }
        }

    }

}


