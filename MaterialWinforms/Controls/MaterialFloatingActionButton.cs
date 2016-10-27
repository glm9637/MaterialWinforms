using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MaterialWinforms.Animations;
using System;

namespace MaterialWinforms.Controls
{

    public class MaterialFloatingActionButton : Button, IShadowedMaterialControl
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

        public int Elevation { get; set; }
        [Browsable(false)]
        public GraphicsPath ShadowBorder { get; set; }

        public new Color BackColor { get { return SkinManager.ColorScheme.AccentColor; } }

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
            Elevation = 5;
            
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
            SizeChanged += Redraw;
            LocationChanged += Redraw;
            ParentChanged += new System.EventHandler(onParentChanged);
            MouseEnter += MaterialCard_MouseEnter;
            MouseLeave += MaterialCard_MouseLeave;
        }

        void MaterialCard_MouseLeave(object sender, System.EventArgs e)
        {
            Elevation /= 2;
            Redraw(null, null);
        }

        void MaterialCard_MouseEnter(object sender, System.EventArgs e)
        {
            Elevation *= 2;
            Redraw(null, null);
        }

        private void Redraw(object sender, System.EventArgs e)
        {
            if (ShadowBorder != null)
            {
                ShadowBorder.Dispose();
            }
            ShadowBorder = new GraphicsPath();
            ShadowBorder = DrawHelper.CreateCircle(Location.X ,
                                    Location.Y,
                                    ClientRectangle.Width/2 -1);
            if (Width != Height)
            {
                Width = Math.Min(Width, Height);
                Height = Math.Min(Width, Height);
            }

            Invalidate();

        }

        private void onParentChanged(object sender, System.EventArgs e)
        {
            if (Parent != null)
            {
                Parent.BackColorChanged += new System.EventHandler(Redraw);
                Parent.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            animationManager.StartNewAnimation(AnimationDirection.In, mevent.Location);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int iCropping = ClientRectangle.Width / 3;
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
           

            Region = new Region(DrawHelper.CreateCircle(ClientRectangle.X ,
                                    ClientRectangle.Y ,
                                    ClientRectangle.Width / 2));


            g.Clear(SkinManager.ColorScheme.AccentColor);


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

