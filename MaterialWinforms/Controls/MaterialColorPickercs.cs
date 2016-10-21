using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using MaterialWinforms.Animations;

namespace MaterialWinforms.Controls
{
    public class MaterialColorPicker : Control, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        private List<ColorRect> ColorRectangles;

        private AnimationManager objAnimationManager;

        public delegate void ColorChanged(Color newColor);
        public event ColorChanged onColorChanged;

        private MaterialColorSlider RedSlider, GreenSlider, BlueSlider;
        private Color pBaseColor;
        private GraphicsPath objShadowPath;

        public Color BackColor { get { return SkinManager.GetCardsColor(); } set{} }

        private GraphicsPath CurrentHoveredPath;
        private int HoveredIndex;
        private Color TempColor;

        public Color Value
        {
            get { return pBaseColor; }
            set
            {
                pBaseColor = value;
                RedSlider.Value = pBaseColor.R;
                GreenSlider.Value = pBaseColor.G;
                BlueSlider.Value = pBaseColor.B;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            for (int i = 0; i < ColorRectangles.Count; i++)
            {
                if (ColorRectangles[i].Rect.Contains(e.Location))
                {
                    if (i != HoveredIndex)
                    {
                        HoveredIndex = i;
                        CurrentHoveredPath = new GraphicsPath();
                        CurrentHoveredPath.AddRectangle(new Rectangle(ColorRectangles[i].Rect.X-3,ColorRectangles[i].Rect.Y-3,ColorRectangles[i].Rect.Width,ColorRectangles[i].Rect.Height));
                        Invalidate();   
                    }
                    return;
                }
            }
            if(HoveredIndex>=0){
                HoveredIndex = -1;
                CurrentHoveredPath = new GraphicsPath();
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (HoveredIndex >= 0)
                {
                    TempColor = ColorRectangles[HoveredIndex].Color;
                    objAnimationManager.SetProgress(0);
                    objAnimationManager.StartNewAnimation(AnimationDirection.In);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            HoveredIndex = -1;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            objShadowPath = new GraphicsPath();
            objShadowPath.AddLine(0, (int)(Height * 0.6), Width, (int)(Height * 0.6));
            Invalidate();
        }

        private void initColorHints()
        {
            ColorRectangles = new List<ColorRect>();
            int x = 0;
            int y = 0;
            
            int RectWidth = (Width / 8);
          
            int RectY = BlueSlider.Bottom + 5;
            int RectHeight = (Height - RectY) / 3;


            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Red200).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Red500).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Red700).ToColor()));
            y = 0;
            x++;

            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Purple200).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Purple500).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Purple700).ToColor()));
            y = 0;
            x++;

            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Indigo200).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Indigo500).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Indigo700).ToColor()));
            y = 0;
            x++;

            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.LightBlue200).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.LightBlue500).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.LightBlue700).ToColor()));
            y = 0;
            x++;

            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Teal200).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Teal500).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Teal700).ToColor()));
            y = 0;
            x++;

            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.LightGreen200).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.LightGreen500).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.LightGreen700).ToColor()));
            y = 0;
            x++;

            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Yellow200).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Yellow500).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Yellow700).ToColor()));
            y = 0;
            x++;

            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Orange200).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Orange500).ToColor()));
            y++;
            ColorRectangles.Add(new ColorRect(new Rectangle(RectWidth * x, RectHeight * y + RectY, RectWidth, RectHeight), ((int)Primary.Orange700).ToColor()));

        }

        public MaterialColorPicker()
        {
            pBaseColor = SkinManager.ColorScheme.AccentColor;
            Width = 248;
            Height = 425;

            RedSlider = new MaterialColorSlider("R", Color.Red, pBaseColor.R);
            GreenSlider = new MaterialColorSlider("G", Color.Green, pBaseColor.G);
            BlueSlider = new MaterialColorSlider("B", Color.Blue, pBaseColor.B);

            RedSlider.Width = Width;
            GreenSlider.Width = Width;
            BlueSlider.Width = Width;

            RedSlider.onValueChanged += RedSlider_onValueChanged;
            GreenSlider.onValueChanged += GreenSlider_onValueChanged;
            BlueSlider.onValueChanged += BlueSlider_onValueChanged;

            Controls.Add(RedSlider);
            Controls.Add(GreenSlider);
            Controls.Add(BlueSlider);

            RedSlider.Location = new Point(0, (int)(Height * 0.61));
            GreenSlider.Location = new Point(0, RedSlider.Location.Y + RedSlider.Height + 5);
            BlueSlider.Location = new Point(0, GreenSlider.Location.Y + GreenSlider.Height + 5);

            objShadowPath = new GraphicsPath();
            objShadowPath.AddLine(0, (int)(Height * 0.6), Width, (int)(Height * 0.6));

            HoveredIndex = -1;
            initColorHints();

            DoubleBuffered = true;

            objAnimationManager = new AnimationManager()
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseInOut
            };
            objAnimationManager.OnAnimationProgress += sender => Invalidate();
            objAnimationManager.OnAnimationFinished += objAnimationManager_OnAnimationFinished;
        }

        void objAnimationManager_OnAnimationFinished(object sender)
        {
            Value = TempColor;
            if (onColorChanged != null) onColorChanged(pBaseColor);
        }

        void RedSlider_onValueChanged(int newValue)
        {
            if (!objAnimationManager.IsAnimating()) { 
            pBaseColor = Color.FromArgb(newValue, pBaseColor.G, pBaseColor.B);
                if(onColorChanged!= null) onColorChanged(pBaseColor);
            Invalidate();
            }
        }

        void GreenSlider_onValueChanged(int newValue)
        {
            if (!objAnimationManager.IsAnimating())
            {
                pBaseColor = Color.FromArgb(pBaseColor.R, newValue, pBaseColor.B);
                if(onColorChanged!= null) onColorChanged(pBaseColor);
                Invalidate();
            }
        }

        void BlueSlider_onValueChanged(int newValue)
        {
            if (!objAnimationManager.IsAnimating()) { 
            pBaseColor = Color.FromArgb(pBaseColor.R, pBaseColor.G, newValue);
                if(onColorChanged!= null) onColorChanged(pBaseColor);
            Invalidate();
            }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(SkinManager.GetCardsColor());
            g.FillRectangle(new SolidBrush(pBaseColor), 0, 0, Width, (int)(Height * 0.6));
            DrawHelper.drawShadow(g, objShadowPath, 20, SkinManager.GetCardsColor());

            foreach (ColorRect objColor in ColorRectangles)
            {
                g.FillRectangle(new SolidBrush(objColor.Color), objColor.Rect);

            }

            if (HoveredIndex >= 0)
            {
                DrawHelper.drawShadow(g, CurrentHoveredPath, 4, Color.Black);
                g.FillRectangle(new SolidBrush(ColorRectangles[HoveredIndex].Color), ColorRectangles[HoveredIndex].Rect);
            }

            if (objAnimationManager.IsAnimating())
            {
                RedSlider.Value =(int)(pBaseColor.R + (TempColor.R - pBaseColor.R) * objAnimationManager.GetProgress());
                GreenSlider.Value = (int)(pBaseColor.G + (TempColor.G - pBaseColor.G) * objAnimationManager.GetProgress());
                BlueSlider.Value = (int)(pBaseColor.B + (TempColor.B - pBaseColor.B) * objAnimationManager.GetProgress());
                Rectangle clip = new Rectangle(0, 0, Width, (int)(Height * 0.6));
                g.SetClip(clip);
                int xPos, yPos;
                xPos = (int)((clip.Width * 0.5) - (clip.Width * objAnimationManager.GetProgress()));
                yPos = (int)((clip.Height * 0.5) - (clip.Height * objAnimationManager.GetProgress()));
                g.FillEllipse(new SolidBrush(TempColor), new Rectangle(xPos, yPos, (int)(clip.Width * 2 * objAnimationManager.GetProgress()), (int)(clip.Height * 2 * objAnimationManager.GetProgress())));
                g.ResetClip();
            }

        }
    }

    class ColorRect
    {
        public Rectangle Rect;
        public Color Color;

        public ColorRect(Rectangle pRect, Color pColor)
        {
            Rect = pRect;
            Color = pColor;
        }
    }

    class MaterialColorSlider : Control, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        [Browsable(false)]

        private int _Value;
        public int Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                MouseX = SliderRectangle.X + ((int)((double)_Value / (double)(MaxValue - MinValue) * (double)(SliderRectangle.Width - IndicatorSize)));
                RecalcutlateIndicator();
            }
        }
        private int _MaxValue;
        public int MaxValue
        {
            get { return _MaxValue; }
            set
            {
                _MaxValue = value;
                MouseX = SliderRectangle.X + ((int)((double)_Value / (double)(MaxValue - MinValue) * (double)(SliderRectangle.Width - IndicatorSize)));
                RecalcutlateIndicator();
            }
        }
        private int _MinValue;
        public int MinValue
        {
            get { return _MinValue; }
            set
            {
                _MinValue = value;
                MouseX = SliderRectangle.X + ((int)((double)_Value / (double)(MaxValue - MinValue) * (double)(SliderRectangle.Width - IndicatorSize)));
                RecalcutlateIndicator();
            }
        }

        private bool MousePressed;
        private int MouseX;

        public static int IndicatorSize = 25;
        private bool hovered = false;

        private Rectangle IndicatorRectangle;
        private Rectangle IndicatorRectangleNormal;
        private Rectangle IndicatorRectanglePressed;
        private Rectangle IndicatorRectangleDisabled;
        private Rectangle DescriptioRectangle;
        private Rectangle ValueRectangle;
        private Rectangle SliderRectangle;
        private String Beschreibung;
        private Color AccentColor;
        private Brush AccentBrush;
        private int BorderDistance;

        public delegate void ValueChanged(int newValue);
        public event ValueChanged onValueChanged;

        public MaterialColorSlider(string pBeschreibung, Color pAccent, int pValue)
        {

            SetStyle(ControlStyles.Selectable, true);
            Beschreibung = pBeschreibung;
            AccentColor = pAccent;
            AccentBrush = new SolidBrush(AccentColor);
            MaxValue = 255;
            MinValue = 0;
            Height = IndicatorSize;
            BorderDistance = Width / 4;
            Value = pValue;

            ValueRectangle = new Rectangle((int)(Width * 0.8), 0, (int)(Width * 0.2), IndicatorSize);
            DescriptioRectangle = new Rectangle(0, 0, (int)(Width * 0.2), IndicatorSize);
            SliderRectangle = new Rectangle(DescriptioRectangle.Right, 0, (int)(Width * 0.6), IndicatorSize);

            IndicatorRectangle = new Rectangle(0, 0, IndicatorSize, IndicatorSize);
            IndicatorRectangleNormal = new Rectangle();
            IndicatorRectanglePressed = new Rectangle();

            EnabledChanged += MaterialSlider_EnabledChanged;

            DoubleBuffered = true;

        }

        void MaterialSlider_EnabledChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Height = IndicatorSize;
            ValueRectangle = new Rectangle((int)(Width * 0.8), 0, (int)(Width * 0.2), IndicatorSize);
            DescriptioRectangle = new Rectangle(0, 0, (int)(Width * 0.2), IndicatorSize);
            SliderRectangle = new Rectangle(DescriptioRectangle.Right, 0, (int)(Width * 0.6), IndicatorSize);
            MouseX = SliderRectangle.X + ((int)((double)_Value / (double)(MaxValue - MinValue) * (double)(SliderRectangle.Width - IndicatorSize)));
            BorderDistance = Width / 4;
            RecalcutlateIndicator();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            hovered = true;
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            hovered = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Y > IndicatorRectanglePressed.Top && e.Y < IndicatorRectanglePressed.Bottom)
            {
                MousePressed = true;
                if (e.X >= SliderRectangle.X + (IndicatorSize / 2) && e.X <= SliderRectangle.Right - IndicatorSize / 2)
                {
                    MouseX = e.X - IndicatorSize / 2;
                    double ValuePerPx = ((double)(MaxValue - MinValue)) / (SliderRectangle.Width - IndicatorSize);
                    int v = (int)(ValuePerPx * (MouseX - SliderRectangle.X));
                    if (v != _Value)
                    {
                        _Value = v;
                        if (onValueChanged != null) onValueChanged(_Value);
                    }
                    RecalcutlateIndicator();
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            hovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            hovered = false;
            
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MousePressed = false;
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (MousePressed)
            {
                if (e.X >= SliderRectangle.X + (IndicatorSize / 2) && e.X <= SliderRectangle.Right - IndicatorSize * 0.5)
                {
                    MouseX = e.X - IndicatorSize / 2;
                    double ValuePerPx = ((double)(MaxValue - MinValue)) / (SliderRectangle.Width - IndicatorSize);
                    int v = (int)(ValuePerPx * (MouseX - SliderRectangle.X));
                    if (v > MaxValue) v = MaxValue;
                    if (v != _Value)
                    {
                        _Value = v;
                        if (onValueChanged != null) onValueChanged(_Value);
                    }
                    RecalcutlateIndicator();
                }
            }
        }

        private void RecalcutlateIndicator()
        {
            int iWidht = Width - IndicatorSize;

            IndicatorRectangle = new Rectangle(MouseX, Height - IndicatorSize, IndicatorSize, IndicatorSize);
            IndicatorRectangleNormal = new Rectangle(IndicatorRectangle.X + (int)(IndicatorRectangle.Width * 0.25), IndicatorRectangle.Y + (int)(IndicatorRectangle.Height * 0.25), (int)(IndicatorRectangle.Width * 0.5), (int)(IndicatorRectangle.Height * 0.5));
            IndicatorRectanglePressed = new Rectangle(IndicatorRectangle.X + (int)(IndicatorRectangle.Width * 0.165), IndicatorRectangle.Y + (int)(IndicatorRectangle.Height * 0.165), (int)(IndicatorRectangle.Width * 0.66), (int)(IndicatorRectangle.Height * 0.66));
            IndicatorRectangleDisabled = new Rectangle(IndicatorRectangle.X + (int)(IndicatorRectangle.Width * 0.34), IndicatorRectangle.Y + (int)(IndicatorRectangle.Height * 0.34), (int)(IndicatorRectangle.Width * 0.33), (int)(IndicatorRectangle.Height * 0.33));
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.Clear(SkinManager.GetCardsColor());
            Color LineColor;
            Brush DisabledBrush;
            Color BalloonColor;
            Brush AccentBrush = new SolidBrush(AccentColor);
            Pen AccentPen = new Pen(AccentBrush, 2);

            if (SkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                LineColor = Color.FromArgb((int)(2.55 * 30), 255, 255, 255);
            }
            else
            {
                LineColor = Color.FromArgb((int)(2.55 * (hovered ? 38 : 26)), 0, 0, 0);
            }

            DisabledBrush = new SolidBrush(LineColor);
            BalloonColor = Color.FromArgb((int)(2.55 * 30), (Value == 0 ? Color.Gray : AccentColor));

            Pen LinePen = new Pen(LineColor, 2);


            g.DrawLine(LinePen, SliderRectangle.X + (IndicatorSize / 2), Height / 2, SliderRectangle.Right - (IndicatorSize / 2), Height / 2);


            if (Enabled)
            {
                g.DrawLine(AccentPen, IndicatorSize / 2 + SliderRectangle.X, Height / 2, IndicatorRectangleNormal.X, Height / 2);

                if (MousePressed)
                {
                    if (Value > MinValue)
                    {
                        g.FillEllipse(AccentBrush, IndicatorRectanglePressed);
                    }
                    else
                    {
                        g.FillEllipse(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), IndicatorRectanglePressed);
                        g.DrawEllipse(LinePen, IndicatorRectanglePressed);
                    }
                }
                else
                {
                    if (Value > MinValue)
                    {
                        g.FillEllipse(AccentBrush, IndicatorRectangleNormal);
                    }
                    else
                    {
                        g.FillEllipse(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), IndicatorRectangleNormal);
                        g.DrawEllipse(LinePen, IndicatorRectangleNormal);
                    }


                    if (hovered)
                    {
                        g.FillEllipse(new SolidBrush(BalloonColor), IndicatorRectangle);
                    }
                }
            }
            else
            {
                if (Value > MinValue)
                {
                    g.FillEllipse(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), IndicatorRectangleNormal);
                    g.FillEllipse(DisabledBrush, IndicatorRectangleDisabled);
                }
                else
                {
                    g.FillEllipse(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), IndicatorRectangleNormal);
                    g.DrawEllipse(LinePen, IndicatorRectangleDisabled);
                }
            }


            g.DrawString(Beschreibung, SkinManager.ROBOTO_MEDIUM_10, SkinManager.GetPrimaryTextBrush(), DescriptioRectangle, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            g.DrawString(Value.ToString(), SkinManager.ROBOTO_MEDIUM_10, SkinManager.GetPrimaryTextBrush(), ValueRectangle, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            e.Graphics.DrawImage((Image)bmp.Clone(), 0, 0);
        }
    }

}
