using System;
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
        public GraphicsPath ShadowBorder { get; set; }

        private int HoveredXButtonIndex = -1;
        public MaterialContextMenuStrip RightClickMenu { get; set; }

        private int _MaxTabWidth;
        public int MaxTabWidht
        {
            get { return _MaxTabWidth; }
            set { _MaxTabWidth = value; Invalidate(); }
        }

        private int _Elevation;
        public int Elevation
        {
            get { return _Elevation; }
            set
            {
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

        private List<TabRectangle> tabRects;
        private const int TAB_HEADER_PADDING = 24;
        private const int TAB_INDICATOR_HEIGHT = 2;
        private bool mouseDown = false;
        private int offset = 0;
        private int TabOffset = 0;
        private int oldXLocation = -1;
        private int TabLength = 0;

        struct TabRectangle
        {
            public Rectangle TabRect;
            public Rectangle XButtonRect;
        }

        public MaterialTabSelector()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            Height = 35;
            _MaxTabWidth = -1;
            animationManager = new AnimationManager
            {
                AnimationType = AnimationType.EaseOut,
                Increment = 0.04
            };
            animationManager.OnAnimationProgress += sender => Invalidate();
            ShadowBorder = new GraphicsPath();
            Elevation = 10;
            ShadowBorder.AddLine(new Point(Location.X, Location.Y + Height), new Point(Location.X + Width, Location.Y + Height));
            SetupRightClickMenu();
        }

        private void SetupRightClickMenu()
        {
            RightClickMenu = new MaterialContextMenuStrip();
            ToolStripMenuItem CloseAllTabs = new ToolStripMenuItem();
            ToolStripMenuItem TabPositionZurruecksetzten = new ToolStripMenuItem();
            ToolStripMenuItem CloseAllExeptCurrent = new ToolStripMenuItem();

            CloseAllTabs.Text = "Alle Tabs schließen";
            CloseAllTabs.Click += CloseAllTabs_Click;
            RightClickMenu.Items.Add(CloseAllTabs);

            CloseAllExeptCurrent.Text = "Alle Anderen Tabs Schließen";
            CloseAllExeptCurrent.Click += CloseAllExeptCurrent_Click;
            RightClickMenu.Items.Add(CloseAllExeptCurrent);

            TabPositionZurruecksetzten.Text = "Tab Positionen Zurrücksetzen";
            TabPositionZurruecksetzten.Click += TabPositionZurruecksetzten_Click;
            RightClickMenu.Items.Add(TabPositionZurruecksetzten);
        }


        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            //  SetupRightClickMenu();
        }

        void CloseAllExeptCurrent_Click(object sender, EventArgs e)
        {
            for (int i = baseTabControl.TabPages.Count - 1; i >= 0; i--)
            {
                if (i != baseTabControl.SelectedIndex)
                {
                    if (((MaterialTabPage)BaseTabControl.TabPages[i]).Closable)
                        baseTabControl.TabPages.RemoveAt(i);
                }
            }
            previousSelectedTabIndex = -1;
            TabOffset = 0;
            offset = 0;
            UpdateTabRects();
            Invalidate();
        }

        void TabPositionZurruecksetzten_Click(object sender, EventArgs e)
        {
            TabOffset = 0;
            offset = 0;
            UpdateTabRects();
            Invalidate();
        }

        void CloseAllTabs_Click(object sender, EventArgs e)
        {
            TabOffset = 0;
            offset = 0;
            for (int i = baseTabControl.TabPages.Count - 1; i >= 0; i--)
            {
                if (((MaterialTabPage)BaseTabControl.TabPages[i]).Closable)
                    baseTabControl.TabPages.RemoveAt(i);
            }
            UpdateTabRects();
            Invalidate();
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
            Height = 35;
            ShadowBorder = new GraphicsPath();
            ShadowBorder.AddLine(new Point(Location.X, Location.Y + Height), new Point(Location.X + Width, Location.Y + Height));
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(SkinManager.ColorScheme.PrimaryColor);

            if (baseTabControl == null) return;

            if (!animationManager.IsAnimating() || tabRects == null || tabRects.Count != baseTabControl.TabCount)
                UpdateTabRects();

            if (baseTabControl.TabPages.Count == 0)
            {
                baseTabControl.Visible = false;
                return;
            }
            else
            {
                baseTabControl.Visible = true;
            }

            double animationProgress = animationManager.GetProgress();

            //Click feedback
            if (animationManager.IsAnimating())
            {
                var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationProgress * 50)), Color.White));
                var rippleSize = (int)(animationProgress * tabRects[baseTabControl.SelectedIndex].TabRect.Width * 1.75);

                g.SetClip(tabRects[baseTabControl.SelectedIndex].TabRect);
                g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                g.ResetClip();
                rippleBrush.Dispose();
            }
            //Draw tab headers
            foreach (TabPage tabPage in baseTabControl.TabPages)
            {
                int currentTabIndex = baseTabControl.TabPages.IndexOf(tabPage);
                Brush textBrush = new SolidBrush(Color.FromArgb(CalculateTextAlpha(currentTabIndex, animationProgress), SkinManager.ColorScheme.TextColor));
                var hoverBrush = SkinManager.GetFlatButtonHoverBackgroundBrush();
                Pen closePen = new Pen(textBrush, 2);

                if (currentTabIndex == HoveredXButtonIndex)
                {
                    g.FillEllipse(hoverBrush, tabRects[currentTabIndex].XButtonRect);
                }

                g.DrawString(
                    tabPage.Text.ToUpper(),
                    SkinManager.ROBOTO_MEDIUM_10,
                    textBrush,
                    new Rectangle(tabRects[currentTabIndex].TabRect.X + offset, tabRects[currentTabIndex].TabRect.Y, tabRects[currentTabIndex].TabRect.Width - tabRects[currentTabIndex].XButtonRect.Width, tabRects[currentTabIndex].TabRect.Height),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                if (((MaterialTabPage)BaseTabControl.TabPages[currentTabIndex]).Closable)
                {
                    g.DrawLine(
                       closePen,
                       tabRects[currentTabIndex].XButtonRect.X + (int)(tabRects[currentTabIndex].XButtonRect.Width * 0.33) + offset,
                       tabRects[currentTabIndex].XButtonRect.Y + (int)(tabRects[currentTabIndex].XButtonRect.Height * 0.33),
                       tabRects[currentTabIndex].XButtonRect.X + (int)(tabRects[currentTabIndex].XButtonRect.Width * 0.66) + offset,
                       tabRects[currentTabIndex].XButtonRect.Y + (int)(tabRects[currentTabIndex].XButtonRect.Height * 0.66)
                    );

                    g.DrawLine(
                        closePen,
                        tabRects[currentTabIndex].XButtonRect.X + (int)(tabRects[currentTabIndex].XButtonRect.Width * 0.66) + offset,
                        tabRects[currentTabIndex].XButtonRect.Y + (int)(tabRects[currentTabIndex].XButtonRect.Height * 0.33),
                        tabRects[currentTabIndex].XButtonRect.X + (int)(tabRects[currentTabIndex].XButtonRect.Width * 0.33) + offset,
                        tabRects[currentTabIndex].XButtonRect.Y + (int)(tabRects[currentTabIndex].XButtonRect.Height * 0.66));
                    textBrush.Dispose();

                }
            }

            if (tabRects.Count >= baseTabControl.SelectedIndex)
            {
                try
                {
                    //Animate tab indicator
                    int previousSelectedTabIndexIfHasOne = previousSelectedTabIndex == -1 ? baseTabControl.SelectedIndex : previousSelectedTabIndex;
                    Rectangle previousActiveTabRect = tabRects[previousSelectedTabIndexIfHasOne].TabRect;
                    Rectangle activeTabPageRect = tabRects[baseTabControl.SelectedIndex].TabRect;

                    int y = activeTabPageRect.Bottom - 2;
                    int x = previousActiveTabRect.X + (int)((activeTabPageRect.X - previousActiveTabRect.X) * animationProgress) + offset;
                    int width = previousActiveTabRect.Width + (int)((activeTabPageRect.Width - previousActiveTabRect.Width) * animationProgress);

                    g.FillRectangle(SkinManager.ColorScheme.AccentBrush, x, y, width, TAB_INDICATOR_HEIGHT);
                }
                catch (Exception)
                {

                    //Todo: Protokollierung
                }

            }
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseDown)
            {
                bool move = false;


                if (oldXLocation > 0)
                {

                    int off = offset;
                    off -= oldXLocation - e.X;
                    if (tabRects[0].TabRect.X + off < 0)
                    {
                        if (tabRects[tabRects.Count - 1].TabRect.Right + off > Width)
                        {
                            move = true;
                        }
                    }
                    else
                    {
                        if (tabRects[tabRects.Count - 1].TabRect.Right + off < Width)
                        {
                            move = true;
                        }
                    }

                    if (move)
                    {
                        offset -= oldXLocation - e.X;
                        oldXLocation = e.X;
                        Refresh();
                    }
                }
                else
                {
                    oldXLocation = e.X;
                    Refresh();
                }


                return;
            }
            for (int i = 0; i < baseTabControl.TabCount; i++)
            {
                if (((MaterialTabPage)BaseTabControl.TabPages[i]).Closable)
                {
                    if (tabRects[i].XButtonRect.Contains(e.Location))
                    {
                        HoveredXButtonIndex = i;
                        Refresh();
                        return;
                    }
                }
            }
            HoveredXButtonIndex = -1;
            return;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                mouseDown = true;
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
                if (tabRects == null) UpdateTabRects();

                if (!ignoreClick)
                {

                    for (int i = 0; i < tabRects.Count; i++)
                    {
                        if (tabRects[i].XButtonRect.Contains(e.Location))
                        {
                            baseTabControl.TabPages.RemoveAt(i);
                            previousSelectedTabIndex = -1;
                            UpdateTabRects();
                            return;
                        }
                        else if (tabRects[i].TabRect.Contains(e.Location))
                        {

                            baseTabControl.SelectedIndex = i;
                        }

                    }
                }
                animationSource = e.Location;
                UpdateTabRects();
                Invalidate();
            }
            else
            {
                RightClickMenu.Show(PointToScreen(e.Location));
            }
        }

        private void UpdateTabRects()
        {
            tabRects = new List<TabRectangle>();
            TabLength = 0;
            //If there isn't a base tab control, the rects shouldn't be calculated
            //If there aren't tab pages in the base tab control, the list should just be empty which has been set already; exit the void
            if (baseTabControl == null || baseTabControl.TabCount == 0) return;

            //Calculate the bounds of each tab header specified in the base tab control
            using (var b = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(b))
                {
                    int xButtonSize = ((MaterialTabPage)BaseTabControl.TabPages[0]).Closable ? 18 : 0;
                    TabRectangle CurrentTab = new TabRectangle();
                    CurrentTab.TabRect = new Rectangle(SkinManager.FORM_PADDING, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(baseTabControl.TabPages[0].Text, SkinManager.ROBOTO_MEDIUM_10).Width + 22, Height);
                    if (MaxTabWidht > 0 && CurrentTab.TabRect.Width > MaxTabWidht)
                    {
                        CurrentTab.TabRect.Width = MaxTabWidht;
                    }
                    CurrentTab.XButtonRect = new Rectangle(CurrentTab.TabRect.X + CurrentTab.TabRect.Width - 20, CurrentTab.TabRect.Y + ((CurrentTab.TabRect.Height - 18) / 2), xButtonSize, xButtonSize);
                    TabLength += CurrentTab.TabRect.Width;
                    tabRects.Add(CurrentTab);
                    for (int i = 1; i < baseTabControl.TabPages.Count; i++)
                    {
                        xButtonSize = ((MaterialTabPage)BaseTabControl.TabPages[i]).Closable ? 18 : 0;
                        CurrentTab = new TabRectangle();
                        CurrentTab.TabRect = new Rectangle(tabRects[i - 1].TabRect.Right, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(baseTabControl.TabPages[i].Text, SkinManager.ROBOTO_MEDIUM_10).Width + 22, Height);
                        if (MaxTabWidht > 0 && CurrentTab.TabRect.Width > MaxTabWidht)
                        {
                            CurrentTab.TabRect.Width = MaxTabWidht;
                        }
                        CurrentTab.XButtonRect = new Rectangle(CurrentTab.TabRect.X + CurrentTab.TabRect.Width - 20, CurrentTab.TabRect.Y + ((CurrentTab.TabRect.Height - 18) / 2), xButtonSize, xButtonSize);
                        TabLength += CurrentTab.TabRect.Width;
                        tabRects.Add(CurrentTab);
                    }

                    if (CenterTabs)
                    {
                        int FreeSpace = Width - (tabRects[tabRects.Count - 1].TabRect.Right - SkinManager.FORM_PADDING);
                        FreeSpace = FreeSpace / 2;
                        for (int i = 0; i < baseTabControl.TabPages.Count; i++)
                        {
                            TabRectangle Centered = tabRects[i];
                            Centered.TabRect.X += FreeSpace;
                            Centered.XButtonRect.X += FreeSpace;
                            tabRects[i] = Centered;
                        }
                    }
                    if (TabOffset != 0)
                    {
                        CurrentTab = tabRects[0];
                        CurrentTab.TabRect = new Rectangle(CurrentTab.TabRect.X + TabOffset, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(baseTabControl.TabPages[0].Text, SkinManager.ROBOTO_MEDIUM_10).Width + 22, Height);
                        if (MaxTabWidht > 0 && CurrentTab.TabRect.Width > MaxTabWidht)
                        {
                            CurrentTab.TabRect.Width = MaxTabWidht;
                        }
                        CurrentTab.XButtonRect = new Rectangle(CurrentTab.TabRect.X + CurrentTab.TabRect.Width - 20, CurrentTab.TabRect.Y + ((CurrentTab.TabRect.Height - 18) / 2), CurrentTab.XButtonRect.Width, CurrentTab.XButtonRect.Height);
                        tabRects[0] = CurrentTab;
                        for (int i = 1; i < baseTabControl.TabPages.Count; i++)
                        {
                            CurrentTab = tabRects[i];
                            CurrentTab.TabRect = new Rectangle(tabRects[i - 1].TabRect.Right, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(baseTabControl.TabPages[i].Text, SkinManager.ROBOTO_MEDIUM_10).Width + 22, Height);
                            if (MaxTabWidht > 0 && CurrentTab.TabRect.Width > MaxTabWidht)
                            {
                                CurrentTab.TabRect.Width = MaxTabWidht;
                            }
                            CurrentTab.XButtonRect = new Rectangle(CurrentTab.TabRect.X + CurrentTab.TabRect.Width - 20, CurrentTab.TabRect.Y + ((CurrentTab.TabRect.Height - 18) / 2), CurrentTab.XButtonRect.Width, CurrentTab.XButtonRect.Height);
                            tabRects[i] = CurrentTab;
                        }
                    }
                }
            }
        }
    }

}