using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MaterialWinforms.Controls
{
    [System.ComponentModel.ToolboxItem(true)]
    public class MaterialTabPage : TabPage
    {

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.White);
            foreach (Control objChild in Controls)
            {
                if (typeof(IShadowedMaterialControl).IsAssignableFrom(objChild.GetType()))
                {
                    IShadowedMaterialControl objCurrent = (IShadowedMaterialControl)objChild;
                    DrawHelper.drawShadow(e.Graphics, objCurrent.ShadowBorder, objCurrent.Elevation, BackColor);
                    
                }
            }
        }
    }


}
