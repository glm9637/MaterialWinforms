using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using System.Drawing.Drawing2D;

namespace MaterialWinforms.Controls
{

    [Designer(typeof(ParentControlDesigner))]
    public class MaterialFlowLayoutPanel : Control, IShadowedMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public Boolean AutoScroll
        {
            get
            {
                return MainPanel.AutoScroll;
            }
            set
            {
                MainPanel.AutoScroll = value;
                VerticalScrollbar.Visible = MainPanel.VerticalScroll.Visible;
                VerticalScrollbarAdded = VerticalScrollbar.Visible;
                HorizontalScrollbar.Visible = MainPanel.HorizontalScroll.Visible;
                HorizontalScrollbarAdded = HorizontalScrollbar.Visible;
            }
        }

        public new bool AutoSize
        {
            get
            {
                return MainPanel.AutoSize;
            }

            set
            {
                MainPanel.AutoSize = value;
                base.AutoSize = value;
            }
        }

        private MaterialScrollBar VerticalScrollbar, HorizontalScrollbar;
        private Boolean VerticalScrollbarAdded, HorizontalScrollbarAdded;
        private MaterialFlowLayoutDisplayingPanel MainPanel;

        private bool ignoreResize = true;
        private bool ignoreMainPanelResize = false;
        public override Color BackColor { get { return SkinManager.GetCardsColor(); } }

        public new ControlCollection Controls
        {
            get
            {
                return MainPanel.Controls;
            }
        }

        public MaterialFlowLayoutPanel() : base()
        {
            
            DoubleBuffered = true;
            VerticalScrollbar = new MaterialScrollBar(MaterialScrollOrientation.Vertical);
            VerticalScrollbar.Scroll += Scrolled;
            VerticalScrollbar.Visible = false;
            VerticalScrollbarAdded = false;

            HorizontalScrollbar = new MaterialScrollBar(MaterialScrollOrientation.Horizontal);
            HorizontalScrollbar.Scroll += Scrolled;
            HorizontalScrollbar.Visible = false;
            HorizontalScrollbarAdded = false;

            MainPanel = new MaterialFlowLayoutDisplayingPanel();
            MainPanel.Resize += MainPanel_Resize;
            MainPanel.Location = new Point(0, 0);

            Size = new Size(90, 90);

            base.Controls.Add(MainPanel);
            base.Controls.Add(VerticalScrollbar);
            base.Controls.Add(HorizontalScrollbar);
            MainPanel.ControlAdded += MaterialPanel_ControlsChanged;
            MainPanel.ControlRemoved += MaterialPanel_ControlsChanged;
            MainPanel.onScrollBarChanged += MainPanel_onScrollBarChanged;
            AutoScroll = true;

            ignoreResize = false;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);


            VerticalScrollbar.BringToFront();
            HorizontalScrollbar.BringToFront();
            
