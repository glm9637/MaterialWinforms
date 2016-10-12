using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Collections.ObjectModel;
using System;

using MaterialWinforms.Animations;

namespace MaterialWinforms.Controls
{
    public partial class MaterialBreadCrumbToolbar : Control, IMaterialControl
    {

        private ObservableCollection<BreadCrumbItem> _Teile;
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public Color BackColor { get { return SkinManager.GetCardsColor() ; } }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public  ObservableCollection<BreadCrumbItem> Items
        {
            get { return _Teile; }
        }

        public delegate void BreadCrumbItemClicked(String pTitel,Object pTag);
        public event BreadCrumbItemClicked onBreadCrumbItemClicked;

        private int ItemLengt;
        private String _Trennzeichen = "  >";
        private int HoveredItem = -1;
        private int SelectedItemIndex = -1;
        private Point animationSource;
        private bool mouseDown = false;
        private int offset = 0;
        private int TabOffset = 0;
        private int oldXLocation = -1;
        private int TabLength = 0;
        private readonly AnimationManager animationManager;

        public MaterialBreadCrumbToolbar()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Height = 1;
            Padding = new Padding(5, 5, 5, 5);
            _Teile = new ObservableCollection<BreadCrumbItem>();
            _Teile.CollectionChanged += RecalculateTabRects;
            ParentChanged += new System.EventHandler(Redraw);
            DoubleBuffered = true;
            animationManager = new AnimationManager
            {
                AnimationType = AnimationType.EaseOut,
                Increment = 0.03
            };
            animationManager.OnAnimationProgress += sender => Invalidate();

        }



        private void Redraw(object sender, System.EventArgs e)
        {
            Invalidate();
            if (Parent != null)
            {
                Parent.BackColorChanged += new System.EventHandler(Redraw);
            }

        }

        private void RecalculateTabRects(object sender, EventArgs e)
        {
            UpdateTabRects();
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseDown && _Teile.Count>0)
            {
                bool move = false;


                if (oldXLocation > 0)
                {

                    int off = offset;
                    off -= oldXLocation - e.X;
                    if (_Teile[0].ItemRect.X + off < 0)
                    {
                        if (_Teile[_Teile.Count - 1].ItemRect.Right + off > Width)
                        {
                            move = true;
                        }
                    }
                    else
                    {
                        if (_Teile[_Teile.Count - 1].ItemRect.Right + off < Width)
                        {
                            move = true;
                        }
                    }

                    if (move)
                    {
                        offset -= oldXLocation - e.X;
                        oldXLocation = e.X;
                        Invalidate();
                    }
                }
                else
                {
                    oldXLocation = e.X;
                    Invalidate();
                }


                return;
            }

            foreach (BreadCrumbItem objItem in _Teile)
            {
                if (objItem.ItemRect.Contains(e.Location))
                {
                    int newItem = _Teile.IndexOf(objItem);
                    if (HoveredItem != newItem)
                    {
                        HoveredItem = newItem;
                        Invalidate();
                    }
                    return;
                }
            }
            if (HoveredItem != -1)
            {
                HoveredItem = -1;
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                mouseDown = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (HoveredItem != -1)
            {
                HoveredItem = -1;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);


            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouseDown = false;
                oldXLocation = -1;
                bool ignoreClick = false;
                if (Math.Abs(offset) > 5)
                {
                    TabOffset += offset;
                    ignoreClick = true;
                }

                offset = 0;
                if (!ignoreClick && HoveredItem > -1)
                {
                    SelectedItemIndex = HoveredItem;
                    animationSource = e.Location;
                    UpdateTabRects();
                    animationManager.SetProgress(0);
                    animationManager.StartNewAnimation(AnimationDirection.In);
                    Invalidate();
                    if (onBreadCrumbItemClicked != null && SelectedItemIndex > -1)
                    {
                        onBreadCrumbItemClicked(_Teile[SelectedItemIndex].Text, _Teile[SelectedItemIndex].Tag);
                    }
                }
                UpdateTabRects();
                Invalidate();
            }

        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int iCropping = ClientRectangle.Width / 3;
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(Parent.BackColor);

