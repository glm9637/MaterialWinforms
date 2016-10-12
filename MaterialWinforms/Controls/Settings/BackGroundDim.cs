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

        private Size finalSize;
        private Point finalLocation;

        public BackGroundDim(MaterialForm FormToDim)
        {
            InitializeComponent();
            SkinManager.AddFormToManage(this);
            StartPosition = FormStartPosition.Manual;
            Size = FormToDim.Size;
            MinimumSize = Size;
            Location = FormToDim.Location;
            BackColor = FormToDim.SkinManager.GetApplicationBackgroundColor();
            Opacity = 0.80;
            SetStyle(ControlStyles.StandardDoubleClick, false);
            Enabled = false;
            finalLocation = FormToDim.Location; ;
            finalSize = FormToDim.Size; ;
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
            Size = finalSize;
            Location = finalLocation;
        }


        protected override void OnLocationChanged(EventArgs e)
        {
            Location = finalLocation;
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
    }
}
