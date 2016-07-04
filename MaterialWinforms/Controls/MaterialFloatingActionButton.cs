using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MaterialWinforms.Animations;

namespace MaterialWinforms.Controls
{

    public class MaterialFloatingActionButton : Button, IMaterialControl
    {

        private Image _Icon;

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        [Category("Appearance")]
        public Image Icon
        {
            get { return _Icon; }
            set
            {
                _Icon = value;
            }
        }

        [Browsable(false)]
        [DefaultValue (typeof(int),"48")]
        public int Breite { get { return this.Width; } set { this.Width = value; } }

        [Browsable(false)]
        [DefaultValue(typeof(int), "48")]
        public int Hoehe { get { return this.Height; } set { this.Height = value; } }

        private readonly AnimationManager animationManager;
        private readonly AnimationManager hoverAnimationManager;

        public MaterialFloatingActionButton()
        {
            Height = 48;
            Width = 48;
            BackColor = Color.Transparent;
            
            animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            hoverAnimationManager = new AnimationManager
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };
            hoverAnimationManager.OnAnimationProgress += sender => Invalidate();
            animationManager.OnAnimationProgress += sender => Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            animationManager.StartNewAnimation(AnimationDirection.In, mevent.Location);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int ShadowDepth = 5;
            int iCropping = ClientRectangle.Width / 3;
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;



            g.Clear(Parent.BackColor);
            for (int i = 0; i < ShadowDepth; i++)
            {
                using (var backgroundPath = DrawHelper.CreateCircle(ClientRectangle.X+i,
                                    ClientRectangle.Y+i,
                                    ClientRectangle.Width /2 -i))
                {
                    g.FillPath(new SolidBrush(Color.FromArgb((50 / ShadowDepth-1) * i, Color.Black)), backgroundPath);
                }
            }

            Region = new Region(DrawHelper.CreateCircle(ClientRectangle.X + 4,
                                    ClientRectangle.Y + 4,
                                    ClientRectangle.Width / 2 - 4));

            using (var backgroundPath = DrawHelper.CreateCircle(ClientRectangle.X + ShadowDepth,
                    ClientRectangle.Y + ShadowDepth,
                    ClientRectangle.Width /2- ShadowDepth))
                {
                    g.FillPath(SkinManager.ColorScheme.AccentBrush, backgroundPath);
                }

            if (_Icon != null)
            {
                g.DrawImage(_Icon, ClientRectangle.X + iCropping / 2, ClientRectangle.X + iCropping / 2, ClientRectangle.Width - iCropping, ClientRectangle.Width - iCropping);
            }

            Color c = SkinManager.GetFlatButtonHoverBackgroundColor();
            using (Brush b = new SolidBrush(Color.FromArgb((int)(hoverAnimationManager.GetProgress() * c.A), c.RemoveAlpha())))
                g.FillEllipse(b, ClientRectangle);

            if (animationManager.IsAnimating())
            {
                for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = animationManager.GetProgress(i);
                    var animationSource = animationManager.GetSource(i);
                    var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationValue * 50)), Color.White));
                    var rippleSize = (int)(animationValue * Width *2);
                    g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                }
            }
           
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;

            MouseState = MouseState.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                hoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                hoverAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };
            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    MouseState = MouseState.DOWN;

                    animationManager.StartNewAnimation(AnimationDirection.In, args.Location);
                    Invalidate();
                }
            };
            MouseUp += (sender, args) =>
            {
                MouseState = MouseState.HOVER;

                Invalidate();
            };
        }
    }
}

