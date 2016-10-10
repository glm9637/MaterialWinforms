using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MaterialWinforms.Controls
{
    public class MaterialTabControl : TabControl, IMaterialControl
    {
        int _CurrentPage;

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        
		protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }

        public MaterialTabControl()
        {
            _CurrentPage = 0;
        }

    }
}