            this.Region = new Region(DrawHelper.CreateRoundRect(ClientRectangle.X + 3,
                                    ClientRectangle.Y + 3,
                                    ClientRectangle.Width - 3, ClientRectangle.Height - 3, 10));

            using (var backgroundPath = DrawHelper.CreateRoundRect(ClientRectangle.X,
                    ClientRectangle.Y,
                    ClientRectangle.Width, ClientRectangle.Height, 3))
            {
                g.FillPath(SkinManager.getCardsBrush(), backgroundPath);

            }
            if (_Teile.Count > 0)
            {
                if (HoveredItem >= 0)
                {
                    g.FillRectangle(SkinManager.GetFlatButtonHoverBackgroundBrush(),
                        new Rectangle(_Teile[HoveredItem].ItemRect.X + offset, _Teile[HoveredItem].ItemRect.Y, _Teile[HoveredItem].ItemRect.Width, _Teile[HoveredItem].ItemRect.Height));
                }
                //Click feedback
                if (animationManager.IsAnimating())
                {
                    double animationProgress = animationManager.GetProgress();

                    var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationProgress * 50)), Color.White));
                    var rippleSize = (int)(animationProgress * _Teile[SelectedItemIndex].ItemRect.Width * 1.75);

                    g.SetClip(_Teile[SelectedItemIndex].ItemRect);
                    g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                    g.ResetClip();
                    rippleBrush.Dispose();
                }
                for (int i = 0; i < _Teile.Count; i++)
                {
                    g.DrawString(
                        _Teile[i].Text + (i == _Teile.Count - 1 ? "" : _Trennzeichen),
                        SkinManager.ROBOTO_MEDIUM_10,
                        SkinManager.GetPrimaryTextBrush(),
                        new Rectangle(_Teile[i].ItemRect.X + offset, _Teile[i].ItemRect.Y, _Teile[i].ItemRect.Width, _Teile[i].ItemRect.Height),
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
            }



        }

        private void UpdateTabRects()
        {
            ItemLengt = 0;
            if (_Teile.Count == 0) return;

            using (var b = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(b))
                {

                    _Teile[0].ItemRect = new Rectangle(10, Convert.ToInt32((Height - g.MeasureString("T", SkinManager.ROBOTO_MEDIUM_10).Height) / 2), (int)g.MeasureString(_Teile[0].Text + (0 == _Teile.Count - 1 ? "" : _Trennzeichen) + 5, SkinManager.ROBOTO_MEDIUM_10).Width + 2, Height);
                    ItemLengt += _Teile[0].ItemRect.Width;
                    for (int i = 1; i < _Teile.Count; i++)
                    {

                        _Teile[i].ItemRect = new Rectangle(_Teile[i - 1].ItemRect.Right, _Teile[i - 1].ItemRect.Y, (int)g.MeasureString(_Teile[i].Text + (i == _Teile.Count - 1 ? "" : _Trennzeichen) + 5, SkinManager.ROBOTO_MEDIUM_10).Width + 2, Height);
                        ItemLengt += _Teile[i].ItemRect.Width;
                    }

                    if (TabOffset != 0)
                    {
                        Rectangle CurrentTab = _Teile[0].ItemRect;
                        CurrentTab = new Rectangle(CurrentTab.X + TabOffset, CurrentTab.Y, CurrentTab.Width, CurrentTab.Height);

                        _Teile[0].ItemRect = CurrentTab;
                        for (int i = 1; i < _Teile.Count; i++)
                        {
                            CurrentTab = _Teile[i].ItemRect;
                            CurrentTab = new Rectangle(CurrentTab.X + TabOffset, CurrentTab.Y, CurrentTab.Width, CurrentTab.Height);
                            _Teile[i].ItemRect = CurrentTab;
                        }
                    }
                }
            }
            Invalidate();
        }
    }

    [Serializable]
    public class BreadCrumbItem
    {
        public string Text{get;set;}
        public Object Tag { get; set; }
        public Rectangle ItemRect;
        public BreadCrumbItem()
        {
            ItemRect = new Rectangle();
        }
        public BreadCrumbItem(string pText)
        {
            Text = pText;
            ItemRect = new Rectangle();
        }


    }
}
