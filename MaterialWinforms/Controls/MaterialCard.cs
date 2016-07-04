using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace MaterialWinforms.Controls
{
    public  class MaterialCard : Panel, IMaterialControl
    {

        private string _Text;
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

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
            
            ParentChanged += new System.EventHandler(Redraw);
        }

        private void Redraw(object sender, System.EventArgs e)
        {
            Invalidate();
            if (Parent != null)
            {
                Parent.BackColorChanged += new System.EventHandler(Redraw);
                BackColor = SkinManager.GetCardsColor();
            }
           
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int ShadowDepth = 4;
            int iCropping = ClientRectangle.Width / 3;
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            
        

            g.Clear(Parent.BackColor);
            for (int i = 0; i < ShadowDepth; i++)
            {
                using (var backgroundPath = DrawHelper.CreateRoundRect(ClientRectangle.X + i,
                                    ClientRectangle.Y + i,
                                    ClientRectangle.Width  - i, ClientRectangle.Height  - i, 10))
                {
                    g.FillPath(new SolidBrush(Color.FromArgb((50 / ShadowDepth - 1) * i, Color.Black)), backgroundPath);
                }
            }

            this.Region = new Region(DrawHelper.CreateRoundRect(ClientRectangle.X + 3,
                                    ClientRectangle.Y + 3,
                                    ClientRectangle.Width - 3, ClientRectangle.Height - 3, 10));

            using (var backgroundPath = DrawHelper.CreateRoundRect(ClientRectangle.X + (ShadowDepth/2),
                    ClientRectangle.Y + (ShadowDepth / 2),
                    ClientRectangle.Width  - ShadowDepth, ClientRectangle.Height   - ShadowDepth, 10))
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
