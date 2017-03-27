using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace MaterialWinforms.Controls
{
    public class MaterialAvatarView : Control, IShadowedMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }


        public int Elevation { get; set; }
        [Browsable(false)]
        public GraphicsPath ShadowBorder { get; set; }

        private Image _Avater;
        private Image _AvatarScaled;
        public Image Avatar
        {
            get
            {
                return _Avater;
            }
            set
            {
                _Avater = value;
                _AvatarScaled = DrawHelper.ResizeImage(_Avater, Width-1, Height-1);
            }

        }

        private String _AvatarLetter;
        public String AvatarLetter
        {
            get
            {
                return _AvatarLetter;
            }
            set
            {
                _AvatarLetter = value;
                CalculateAvatarFont();
            }
        }

        public override Color BackColor { get { return Color.Transparent; } }

        private FontManager objFontManager;
        private Font AvatarFont;
        private Graphics objGraphic;
        private Rectangle TextRect;

        public MaterialAvatarView()
        {
            objFontManager = new FontManager();
            DoubleBuffered = true;
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

            Elevation = 5;
            objGraphic = CreateGraphics();
            Width = 80;
            Height = 80;
        }

        protected override void OnResize(EventArgs e)
        {
            if(Width>Height)
            {
                Height = Width;
            }
            else
            {
                Width = Height;
            }
            TextRect = new Rectangle(Convert.ToInt32(Width * 0.05), Convert.ToInt32(Height * 0.05),Convert.ToInt32(Width * 0.9), Convert.ToInt32(Height * 0.9));
            Region = new Region(DrawHelper.CreateCircle(0, 0, Width / 2));
            CalculateAvatarFont();
            if (ShadowBorder != null)
            {
                ShadowBorder.Dispose();
            }
            ShadowBorder = new GraphicsPath();
            ShadowBorder = DrawHelper.CreateCircle(Location.X,
                                    Location.Y,
                                    ClientRectangle.Width / 2 -1);
            if (_AvatarScaled != null) { 
            _AvatarScaled.Dispose();
            _AvatarScaled = DrawHelper.ResizeImage(_Avater, Width, Height);
            }
        }

        private void CalculateAvatarFont()
        {
            if (!String.IsNullOrEmpty(_AvatarLetter))
            {
                AvatarFont = objFontManager.ScaleTextToRectangle(objGraphic, AvatarLetter, TextRect);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;

           
            if (Avatar == null)
            {
                g.FillPath(SkinManager.ColorScheme.PrimaryBrush, DrawHelper.CreateCircle(1, 1, Height / 2 - 1));
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.DrawString(AvatarLetter, AvatarFont, SkinManager.ACTION_BAR_TEXT_BRUSH(), TextRect, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                 
            }
            else
            {

                using (Brush brush = new TextureBrush(_AvatarScaled))
                {
                    g.FillEllipse(brush, new Rectangle(1, 1, Width-1, Height-1));
                }
            }
        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parms = base.CreateParams;
                parms.ExStyle |= 0x20;
                return parms;
            }
        }
    }
}
