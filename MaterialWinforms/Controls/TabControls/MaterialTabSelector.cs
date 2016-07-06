using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using MaterialWinforms.Animations;
using System.Drawing.Drawing2D;

namespace MaterialWinforms.Controls
{
    public class MaterialTabSelector : Control, IShadowedMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        [Browsable(false)]
        public GraphicsPath ShadowBorder{ get; set; }

        private int _Elevation;
        public int Elevation
        {
            get { return _Elevation; }
            set {
                _Elevation = value;
                Margin = new Padding(0, 0, 0, value);
            }
        }

        public bool CenterTabs { get; set; }

		private MaterialTabControl baseTabControl;
        public MaterialTabControl BaseTabControl
        {
            get { return baseTabControl; }
            set
            {
                baseTabControl = value;
                if (baseTabControl == null) return;
                previousSelectedTabIndex = baseTabControl.SelectedIndex;
                baseTabControl.Deselected += (sender, args) =>
                {
                    previousSelectedTabIndex = baseTabControl.SelectedIndex;
                };
                baseTabControl.SelectedIndexChanged += (sender, args) =>
                {
                    animationManager.SetProgress(0);
                    animationManager.StartNewAnimation(AnimationDirection.In);
                };
                baseTabControl.ControlAdded += delegate
                {
                    Invalidate();
                };
                baseTabControl.ControlRemoved += delegate
                {
                    Invalidate();
                };
            }
        }

        private int previousSelectedTabIndex;
        private Point animationSource;
        private readonly AnimationManager animationManager;

        private List<Rectangle> tabRects;
        private const int TAB_HEADER_PADDING = 24;
        private const int TAB_INDICATOR_HEIGHT = 2;

        public MaterialTabSelector()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            Height = 48;

            animationManager = new AnimationManager
            {
                AnimationType = AnimationType.EaseOut,
                Increment = 0.04
            };
            animationManager.OnAnimationProgress += sender => Invalidate();
            ShadowBorder = new GraphicsPath();
            Elevation = 10;
            ShadowBorder.AddLine(new Point(Location.X, Location.Y + Height), new Point(Location.X + Width, Location.Y + Height));
        }


        protected override void OnLocationChanged(System.EventArgs e)
        {
            base.OnLocationChanged(e);
            ShadowBorder = new GraphicsPath();
            ShadowBorder.AddLine(new Point(Location.X, Location.Y + Height), new Point(Location.X + Width, Location.Y + Height));
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            ShadowBorder = new GraphicsPath();
            ShadowBorder.AddLine(new Point(Location.X, Location.Y + Height), new Point(Location.X + Width, Location.Y + Height));
        }
     

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

			g.Clear(SkinManager.ColorScheme.PrimaryColor);

            if (baseTabControl == null) return;

            if (!animationManager.IsAnimating() || tabRects == null ||  tabRects.Count != baseTabControl.TabCount)
                UpdateTabRects();

            double animationProgress = animationManager.GetProgress();

            //Click feedback
            if (animationManager.IsAnimating())
            {
                var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationProgress * 50)), Color.White));
                var rippleSize = (int)(animationProgress * tabRects[baseTabControl.SelectedIndex].Width * 1.75);

                g.SetClip(tabRects[baseTabControl.SelectedIndex]);
                g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                g.ResetClip();
                rippleBrush.Dispose();
            }

            //Draw tab headers
            foreach (TabPage tabPage in baseTabControl.TabPages)
            {
                int currentTabIndex = baseTabControl.TabPages.IndexOf(tabPage);
				Brush textBrush = new SolidBrush(Color.FromArgb(CalculateTextAlpha(currentTabIndex, animationProgress), SkinManager.ColorScheme.TextColor));

                g.DrawString(
                    tabPage.Text.ToUpper(), 
                    SkinManager.ROBOTO_MEDIUM_10, 
                    textBrush, 
                    tabRects[currentTabIndex], 
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                textBrush.Dispose();
            }

            //Animate tab indicator
            int previousSelectedTabIndexIfHasOne = previousSelectedTabIndex == -1 ? baseTabControl.SelectedIndex : previousSelectedTabIndex;
            Rectangle previousActiveTabRect = tabRects[previousSelectedTabIndexIfHasOne];
            Rectangle activeTabPageRect = tabRects[baseTabControl.SelectedIndex];

            int y = activeTabPageRect.Bottom - 2;
            int x = previousActiveTabRect.X + (int)((activeTabPageRect.X - previousActiveTabRect.X) * animationProgress);
            int width = previousActiveTabRect.Width + (int)((activeTabPageRect.Width - previousActiveTabRect.Width) * animationProgress);

			g.FillRectangle(SkinManager.ColorScheme.AccentBrush, x, y, width, TAB_INDICATOR_HEIGHT);
        }

        private int CalculateTextAlpha(int tabIndex, double animationProgress)
        {
            int primaryA = SkinManager.ACTION_BAR_TEXT.A;
            int secondaryA = SkinManager.ACTION_BAR_TEXT_SECONDARY.A;

            if (tabIndex == baseTabControl.SelectedIndex && !animationManager.IsAnimating())
            {
                return primaryA;
            }
            if (tabIndex != previousSelectedTabIndex && tabIndex != baseTabControl.SelectedIndex)
            {
                return secondaryA;
            }
            if (tabIndex == previousSelectedTabIndex)
            {
                return primaryA - (int)((primaryA - secondaryA) * animationProgress);
            }
            return secondaryA + (int)((primaryA - secondaryA) * animationProgress);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (tabRects == null) UpdateTabRects();
            for (int i = 0; i < tabRects.Count; i++)
            {
                if (tabRects[i].Contains(e.Location))
                {
                    baseTabControl.SelectedIndex = i;
                }
            }

            animationSource = e.Location;
        }

        private void UpdateTabRects()
        {
            tabRects = new List<Rectangle>();

            //If there isn't a base tab control, the rects shouldn't be calculated
            //If there aren't tab pages in the base tab control, the list should just be empty which has been set already; exit the void
            if (baseTabControl == null || baseTabControl.TabCount == 0) return;

            //Calculate the bounds of each tab header specified in the base tab control
            using (var b = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(b))
                {
                    tabRects.Add(new Rectangle(SkinManager.FORM_PADDING, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(baseTabControl.TabPages[0].Text, SkinManager.ROBOTO_MEDIUM_10).Width, Height));
                    for (int i = 1; i < baseTabControl.TabPages.Count; i++)
                    {
                        tabRects.Add(new Rectangle(tabRects[i - 1].Right, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(baseTabControl.TabPages[i].Text, SkinManager.ROBOTO_MEDIUM_10).Width, Height));
                        
                    }

                    if (CenterTabs)
                    {
                        int FreeSpace =Width-( tabRects[tabRects.Count - 1].Right-SkinManager.FORM_PADDING);
                        FreeSpace = FreeSpace / 2;
                        for (int i = 0; i < baseTabControl.TabPages.Count; i++)
                        {
                            Rectangle Centered = tabRects[i];
                            Centered.X += FreeSpace;
                            tabRects[i] = Centered;
                        }
                    }
                }
            }
        }
    }
}
