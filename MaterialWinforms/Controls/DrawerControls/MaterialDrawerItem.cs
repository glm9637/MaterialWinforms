using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MaterialWinforms.Animations;

namespace MaterialWinforms.Controls
{
    public class MaterialDrawerItem : Button, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public bool Primary { get; set; }

        public Color BackColor { get { return Parent==null?SkinManager.GetApplicationBackgroundColor():Parent.BackColor; } }

        public bool Accent { get; set; }

        [Browsable(false)]
        public bool Selected { get; set; }

        private readonly AnimationManager animationManager;
        private readonly AnimationManager hoverAnimationManager;

        private SizeF textSize;

        public MaterialDrawerItem()
        {
            Primary = false;
            Accent = false;

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

            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoSize = true;
            Margin = new Padding(4, 6, 4, 6);
            Padding = new Padding(0);
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                textSize = CreateGraphics().MeasureString(value.ToUpper(), SkinManager.ROBOTO_MEDIUM_10);
                if (IconImage != null)
                    textSize = new Size((int)textSize.Width + (int)ClientRectangle.Height, (int)textSize.Height);
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        public Image IconImage
        {
            get { return base.Image; }
            set
            {
                base.Image = value;
                textSize = CreateGraphics().MeasureString(Text.ToUpper(), SkinManager.ROBOTO_MEDIUM_10);
                if (IconImage != null)
                    textSize = new Size((int)textSize.Width + (int)textSize.Height, (int)textSize.Height);
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            bool ImageDrawn = false;
            var g = pevent.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(Parent.BackColor);

            if (Image != null)
            {
                ImageDrawn = true;
                g.DrawImage(Image, 8, 2, Height - 4, Height - 4);
            }

            if (Selected)
            {
                g.FillRectangle(SkinManager.GetFlatButtonHoverBackgroundBrush(), ClientRectangle);
            }

            //Hover
            Color c = SkinManager.GetFlatButtonHoverBackgroundColor();
            using (Brush b = new SolidBrush(Color.FromArgb((int)(hoverAnimationManager.GetProgress() * c.A), c.RemoveAlpha())))
                g.FillRectangle(b, ClientRectangle);

            //Ripple
            if (animationManager.IsAnimating())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = animationManager.GetProgress(i);
                    var animationSource = animationManager.GetSource(i);

                    using (Brush rippleBrush = new SolidBrush(Color.FromArgb((int)(101 - (animationValue * 100)), Color.Black)))
                    {
                        var rippleSize = (int)(animationValue * Width * 2);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                    }
                }
                g.SmoothingMode = SmoothingMode.None;
            }
            g.DrawString(Text.ToUpper(), SkinManager.ROBOTO_MEDIUM_10, Enabled ? (Primary ? SkinManager.ColorScheme.PrimaryBrush : Accent ? SkinManager.ColorScheme.AccentBrush : SkinManager.GetPrimaryTextBrush()) : SkinManager.GetFlatButtonDisabledTextBrush(), ClientRectangle, new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
        }

        private Size GetPreferredSize()
        {
            return GetPreferredSize(new Size(0, 0));
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            return new Size((int)textSize.Width + 8, 36);
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
