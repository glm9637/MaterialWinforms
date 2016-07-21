using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace MaterialWinforms.Controls
{
    public partial class MaterialDatePicker : Control, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private RectangleF TopDayRect;
        private RectangleF TopDateRect;
        private RectangleF MonthRect;
        private RectangleF DayRect;
        private RectangleF YearRect;

        private RectangleF CurrentCal_Header;

        private Font TopDayFont, MonthFont, DayFont, YeahrFont;

        private RectangleF CurrentCal;
        private RectangleF PreviousCal;
        private RectangleF NextCal;
        private GraphicsPath ShadowPath;
        private DateTime CurrentDate;
        private List<List<Rectangle>> DateRectangles;
        private int DefaultSize;

        public MaterialDatePicker()
        {
            InitializeComponent();
            Width = 250;
            Height = 425;
            TopDayRect = new RectangleF(0f, 0f, Width, 20f);
            TopDateRect = new RectangleF(0f, TopDayRect.Bottom, Width, (float)(Height * 0.3));
            MonthRect = new RectangleF(0f, TopDayRect.Bottom, Width, (float)(TopDateRect.Height * 0.3));
            DayRect = new RectangleF(0f, MonthRect.Bottom, Width, (float)(TopDateRect.Height * 0.4));
            YearRect = new RectangleF(0f, DayRect.Bottom, Width, (float)(TopDateRect.Height * 0.3));
            CurrentCal = new RectangleF(0f, TopDateRect.Bottom, Width, (float)(Height * 0.75));
            CurrentCal_Header = new RectangleF(0f,TopDateRect.Bottom+3,Width,(float)(CurrentCal.Height*0.1));
            PreviousCal = new RectangleF(0f, CurrentCal_Header.Y, CurrentCal_Header.Height, CurrentCal_Header.Height);
            NextCal = new RectangleF(Width - CurrentCal_Header.Height, CurrentCal_Header.Y, CurrentCal_Header.Height, CurrentCal_Header.Height);
            ShadowPath = new GraphicsPath();
            ShadowPath.AddLine(-5, TopDateRect.Bottom, Width, TopDateRect.Bottom);
            TopDayFont = SkinManager.ROBOTO_MEDIUM_10;
            MonthFont = new Font(SkinManager.ROBOTO_MEDIUM_10.Name, 16, FontStyle.Bold);
            DayFont = new Font(SkinManager.ROBOTO_MEDIUM_10.Name, 40, FontStyle.Bold);
            YeahrFont = new Font(SkinManager.ROBOTO_MEDIUM_10.Name, 15, FontStyle.Bold);
   
            DefaultSize = (Width - 10) / 7;
            CurrentDate = DateTime.Now;
            CalculateRectangles();

        }

        private void CalculateRectangles()
        {
            DateRectangles = new List<List<Rectangle>>();
            for (int i = 0; i < 7; i++)
            {
                DateRectangles.Add(new List<Rectangle>());
                for (int j = 0; j < 7; j++)
                {
                    DateRectangles[i].Add(new Rectangle(5 + (j * DefaultSize),(int)(CurrentCal_Header.Bottom + (i * DefaultSize)), DefaultSize, DefaultSize));
                }
            }

        }


        protected override void OnResize(EventArgs e)
        {
            Width = 250;
            Height = 425;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.Clear(SkinManager.GetCardsColor());

            DrawHelper.drawShadow(g, ShadowPath, 8, SkinManager.GetCardsColor());

            g.FillRectangle(SkinManager.ColorScheme.DarkPrimaryBrush, TopDayRect);
            g.FillRectangle(SkinManager.ColorScheme.PrimaryBrush, TopDateRect);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
             

            g.DrawString(CurrentDate.ToString("dddd"), TopDayFont, SkinManager.ACTION_BAR_TEXT_BRUSH, TopDayRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            g.DrawString(CurrentDate.ToString("MMMM"), MonthFont, SkinManager.ACTION_BAR_TEXT_BRUSH, MonthRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far });
            g.DrawString(CurrentDate.ToString("dd"), DayFont, SkinManager.ACTION_BAR_TEXT_BRUSH, DayRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            g.DrawString(CurrentDate.ToString("yyyy"), YeahrFont,new SolidBrush(Color.FromArgb(80,SkinManager.ACTION_BAR_TEXT)), YearRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            g.DrawString(CurrentDate.ToString("MMMM"), SkinManager.ROBOTO_REGULAR_11, SkinManager.ColorScheme.TextBrush, CurrentCal_Header, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            g.FillEllipse(SkinManager.ColorScheme.PrimaryBrush, PreviousCal);

            using (var ButtonPen = new Pen(SkinManager.ColorScheme.TextBrush,2)){

                g.DrawLine(ButtonPen,
                        (int)( PreviousCal.X + PreviousCal.Width * 0.6),
                        (int)(PreviousCal.Y + PreviousCal.Height * 0.4),
                        (int)(PreviousCal.X + PreviousCal.Width * 0.4),
                        (int)(PreviousCal.Y + PreviousCal.Height * 0.5));

                g.DrawLine(ButtonPen,
                        (int)(PreviousCal.X + PreviousCal.Width * 0.6),
                        (int)(PreviousCal.Y + PreviousCal.Height * 0.6),
                        (int)(PreviousCal.X + PreviousCal.Width * 0.4),
                        (int)(PreviousCal.Y + PreviousCal.Height * 0.5));

                g.DrawLine(ButtonPen,
                       (int)(NextCal.X + NextCal.Width * 0.4),
                       (int)(NextCal.Y + NextCal.Height * 0.4),
                       (int)(NextCal.X + NextCal.Width * 0.6),
                       (int)(NextCal.Y + NextCal.Height * 0.5));

                g.DrawLine(ButtonPen,
                        (int)(NextCal.X + NextCal.Width * 0.4),
                        (int)(NextCal.Y + NextCal.Height * 0.6),
                        (int)(NextCal.X + NextCal.Width * 0.6),
                        (int)(NextCal.Y + NextCal.Height * 0.5));
            }

            for (DateTime date = FirstDayOfMonth(CurrentDate); date <= LastDayOfMonth(CurrentDate); date = date.AddDays(1))
            {

            }

        }

        public DateTime FirstDayOfMonth(DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, DateTime.DaysInMonth(value.Year,value.Day));
        }

       
    }
}
