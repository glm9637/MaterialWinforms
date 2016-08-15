using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MaterialWinforms.Controls
{
    public sealed class MaterialDivider : Control, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public Color BackColor { get { return SkinManager.GetDividersColor(); } }
        
        public MaterialDivider()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Height = 1;
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Height = 1;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SkinManager.GetDividersColor());
        }
    }
}
