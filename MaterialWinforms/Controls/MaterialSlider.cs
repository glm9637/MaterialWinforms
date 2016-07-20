using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialWinforms.Controls
{
    public partial class MaterialSlider : Control, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        [Browsable(false)]

        public int Value;

        public int MaxValue;
        public int MinValue;

        private int MouseX;
        private bool MousePressed;

        private int IndicatorSize;

        private Rectangle IndicatorRectangle;
        private Rectangle IndicatorRectangleNormal;
        private Rectangle IndicatorRectanglePressed;
        public MaterialSlider()
        {
            InitializeComponent();
            IndicatorSize = 30;
            Width = 40;
            MinValue = 0;
            Height = IndicatorSize;
            MaxValue = 100;
            Value = 50;
       
            IndicatorRectangle = new Rectangle(0, 0, IndicatorSize, IndicatorSize);
            IndicatorRectangleNormal = new Rectangle();
            IndicatorRectanglePressed = new Rectangle();
            DoubleBuffered = true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            int iWidht = Width - 10;
            Height = IndicatorSize;
            IndicatorRectangle = new Rectangle(iWidht / 2 - IndicatorRectangle.Width / 2, 0, Height, Height);
            IndicatorRectangleNormal = new Rectangle(IndicatorRectangle.X + (IndicatorRectangle.Width / 4), IndicatorRectangle.Y + (IndicatorRectangle.Height / 4), (IndicatorRectangle.Width / 2), (IndicatorRectangle.Height / 2));
            IndicatorRectanglePressed = new Rectangle(IndicatorRectangle.X + (int)(IndicatorRectangle.Width * 0.375), IndicatorRectangle.Y + (int)(IndicatorRectangle.Height * 0.375), (int)(IndicatorRectangle.Width * 0.75), (int)(IndicatorRectangle.Height * 0.75));
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(SkinManager.GetApplicationBackgroundColor());
            Color LineColor;
            Color BalloonColor;

            if (SkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                LineColor = Color.FromArgb((int)(2.55 * 30), 255, 255, 255);
            }
            else
            {
                LineColor = Color.FromArgb((int)(2.55 * (Focused ? 38 : 26)), 0, 0, 0);
            }

            BalloonColor = Color.FromArgb((int)(2.55 * 30), (Value == 0 ? Color.Gray : SkinManager.ColorScheme.PrimaryColor));

            Pen LinePen = new Pen(LineColor, 2);

            g.DrawLine(LinePen, 5, Height / 2, Width - 5, Height / 2);

            g.DrawLine(SkinManager.ColorScheme.AccentPen, 5, Height / 2, IndicatorRectangleNormal.X, Height / 2);

            if (MousePressed)
            {
                g.FillEllipse(SkinManager.ColorScheme.AccentBrush, IndicatorRectanglePressed);
            }
            else
            {
                g.FillEllipse(SkinManager.ColorScheme.AccentBrush, IndicatorRectangleNormal);
                if (Focused)
                {
                    g.FillEllipse(new SolidBrush(BalloonColor), IndicatorRectangle);
                }
            }

            e.Graphics.DrawImage((Image)bmp.Clone(), 0, 0);
        }
    }
}
