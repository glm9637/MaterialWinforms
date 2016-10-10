using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MaterialWinforms.Animations;

namespace MaterialWinforms.Controls
{
    public class MaterialRaisedButton : Button, IShadowedMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public bool Primary { get; set; }
        public int Elevation { get; set; }
        [Browsable(false)]
        public GraphicsPath ShadowBorder { get; set; }

        public Color BackColor { get { return Primary ? SkinManager.ColorScheme.PrimaryColor : SkinManager.getRaisedButtonBackroundColor(); } }

        private readonly AnimationManager animationManager;

        public MaterialRaisedButton()
        {
            Primary = true;

            animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            Elevation = 5;
            animationManager.OnAnimationProgress += sender => Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            animationManager.StartNewAnimation(AnimationDirection.In, mevent.Location);
        }

        protected override void OnLocationChanged(System.EventArgs e)
        {
            base.OnLocationChanged(e);
            ShadowBorder = new GraphicsPath();
            ShadowBorder.AddRectangle(new Rectangle(this.Location.X,this.Location.Y,Width,Height));
        }

        void MaterialCard_MouseLeave(object sender, System.EventArgs e)
        {
            Elevation /= 2;
            Refresh();
        }

        void MaterialCard_MouseEnter(object sender, System.EventArgs e)
        {
            Elevation *= 2;
            Refresh();
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            ShadowBorder = new GraphicsPath();
            ShadowBorder.AddRectangle(new Rectangle(this.Location.X, this.Location.Y, Width, Height));
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(Parent.BackColor);
            
            using (var backgroundPath = DrawHelper.CreateRoundRect(ClientRectangle.X,
                ClientRectangle.Y,
                ClientRectangle.Width ,
                ClientRectangle.Height ,
                1f))
            {
                g.FillPath(Primary ? SkinManager.ColorScheme.PrimaryBrush : SkinManager.GetRaisedButtonBackgroundBrush(), backgroundPath);
            }

            if (animationManager.IsAnimating())
            {
                for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = animationManager.GetProgress(i);
                    var animationSource = animationManager.GetSource(i);
                    var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationValue * 50)), Color.White));
                    var rippleSize = (int)(animationValue * Width * 2);
                    g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                }
            }

            g.DrawString(
                Text.ToUpper(),
                SkinManager.ROBOTO_MEDIUM_10, 
                SkinManager.GetRaisedButtonTextBrush(Primary),
                ClientRectangle,
                new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
    }
}
