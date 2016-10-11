using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace MaterialWinforms.Controls.Settings
{
    public class MaterialSettings : MaterialForm
    {
        private const int CS_DROPSHADOW = 0x00020000;
        private MaterialSideDrawer SettingsDrawer;

        private Boolean _ThemeSettings;
        private MaterialPanel pnl_SettingsView;
        private MaterialContextMenuStrip SettingsDrawerItems;



        public Boolean ShowThemeSettings
        {
            get { return _ThemeSettings; }
            set
            {
                _ThemeSettings = value;
                if (SettingsDrawer != null)
                {
                    if (_ThemeSettings)
                    {
                        SettingsDrawerItems.Items.Add(_ThemeSettingsToolStripItem);
                        SideDrawer.Invalidate();
                    }
                    else
                    {
                        SettingsDrawerItems.Items.Remove(_ThemeSettingsToolStripItem);
                        SideDrawer.Invalidate();
                    }
                }
            }
        }
        private MaterialToolStripMenuItem _ThemeSettingsToolStripItem;

        public MaterialSettings()
        {
            SkinManager.AddFormToManage(this);

            InitializeComponent();

            _ThemeSettingsToolStripItem = new MaterialToolStripMenuItem();
            _ThemeSettingsToolStripItem.Text = "Theme";
            _ThemeSettingsToolStripItem.Click += DisplayThemeSettings;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

        private void DisplayThemeSettings(object sender, EventArgs e)
        {
            MaterialThemeSettings objSettings = new MaterialThemeSettings();
            objSettings.Dock = DockStyle.Fill;
            pnl_SettingsView.Controls.Clear();
            pnl_SettingsView.Controls.Add(objSettings);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void InitializeComponent()
        {
            System.Drawing.Drawing2D.GraphicsPath graphicsPath1 = new System.Drawing.Drawing2D.GraphicsPath();
            this.SettingsDrawer = new MaterialWinforms.Controls.MaterialSideDrawer();
            this.SettingsDrawerItems = new MaterialWinforms.Controls.MaterialContextMenuStrip();
            this.pnl_SettingsView = new MaterialWinforms.Controls.MaterialPanel();
            this.SuspendLayout();
            // 
            // SettingsDrawer
            // 
            this.SettingsDrawer.AutoScroll = true;
            this.SettingsDrawer.Depth = 0;
            this.SettingsDrawer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsDrawer.Elevation = 10;
            this.SettingsDrawer.HideSideDrawer = false;
            this.SettingsDrawer.Location = new System.Drawing.Point(0, 24);
            this.SettingsDrawer.MaximumSize = new System.Drawing.Size(210, 10000);
            this.SettingsDrawer.MouseState = MaterialWinforms.MouseState.HOVER;
            this.SettingsDrawer.Name = "SettingsDrawer";
            this.SettingsDrawer.SelectOnClick = true;
            graphicsPath1.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.SettingsDrawer.ShadowBorder = graphicsPath1;
            this.SettingsDrawer.SideDrawer = this.SettingsDrawerItems;
            this.SettingsDrawer.SideDrawerFixiert = true;
            this.SettingsDrawer.SideDrawerUnterActionBar = false;
            this.SettingsDrawer.Size = new System.Drawing.Size(210, 717);
            this.SettingsDrawer.TabIndex = 0;
            this.SettingsDrawer.onSideDrawerItemClicked += new MaterialWinforms.Controls.MaterialSideDrawer.SideDrawerEventHandler(this.SettingsDrawer_onSideDrawerItemClicked);
            // 
            // SettingsDrawerItems
            // 
            this.SettingsDrawerItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.SettingsDrawerItems.Depth = 0;
            this.SettingsDrawerItems.MouseState = MaterialWinforms.MouseState.HOVER;
            this.SettingsDrawerItems.Name = "materialContextMenuStrip1";
            this.SettingsDrawerItems.Size = new System.Drawing.Size(61, 4);
            // 
            // pnl_SettingsView
            // 
            this.pnl_SettingsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_SettingsView.Depth = 0;
            this.pnl_SettingsView.Location = new System.Drawing.Point(218, 33);
            this.pnl_SettingsView.MouseState = MaterialWinforms.MouseState.HOVER;
            this.pnl_SettingsView.Name = "pnl_SettingsView";
            this.pnl_SettingsView.Size = new System.Drawing.Size(522, 705);
            this.pnl_SettingsView.TabIndex = 2;
            // 
            // MaterialSettings
            // 
            this.ClientSize = new System.Drawing.Size(743, 741);
            this.Controls.Add(this.SettingsDrawer);
            this.Controls.Add(this.pnl_SettingsView);
            this.Name = "MaterialSettings";
            this.SideDrawer = this.SettingsDrawer;
            this.Text = "Settings";
            this.ResumeLayout(false);

        }

        private void SettingsDrawer_onSideDrawerItemClicked(object sender, MaterialSideDrawer.SideDrawerEventArgs e)
        {
            MaterialThemeSettings objSettings = new MaterialThemeSettings();
            objSettings.Dock = DockStyle.Fill;
            pnl_SettingsView.Controls.Clear();
            pnl_SettingsView.Controls.Add(objSettings);
        }

    }

}
