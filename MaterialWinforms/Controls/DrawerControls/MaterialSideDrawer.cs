using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

using System.Runtime.InteropServices;

namespace MaterialWinforms.Controls
{
    public partial class MaterialSideDrawer : FlowLayoutPanel, IShadowedMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public int Elevation { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        [Browsable(false)]
        public GraphicsPath ShadowBorder { get; set; }

        public override Color BackColor { get { return SkinManager.GetApplicationBackgroundColor(); } }

        private MaterialContextMenuStrip _SideDrawer;

        public bool SelectOnClick { get; set; }

        public delegate void HiddenOnStartChanged(bool newValue);
        public event HiddenOnStartChanged onHiddenOnStartChanged;
        private bool _HiddenOnStart;
        public bool HiddenOnStart
        {
            get
            {
                return _HiddenOnStart;
            }
            set
            {
                _HiddenOnStart = value;
                if (onHiddenOnStartChanged != null)
                {
                    onHiddenOnStartChanged(value);
                }
            }
        }

        public bool _SideDrawerFixiert;
        public bool _SideDrawerUnterActionBar;
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
                    Size = new Size(MaximumSize.Width, Height);
                }
                else
                {
                    Size = new Size(0, Height);
                }


            }
        }

        public bool SideDrawerUnterActionBar
        {
            get
            {
                return _SideDrawerUnterActionBar;
            }
            set
            {
                _SideDrawerUnterActionBar = value;
                Location = new Point(0, MaterialActionBar.ACTION_BAR_HEIGHT + MaterialForm.STATUS_BAR_HEIGHT + (_SideDrawerUnterActionBar ? 48 : 0));


            }
        }

        public void Redraw()
        {
            base.Invalidate();
            foreach (Control objItem in Controls)
            {
                objItem.Invalidate();
            }
        }

        private bool _HideSideDrawer;
        public bool HideSideDrawer
        {
            get
            {
                return _HideSideDrawer;
            }
            set
            {
                _HideSideDrawer = value;
                Visible = !value;


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
                if (_SideDrawer != null)
                {
                    _SideDrawer.ItemAdded += ItemCollectionChanged;
                    _SideDrawer.ItemRemoved += ItemCollectionChanged;
                    initSideDrawer();
                }
            }
        }

        public delegate void SideDrawerEventHandler(object sender, SideDrawerEventArgs e);

        public class SideDrawerEventArgs : EventArgs
        {
            private String ClickedItem;
            private Object Tag;
            public SideDrawerEventArgs(String pItem, Object pTag)
            {
                ClickedItem = pItem;
                Tag = pTag;
            }

            public String getClickedItem()
            {
                return ClickedItem;
            }

            public Object getTag()
            {
                return Tag;
            }
        }



        public event SideDrawerEventHandler onSideDrawerItemClicked;

        public MaterialSideDrawer()
        {
            InitializeComponent();
            Dock = DockStyle.Left;
            AutoScroll = true;
            Elevation = 10;
            HiddenOnStart = true;
            MinimumSize = new Size(0, MaximumSize.Height);

        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            MaximumSize = new Size(Math.Min(Parent.Width * 80, MaterialActionBar.ACTION_BAR_HEIGHT * 5), 10000);
            Width = _SideDrawerFixiert ? MaximumSize.Width : 0;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            if (Parent != null)
            {
                Width = _SideDrawerFixiert ? MaximumSize.Width : Width;
            }
            ShadowBorder = new GraphicsPath();
            if (Width == 0)
            {
                Elevation = 0;
            }
            else
            {
                Elevation = 10;
            }

            ShadowBorder.AddLine(new Point(Location.X + Width, Location.Y), new Point(Location.X + Width, Location.Y + Height));

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SkinManager.GetApplicationBackgroundColor());
        }

        private void initSideDrawer()
        {
            bool LastControlWasDivider = false;
            if (_SideDrawer != null)
            {

                Controls.Clear();

                foreach (ToolStripItem objMenuItem in _SideDrawer.Items)
                {
                    if (objMenuItem.GetType() == typeof(ToolStripSeparator))
                    {
                        MaterialDivider objDivider = new MaterialDivider();
                        objDivider.Size = new Size(MaximumSize.Width - Margin.Left - Margin.Right - SystemInformation.VerticalScrollBarWidth, 2);
                        Controls.Add(objDivider);
                        LastControlWasDivider = true;
                    }
                    else
                    {
                        bool Verarbeitet = false;

                        if (objMenuItem.GetType() == typeof(MaterialToolStripMenuItem))
                        {
                            MaterialToolStripMenuItem t = (MaterialToolStripMenuItem)objMenuItem;
                            if (t.DropDownItems.Count > 0)
                            {
                                Verarbeitet = true;
                                if (Controls.Count > 0 && !LastControlWasDivider)
                                {
                                    MaterialDivider objTopDivider = new MaterialDivider();
                                    objTopDivider.Size = new Size(MaximumSize.Width - Margin.Left - Margin.Right - SystemInformation.VerticalScrollBarWidth, 2);
                                    Controls.Add(objTopDivider);
                                    LastControlWasDivider = true;
                                }
                                MaterialLabel objLabel = new MaterialLabel();
                                objLabel.Text = objMenuItem.Text;
                                objLabel.Tag = objMenuItem.Tag;
                                objLabel.Margin = new Padding(0);
                                objLabel.Font = SkinManager.ROBOTO_MEDIUM_10;
                                LastControlWasDivider = false;
                                Controls.Add(objLabel);

                                foreach (ToolStripItem objSubMenuItem in t.DropDownItems)
                                {
                                    MaterialDrawerItem objSubItem = new MaterialDrawerItem();
                                    objSubItem.Text = objSubMenuItem.Text;
                                    objSubItem.Tag = objSubMenuItem.Tag;
                                    objSubItem.Enabled = objSubMenuItem.Enabled;
                                    objSubItem.AutoSize = false;
                                    objSubItem.Margin = new Padding(10, 0, 0, 0);
                                    if (objSubMenuItem.GetType() == typeof(MaterialToolStripMenuItem))
                                    {
                                        objSubItem.IconImage = ((MaterialToolStripMenuItem)objSubMenuItem).Image;
                                    }
                                    objSubItem.MouseClick += new MouseEventHandler(DrawerItemClicked);
                                    objSubItem.Size = new Size(MaximumSize.Width - Margin.Left - Margin.Right - SystemInformation.VerticalScrollBarWidth - 10, 40);
                                    objSubItem.MouseClick -= new MouseEventHandler(DrawerItemClicked);
                                    objSubItem.MouseClick += new MouseEventHandler(DrawerItemClicked);

                                    Controls.Add(objSubItem);
                                    LastControlWasDivider = false;
                                    objSubItem.Location = new Point(10, objSubItem.Location.Y);
                                }

                                MaterialDivider objBottomDivider = new MaterialDivider();
                                objBottomDivider.Size = new Size(MaximumSize.Width - Margin.Left - Margin.Right - SystemInformation.VerticalScrollBarWidth, 2);
                                Controls.Add(objBottomDivider);
                                LastControlWasDivider = true;
                            }
                        }
                        if (!Verarbeitet)
                        {
                            MaterialFlatButton objItem = new MaterialFlatButton();
                            objItem.Text = objMenuItem.Text;
                            objItem.Tag = objMenuItem.Tag;
                            objItem.Enabled = objMenuItem.Enabled;
                            objItem.AutoSize = false;
                            objItem.Margin = new Padding(0, 0, 0, 0);
                            objItem.Size = new Size(MaximumSize.Width - Margin.Left - Margin.Right - SystemInformation.VerticalScrollBarWidth, 40);
                            objItem.MouseClick -= new MouseEventHandler(DrawerItemClicked);
                            objItem.MouseClick += new MouseEventHandler(DrawerItemClicked);
                            LastControlWasDivider = false;
                            Controls.Add(objItem);
                        }
                    }
                }
            }
        }

        public void SelectItem(int pPosition)
        {
            if(Controls[pPosition].GetType() == typeof(MaterialFlatButton))
            {
                DrawerItemClicked((MaterialFlatButton)Controls[pPosition],EventArgs.Empty);
            }
        }

        private void ItemCollectionChanged(object sender, EventArgs e)
        {
            initSideDrawer();
        }

        private void DrawerItemClicked(object sender, EventArgs e)
        {
            String strText;
            Object objTag;

            if (sender.GetType() == typeof(MaterialDrawerItem))
            {
                MaterialDrawerItem t = (MaterialDrawerItem)sender;
                if (t.Selected)
                {
                    return;
                }
            }
            else
            {
                MaterialFlatButton t = (MaterialFlatButton)sender;
                if (t.Selected)
                {
                    return;
                }
            }

            foreach (Control objSideControl in Controls)
            {
                if (objSideControl.GetType() == typeof(MaterialFlatButton))
                {
                    MaterialFlatButton objItem = (MaterialFlatButton)objSideControl;
                    objItem.Selected = false;
                    objItem.Invalidate();
                }
                else if (objSideControl.GetType() == typeof(MaterialDrawerItem))
                {
                    MaterialDrawerItem t = (MaterialDrawerItem)objSideControl;
                    t.Selected = false;
                    t.Invalidate();
                }
            }

            if (sender.GetType() == typeof(MaterialDrawerItem))
            {
                MaterialDrawerItem t = (MaterialDrawerItem)sender;
                t.Selected = true && SelectOnClick;
                strText = t.Text;
                objTag = t.Tag;
            }
            else
            {
                MaterialFlatButton t = (MaterialFlatButton)sender;
                t.Selected = true && SelectOnClick;
                strText = t.Text;
                objTag = t.Tag;
            }


            if (onSideDrawerItemClicked != null)
            {
                onSideDrawerItemClicked(sender, new SideDrawerEventArgs(strText, objTag));
            }
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


        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            ShowScrollBar(this.Handle, (int)ScrollBarDirection.SB_HORZ, false);
            base.WndProc(ref m);
        }
    }
}
