using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MaterialWinforms.Controls
{
    public partial class MaterialTimeLineEntry : MaterialUserControl
    {
        private StringFormat _StringFormat;
        private Graphics StringGraphics;
        private FontManager _FontManager;

        private Rectangle NameRect;
        private String _UserName;
        private String _AdditionalInfo;

        public String UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                if(String.IsNullOrEmpty(_UserInitialien))
                {
                    UserInitialien = _UserName.Substring(0, 1);
                }
                NameRect.Size = MeasureString(value, SkinManager.ROBOTO_REGULAR_11);
                OnResize(EventArgs.Empty);
            }
        }

        private Font _UserInitialienFont;
        private String _UserInitialien;
        public String UserInitialien {get{return _UserInitialien;}set{_UserInitialien = value; _UserInitialienFont = _FontManager.ScaleTextToRectangle(StringGraphics,value,new Rectangle(AvatarRect.X,AvatarRect.Y,AvatarRect.Width-4,AvatarRect.Height-4));}}

        private Image _UserScaled;
        private Image _User;
        public Image User
        {
            get { return _User; }
            set
            {
                _User = value;
                _UserScaled = DrawHelper.ResizeImage(value, AvatarRect.Size.Width-1, AvatarRect.Size.Height-1);
            }
        }

        private Rectangle TitleRect;
        private String _Title;
        public String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                TitleRect.Size = MeasureString(_Title,_FontManager.Roboto_Medium15);
                OnResize(EventArgs.Empty);
            }
        }

        private Rectangle ContentRect;
        private String _Text;
        public new String Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
               ContentRect.Size = MeasureString(_Text,SkinManager.ROBOTO_REGULAR_11);
                OnResize(EventArgs.Empty);
            }
        }

        private Rectangle TimeRectangle;
        private DateTime _Time;
        public DateTime Time
            {
            get { return _Time;}
            set
                {
                    _Time = value;
                }
        }

        private Rectangle AdditionalInfoRectangle;
        public String AdditionalInfo
        {
            get { return _AdditionalInfo; }
            set
            {
                _AdditionalInfo = value;
            }
        }

        private Rectangle AvatarRect;
        private Rectangle CardRectangle;
        private GraphicsPath CardShadow;

        public MaterialTimeLineEntry()
        {
            _StringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            _FontManager = new FontManager();
            DoubleBuffered = true;
            StringGraphics = Graphics.FromImage(new Bitmap(10, 10));
            AutoSize = true;
            TimeRectangle = new Rectangle(new Point(0, 30), MeasureString(DateTime.Now.ToString("dd.MM.yyyy") + "\r\n" + DateTime.Now.ToString("HH:mm:ss"), SkinManager.ROBOTO_REGULAR_11));
            AvatarRect = new Rectangle(TimeRectangle.Right+5, 20, 50, 50);
            CardRectangle = new Rectangle(AvatarRect.Right + 20, 20,40,40);
            TitleRect = new Rectangle(CardRectangle.X + 10, CardRectangle.Y + 10, 30, 30);
            ContentRect = new Rectangle(TitleRect.X, TitleRect.Bottom + 5, TitleRect.Width, TitleRect.Height);
            NameRect = new Rectangle(ContentRect.X, ContentRect.Bottom + 5, TitleRect.Width, TitleRect.Height);
            AdditionalInfoRectangle = new Rectangle(TimeRectangle.Left, TimeRectangle.Bottom+5, TimeRectangle.Width, TimeRectangle.Height);
   
        }

        private Size MeasureString(String pStringToMeasuer, Font pFontToUse)
        {
            SizeF tmp = StringGraphics.MeasureString(pStringToMeasuer, pFontToUse);
            return new Size((int)Math.Ceiling(tmp.Width), (int)Math.Ceiling(tmp.Height));
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ContentRect.Location = new Point(TitleRect.X, TitleRect.Bottom + 10);
            NameRect.Location = new Point(ContentRect.X, ContentRect.Bottom + 10);
            CardRectangle.Size = new Size(Math.Max(Math.Max(ContentRect.Size.Width, NameRect.Size.Width), TitleRect.Size.Width)+20, NameRect.Bottom-TitleRect.Y +20);
            CardShadow = DrawHelper.CreateRoundRect(CardRectangle, 10);
            Size = new Size(CardRectangle.Right + 20, CardRectangle.Bottom + 20);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //Draw Background
            //Draw Vertical Line
            e.Graphics.FillRectangle(MaterialSkinManager.Instance.ColorScheme.PrimaryBrush, new Rectangle(AvatarRect.X+(int)(AvatarRect.Width/2)-5, -5, 10, Height+5));
            //Draw Avatar
            if (_User == null)
            {
                e.Graphics.FillEllipse(SkinManager.ColorScheme.PrimaryBrush, AvatarRect);
            }
            else
            {
                using (TextureBrush brush = new TextureBrush(_UserScaled))
                {
                    brush.WrapMode = WrapMode.Clamp;
                    Point xDislpayCenterRelativ = new Point(AvatarRect.Width / 2, AvatarRect.Height / 2);
                    Point xImageCenterRelativ = new Point(_UserScaled.Width / 2, _UserScaled.Height / 2);
                    Point xOffSetRelativ = new Point(xDislpayCenterRelativ.X - xImageCenterRelativ.X, xDislpayCenterRelativ.Y - xImageCenterRelativ.Y);

                    Point xAbsolutePixel = xOffSetRelativ + new Size(AvatarRect.Location);
                    brush.TranslateTransform(xAbsolutePixel.X, xAbsolutePixel.Y);
                    e.Graphics.FillEllipse(brush, AvatarRect);
                }
            }
            // Draw Card
            DrawHelper.drawShadow(e.Graphics, CardShadow, 4, Color.Black);
            e.Graphics.FillPath(SkinManager.getCardsBrush(), CardShadow);

            //Draw Strings
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //Draw Time
            e.Graphics.DrawString(_Time.ToString("dd.MM.yyyy") + "\r\n" + _Time.ToString("HH:mm:ss"), SkinManager.ROBOTO_REGULAR_11, SkinManager.ColorScheme.TextBrush, TimeRectangle, _StringFormat);
            //Draw Avatar
            if (_User == null)
            {
                e.Graphics.DrawString(_UserInitialien, _UserInitialienFont, SkinManager.ColorScheme.TextBrush, AvatarRect, _StringFormat);
            }
            //Draw Title
            e.Graphics.DrawString(_Title, _FontManager.Roboto_Medium15, SkinManager.ColorScheme.PrimaryBrush, TitleRect, _StringFormat);
            //Draw Content
            e.Graphics.DrawString(_Text, SkinManager.ROBOTO_REGULAR_11, SkinManager.ColorScheme.TextBrush, ContentRect, new StringFormat { LineAlignment = StringAlignment.Center });
            //Draw Name
            e.Graphics.DrawString(_UserName, SkinManager.ROBOTO_REGULAR_11, SkinManager.ColorScheme.TextBrush, NameRect, _StringFormat);

            e.Graphics.DrawString(_AdditionalInfo, SkinManager.ROBOTO_REGULAR_11, SkinManager.ColorScheme.TextBrush, AdditionalInfoRectangle, _StringFormat);
        }

    
    }
}