            MainPanel.BringToFront();
        }

        void MainPanel_onScrollBarChanged(Orientation pScrollOrientation, bool pVisible)
        {
            UpdateScrollbars();
        }

        void Scrolled(object sender, ScrollEventArgs e)
        {
            MainPanel.AutoScrollPosition = new Point(HorizontalScrollbar.Value, VerticalScrollbar.Value);
        }

        void MaterialPanel_ControlsChanged(object sender, ControlEventArgs e)
        {
            UpdateScrollbars();
            MainPanel.BringToFront();
            VerticalScrollbar.BringToFront();
            HorizontalScrollbar.BringToFront();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            MainPanel.BringToFront();
            VerticalScrollbar.BringToFront();
            HorizontalScrollbar.BringToFront();
        }

        void MainPanel_Resize(object sender, EventArgs e)
        {
            if (!ignoreMainPanelResize)
                UpdateScrollbars();
            else
                ignoreMainPanelResize = false;
        }


        protected override void OnResize(EventArgs eventargs)
        {
            VerticalScrollbar.Location = new Point(Width - VerticalScrollbar.Width, 0);
            VerticalScrollbar.Size = new Size(VerticalScrollbar.Width, Height - HorizontalScrollbar.Height);
            VerticalScrollbar.Anchor = ((AnchorStyles)AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
            HorizontalScrollbar.Location = new Point(0, Height - HorizontalScrollbar.Height);
            HorizontalScrollbar.Size = new Size(Width - VerticalScrollbar.Width, HorizontalScrollbar.Height);
            HorizontalScrollbar.Anchor = ((AnchorStyles)AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);
            ShadowBorder = new GraphicsPath();
            ShadowBorder.AddRectangle(new Rectangle(Location,Size));
            base.OnResize(eventargs);
            UpdateScrollbars();

        }


        private void UpdateScrollbars()
        {
            if(ignoreResize)
            {
                return;
            }
            VerticalScrollbar.Minimum = MainPanel.VerticalScroll.Minimum;
            VerticalScrollbar.Maximum = MainPanel.VerticalScroll.Maximum;
            VerticalScrollbar.LargeChange = MainPanel.VerticalScroll.LargeChange;
            VerticalScrollbar.SmallChange = MainPanel.VerticalScroll.SmallChange;

            HorizontalScrollbar.Minimum = MainPanel.HorizontalScroll.Minimum;
            HorizontalScrollbar.Maximum = MainPanel.HorizontalScroll.Maximum;
            HorizontalScrollbar.LargeChange = MainPanel.HorizontalScroll.LargeChange;
            HorizontalScrollbar.SmallChange = MainPanel.HorizontalScroll.SmallChange;

            if (MainPanel.VerticalScroll.Visible && !VerticalScrollbarAdded)
            {
                VerticalScrollbarAdded = true;
                VerticalScrollbar.Visible = true;
            }
            else if (!MainPanel.VerticalScroll.Visible && VerticalScrollbarAdded)
            {
                VerticalScrollbarAdded = false;
                VerticalScrollbar.Visible = false;
            }
            if (MainPanel.HorizontalScroll.Visible && !HorizontalScrollbarAdded)
            {
                HorizontalScrollbarAdded = true;
                HorizontalScrollbar.Visible = true;
            }
            else if (!MainPanel.HorizontalScroll.Visible && HorizontalScrollbarAdded)
            {
                HorizontalScrollbarAdded = false;
                HorizontalScrollbar.Visible = false;
            }
            ignoreMainPanelResize = true;


            Size MainPanelSize = new Size(Width - (VerticalScrollbarAdded ? VerticalScrollbar.Width : 0), Height - (HorizontalScrollbarAdded ? HorizontalScrollbar.Height : 0));

            MainPanel.IgnoreResizing = true;
            ignoreMainPanelResize = true;
            MainPanel.Size = new Size(Width - (VerticalScrollbarAdded ? VerticalScrollbar.Width : 0), Height - (HorizontalScrollbarAdded ? HorizontalScrollbar.Height : 0));
            MainPanel.IgnoreResizing = false;
            ignoreMainPanelResize = false;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

        }



        [Browsable(false)]
        public GraphicsPath ShadowBorder { get; set; }

        public int Elevation { get; set; }

    }


    internal class MaterialFlowLayoutDisplayingPanel : FlowLayoutPanel, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public override Color BackColor { get { return SkinManager.GetApplicationBackgroundColor(); } }

        public delegate void ScrollbarChanged(Orientation pScrollOrientation, Boolean pVisible);

        public bool IgnoreResizing = false;

        public event ScrollbarChanged onScrollBarChanged;
        public MaterialFlowLayoutDisplayingPanel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Padding = new Padding(3, 3, 3, 3);
        }

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

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (onScrollBarChanged != null)
            {
                onScrollBarChanged(Orientation.Horizontal, HorizontalScroll.Visible);
                onScrollBarChanged(Orientation.Vertical, VerticalScroll.Visible);
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (onScrollBarChanged != null && ! IgnoreResizing)
            {
                onScrollBarChanged(Orientation.Horizontal, HorizontalScroll.Visible);
                onScrollBarChanged(Orientation.Vertical, VerticalScroll.Visible);
            }
            ShowScrollBar(this.Handle, (int)ScrollBarDirection.SB_HORZ, false);
            ShowScrollBar(this.Handle, (int)ScrollBarDirection.SB_VERT, false);
            base.WndProc(ref m);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            foreach (Control objChild in Controls)
            {
                if (typeof(IShadowedMaterialControl).IsAssignableFrom(objChild.GetType()))
                {
                    IShadowedMaterialControl objCurrent = (IShadowedMaterialControl)objChild;
                    DrawHelper.drawShadow(e.Graphics, objCurrent.ShadowBorder, objCurrent.Elevation, SkinManager.GetApplicationBackgroundColor());
                }

            }
        }
    }
}

