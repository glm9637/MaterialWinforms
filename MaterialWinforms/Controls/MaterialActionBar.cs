using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialWinforms.Controls
{
    public partial class MaterialActionBar : Control, IShadowedMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public int Elevation { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        [Browsable(false)]
        public GraphicsPath ShadowBorder { get; set; }
        public delegate void SideDrawerButtonClicked();
        public event SideDrawerButtonClicked onSideDrawerButtonClicked;
        private Rectangle menuButtonBounds;
        private Rectangle drawerButtonBounds;

        private ButtonState buttonState;
        private int DrawerAnimationProgress;

        private enum ButtonState
        {
            DrawerOver,
            MenuOver,
            DrawerDown,
            MenuDown,
            None
        }

        public MaterialContextMenuStrip ActionBarMenu
        {
            get;
            set;
        }


        public const int ACTION_BAR_HEIGHT = 40;
        public MaterialActionBar()
        {
            Elevation = 10;
            Width = ACTION_BAR_HEIGHT;
            buttonState = ButtonState.None;
            InitializeComponent();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            Dock = DockStyle.Top;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (menuButtonBounds.Contains(e.Location))
            {
                if (buttonState != ButtonState.MenuOver)
                {
                    buttonState = ButtonState.MenuOver;
                    Invalidate();
                }
                return;
            }
            if (drawerButtonBounds.Contains(e.Location))
            {
                if (buttonState != ButtonState.DrawerOver)
                {
                    buttonState = ButtonState.DrawerOver;
                    Invalidate();
                }
                return;
            }
            else
            {
                if (buttonState != ButtonState.None)
                {
                    buttonState = ButtonState.None;
                    Invalidate();
                }
                return;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (buttonState != ButtonState.None)
            {
                buttonState = ButtonState.None;
                Invalidate();
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (menuButtonBounds.Contains(e.Location) && ActionBarMenu != null)
            {

                buttonState = ButtonState.MenuDown;
                ActionBarMenu.Show(PointToScreen(e.Location));
                Invalidate();
            }
            else if (drawerButtonBounds.Contains(e.Location) && onSideDrawerButtonClicked != null)
            {
                buttonState = ButtonState.DrawerDown;
                onSideDrawerButtonClicked();
                Invalidate();

            }
            base.OnMouseUp(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            drawerButtonBounds = new Rectangle(SkinManager.FORM_PADDING, 0, ACTION_BAR_HEIGHT, ACTION_BAR_HEIGHT);
            menuButtonBounds = new Rectangle(Width - SkinManager.FORM_PADDING - ACTION_BAR_HEIGHT, 0, ACTION_BAR_HEIGHT, ACTION_BAR_HEIGHT);
            ShadowBorder = new GraphicsPath();
            ShadowBorder.AddLine(new Point(Location.X, Location.Y + Height), new Point(Location.X + Width, Location.Y + Height));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(SkinManager.ColorScheme.PrimaryColor);

            var hoverBrush = SkinManager.GetFlatButtonHoverBackgroundBrush();
            var downBrush = SkinManager.GetFlatButtonPressedBackgroundBrush();

            if (buttonState == ButtonState.MenuOver && ActionBarMenu != null)
                g.FillEllipse(hoverBrush, menuButtonBounds);



            if (ActionBarMenu != null)
            {
                if (buttonState == ButtonState.MenuOver)
                {
                    g.FillEllipse(hoverBrush, menuButtonBounds);
                }
                using (var MenuButtonBrush = new SolidBrush(Color.White))
                {
                    int CircleRadius = 5;
                    g.FillEllipse(
                       MenuButtonBrush,
                       menuButtonBounds.X + (int)(menuButtonBounds.Width * 0.5) - CircleRadius / 2,
                       menuButtonBounds.Y + (int)(menuButtonBounds.Height * 0.3) - CircleRadius / 2,
                      CircleRadius, CircleRadius);
                    g.FillEllipse(
                       MenuButtonBrush,
                       menuButtonBounds.X + (int)(menuButtonBounds.Width * 0.5) - CircleRadius / 2,
                       menuButtonBounds.Y + (int)(menuButtonBounds.Height * 0.5) - CircleRadius / 2,
                      CircleRadius, CircleRadius);
                    g.FillEllipse(
                      MenuButtonBrush,
                      menuButtonBounds.X + (int)(menuButtonBounds.Width * 0.5) - CircleRadius / 2,
                      menuButtonBounds.Y + (int)(menuButtonBounds.Height * 0.7) - CircleRadius / 2,
                     CircleRadius, CircleRadius);
                }
            }
            MaterialForm objParent = (MaterialForm)Parent;
            if (objParent.SideDrawer != null)
            {

                if (buttonState == ButtonState.DrawerOver)
                {
                    g.FillEllipse(hoverBrush, drawerButtonBounds);
                }
                if (!objParent.SideDrawer.SideDrawerFixiert)
                {
                    using (var DrawerButtonPen = new Pen(SkinManager.ACTION_BAR_TEXT, 2))
                    {


                        g.DrawLine(
                           DrawerButtonPen,
                           drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.2 + (0.3 * DrawerAnimationProgress / 100))),
                           drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.35 + (0.3 * DrawerAnimationProgress / 100))),
                           drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.8 - (0.6 * DrawerAnimationProgress / 100))),
                           drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.35 + (0.15 * DrawerAnimationProgress / 100))));
                        g.DrawLine(
                           DrawerButtonPen,
                           drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.2 + (0.6 * DrawerAnimationProgress / 100))),
                           drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.65 - (0.3 * Math.Abs(DrawerAnimationProgress - 50) / 100))),
                           drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.8 - (0.6 * DrawerAnimationProgress / 100))),
                           drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.35 + (0.3 * Math.Abs(DrawerAnimationProgress - 50) / 100))));
                        g.DrawLine(
                          DrawerButtonPen,
                          drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.8 - (0.9 * Math.Abs(DrawerAnimationProgress - 66) / 100))),
                          drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.65 - (0.3 * DrawerAnimationProgress / 100))),
                          drawerButtonBounds.X + (int)(drawerButtonBounds.Width * (0.8 - (0.6 * DrawerAnimationProgress / 100))),
                          drawerButtonBounds.Y + (int)(drawerButtonBounds.Height * (0.36 + (0.45 * Math.Abs(DrawerAnimationProgress - 66) / 100))));
                    }
                }
            }
        }
        public void setDrawerAnimationProgress(int newProgress)
        {
            DrawerAnimationProgress = newProgress;
            Invalidate();
        }
    }
}

