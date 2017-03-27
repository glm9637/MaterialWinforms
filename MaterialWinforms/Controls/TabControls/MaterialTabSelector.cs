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

        public Color BackColor { get { return SkinManager.ColorScheme.PrimaryColor; } }

        private int HoveredXButtonIndex = -1;
        public ContextMenuStrip RightClickMenu { get; set; }

        private Point RightClickLocation;

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

        public int TabPadding
        {
            get { return TAB_HEADER_PADDING; }
            set { TAB_HEADER_PADDING = value; }
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
                    UpdateTabRects();
                    Invalidate();
                };
                baseTabControl.ControlRemoved += delegate
                {
                    UpdateTabRects();
                    Invalidate();
                };
            }
        }

        private int previousSelectedTabIndex;
        private Point animationSource;
        private readonly AnimationManager animationManager;
        private readonly AnimationManager HoverAnimationManager;

        private List<TabRectangle> tabRects;
        private int TAB_HEADER_PADDING = 24;
        private const int TAB_INDICATOR_HEIGHT = 2;
        private bool mouseDown = false;
        private int offset = 0;
        private int TabOffset = 0;
        private int oldXLocation = -1;
        private int TabLength = 0;
        private int HoveredTab = -1;

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
            HoverAnimationManager = new AnimationManager
            {
                AnimationType = AnimationType.EaseOut,
                Increment = 0.04
            };
            HoverAnimationManager.OnAnimationProgress += sender => Refresh();
            ShadowBorder = new GraphicsPath();
            Elevation = 10;
            ShadowBorder.AddLine(new Point(Location.X, Location.Y + Height), new Point(Location.X + Width, Location.Y + Height));
            SetupRightClickMenu();
        }

        private void SetupRightClickMenu()
        {
            RightClickMenu = new MaterialContextMenuStrip();
            RightClickMenu.AutoSize = true;
            ToolStripMenuItem CloseAllTabs = new MaterialToolStripMenuItem();
            ToolStripMenuItem TabPositionZurruecksetzten = new MaterialToolStripMenuItem();
            ToolStripMenuItem CloseAllExeptCurrent = new MaterialToolStripMenuItem();
            ToolStripMenuItem OpenInNewWindow = new MaterialToolStripMenuItem();

            CloseAllTabs.Text = "Alle Tabs schließen";
            CloseAllTabs.Click += CloseAllTabs_Click;
            RightClickMenu.Items.Add(CloseAllTabs);

            CloseAllExeptCurrent.Text = "Alle anderen Tabs Schließen";
            CloseAllExeptCurrent.Click += CloseAllExeptCurrent_Click;
            RightClickMenu.Items.Add(CloseAllExeptCurrent);

            TabPositionZurruecksetzten.Text = "Tab Positionen zurrücksetzen";
            TabPositionZurruecksetzten.Click += TabPositionZurruecksetzten_Click;
            RightClickMenu.Items.Add(TabPositionZurruecksetzten);

            OpenInNewWindow.Text = "Tab in neuem Fenster öffnen";
            OpenInNewWindow.Click += OpenInNewWindow_Click;
            RightClickMenu.Items.Add(OpenInNewWindow);
        }

        void OpenInNewWindow_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabRects.Count; i++)
            {
                if (tabRects[i].TabRect.Contains(RightClickLocation))
                {
                    var me = this;
                    TabWindow t = new TabWindow((MaterialTabPage)baseTabControl.TabPages[i], ref me);
                    t.Show();
                    return;
                }
            }
        }


        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
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
            UpdateTabRects();
            Invalidate();

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
                var hoverBrush = new SolidBrush(Color.FromArgb((int)(SkinManager.GetFlatButtonHoverBackgroundColor().A * HoverAnimationManager.GetProgress()), SkinManager.GetFlatButtonHoverBackgroundColor()));
                Pen closePen = new Pen(textBrush, 2);

                if (currentTabIndex == HoveredXButtonIndex)
                {
                    g.FillEllipse(hoverBrush, tabRects[currentTabIndex].XButtonRect);
                }
                else
                    if (currentTabIndex == HoveredTab)
                    {
                        g.FillRectangle(hoverBrush, new Rectangle(tabRects[currentTabIndex].TabRect.X + offset, tabRects[currentTabIndex].TabRect.Y, tabRects[currentTabIndex].TabRect.Width, tabRects[currentTabIndex].TabRect.Height));
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
            try
            {
                if (tabRects.Count >= baseTabControl.SelectedIndex)
                {
                    //Animate tab indicator
                    int previousSelectedTabIndexIfHasOne = previousSelectedTabIndex == -1 ? baseTabControl.SelectedIndex : previousSelectedTabIndex;
                    if (previousSelectedTabIndex > BaseTabControl.TabCount - 1) //Last tab was active and got closed
                    {
                        previousSelectedTabIndex = BaseTabControl.TabCount - 1;
                    }
                    if (baseTabControl.SelectedIndex < 0)
                    {
                        return;
                    }

                    Rectangle previousActiveTabRect = tabRects[previousSelectedTabIndexIfHasOne].TabRect;
                    Rectangle activeTabPageRect = tabRects[baseTabControl.SelectedIndex].TabRect;

                    int y = activeTabPageRect.Bottom - 2;
                    int x = previousActiveTabRect.X + (int)((activeTabPageRect.X - previousActiveTabRect.X) * animationProgress) + offset;
                    int width = previousActiveTabRect.Width + (int)((activeTabPageRect.Width - previousActiveTabRect.Width) * animationProgress);

                    g.FillRectangle(SkinManager.ColorScheme.AccentBrush, x, y, width, TAB_INDICATOR_HEIGHT);
                }
            }
            catch (Exception ex)
            {
                String str = ex.StackTrace;
                str = ex.Message;
            }
        }


        private int CalculateTextAlpha(int tabIndex, double animationProgress)
        {
            try
            {
                int primaryA = SkinManager.ACTION_BAR_TEXT().A;
                int secondaryA = SkinManager.ACTION_BAR_TEXT_SECONDARY().A;

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

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool HoveredTabSet = false;
            bool HoveredXButtonSet = false;
            try
            {
                base.OnMouseMove(e);
                if (baseTabControl != null && tabRects != null)
                {
                    if (mouseDown && baseTabControl.TabPages.Count > 0)
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
                    for (int i = 0; i < tabRects.Count; i++)
                    {
                        if (tabRects[i].TabRect.Contains(e.Location))
                        {
                            if (HoveredTab != i)
                            {
                                HoveredTab = i;
                                HoverAnimationManager.SetProgress(0);
                                HoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                            }
                            HoveredTabSet = true;
                            if (((MaterialTabPage)BaseTabControl.TabPages[i]).Closable)
                            {

                                if (tabRects[i].XButtonRect.Contains(e.Location))
                                {
                                    if (HoveredXButtonIndex != i)
                                    {
                                        HoveredXButtonIndex = i;
                                        HoverAnimationManager.SetProgress(0);
                                        HoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                                    }
                                    HoveredXButtonSet = true;
                                }
                            }
                        }

                    }

                    bool refresh = false;
                    if (HoveredTab != -1 && !HoveredTabSet)
                    {
                        HoveredTab = -1;
                        refresh = true;
                    }
                    if (HoveredXButtonIndex != -1 && !HoveredXButtonSet)
                    {
                        HoveredXButtonIndex = -1;
                        refresh = true;
                    }

                    if (refresh)
                    {
                        Refresh();
                    }
                    return;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {

            if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                HoveredXButtonIndex = -1;
                HoveredTab = -1;

                Refresh();
            }
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
                            if (baseTabControl.TabCount > 1 && i == BaseTabControl.SelectedIndex)
                            {
                                if (i == 0)
                                {
                                    baseTabControl.SelectTab(1);
                                }
                                else
                                {
                                    baseTabControl.SelectTab(i - 1);
                                }
                                Application.DoEvents();

                            }
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
                RightClickLocation = e.Location;
                RightClickMenu.Show(PointToScreen(e.Location));
            }
        }

        private void UpdateTabRects()
        {
            try
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
                        CurrentTab.XButtonRect = new Rectangle(CurrentTab.TabRect.X + CurrentTab.TabRect.Width, CurrentTab.TabRect.Y + ((CurrentTab.TabRect.Height - 18) / 2), xButtonSize, xButtonSize);
                        CurrentTab.TabRect.Width += CurrentTab.XButtonRect.Width;
                        if (MaxTabWidht > 0 && CurrentTab.TabRect.Width > MaxTabWidht)
                        {
                            CurrentTab.TabRect.Width = MaxTabWidht;
                        }
                        TabLength += CurrentTab.TabRect.Width;
                        tabRects.Add(CurrentTab);
                        for (int i = 1; i < baseTabControl.TabPages.Count; i++)
                        {
                            xButtonSize = ((MaterialTabPage)BaseTabControl.TabPages[i]).Closable ? 18 : 0;
                            CurrentTab = new TabRectangle();
                            CurrentTab.TabRect = new Rectangle(tabRects[i - 1].TabRect.Right, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(baseTabControl.TabPages[i].Text, SkinManager.ROBOTO_MEDIUM_10).Width + 22, Height);
                            CurrentTab.XButtonRect = new Rectangle(CurrentTab.TabRect.X + CurrentTab.TabRect.Width, CurrentTab.TabRect.Y + ((CurrentTab.TabRect.Height - 18) / 2), xButtonSize, xButtonSize);
                            CurrentTab.TabRect.Width += CurrentTab.XButtonRect.Width;
                            if (MaxTabWidht > 0 && CurrentTab.TabRect.Width > MaxTabWidht)
                            {
                                CurrentTab.TabRect.Width = MaxTabWidht;
                            }
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
                            CurrentTab.XButtonRect = new Rectangle(CurrentTab.TabRect.X + CurrentTab.TabRect.Width, CurrentTab.TabRect.Y + ((CurrentTab.TabRect.Height - 18) / 2), CurrentTab.XButtonRect.Width, CurrentTab.XButtonRect.Height);
                            CurrentTab.TabRect.Width += CurrentTab.XButtonRect.Width;
                            if (MaxTabWidht > 0 && CurrentTab.TabRect.Width > MaxTabWidht)
                            {
                                CurrentTab.TabRect.Width = MaxTabWidht;
                            }
                            tabRects[0] = CurrentTab;
                            for (int i = 1; i < baseTabControl.TabPages.Count; i++)
                            {
                                CurrentTab = tabRects[i];
                                CurrentTab.TabRect = new Rectangle(tabRects[i - 1].TabRect.Right, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(baseTabControl.TabPages[i].Text, SkinManager.ROBOTO_MEDIUM_10).Width + 22, Height);
                                CurrentTab.XButtonRect = new Rectangle(CurrentTab.TabRect.X + CurrentTab.TabRect.Width, CurrentTab.TabRect.Y + ((CurrentTab.TabRect.Height - 18) / 2), CurrentTab.XButtonRect.Width, CurrentTab.XButtonRect.Height);
                                CurrentTab.TabRect.Width += CurrentTab.XButtonRect.Width;
                                if (MaxTabWidht > 0 && CurrentTab.TabRect.Width > MaxTabWidht)
                                {
                                    CurrentTab.TabRect.Width = MaxTabWidht;
                                }
                                tabRects[i] = CurrentTab;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}