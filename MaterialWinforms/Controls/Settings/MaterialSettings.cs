using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace MaterialWinforms.Controls.Settings
{
    public class MaterialSettings : MaterialForm
    {
        private const int CS_DROPSHADOW = 0x00020000;
        private MaterialSideDrawer SettingsDrawer;

        private Boolean _ThemeSettings;
        private MaterialPanel pnl_SettingsView;
        private MaterialContextMenuStrip SettingsDrawerItems;
        private MaterialForm _BaseForm;
        private BackGroundDim objDimmer;
        private Boolean _IgnoreActivate;

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

        public MaterialSettings(MaterialForm Parent)
        {
            _IgnoreActivate = false;
            StartPosition = FormStartPosition.Manual;

            Opacity = 0;
            _BaseForm = Parent;
            SkinManager.AddFormToManage(this);
            MinimizeBox = false;
            MaximizeBox = false;


            InitializeComponent();


           
            _ThemeSettingsToolStripItem = new MaterialToolStripMenuItem();
            _ThemeSettingsToolStripItem.Text = "Theme";
            MaterialThemeSettings objSettings = new MaterialThemeSettings(_BaseForm, this);
            objSettings.Dock = DockStyle.Fill;
            _ThemeSettingsToolStripItem.Tag = objSettings;
        }

        void _BaseForm_LocationChanged(object sender, EventArgs e)
        {
            CalculateStart();
        }


        void _BaseForm_GotFocus(object sender, EventArgs e)
        {
            objDimmer.Focus();
            Focus();
        }

        private void CalculateStart()
        {
            Location = new Point(Convert.ToInt32(_BaseForm.Location.X + _BaseForm.Width * 0.1), Convert.ToInt32(_BaseForm.Location.Y + _BaseForm.Height * 0.1));
            Size objNewSize = new Size(Convert.ToInt32(_BaseForm.Width * 0.8), Convert.ToInt32(_BaseForm.Height * 0.8));
            MaximumSize = objNewSize;
            MinimumSize = objNewSize;
            Size = objNewSize;
        }


        protected override void OnLoad(EventArgs e)
        {
            CalculateStart();

            if (SettingsDrawerItems.Items.Count > 0)
            {
                SettingsDrawer.SelectItem(0);
            }
            base.OnLoad(e);

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (!_IgnoreActivate)
            {
                CalculateStart();
                _BaseForm.GotFocus += _BaseForm_GotFocus;
                _BaseForm.Activated += _BaseForm_GotFocus;
                _BaseForm.LocationChanged += _BaseForm_LocationChanged;
                objDimmer = new BackGroundDim(_BaseForm);
                objDimmer.Show();
                while (!objDimmer.IsVisible)
                {
                    Application.DoEvents();
                }
                TopMost = true;
                BringToFront();
                TopMost = false;
                Opacity = 1;
                _IgnoreActivate = true;
            }
            
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            Opacity = 0;
            objDimmer.Close();
            _IgnoreActivate = false;
            _BaseForm.GotFocus -= _BaseForm_GotFocus;
            _BaseForm.Activated -= _BaseForm_GotFocus;
            _BaseForm.LocationChanged -= _BaseForm_LocationChanged;
            base.OnClosing(e);
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
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

        public void AddPage(string Name, UserControl Content)
        {
            MaterialToolStripMenuItem objNeuesItem = new MaterialToolStripMenuItem();
            objNeuesItem.Text = Name;
            objNeuesItem.Name = Name;
            objNeuesItem.Tag = Content;
            objNeuesItem.Click += OpenPage;
            objNeuesItem.PerformClick();
            SettingsDrawerItems.Items.Add(objNeuesItem);

        }

        private void OpenPage(object sender, EventArgs e)
        {
            pnl_SettingsView.Controls.Clear();
            pnl_SettingsView.Controls.Add((UserControl)((MaterialToolStripMenuItem)sender).Tag);
            pnl_SettingsView.Controls[0].Dock = DockStyle.Fill;
        }

        private void InitializeComponent()
        {
            System.Drawing.Drawing2D.GraphicsPath graphicsPath2 = new System.Drawing.Drawing2D.GraphicsPath();
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
            this.SettingsDrawer.HiddenOnStart = true;
            this.SettingsDrawer.HideSideDrawer = false;
            this.SettingsDrawer.Location = new System.Drawing.Point(0, 24);
            this.SettingsDrawer.MaximumSize = new System.Drawing.Size(210, 10000);
            this.SettingsDrawer.MouseState = MaterialWinforms.MouseState.HOVER;
            this.SettingsDrawer.Name = "SettingsDrawer";
            this.SettingsDrawer.SelectOnClick = true;
            graphicsPath2.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.SettingsDrawer.ShadowBorder = graphicsPath2;
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
            this.pnl_SettingsView.AutoScroll = true;
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
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SideDrawer = this.SettingsDrawer;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);

        }

        private void SettingsDrawer_onSideDrawerItemClicked(object sender, MaterialSideDrawer.SideDrawerEventArgs e)
        {

            pnl_SettingsView.Controls.Clear();
            pnl_SettingsView.Controls.Add((UserControl)e.getTag());
        }

        public Bitmap CreateImage()
        {
            Bitmap bmp = new Bitmap(_BaseForm.Width, _BaseForm.Height);
            _BaseForm.DrawToBitmap(bmp, new Rectangle(0, 0, _BaseForm.Width, _BaseForm.Height));
            Bitmap DimmerBitmap = new Bitmap(_BaseForm.Width, _BaseForm.Height);
            objDimmer.DrawToBitmap(DimmerBitmap, new Rectangle(0, 0, _BaseForm.Width, _BaseForm.Height));
            Graphics.FromImage(bmp).DrawImageUnscaled(ChangeImageOpacity(DimmerBitmap, objDimmer.Opacity), 0, 0);
            DrawToBitmap(bmp, new Rectangle(Location.X - _BaseForm.Location.X, Location.Y - _BaseForm.Location.Y, Width, Height));

            return bmp;
        }

        private const int bytesPerPixel = 4;

        /// <summary>
        /// Change the opacity of an image
        /// </summary>
        /// <param name="originalImage">The original image</param>
        /// <param name="opacity">Opacity, where 1.0 is no opacity, 0.0 is full transparency</param>
        /// <returns>The changed image</returns>
        private Bitmap ChangeImageOpacity(Bitmap originalImage, double opacity)
        {
            if ((originalImage.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
            {
                // Cannot modify an image with indexed colors
                return originalImage;
            }

            Bitmap bmp = (Bitmap)originalImage.Clone();

            // Specify a pixel format.
            PixelFormat pxf = PixelFormat.Format32bppArgb;

            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            // This code is specific to a bitmap with 32 bits per pixels 
            // (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha).
            int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
            byte[] argbValues = new byte[numBytes];

            // Copy the ARGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);

            // Manipulate the bitmap, such as changing the
            // RGB values for all pixels in the the bitmap.
            for (int counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
            {
                // argbValues is in format BGRA (Blue, Green, Red, Alpha)

                // If 100% transparent, skip pixel
                if (argbValues[counter + bytesPerPixel - 1] == 0)
                    continue;

                int pos = 0;
                pos++; // B value
                pos++; // G value
                pos++; // R value

                argbValues[counter + pos] = (byte)(argbValues[counter + pos] * opacity);
            }

            // Copy the ARGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            return bmp;
        }

    }

}
