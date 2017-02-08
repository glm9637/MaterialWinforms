using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MaterialWinforms.Controls
{
    public class MaterialLabel : Label, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        public override Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : typeof(IMaterialControl).IsAssignableFrom(Parent.GetType())?((IMaterialControl)Parent).BackColor:Parent.BackColor; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            ForeColor = SkinManager.GetPrimaryTextColor();
            Font = SkinManager.ROBOTO_REGULAR_11;
            
            BackColorChanged += (sender, args) => ForeColor = SkinManager.GetPrimaryTextColor();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            base.OnPaint(e);
        }

    }
}
