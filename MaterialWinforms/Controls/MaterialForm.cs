using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MaterialWinforms.Controls
{
    public class MaterialForm : Form, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public new FormBorderStyle FormBorderStyle { get { return base.FormBorderStyle; } set { base.FormBorderStyle = value; } }
        public bool Sizable { get; set; }
        private MaterialContextMenuStrip _SideDrawer;
        private MaterialFlatButton _LastSelected;

        public bool _SideDrawerFixiert;

        public bool SideDrawerFixiert
        {
            get
            {
                return _SideDrawerFixiert;
            }
            set
            {
                _SideDrawerFixiert = value;
                if (_SideDrawerFixiert)
                {
                    SideDrawerPanel.Size = new Size(SideDrawerPanel.MaximumSize.Width, SideDrawerPanel.Height);
                }
                else
                {
                    SideDrawerPanel.Size = new Size(0, SideDrawerPanel.Height);
                }


            }
        }


        public MaterialContextMenuStrip SideDrawer
        {
            get
            {
                return _SideDrawer;
            }
            set
            {
                _SideDrawer = value;
                initSideDrawer();
            }
        }
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX info);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTBOTTOM = 15;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int BORDER_WIDTH = 7;
        private ResizeDirection resizeDir;
        private ButtonState buttonState = ButtonState.None;

        private const int WMSZ_TOP = 3;
        private const int WMSZ_TOPLEFT = 4;
        private const int WMSZ_TOPRIGHT = 5;
        private const int WMSZ_LEFT = 1;
        private const int WMSZ_RIGHT = 2;
        private const int WMSZ_BOTTOM = 6;
        private const int WMSZ_BOTTOMLEFT = 7;
        private const int WMSZ_BOTTOMRIGHT = 8;

        private readonly Dictionary<int, int> resizingLocationsToCmd = new Dictionary<int, int>
        {
            {HTTOP,         WMSZ_TOP},
            {HTTOPLEFT,     WMSZ_TOPLEFT},
            {HTTOPRIGHT,    WMSZ_TOPRIGHT},
            {HTLEFT,        WMSZ_LEFT},
            {HTRIGHT,       WMSZ_RIGHT},
            {HTBOTTOM,      WMSZ_BOTTOM},
            {HTBOTTOMLEFT,  WMSZ_BOTTOMLEFT},
            {HTBOTTOMRIGHT, WMSZ_BOTTOMRIGHT}
        };

        private const int STATUS_BAR_BUTTON_WIDTH = STATUS_BAR_HEIGHT;
        private const int STATUS_BAR_HEIGHT = 24;
        private const int ACTION_BAR_HEIGHT = 40;

        private const uint TPM_LEFTALIGN = 0x0000;
        private const uint TPM_RETURNCMD = 0x0100;

        private const int WM_SYSCOMMAND = 0x0112;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int WS_SYSMENU = 0x00080000;

        private const int MONITOR_DEFAULTTONEAREST = 2;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public class MONITORINFOEX
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szDevice = new char[32];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public int Width()
            {
                return right - left;
            }

            public int Height()
            {
                return bottom - top;
            }
        }

        private enum ResizeDirection
        {
            BottomLeft,
            Left,
            Right,
            BottomRight,
            Bottom,
            None
        }

        private enum ButtonState
        {
            XOver,
            MaxOver,
            MinOver,
            DrawerOver,
            XDown,
            MaxDown,
            MinDown,
            DrawerDown,
            None
        }

        private readonly Cursor[] resizeCursors = { Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeWE, Cursors.SizeNS };

        private Rectangle minButtonBounds;
        private Rectangle maxButtonBounds;
        private Rectangle xButtonBounds;
        private Rectangle actionBarBounds;
        private Rectangle statusBarBounds;
        private Rectangle drawerButtonBounds;

        private bool Maximized;
        private Size previousSize;
        private Point previousLocation;
        private bool headerMouseDown;
        private bool DrawerOpen;
        private bool AnimationDirection;
        private int DrawerAnimationProgress;
        private Timer DrawerAnimationTimer;
        private FlowLayoutPanel SideDrawerPanel;

        public delegate void SideDrawerEventHandler(object sender, SideDrawerEventArgs e);

        public class SideDrawerEventArgs : EventArgs
        {
            private ToolStripItem EventInfo;
            public SideDrawerEventArgs(ToolStripItem pClickedItem)
            {
                EventInfo = pClickedItem;
            }

            public ToolStripItem getClickedItem()
            {
                return EventInfo;
            }
        }

        public event SideDrawerEventHandler onSideDrawerItemClicked;

        public MaterialForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            Sizable = true;
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            DrawerOpen = false;

            // This enables the form to trigger the MouseMove event even when mouse is over another control
            Application.AddMessageFilter(new MouseMessageFilter());
            MouseMessageFilter.MouseMove += OnGlobalMouseMove;
            DrawerAnimationTimer = new Timer();
            DrawerAnimationTimer.Interval = 10;
            DrawerAnimationProgress = 0;
            AnimationDirection = false;
            DrawerAnimationTimer.Tick += DrawerAnimationTimer_Tick;
            this.ControlAdded += MaterialForm_ControlAdded;
            SideDrawerPanel = new FlowLayoutPanel();

            SideDrawerPanel.AutoScroll = true;
            SideDrawerPanel.MaximumSize = new Size(Math.Min(Width * 80, ACTION_BAR_HEIGHT * 5), Height - ACTION_BAR_HEIGHT);
            SideDrawerPanel.Width = _SideDrawerFixiert ? SideDrawerPanel.MaximumSize.Width : 0;
            SideDrawerPanel.Height = Height - ACTION_BAR_HEIGHT - STATUS_BAR_HEIGHT;
            SideDrawerPanel.Location = new Point(0, ACTION_BAR_HEIGHT + STATUS_BAR_HEIGHT);
            SideDrawerPanel.MinimumSize = new Size(0, SideDrawerPanel.MaximumSize.Height);

            SideDrawerPanel.BringToFront();
            Controls.Add(SideDrawerPanel);


        }

        void DarkBackgroundControl_Click(object sender, EventArgs e)
        {
            DrawerAnimationTimer.Start();
        }


        private void MaterialForm_ControlAdded(object sender, ControlEventArgs e)
        {
            if (SideDrawerPanel != null)
            {
                SideDrawerPanel.BringToFront();
            }
        }


        private void initSideDrawer()
        {
            if (_SideDrawer != null)
            {

                SideDrawerPanel.Controls.Clear();

                foreach (ToolStripItem objMenuItem in _SideDrawer.Items)
                {
                    if (objMenuItem.GetType() == typeof(ToolStripSeparator))
                    {
                        MaterialDivider objDivider = new MaterialDivider();
                        objDivider.Size = new Size(SideDrawerPanel.MaximumSize.Width - SideDrawerPanel.Margin.Left - SideDrawerPanel.Margin.Right - SystemInformation.VerticalScrollBarWidth, 2);
                        SideDrawerPanel.Controls.Add(objDivider);
                    }
                    else
                    {
                        bool Verarbeitet = false;

                        if (objMenuItem.GetType() == typeof(MaterialToolStripMenuItem))
                        {
                            ToolStripMenuItem t = (ToolStripMenuItem)objMenuItem;
                            if (t.DropDownItems.Count > 0)
                            {
                                Verarbeitet = true;
                                MaterialDivider objTopDivider = new MaterialDivider();
                                objTopDivider.Size = new Size(SideDrawerPanel.MaximumSize.Width - SideDrawerPanel.Margin.Left - SideDrawerPanel.Margin.Right - SystemInformation.VerticalScrollBarWidth, 2);
                                SideDrawerPanel.Controls.Add(objTopDivider);
                                MaterialLabel objLabel = new MaterialLabel();
                                objLabel.Text = objMenuItem.Text;
                                objLabel.Tag = objMenuItem.Tag;
                                objLabel.Margin = new Padding(0);
                                objLabel.Font = SkinManager.ROBOTO_MEDIUM_10;
                                SideDrawerPanel.Controls.Add(objLabel);

                                foreach (ToolStripItem objSubMenuItem in t.DropDownItems)
                                {
                                    MaterialFlatButton objSubItem = new MaterialFlatButton();
                                    objSubItem.Text = objSubMenuItem.Text;
                                    objSubItem.Tag = objSubMenuItem;
                                    objSubItem.Enabled = objSubMenuItem.Enabled;
                                    objSubItem.AutoSize = false;
                                    objSubItem.Margin = new Padding(10, 0, 0, 0);
                                    objSubItem.MouseClick += new MouseEventHandler(DrawerItemClicked);
                                    objSubItem.Size = new Size(SideDrawerPanel.MaximumSize.Width - SideDrawerPanel.Margin.Left - SideDrawerPanel.Margin.Right - SystemInformation.VerticalScrollBarWidth - 10, 40);

                                    objSubItem.MouseClick += new MouseEventHandler(DrawerItemClicked);

                                    SideDrawerPanel.Controls.Add(objSubItem);
                                    objSubItem.Location = new Point(10, objSubItem.Location.Y);
                                }

                                MaterialDivider objBottomDivider = new MaterialDivider();
                                objBottomDivider.Size = new Size(SideDrawerPanel.MaximumSize.Width - SideDrawerPanel.Margin.Left - SideDrawerPanel.Margin.Right - SystemInformation.VerticalScrollBarWidth, 2);
                                SideDrawerPanel.Controls.Add(objBottomDivider);
                            }
                        }
                        if (!Verarbeitet) { }
                        MaterialFlatButton objItem = new MaterialFlatButton();
                        objItem.Text = objMenuItem.Text;
                        objItem.Tag = objMenuItem;
                        objItem.Enabled = objMenuItem.Enabled;
                        objItem.AutoSize = false;
                        objItem.Margin = new Padding(0, 0, 0, 0);
                        objItem.Size = new Size(SideDrawerPanel.MaximumSize.Width - SideDrawerPanel.Margin.Left - SideDrawerPanel.Margin.Right - SystemInformation.VerticalScrollBarWidth, 40);
                        objItem.MouseClick += new MouseEventHandler(DrawerItemClicked);

                        SideDrawerPanel.Controls.Add(objItem);
                    }
                }

            }
        }

        private void DrawerItemClicked(object sender, EventArgs e)
        {
            foreach (Control objSideControl in SideDrawerPanel.Controls)
            {
                if (objSideControl.GetType() == typeof(MaterialFlatButton))
                {
                    MaterialFlatButton objItem = (MaterialFlatButton)objSideControl;
                    objItem.Selected = false;
                    objItem.Invalidate();
                }
            }
            MaterialFlatButton t = (MaterialFlatButton)sender;
            t.Selected = true;

            if (onSideDrawerItemClicked != null)
            {
                onSideDrawerItemClicked(sender, new SideDrawerEventArgs((ToolStripItem)sender));
            }
        }

        private void DrawerAnimationTimer_Tick(object sender, EventArgs e)
        {
            int AnimationStep = 5;
            if (AnimationDirection)
            {
                if (DrawerAnimationProgress == 100 - AnimationStep)
                {
                    DrawerAnimationTimer.Stop();
                }
                DrawerAnimationProgress += AnimationStep;
            }
            else
            {
                if (DrawerAnimationProgress == AnimationStep)
                {
                    DrawerAnimationTimer.Stop();
                }
                DrawerAnimationProgress -= AnimationStep;
            }


            Invalidate();


        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (DesignMode || IsDisposed) return;

            if (m.Msg == WM_LBUTTONDBLCLK)
            {
                MaximizeWindow(!Maximized);
            }
            else if (m.Msg == WM_MOUSEMOVE && Maximized &&
                (statusBarBounds.Contains(PointToClient(Cursor.Position)) || actionBarBounds.Contains(PointToClient(Cursor.Position))) &&
                !(minButtonBounds.Contains(PointToClient(Cursor.Position)) || maxButtonBounds.Contains(PointToClient(Cursor.Position)) || xButtonBounds.Contains(PointToClient(Cursor.Position))))
            {
                if (headerMouseDown)
                {
                    Maximized = false;
                    headerMouseDown = false;

                    Point mousePoint = PointToClient(Cursor.Position);
                    if (mousePoint.X < Width / 2)
                        Location = mousePoint.X < previousSize.Width / 2 ?
                            new Point(Cursor.Position.X - mousePoint.X, Cursor.Position.Y - mousePoint.Y) :
                            new Point(Cursor.Position.X - previousSize.Width / 2, Cursor.Position.Y - mousePoint.Y);
                    else
                        Location = Width - mousePoint.X < previousSize.Width / 2 ?
                            new Point(Cursor.Position.X - previousSize.Width + Width - mousePoint.X, Cursor.Position.Y - mousePoint.Y) :
                            new Point(Cursor.Position.X - previousSize.Width / 2, Cursor.Position.Y - mousePoint.Y);

                    Size = previousSize;
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            else if (m.Msg == WM_LBUTTONDOWN &&
                (statusBarBounds.Contains(PointToClient(Cursor.Position)) || actionBarBounds.Contains(PointToClient(Cursor.Position))) &&
                !(minButtonBounds.Contains(PointToClient(Cursor.Position)) || maxButtonBounds.Contains(PointToClient(Cursor.Position)) || xButtonBounds.Contains(PointToClient(Cursor.Position))))
            {
                if (!Maximized)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
                else
                {
                    headerMouseDown = true;
                }
            }
            else if (m.Msg == WM_RBUTTONDOWN)
            {
                Point cursorPos = PointToClient(Cursor.Position);

                if (statusBarBounds.Contains(cursorPos) && !minButtonBounds.Contains(cursorPos) &&
                    !maxButtonBounds.Contains(cursorPos) && !xButtonBounds.Contains(cursorPos))
                {
                    // Show default system menu when right clicking titlebar
                    int id = TrackPopupMenuEx(
                        GetSystemMenu(Handle, false),
                        TPM_LEFTALIGN | TPM_RETURNCMD,
                        Cursor.Position.X, Cursor.Position.Y, Handle, IntPtr.Zero);

                    // Pass the command as a WM_SYSCOMMAND message
                    SendMessage(Handle, WM_SYSCOMMAND, id, 0);
                }
            }
            else if (m.Msg == WM_NCLBUTTONDOWN)
            {
                // This re-enables resizing by letting the application know when the
                // user is trying to resize a side. This is disabled by default when using WS_SYSMENU.
                if (!Sizable) return;

                byte bFlag = 0;

                // Get which side to resize from
                if (resizingLocationsToCmd.ContainsKey((int)m.WParam))
                    bFlag = (byte)resizingLocationsToCmd[(int)m.WParam];

                if (bFlag != 0)
                    SendMessage(Handle, WM_SYSCOMMAND, 0xF000 | bFlag, (int)m.LParam);
            }
            else if (m.Msg == WM_LBUTTONUP)
            {
                headerMouseDown = false;
            }
            if (!DesignMode)
            {

            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams par = base.CreateParams;
                // WS_SYSMENU: Trigger the creation of the system menu
                // WS_MINIMIZEBOX: Allow minimizing from taskbar
                par.Style = par.Style | WS_MINIMIZEBOX | WS_SYSMENU; // Turn on the WS_MINIMIZEBOX style flag
                return par;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (DesignMode) return;
            UpdateButtons(e);

            if (e.Button == MouseButtons.Left && !Maximized)
                ResizeForm(resizeDir);
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (DesignMode) return;
            buttonState = ButtonState.None;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode) return;

            if (Sizable)
            {
                //True if the mouse is hovering over a child control
                bool isChildUnderMouse = GetChildAtPoint(e.Location) != null;

                if (e.Location.X < BORDER_WIDTH && e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !Maximized)
                {
                    resizeDir = ResizeDirection.BottomLeft;
                    Cursor = Cursors.SizeNESW;
                }
                else if (e.Location.X < BORDER_WIDTH && !isChildUnderMouse && !Maximized)
                {
                    resizeDir = ResizeDirection.Left;
                    Cursor = Cursors.SizeWE;
                }
                else if (e.Location.X > Width - BORDER_WIDTH && e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !Maximized)
                {
                    resizeDir = ResizeDirection.BottomRight;
                    Cursor = Cursors.SizeNWSE;
                }
                else if (e.Location.X > Width - BORDER_WIDTH && !isChildUnderMouse && !Maximized)
                {
                    resizeDir = ResizeDirection.Right;
                    Cursor = Cursors.SizeWE;
                }
                else if (e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !Maximized)
                {
                    resizeDir = ResizeDirection.Bottom;
                    Cursor = Cursors.SizeNS;
                }
                else
                {
                    resizeDir = ResizeDirection.None;

                    //Only reset the cursor when needed, this prevents it from flickering when a child control changes the cursor to its own needs
                    if (resizeCursors.Contains(Cursor))
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }

            UpdateButtons(e);
        }

        protected void OnGlobalMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDisposed)
            {
                // Convert to client position and pass to Form.MouseMove
                Point clientCursorPos = PointToClient(e.Location);
                MouseEventArgs new_e = new MouseEventArgs(MouseButtons.None, 0, clientCursorPos.X, clientCursorPos.Y, 0);
                OnMouseMove(new_e);
            }
        }

        private void UpdateButtons(MouseEventArgs e, bool up = false)
        {
            if (DesignMode) return;
            ButtonState oldState = buttonState;
            bool showMin = MinimizeBox && ControlBox;
            bool showMax = MaximizeBox && ControlBox;

            if (e.Button == MouseButtons.Left && !up)
            {
                if (showMin && !showMax && maxButtonBounds.Contains(e.Location))
                    buttonState = ButtonState.MinDown;
                else if (showMin && showMax && minButtonBounds.Contains(e.Location))
                    buttonState = ButtonState.MinDown;
                else if (showMax && maxButtonBounds.Contains(e.Location))
                    buttonState = ButtonState.MaxDown;
                else if (ControlBox && xButtonBounds.Contains(e.Location))
                    buttonState = ButtonState.XDown;
                else if (drawerButtonBounds.Contains(e.Location))
                    buttonState = ButtonState.DrawerDown;
                else
                    buttonState = ButtonState.None;
            }
            else
            {
                if (showMin && !showMax && maxButtonBounds.Contains(e.Location))
                {
                    buttonState = ButtonState.MinOver;

                    if (oldState == ButtonState.MinDown)
                        WindowState = FormWindowState.Minimized;
                }
                else if (showMin && showMax && minButtonBounds.Contains(e.Location))
                {
                    buttonState = ButtonState.MinOver;

                    if (oldState == ButtonState.MinDown)
                        WindowState = FormWindowState.Minimized;
                }
                else if (MaximizeBox && ControlBox && maxButtonBounds.Contains(e.Location))
                {
                    buttonState = ButtonState.MaxOver;

                    if (oldState == ButtonState.MaxDown)
                        MaximizeWindow(!Maximized);

                }
                else if (ControlBox && xButtonBounds.Contains(e.Location))
                {
                    buttonState = ButtonState.XOver;

                    if (oldState == ButtonState.XDown)
                        Close();
                }
                else if (drawerButtonBounds.Contains(e.Location))
                {
                    buttonState = ButtonState.DrawerOver;
                    if (oldState == ButtonState.DrawerDown)
                    {
                        DrawerOpen = !DrawerOpen;
                        AnimationDirection = DrawerOpen;
                        DrawerAnimationTimer.Start();

                    }
                }

                else buttonState = ButtonState.None;
            }

            if (oldState != buttonState) Invalidate();
        }

        private void MaximizeWindow(bool maximize)
        {
            if (!MaximizeBox || !ControlBox) return;

            Maximized = maximize;

            if (maximize)
            {
                IntPtr monitorHandle = MonitorFromWindow(Handle, MONITOR_DEFAULTTONEAREST);
                MONITORINFOEX monitorInfo = new MONITORINFOEX();
                GetMonitorInfo(new HandleRef(null, monitorHandle), monitorInfo);
                previousSize = Size;
                previousLocation = Location;
                Size = new Size(monitorInfo.rcWork.Width(), monitorInfo.rcWork.Height());
                Location = new Point(monitorInfo.rcWork.left, monitorInfo.rcWork.top);
            }
            else
            {
                Size = previousSize;
                Location = previousLocation;
            }

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (DesignMode) return;
            UpdateButtons(e, true);

            base.OnMouseUp(e);
            ReleaseCapture();
        }

        private void ResizeForm(ResizeDirection direction)
        {
            if (DesignMode) return;
            int dir = -1;
            switch (direction)
            {
                case ResizeDirection.BottomLeft:
                    dir = HTBOTTOMLEFT;
                    break;
                case ResizeDirection.Left:
                    dir = HTLEFT;
                    break;
                case ResizeDirection.Right:
                    dir = HTRIGHT;
                    break;
                case ResizeDirection.BottomRight:
                    dir = HTBOTTOMRIGHT;
                    break;
                case ResizeDirection.Bottom:
                    dir = HTBOTTOM;
                    break;
            }

            ReleaseCapture();
            if (dir != -1)
            {
                SendMessage(Handle, WM_NCLBUTTONDOWN, dir, 0);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            minButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - 3 * STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            maxButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - 2 * STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            xButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            statusBarBounds = new Rectangle(0, 0, Width, STATUS_BAR_HEIGHT);
            actionBarBounds = new Rectangle(0, STATUS_BAR_HEIGHT, Width, ACTION_BAR_HEIGHT);
            drawerButtonBounds = new Rectangle(SkinManager.FORM_PADDING, STATUS_BAR_HEIGHT, ACTION_BAR_HEIGHT, ACTION_BAR_HEIGHT);
            if (SideDrawerPanel != null)
            {
                SideDrawerPanel.Height = Height - ACTION_BAR_HEIGHT - STATUS_BAR_HEIGHT;
                SideDrawerPanel.MaximumSize = new Size(Math.Min(Convert.ToInt32(Width * 0.8), ACTION_BAR_HEIGHT * 5), Height - ACTION_BAR_HEIGHT - STATUS_BAR_HEIGHT);
                SideDrawerPanel.Width = SideDrawerPanel.MaximumSize.Width;
            }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(SkinManager.GetApplicationBackgroundColor());
            g.FillRectangle(SkinManager.ColorScheme.DarkPrimaryBrush, statusBarBounds);
            g.FillRectangle(SkinManager.ColorScheme.PrimaryBrush, actionBarBounds);

            //Draw border
            using (var borderPen = new Pen(SkinManager.GetDividersColor(), 1))
            {
                g.DrawLine(borderPen, new Point(0, actionBarBounds.Bottom), new Point(0, Height - 2));
                g.DrawLine(borderPen, new Point(Width - 1, actionBarBounds.Bottom), new Point(Width - 1, Height - 2));
                g.DrawLine(borderPen, new Point(0, Height - 1), new Point(Width - 1, Height - 1));
            }

            // Determine whether or not we even should be drawing the buttons.
            bool showMin = MinimizeBox && ControlBox;
            bool showMax = MaximizeBox && ControlBox;
            var hoverBrush = SkinManager.GetFlatButtonHoverBackgroundBrush();
            var downBrush = SkinManager.GetFlatButtonPressedBackgroundBrush();

            // When MaximizeButton == false, the minimize button will be painted in its place
            if (buttonState == ButtonState.MinOver && showMin)
                g.FillRectangle(hoverBrush, showMax ? minButtonBounds : maxButtonBounds);

            if (buttonState == ButtonState.MinDown && showMin)
                g.FillRectangle(downBrush, showMax ? minButtonBounds : maxButtonBounds);

            if (buttonState == ButtonState.MaxOver && showMax)
                g.FillRectangle(hoverBrush, maxButtonBounds);

            if (buttonState == ButtonState.MaxDown && showMax)
                g.FillRectangle(downBrush, maxButtonBounds);

            if (buttonState == ButtonState.XOver && ControlBox)
                g.FillRectangle(hoverBrush, xButtonBounds);

            if (buttonState == ButtonState.XDown && ControlBox)
                g.FillRectangle(downBrush, xButtonBounds);
            if (buttonState == ButtonState.DrawerOver)
            {
                g.FillEllipse(hoverBrush, drawerButtonBounds);
            }

            using (var formButtonsPen = new Pen(SkinManager.ACTION_BAR_TEXT_SECONDARY, 2))
            {
                // Minimize button.
                if (showMin)
                {

                    int x = showMax ? minButtonBounds.X : maxButtonBounds.X;
                    int y = showMax ? minButtonBounds.Y : maxButtonBounds.Y;

                    g.DrawLine(
                        formButtonsPen,
                        x + (int)(minButtonBounds.Width * 0.33),
                        y + (int)(minButtonBounds.Height * 0.66),
                        x + (int)(minButtonBounds.Width * 0.66),
                        y + (int)(minButtonBounds.Height * 0.66)
                   );
                }

                // Maximize button
                if (showMax)
                {
                    g.DrawRectangle(
                        formButtonsPen,
                        maxButtonBounds.X + (int)(maxButtonBounds.Width * 0.33),
                        maxButtonBounds.Y + (int)(maxButtonBounds.Height * 0.36),
                        (int)(maxButtonBounds.Width * 0.39),
                        (int)(maxButtonBounds.Height * 0.31)
                   );
                }

                // Close button
                if (ControlBox)
                {
                    g.DrawLine(
                        formButtonsPen,
                        xButtonBounds.X + (int)(xButtonBounds.Width * 0.33),
                        xButtonBounds.Y + (int)(xButtonBounds.Height * 0.33),
                        xButtonBounds.X + (int)(xButtonBounds.Width * 0.66),
                        xButtonBounds.Y + (int)(xButtonBounds.Height * 0.66)
                   );

                    g.DrawLine(
                        formButtonsPen,
                        xButtonBounds.X + (int)(xButtonBounds.Width * 0.66),
                        xButtonBounds.Y + (int)(xButtonBounds.Height * 0.33),
                        xButtonBounds.X + (int)(xButtonBounds.Width * 0.33),
                        xButtonBounds.Y + (int)(xButtonBounds.Height * 0.66));
                }
            }

            if (_SideDrawer != null && !_SideDrawerFixiert)
            {
                using (var DrawerButtonPen = new Pen(SkinManager.ACTION_BAR_TEXT, 2))
                {


                    g.DrawLine(
                       DrawerButtonPen,
                       drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.2 + (0.3 * DrawerAnimationProgress / 100))),
                       drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.35 + (0.3 * DrawerAnimationProgress / 100))),
                       drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.8 - (0.6 * DrawerAnimationProgress / 100))),
                       drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.35 + (0.15 * DrawerAnimationProgress / 100))));
                    g.DrawLine(
                       DrawerButtonPen,
                       drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.2 + (0.6 * DrawerAnimationProgress / 100))),
                       drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.65 - (0.3 * Math.Abs(DrawerAnimationProgress - 50) / 100))),
                       drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.8 - (0.6 * DrawerAnimationProgress / 100))),
                       drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.35 + (0.3 * Math.Abs(DrawerAnimationProgress - 50) / 100))));
                    g.DrawLine(
                      DrawerButtonPen,
                      drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.8 - (0.9 * Math.Abs(DrawerAnimationProgress - 66) / 100))),
                      drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.65 - (0.3 * DrawerAnimationProgress / 100))),
                      drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.8 - (0.6 * DrawerAnimationProgress / 100))),
                      drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.36 + (0.45 * Math.Abs(DrawerAnimationProgress - 66) / 100))));
                }
            }



            //Form title

            g.DrawString(Text, SkinManager.ROBOTO_MEDIUM_12, SkinManager.ColorScheme.TextBrush, new Rectangle(SkinManager.FORM_PADDING + (true ? drawerButtonBounds.Width + drawerButtonBounds.X : 0), STATUS_BAR_HEIGHT, Width, ACTION_BAR_HEIGHT), new StringFormat { LineAlignment = StringAlignment.Center });

            if (SideDrawerPanel != null)
            {
                if (!_SideDrawerFixiert)
                {
                    SideDrawerPanel.Width = SideDrawerPanel.MaximumSize.Width * DrawerAnimationProgress / 100;
                }
                if (SideDrawerPanel.Width > 0)
                {
                    GraphicsPath t = new GraphicsPath();
                    t.AddLine(new Point(SideDrawerPanel.Width, SideDrawerPanel.Location.Y), new Point(SideDrawerPanel.Width, Height));
                    DrawHelper.drawShadow(g, t, 10, BackColor);

                }
            }

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MaterialForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "MaterialForm";
            this.ResumeLayout(false);

        }


    }

    public class MouseMessageFilter : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;

        public static event MouseEventHandler MouseMove;

        public bool PreFilterMessage(ref Message m)
        {

            if (m.Msg == WM_MOUSEMOVE)
            {
                if (MouseMove != null)
                {
                    int x = Control.MousePosition.X, y = Control.MousePosition.Y;

                    MouseMove(null, new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
                }
            }
            return false;
        }
    }

}
