using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Collections.Generic;
using System;

namespace MaterialWinforms.Controls
{
    public partial class MaterialBreadCrumbToolbar : Control, IMaterialControl
    {

        private List<BreadCrumbItem> _Teile;
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        [Category("Appearance")]
        public List<BreadCrumbItem> Items
        {
            get { return _Teile; }
            set
            {
                _Teile = value;
            }
        }
        

        public MaterialBreadCrumbToolbar()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Height = 1;
            BackColor = SkinManager.GetCardsColor();
            Padding = new Padding(5, 5, 5, 5);
            _Teile = new List<BreadCrumbItem>();
            
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
                    ClientRectangle.Width  - ShadowDepth, ClientRectangle.Height   - ShadowDepth, 3))
            {
                g.FillPath(SkinManager.getCardsBrush(), backgroundPath);
               
            }
            if (_Teile.Count > 0)
            {
                Point pPosition = new Point(10, Convert.ToInt32((Height- g.MeasureString("T", SkinManager.ROBOTO_MEDIUM_10).Height)/2));
                for (int i = 0; i < _Teile.Count; i++)
                {
                    if(!string.IsNullOrWhiteSpace(_Teile[i].Text)) { 
                    g.DrawString(
                        _Teile[i].Text + "  >  ",
                        SkinManager.ROBOTO_MEDIUM_10,
                        SkinManager.GetPrimaryTextBrush(), pPosition);
                    pPosition.X += Convert.ToInt32(g.MeasureString(_Teile[i].Text+"  >  ", SkinManager.ROBOTO_MEDIUM_10).Width);
                }
                }
            }
        }
    }

    [Serializable]
    public class BreadCrumbItem
    {
        public string Text;
        public BreadCrumbItem()
        {

        }
        public BreadCrumbItem(string pText)
        {
            Text = pText;
        }

        
    }
}
