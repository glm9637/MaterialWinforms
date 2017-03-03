using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialWinforms.Controls.Settings
{
    public partial class BackGroundDim : MaterialForm
    {

        public new bool CanFocus { get { return false; } }
        public bool IsVisible = false;

        private Size finalSize;
        private Point finalLocation;
        private Form _FormToDim;

        public BackGroundDim(MaterialForm FormToDim)
        {
            _FormToDim = FormToDim;
            InitializeComponent();
            SkinManager.AddFormToManage(this);
            StartPosition = FormStartPosition.Manual;
            Size = FormToDim.Size;
            MinimumSize = Size;
            Location = FormToDim.Location;
            BackColor = FormToDim.SkinManager.GetApplicationBackgroundColor();
            Opacity = 0;

            SetStyle(ControlStyles.StandardDoubleClick, false);
            //Enabled = false;
            finalLocation = FormToDim.Location; ;
            finalSize = FormToDim.Size;

            FormToDim.LocationChanged += FormToDim_LocationChanged;
        }

        void FormToDim_LocationChanged(object sender, EventArgs e)
        {
            Location = ((Form)sender).Location;
            Application.DoEvents();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SkinManager.GetApplicationBackgroundColor());
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Size = _FormToDim.Size;
            Location = _FormToDim.Location;
        }


        protected override void OnLocationChanged(EventArgs e)
        {
            Location = _FormToDim.Location;
        }

        private const int WM_MOUSEACTIVATE = 0x0021, MA_NOACTIVATE = 0x0003;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = (IntPtr)MA_NOACTIVATE;
                return;
            }
            base.WndProc(ref m);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            for (int i = 0; i < 40; i++)
            {
                Opacity = Opacity + 0.02;
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            IsVisible = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            for (int i = 0; i < 40; i++)
            {
                Opacity = Opacity - 0.02;
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            IsVisible = false;
        }
    }
}
