using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace MaterialWinforms.Controls
{
    public class MaterialCard : Panel, IShadowedMaterialControl
    {

        private string _Text;
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public Color BackColor { get { return SkinManager.GetCardsColor(); } }

        public int Elevation { get; set; }
        [Browsable(false)]
        public GraphicsPath ShadowBorder { get; set; }

        private bool _LargeTitle;
        public bool LargeTitle
        {
            get
            {
                return _LargeTitle;
            }
            set
            {
                _LargeTitle = value;
            }
        }

        [Category("Appearance")]
        public string Title
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                Invalidate();
            }
        }

        public SizeF TitleSize { get { return CreateGraphics().MeasureString(_Text, LargeTitle ? new FontManager().Roboto_Medium15 : SkinManager.ROBOTO_MEDIUM_10); } }
        public MaterialCard()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Height = 1;
            Padding = new Padding(5, 25, 5, 5);
            Elevation = 5;
            SizeChanged += Redraw;
            LocationChanged += Redraw;
            DoubleBuffered = true;
            ParentChanged += new System.EventHandler(Redraw);
        }


        private void Redraw(object sender, System.EventArgs e)
        {
            ShadowBorder = new GraphicsPath();
            ShadowBorder = DrawHelper.CreateRoundRect(Location.X,
                                    Location.Y,
                                    ClientRectangle.Width, ClientRectangle.Height, 10);
            this.Region = new Region(DrawHelper.CreateRoundRect(ClientRectangle.X,
                                    ClientRectangle.Y,
                                    ClientRectangle.Width, ClientRectangle.Height, 10));
            Invalidate();


        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int iCropping = ClientRectangle.Width / 3;
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(SkinManager.GetCardsColor());

            if (!string.IsNullOrWhiteSpace(_Text))
            {
                g.DrawString(
               _Text,
               LargeTitle?new FontManager().Roboto_Medium15: SkinManager.ROBOTO_MEDIUM_10,
               SkinManager.ColorScheme.PrimaryBrush,
               new Rectangle(ClientRectangle.X + 10, ClientRectangle.Y + 10, ClientRectangle.Width, (int)TitleSize.Height),
               new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
            }
            g.ResetClip();
        }
    }
}
