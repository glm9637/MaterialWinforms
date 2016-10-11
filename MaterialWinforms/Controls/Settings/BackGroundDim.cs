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
        public BackGroundDim(MaterialForm FormToDim)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Size = FormToDim.Size;
            MinimumSize = Size;
            Location = FormToDim.Location;
            BackColor = FormToDim.SkinManager.GetApplicationBackgroundColor();
            Opacity = 0.80;
       
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SkinManager.GetApplicationBackgroundColor());
        }
    }
}
