using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace MaterialWinforms.Controls
{
    public  class MaterialCard : Panel, IShadowedMaterialControl
    {

        private string _Text;
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public int Elevation { get; set; }
          [Browsable(false)]
        public GraphicsPath ShadowBorder { get; set; }

        [Category("Appearance")]
        public string Title
        {
            get { return _Text;
            Invalidate();
            }
            set
            {
                _Text = value;
            }
        }


        public MaterialCard()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Height = 1;
            BackColor = SkinManager.GetCardsColor();
            Padding = new Padding(5, 25, 5, 5);
            Elevation = 5;
            SizeChanged += Redraw;
            LocationChanged += Redraw;
            ParentChanged += new System.EventHandler(Redraw);
        }


        private void Redraw(object sender, System.EventArgs e)
        {
            ShadowBorder = new GraphicsPath();
            ShadowBorder = DrawHelper.CreateRoundRect(Location.X,
                                    Location.Y,
                                    ClientRectangle.Width, ClientRectangle.Height, 10);
            this.Region = new Region(DrawHelper.CreateRoundRect(ClientRectangle.X,
                                    ClientRectangle.Y ,
                                    ClientRectangle.Width, ClientRectangle.Height, 10));
            Invalidate();
            if (Parent != null)
            {
                Parent.BackColorChanged += new System.EventHandler(Redraw);
                BackColor = SkinManager.GetCardsColor();
                Parent.Invalidate();
            }
           
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int iCropping = ClientRectangle.Width / 3;
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            
        

            g.Clear(Parent.BackColor);
          

            using (var backgroundPath = DrawHelper.CreateRoundRect(ClientRectangle.X ,
                    ClientRectangle.Y,
                    ClientRectangle.Width  , ClientRectangle.Height  , 10))
            {
                g.FillPath(SkinManager.getCardsBrush(), backgroundPath);
               
            }
            if (!string.IsNullOrWhiteSpace(_Text))
            {
                g.DrawString(
               _Text,
               SkinManager.ROBOTO_MEDIUM_10,
               SkinManager.ColorScheme.PrimaryBrush,
               new Rectangle(ClientRectangle.X+10,ClientRectangle.Y+10,ClientRectangle.Width,ClientRectangle.Height),
               new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
            }
        }
    }
}
