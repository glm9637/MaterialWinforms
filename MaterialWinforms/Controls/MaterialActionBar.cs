using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using MaterialWinforms.Animations;
using System.Reflection;
using System.Collections;

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

        public Color BackColor { get { return SkinManager.ColorScheme.PrimaryColor; } }
        public delegate void SideDrawerButtonClicked();
        public event SideDrawerButtonClicked onSideDrawerButtonClicked;


        public delegate void FilterButtonClicked();
        public event FilterButtonClicked onFilterButtonClicked;
        public delegate void Searched(String pText);
        public event Searched onSearched;
        public Boolean DrawBackArrow;

        private bool _IntegratedSearchBar;
        public bool IntegratedSearchBar
        {
            get { return _IntegratedSearchBar; }
            set
            {
                _IntegratedSearchBar = value;
                Invalidate();
            }
        }

        public bool SearchBarFilterIcon { get; set; }

        private Rectangle menuButtonBounds;
        private Rectangle drawerButtonBounds;
        private Rectangle SearchButtonBounds;
        private Rectangle FilterButtonBounds;
        private ButtonState buttonState;
        private int DrawerAnimationProgress;
        private bool searchOpen = false;
        private SearchTextField SearchTextBox;

        private AnimationManager objAnimationManager;

        private ActionBarButtonCollection _ActionBarButtons;

        private List<MaterialActionBarButton> _ActionBarButtonsToReveal;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ActionBarButtonCollection ActionBarButtons
        {
            get { return _ActionBarButtons; }
        }

        private enum ButtonState
        {
            DrawerOver,
            MenuOver,
            SearchOver,
            DrawerDown,
            MenuDown,
            SearchDown,
            FilterOver,
            FilterDown,
            None
        }

        public MaterialContextMenuStrip ActionBarMenu
        {
            get;
            set;
        }


        public const int ACTION_BAR_HEIGHT = 42;
        public MaterialActionBar()
        {
            DrawBackArrow = false;
            _ActionBarButtons = new ActionBarButtonCollection();
            _ActionBarButtons.onCollectionChanged += _ActionBarButtons_onCollectionChanged;
            Elevation = 10;
            Height = ACTION_BAR_HEIGHT;
            buttonState = ButtonState.None;
            objAnimationManager = new AnimationManager()
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            objAnimationManager.OnAnimationProgress += sender => Invalidate();
            objAnimationManager.OnAnimationFinished += objAnimationManager_OnAnimationFinished;
            InitializeComponent();
            SearchTextBox = new SearchTextField();
            SearchTextBox.onEnterDown += SearchTextBox_onEnterDown;
            DoubleBuffered = true;
        }

        void _ActionBarButtons_onCollectionChanged()
        {
            foreach (Control objActionButton in _ActionBarButtons)
            {
                if (!Controls.Contains(objActionButton))
                {
                    Controls.Add(objActionButton);
                }
            }

            List<Control> objButtonsToRemove = new List<Control>();

            foreach (Control objActionButton in Controls)
            {
                if (objActionButton.GetType() == typeof(MaterialActionBarButton) && !_ActionBarButtons.Contains(objActionButton))
                {
                    objButtonsToRemove.Add(objActionButton);
                }
            }

            foreach (Control objActionButton in objButtonsToRemove)
            {
                Controls.Remove(objActionButton);
            }
        }

        void SearchTextBox_onEnterDown()
        {
            if (onSearched != null)
            {
                onSearched(SearchTextBox.Text);
            }
        }


        void objAnimationManager_OnAnimationFinished(object sender)
        {

            if (searchOpen)
            {
                SearchTextBox.Hint = "Suchbegriff eingeben";
                SearchTextBox.Size = new Size(FilterButtonBounds.X - 15, Height);
                Controls.Add(SearchTextBox);
                SearchTextBox.Location = new Point(15, SearchTextBox.Location.Y);
                SearchTextBox.Select();
                         
            }
            else
            {
                if (_ActionBarButtonsToReveal != null)
                {
                    foreach (MaterialActionBarButton objItem in _ActionBarButtonsToReveal)
                    {
                        objItem.Visible = true;
                    }
                }
            }

        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            Dock = DockStyle.Top;
            if(Parent is MaterialForm) { 
            DrawBackArrow = ((MaterialForm)Parent).SideDrawer != null;
            if(DrawBackArrow)
                DrawBackArrow = ((MaterialForm)Parent).SideDrawer.SideDrawerFixiert;
            }
            Refresh();
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
            else if (drawerButtonBounds.Contains(e.Location))
            {
                if (buttonState != ButtonState.DrawerOver)
                {
                    buttonState = ButtonState.DrawerOver;
                    Invalidate();
                }
                return;
            }
            else if (SearchButtonBounds.Contains(e.Location))
            {
                if (buttonState != ButtonState.SearchOver)
                {
                    buttonState = ButtonState.SearchOver;
                    Invalidate();
                }
                return;
            }
             else if(FilterButtonBounds.Contains(e.Location) && searchOpen && SearchBarFilterIcon)
            {
                 if (buttonState != ButtonState.FilterOver)
                 {
                     buttonState = ButtonState.FilterOver;
                     Invalidate();
                 }
                
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
            if (menuButtonBounds.Contains(e.Location))
            {
                if (searchOpen)
                {
                    buttonState = ButtonState.MenuDown;
                    objAnimationManager.StartNewAnimation(AnimationDirection.Out);
                    SearchTextBox.Text = "";
                    searchOpen = false;
                    Controls.Remove(SearchTextBox);
                }
                else if (ActionBarMenu != null)
                {
                    buttonState = ButtonState.MenuDown;
                    ActionBarMenu.Show(PointToScreen(e.Location));
                }

                Invalidate();
            }
            else if (SearchButtonBounds.Contains(e.Location) && IntegratedSearchBar)
            {
                buttonState = ButtonState.MenuDown;
                if (searchOpen)
                {
                    if (onSearched != null)
                    {
                        onSearched(SearchTextBox.Text);
                    }
                }
                else
                {
                    _ActionBarButtonsToReveal = new List<MaterialActionBarButton>();
                    foreach (MaterialActionBarButton objItem in _ActionBarButtons)
                    {
                        if (objItem.Visible)
                        {
                            objItem.Visible = false;
                            _ActionBarButtonsToReveal.Add(objItem);

                        }
                    }
                    objAnimationManager.StartNewAnimation(AnimationDirection.In);
                    searchOpen = true;
                   
                }
            }
            else if (drawerButtonBounds.Contains(e.Location) && onSideDrawerButtonClicked != null && DrawBackArrow)
            {
                buttonState = ButtonState.DrawerDown;
                onSideDrawerButtonClicked();
                Invalidate();

            }
            else if (FilterButtonBounds.Contains(e.Location) && onFilterButtonClicked != null && searchOpen && SearchBarFilterIcon)
            {
                buttonState = ButtonState.FilterDown;
                onFilterButtonClicked();
                Invalidate();
            }
            base.OnMouseUp(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            drawerButtonBounds = new Rectangle(SkinManager.FORM_PADDING, 0, ACTION_BAR_HEIGHT, ACTION_BAR_HEIGHT);
            menuButtonBounds = new Rectangle(Width - SkinManager.FORM_PADDING - ACTION_BAR_HEIGHT, 0, ACTION_BAR_HEIGHT, ACTION_BAR_HEIGHT);
            SearchButtonBounds = new Rectangle(menuButtonBounds.X - ACTION_BAR_HEIGHT, 0, ACTION_BAR_HEIGHT, ACTION_BAR_HEIGHT);
            FilterButtonBounds = new Rectangle(SearchButtonBounds.X - ACTION_BAR_HEIGHT, 0, ACTION_BAR_HEIGHT, ACTION_BAR_HEIGHT);
            ShadowBorder = new GraphicsPath();
            ShadowBorder.AddLine(new Point(Location.X, Location.Y + Height), new Point(Location.X + Width, Location.Y + Height));
            Height = ACTION_BAR_HEIGHT;
            CalculateActionBarButtonPosition();
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Bitmap B = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(B);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.Clear(SkinManager.ColorScheme.PrimaryColor);
            bool DrawerIcon = false;
            if (objAnimationManager.GetProgress() == 1)
            {
                g.Clear(Color.White);

                using (var CloseButtonPen = new Pen(Color.DarkGray, 2))
                {
                    int BorderDistance = 12;
                    g.DrawLine(
                        CloseButtonPen,
                        menuButtonBounds.X + BorderDistance, menuButtonBounds.Y + BorderDistance,
                        menuButtonBounds.X + menuButtonBounds.Width - BorderDistance, menuButtonBounds.Y + menuButtonBounds.Height - BorderDistance
                        );


                    g.DrawLine(
                        CloseButtonPen,
                        menuButtonBounds.X + BorderDistance, menuButtonBounds.Y + menuButtonBounds.Height - BorderDistance,
                        menuButtonBounds.X + menuButtonBounds.Width - BorderDistance, menuButtonBounds.Y + BorderDistance
                        );


                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    float borderDistance = 0.2f;
                    g.DrawEllipse(CloseButtonPen, new RectangleF(SearchButtonBounds.X + SearchButtonBounds.Width * borderDistance, SearchButtonBounds.Y + SearchButtonBounds.Height * borderDistance, SearchButtonBounds.Width * 0.4f, SearchButtonBounds.Height * 0.4f));
                    g.DrawLine(CloseButtonPen, new PointF(SearchButtonBounds.Right - SearchButtonBounds.Width * borderDistance, SearchButtonBounds.Bottom - SearchButtonBounds.Height * borderDistance), new PointF(SearchButtonBounds.X + SearchButtonBounds.Width * 0.53f, SearchButtonBounds.Y + SearchButtonBounds.Height * 0.53f));

                }

                if (SearchBarFilterIcon)
                {
                    using (var CloseButtonPen = new Pen(Color.DarkGray, 2))
                    {
                        g.DrawLine(
                               CloseButtonPen,
                               FilterButtonBounds.X + (int)(FilterButtonBounds.Width * (0.2)),
                               FilterButtonBounds.Y + (int)(FilterButtonBounds.Height * (0.35 )),
                               FilterButtonBounds.X + (int)(FilterButtonBounds.Width * (0.8 )),
                               FilterButtonBounds.Y + (int)(FilterButtonBounds.Height * (0.35 )));
                        g.DrawLine(
                           CloseButtonPen,
                           FilterButtonBounds.X + (int)(FilterButtonBounds.Width * (0.3 )),
                           FilterButtonBounds.Y + (int)(FilterButtonBounds.Height * (0.5 )),
                           FilterButtonBounds.X + (int)(FilterButtonBounds.Width * (0.7 )),
                           FilterButtonBounds.Y + (int)(FilterButtonBounds.Height * (0.5)));
                        g.DrawLine(
                          CloseButtonPen,
                          FilterButtonBounds.X + (int)(FilterButtonBounds.Width * (0.45)),
                          FilterButtonBounds.Y + (int)(FilterButtonBounds.Height * (0.65 )),
                          FilterButtonBounds.X + (int)(FilterButtonBounds.Width * (0.55 )),
                          FilterButtonBounds.Y + (int)(FilterButtonBounds.Height * (0.65)));
                    }
                }

                var hoverBrush = new SolidBrush(Color.FromArgb(20.PercentageToColorComponent(), 0x999999.ToColor()));

                if (buttonState == ButtonState.MenuOver)
                    g.FillEllipse(hoverBrush, menuButtonBounds);

                if (buttonState == ButtonState.SearchOver)
                    g.FillEllipse(hoverBrush, SearchButtonBounds);

                if (buttonState == ButtonState.FilterOver)
                    g.FillEllipse(hoverBrush, FilterButtonBounds);
            }
            else
            {

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
                if (IntegratedSearchBar)
                {

                    if (buttonState == ButtonState.SearchOver)
                    {
                        g.FillEllipse(hoverBrush, SearchButtonBounds);
                    }
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    Pen IconPen = new Pen(SkinManager.ColorScheme.TextColor, 2);
                    float borderDistance = 0.2f;
                    g.DrawEllipse(IconPen, new RectangleF(SearchButtonBounds.X + SearchButtonBounds.Width * borderDistance, SearchButtonBounds.Y + SearchButtonBounds.Height * borderDistance, SearchButtonBounds.Width * 0.4f, SearchButtonBounds.Height * 0.4f));
                    g.DrawLine(IconPen, new PointF(SearchButtonBounds.Right - SearchButtonBounds.Width * borderDistance, SearchButtonBounds.Bottom - SearchButtonBounds.Height * borderDistance), new PointF(SearchButtonBounds.X + SearchButtonBounds.Width * 0.53f, SearchButtonBounds.Y + SearchButtonBounds.Height * 0.53f));
                }


                Control objParent = (Control)Parent;
                if (DrawBackArrow)
                {

                    if (buttonState == ButtonState.DrawerOver)
                    {
                        g.FillEllipse(hoverBrush, drawerButtonBounds);
                    }
                  
                        DrawerIcon = true;
                        using (var DrawerButtonPen = new Pen(SkinManager.ACTION_BAR_TEXT(), 2))
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

                //Form title
                g.DrawString(Parent.Text, SkinManager.ROBOTO_MEDIUM_12, SkinManager.ColorScheme.TextBrush, new Rectangle(SkinManager.FORM_PADDING + (DrawerIcon ? drawerButtonBounds.Right : 0), 0, Width, Height), new StringFormat { LineAlignment = StringAlignment.Center });


                if (objAnimationManager.IsAnimating())
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    float animWidht = Width * (float)objAnimationManager.GetProgress();
                    float Left = (Width / 2) - (animWidht / 2);
                    float Top = (Height / 2) - (animWidht / 2);
                    g.FillEllipse(new SolidBrush(Color.White), new RectangleF(new PointF(Left, Top), new SizeF(animWidht, animWidht)));
                    g.SmoothingMode = SmoothingMode.None;
                }
            }
            e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
            g.Dispose();
            B.Dispose();
        }
        public void setDrawerAnimationProgress(int newProgress)
        {
            DrawerAnimationProgress = newProgress;
            Invalidate();
        }

        public void CalculateActionBarButtonPosition()
        {
            if (ActionBarButtons != null)
            {
                int RightX = Right;
                if (IntegratedSearchBar)
                {
                    RightX = SearchButtonBounds.X;
                }
                else if (ActionBarMenu != null)
                {
                    RightX = menuButtonBounds.X;
                }

                RightX -= 5;

                for (int i = ActionBarButtons.Count - 1; i >= 0; i--)
                {

                    ActionBarButtons[i].Location = new Point(RightX - ACTION_BAR_HEIGHT, 0);
                    RightX = RightX - ACTION_BAR_HEIGHT;
                }
            }
            Invalidate();
        }

        public void OpenSearchBar()
        {
            if (!searchOpen)
            {
                _ActionBarButtonsToReveal = new List<MaterialActionBarButton>();
                foreach (MaterialActionBarButton objItem in _ActionBarButtons)
                {
                    if (objItem.Visible)
                    {
                        objItem.Visible = false;
                        _ActionBarButtonsToReveal.Add(objItem);

                    }
                }
                objAnimationManager.StartNewAnimation(AnimationDirection.In);
                searchOpen = true;
            }
        }

        public void CloseSearchBar()
        {
            if (searchOpen)
            {
                objAnimationManager.StartNewAnimation(AnimationDirection.Out);
                SearchTextBox.Text = "";
                searchOpen = false;
                Controls.Remove(SearchTextBox);
            }
        }

        class SearchTextField : Control, IMaterialControl
        {

            //Properties for managing the material design properties
            [Browsable(false)]
            public int Depth { get; set; }
            [Browsable(false)]
            public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
            [Browsable(false)]
            public MouseState MouseState { get; set; }

            public override string Text { get { return baseTextBox.Text; } set { baseTextBox.Text = value; } }
            public new object Tag { get { return baseTextBox.Tag; } set { baseTextBox.Tag = value; } }
            public new int MaxLength { get { return baseTextBox.MaxLength; } set { baseTextBox.MaxLength = value; } }

            public string SelectedText { get { return baseTextBox.SelectedText; } set { baseTextBox.SelectedText = value; } }
            public string Hint { get { return baseTextBox.Hint; } set { baseTextBox.Hint = value; } }

            public int SelectionStart { get { return baseTextBox.SelectionStart; } set { baseTextBox.SelectionStart = value; } }
            public int SelectionLength { get { return baseTextBox.SelectionLength; } set { baseTextBox.SelectionLength = value; } }
            public int TextLength { get { return baseTextBox.TextLength; } }
            public void SelectAll() { baseTextBox.SelectAll(); }
            public void Clear() { baseTextBox.Clear(); }

            public void Copy() { baseTextBox.Copy(); }

            public void Cut() { baseTextBox.Cut(); }

            public new void Select() { baseTextBox.Select(); }

            public bool ReadOnly { get { return baseTextBox.ReadOnly; } set { baseTextBox.ReadOnly = value; } }

            public HorizontalAlignment TextAlign { get { return baseTextBox.TextAlign; } set { baseTextBox.TextAlign = value; } }


            # region Forwarding events to baseTextBox
            public event EventHandler AcceptsTabChanged
            {
                add
                {

                    baseTextBox.AcceptsTabChanged += value;
                }
                remove
                {
                    baseTextBox.AcceptsTabChanged -= value;
                }
            }

            public new event EventHandler AutoSizeChanged
            {
                add
                {
                    baseTextBox.AutoSizeChanged += value;
                }
                remove
                {
                    baseTextBox.AutoSizeChanged -= value;
                }
            }

            public new event EventHandler BackgroundImageChanged
            {
                add
                {
                    baseTextBox.BackgroundImageChanged += value;
                }
                remove
                {
                    baseTextBox.BackgroundImageChanged -= value;
                }
            }

            public new event EventHandler BackgroundImageLayoutChanged
            {
                add
                {
                    baseTextBox.BackgroundImageLayoutChanged += value;
                }
                remove
                {
                    baseTextBox.BackgroundImageLayoutChanged -= value;
                }
            }

            public new event EventHandler BindingContextChanged
            {
                add
                {
                    baseTextBox.BindingContextChanged += value;
                }
                remove
                {
                    baseTextBox.BindingContextChanged -= value;
                }
            }

            public event EventHandler BorderStyleChanged
            {
                add
                {
                    baseTextBox.BorderStyleChanged += value;
                }
                remove
                {
                    baseTextBox.BorderStyleChanged -= value;
                }
            }

            public new event EventHandler CausesValidationChanged
            {
                add
                {
                    baseTextBox.CausesValidationChanged += value;
                }
                remove
                {
                    baseTextBox.CausesValidationChanged -= value;
                }
            }

            public new event UICuesEventHandler ChangeUICues
            {
                add
                {
                    baseTextBox.ChangeUICues += value;
                }
                remove
                {
                    baseTextBox.ChangeUICues -= value;
                }
            }

            public new event EventHandler Click
            {
                add
                {
                    baseTextBox.Click += value;
                }
                remove
                {
                    baseTextBox.Click -= value;
                }
            }

            public new event EventHandler ClientSizeChanged
            {
                add
                {
                    baseTextBox.ClientSizeChanged += value;
                }
                remove
                {
                    baseTextBox.ClientSizeChanged -= value;
                }
            }

            public new event EventHandler ContextMenuChanged
            {
                add
                {
                    baseTextBox.ContextMenuChanged += value;
                }
                remove
                {
                    baseTextBox.ContextMenuChanged -= value;
                }
            }

            public new event EventHandler ContextMenuStripChanged
            {
                add
                {
                    baseTextBox.ContextMenuStripChanged += value;
                }
                remove
                {
                    baseTextBox.ContextMenuStripChanged -= value;
                }
            }

            public new event ControlEventHandler ControlAdded
            {
                add
                {
                    baseTextBox.ControlAdded += value;
                }
                remove
                {
                    baseTextBox.ControlAdded -= value;
                }
            }

            public new event ControlEventHandler ControlRemoved
            {
                add
                {
                    baseTextBox.ControlRemoved += value;
                }
                remove
                {
                    baseTextBox.ControlRemoved -= value;
                }
            }

            public new event EventHandler CursorChanged
            {
                add
                {
                    baseTextBox.CursorChanged += value;
                }
                remove
                {
                    baseTextBox.CursorChanged -= value;
                }
            }

            public new event EventHandler Disposed
            {
                add
                {
                    baseTextBox.Disposed += value;
                }
                remove
                {
                    baseTextBox.Disposed -= value;
                }
            }

            public new event EventHandler DockChanged
            {
                add
                {
                    baseTextBox.DockChanged += value;
                }
                remove
                {
                    baseTextBox.DockChanged -= value;
                }
            }

            public new event EventHandler DoubleClick
            {
                add
                {
                    baseTextBox.DoubleClick += value;
                }
                remove
                {
                    baseTextBox.DoubleClick -= value;
                }
            }

            public new event DragEventHandler DragDrop
            {
                add
                {
                    baseTextBox.DragDrop += value;
                }
                remove
                {
                    baseTextBox.DragDrop -= value;
                }
            }

            public new event DragEventHandler DragEnter
            {
                add
                {
                    baseTextBox.DragEnter += value;
                }
                remove
                {
                    baseTextBox.DragEnter -= value;
                }
            }

            public new event EventHandler DragLeave
            {
                add
                {
                    baseTextBox.DragLeave += value;
                }
                remove
                {
                    baseTextBox.DragLeave -= value;
                }
            }

            public new event DragEventHandler DragOver
            {
                add
                {
                    baseTextBox.DragOver += value;
                }
                remove
                {
                    baseTextBox.DragOver -= value;
                }
            }

            public new event EventHandler EnabledChanged
            {
                add
                {
                    baseTextBox.EnabledChanged += value;
                }
                remove
                {
                    baseTextBox.EnabledChanged -= value;
                }
            }

            public new event EventHandler Enter
            {
                add
                {
                    baseTextBox.Enter += value;
                }
                remove
                {
                    baseTextBox.Enter -= value;
                }
            }

            public new event EventHandler FontChanged
            {
                add
                {
                    baseTextBox.FontChanged += value;
                }
                remove
                {
                    baseTextBox.FontChanged -= value;
                }
            }

            public new event EventHandler ForeColorChanged
            {
                add
                {
                    baseTextBox.ForeColorChanged += value;
                }
                remove
                {
                    baseTextBox.ForeColorChanged -= value;
                }
            }

            public new event GiveFeedbackEventHandler GiveFeedback
            {
                add
                {
                    baseTextBox.GiveFeedback += value;
                }
                remove
                {
                    baseTextBox.GiveFeedback -= value;
                }
            }

            public new event EventHandler GotFocus
            {
                add
                {
                    baseTextBox.GotFocus += value;
                }
                remove
                {
                    baseTextBox.GotFocus -= value;
                }
            }

            public new event EventHandler HandleCreated
            {
                add
                {
                    baseTextBox.HandleCreated += value;
                }
                remove
                {
                    baseTextBox.HandleCreated -= value;
                }
            }

            public new event EventHandler HandleDestroyed
            {
                add
                {
                    baseTextBox.HandleDestroyed += value;
                }
                remove
                {
                    baseTextBox.HandleDestroyed -= value;
                }
            }

            public new event HelpEventHandler HelpRequested
            {
                add
                {
                    baseTextBox.HelpRequested += value;
                }
                remove
                {
                    baseTextBox.HelpRequested -= value;
                }
            }

            public event EventHandler HideSelectionChanged
            {
                add
                {
                    baseTextBox.HideSelectionChanged += value;
                }
                remove
                {
                    baseTextBox.HideSelectionChanged -= value;
                }
            }

            public new event EventHandler ImeModeChanged
            {
                add
                {
                    baseTextBox.ImeModeChanged += value;
                }
                remove
                {
                    baseTextBox.ImeModeChanged -= value;
                }
            }

            public new event InvalidateEventHandler Invalidated
            {
                add
                {
                    baseTextBox.Invalidated += value;
                }
                remove
                {
                    baseTextBox.Invalidated -= value;
                }
            }

            public new event KeyEventHandler KeyDown
            {
                add
                {
                    baseTextBox.KeyDown += value;
                }
                remove
                {
                    baseTextBox.KeyDown -= value;
                }
            }

            public new event KeyPressEventHandler KeyPress
            {
                add
                {
                    baseTextBox.KeyPress += value;
                }
                remove
                {
                    baseTextBox.KeyPress -= value;
                }
            }

            public new event KeyEventHandler KeyUp
            {
                add
                {
                    baseTextBox.KeyUp += value;
                }
                remove
                {
                    baseTextBox.KeyUp -= value;
                }
            }

            public new event LayoutEventHandler Layout
            {
                add
                {
                    baseTextBox.Layout += value;
                }
                remove
                {
                    baseTextBox.Layout -= value;
                }
            }

            public new event EventHandler Leave
            {
                add
                {
                    baseTextBox.Leave += value;
                }
                remove
                {
                    baseTextBox.Leave -= value;
                }
            }

            public new event EventHandler LocationChanged
            {
                add
                {
                    baseTextBox.LocationChanged += value;
                }
                remove
                {
                    baseTextBox.LocationChanged -= value;
                }
            }

            public new event EventHandler LostFocus
            {
                add
                {
                    baseTextBox.LostFocus += value;
                }
                remove
                {
                    baseTextBox.LostFocus -= value;
                }
            }

            public new event EventHandler MarginChanged
            {
                add
                {
                    baseTextBox.MarginChanged += value;
                }
                remove
                {
                    baseTextBox.MarginChanged -= value;
                }
            }

            public event EventHandler ModifiedChanged
            {
                add
                {
                    baseTextBox.ModifiedChanged += value;
                }
                remove
                {
                    baseTextBox.ModifiedChanged -= value;
                }
            }

            public new event EventHandler MouseCaptureChanged
            {
                add
                {
                    baseTextBox.MouseCaptureChanged += value;
                }
                remove
                {
                    baseTextBox.MouseCaptureChanged -= value;
                }
            }

            public new event MouseEventHandler MouseClick
            {
                add
                {
                    baseTextBox.MouseClick += value;
                }
                remove
                {
                    baseTextBox.MouseClick -= value;
                }
            }

            public new event MouseEventHandler MouseDoubleClick
            {
                add
                {
                    baseTextBox.MouseDoubleClick += value;
                }
                remove
                {
                    baseTextBox.MouseDoubleClick -= value;
                }
            }

            public new event MouseEventHandler MouseDown
            {
                add
                {
                    baseTextBox.MouseDown += value;
                }
                remove
                {
                    baseTextBox.MouseDown -= value;
                }
            }

            public new event EventHandler MouseEnter
            {
                add
                {
                    baseTextBox.MouseEnter += value;
                }
                remove
                {
                    baseTextBox.MouseEnter -= value;
                }
            }

            public new event EventHandler MouseHover
            {
                add
                {
                    baseTextBox.MouseHover += value;
                }
                remove
                {
                    baseTextBox.MouseHover -= value;
                }
            }

            public new event EventHandler MouseLeave
            {
                add
                {
                    baseTextBox.MouseLeave += value;
                }
                remove
                {
                    baseTextBox.MouseLeave -= value;
                }
            }

            public new event MouseEventHandler MouseMove
            {
                add
                {
                    baseTextBox.MouseMove += value;
                }
                remove
                {
                    baseTextBox.MouseMove -= value;
                }
            }

            public new event MouseEventHandler MouseUp
            {
                add
                {
                    baseTextBox.MouseUp += value;
                }
                remove
                {
                    baseTextBox.MouseUp -= value;
                }
            }

            public new event MouseEventHandler MouseWheel
            {
                add
                {
                    baseTextBox.MouseWheel += value;
                }
                remove
                {
                    baseTextBox.MouseWheel -= value;
                }
            }

            public new event EventHandler Move
            {
                add
                {
                    baseTextBox.Move += value;
                }
                remove
                {
                    baseTextBox.Move -= value;
                }
            }

            public event EventHandler MultilineChanged
            {
                add
                {
                    baseTextBox.MultilineChanged += value;
                }
                remove
                {
                    baseTextBox.MultilineChanged -= value;
                }
            }

            public new event EventHandler PaddingChanged
            {
                add
                {
                    baseTextBox.PaddingChanged += value;
                }
                remove
                {
                    baseTextBox.PaddingChanged -= value;
                }
            }

            public new event PaintEventHandler Paint
            {
                add
                {
                    baseTextBox.Paint += value;
                }
                remove
                {
                    baseTextBox.Paint -= value;
                }
            }

            public new event EventHandler ParentChanged
            {
                add
                {
                    baseTextBox.ParentChanged += value;
                }
                remove
                {
                    baseTextBox.ParentChanged -= value;
                }
            }

            public new event PreviewKeyDownEventHandler PreviewKeyDown
            {
                add
                {
                    baseTextBox.PreviewKeyDown += value;
                }
                remove
                {
                    baseTextBox.PreviewKeyDown -= value;
                }
            }

            public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
            {
                add
                {
                    baseTextBox.QueryAccessibilityHelp += value;
                }
                remove
                {
                    baseTextBox.QueryAccessibilityHelp -= value;
                }
            }

            public new event QueryContinueDragEventHandler QueryContinueDrag
            {
                add
                {
                    baseTextBox.QueryContinueDrag += value;
                }
                remove
                {
                    baseTextBox.QueryContinueDrag -= value;
                }
            }

            public event EventHandler ReadOnlyChanged
            {
                add
                {
                    baseTextBox.ReadOnlyChanged += value;
                }
                remove
                {
                    baseTextBox.ReadOnlyChanged -= value;
                }
            }

            public new event EventHandler RegionChanged
            {
                add
                {
                    baseTextBox.RegionChanged += value;
                }
                remove
                {
                    baseTextBox.RegionChanged -= value;
                }
            }

            public new event EventHandler Resize
            {
                add
                {
                    baseTextBox.Resize += value;
                }
                remove
                {
                    baseTextBox.Resize -= value;
                }
            }

            public new event EventHandler RightToLeftChanged
            {
                add
                {
                    baseTextBox.RightToLeftChanged += value;
                }
                remove
                {
                    baseTextBox.RightToLeftChanged -= value;
                }
            }

            public new event EventHandler SizeChanged
            {
                add
                {
                    baseTextBox.SizeChanged += value;
                }
                remove
                {
                    baseTextBox.SizeChanged -= value;
                }
            }

            public new event EventHandler StyleChanged
            {
                add
                {
                    baseTextBox.StyleChanged += value;
                }
                remove
                {
                    baseTextBox.StyleChanged -= value;
                }
            }

            public new event EventHandler SystemColorsChanged
            {
                add
                {
                    baseTextBox.SystemColorsChanged += value;
                }
                remove
                {
                    baseTextBox.SystemColorsChanged -= value;
                }
            }

            public new event EventHandler TabIndexChanged
            {
                add
                {
                    baseTextBox.TabIndexChanged += value;
                }
                remove
                {
                    baseTextBox.TabIndexChanged -= value;
                }
            }

            public new event EventHandler TabStopChanged
            {
                add
                {
                    baseTextBox.TabStopChanged += value;
                }
                remove
                {
                    baseTextBox.TabStopChanged -= value;
                }
            }

            public event EventHandler TextAlignChanged
            {
                add
                {
                    baseTextBox.TextAlignChanged += value;
                }
                remove
                {
                    baseTextBox.TextAlignChanged -= value;
                }
            }

            public new event EventHandler TextChanged
            {
                add
                {
                    baseTextBox.TextChanged += value;
                }
                remove
                {
                    baseTextBox.TextChanged -= value;
                }
            }

            public new event EventHandler Validated
            {
                add
                {
                    baseTextBox.Validated += value;
                }
                remove
                {
                    baseTextBox.Validated -= value;
                }
            }

            public new event CancelEventHandler Validating
            {
                add
                {
                    baseTextBox.Validating += value;
                }
                remove
                {
                    baseTextBox.Validating -= value;
                }
            }

            public new event EventHandler VisibleChanged
            {
                add
                {
                    baseTextBox.VisibleChanged += value;
                }
                remove
                {
                    baseTextBox.VisibleChanged -= value;
                }
            }
            # endregion

            protected readonly BaseTextBox baseTextBox;
            public SearchTextField()
            {
                SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);


                baseTextBox = new BaseTextBox
                {
                    BorderStyle = BorderStyle.None,
                    Font = SkinManager.ROBOTO_REGULAR_11,
                    ForeColor = Color.Black,
                    Location = new Point(0, 15),
                    Width = Width,
                    Height = Height - 5
                };
                baseTextBox.onEnterDown += baseTextBox_onEnterDown;

                if (!Controls.Contains(baseTextBox) && !DesignMode)
                {
                    Controls.Add(baseTextBox);
                }

                baseTextBox.TextChanged += new EventHandler(Redraw);

                //Fix for tabstop
                baseTextBox.TabStop = true;
                this.TabStop = false;
                BackColor = Color.White;
                Text = "";
                baseTextBox.Text = "";
            }

            void baseTextBox_onEnterDown()
            {
                if (onEnterDown != null)
                {
                    onEnterDown();
                }
            }


            private void Redraw(object sencer, EventArgs e)
            {
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                var g = pevent.Graphics;
                g.Clear(Color.White);

            }

            public delegate void EnterDown();
            public event EnterDown onEnterDown;


            public bool Focused()
            {
                return baseTextBox.Focused;
            }

            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);

                baseTextBox.Location = new Point(0, 15);
                baseTextBox.Width = Width;

                Height = baseTextBox.Height + 20;
            }

            protected override void OnCreateControl()
            {
                base.OnCreateControl();

                baseTextBox.BackColor = Color.White;
                baseTextBox.ForeColor = Color.Black;
            }

            protected class BaseTextBox : TextBox
            {
                [DllImport("user32.dll", CharSet = CharSet.Auto)]
                private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

                private const int EM_SETCUEBANNER = 0x1501;
                private const char EmptyChar = (char)0;
                private const char VisualStylePasswordChar = '\u25CF';
                private const char NonVisualStylePasswordChar = '\u002A';

                private string hint = string.Empty;
                public string Hint
                {
                    get { return hint; }
                    set
                    {
                        hint = value;
                        SendMessage(Handle, EM_SETCUEBANNER, (int)IntPtr.Zero, Hint);
                    }
                }

                private char passwordChar = EmptyChar;
                public new char PasswordChar
                {
                    get { return passwordChar; }
                    set
                    {
                        passwordChar = value;
                        SetBasePasswordChar();
                    }
                }

                public delegate void EnterDown();
                public event EnterDown onEnterDown;


                protected override void OnKeyDown(KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (onEnterDown != null)
                        {
                            onEnterDown();
                        }
                    }

                    base.OnKeyDown(e);

                }

                public new void SelectAll()
                {
                    BeginInvoke((MethodInvoker)delegate()
                    {
                        base.Focus();
                        base.SelectAll();
                    });
                }


                private char useSystemPasswordChar = EmptyChar;
                public new bool UseSystemPasswordChar
                {
                    get { return useSystemPasswordChar != EmptyChar; }
                    set
                    {
                        if (value)
                        {
                            useSystemPasswordChar = Application.RenderWithVisualStyles ? VisualStylePasswordChar : NonVisualStylePasswordChar;
                        }
                        else
                        {
                            useSystemPasswordChar = EmptyChar;
                        }

                        SetBasePasswordChar();
                    }
                }

                private void SetBasePasswordChar()
                {
                    base.PasswordChar = UseSystemPasswordChar ? useSystemPasswordChar : passwordChar;
                }

                public BaseTextBox()
                {
                    MaterialContextMenuStrip cms = new TextBoxContextMenuStrip();
                    cms.Opening += ContextMenuStripOnOpening;
                    cms.OnItemClickStart += ContextMenuStripOnItemClickStart;
                    BackColor = Color.White;
                    ContextMenuStrip = cms;
                }

                private void ContextMenuStripOnItemClickStart(object sender, ToolStripItemClickedEventArgs toolStripItemClickedEventArgs)
                {
                    switch (toolStripItemClickedEventArgs.ClickedItem.Text)
                    {
                        case "Undo":
                            Undo();
                            break;
                        case "Cut":
                            Cut();
                            break;
                        case "Copy":
                            Copy();
                            break;
                        case "Paste":
                            Paste();
                            break;
                        case "Delete":
                            SelectedText = string.Empty;
                            break;
                        case "Select All":
                            SelectAll();
                            break;
                    }
                }

                private void ContextMenuStripOnOpening(object sender, CancelEventArgs cancelEventArgs)
                {
                    var strip = sender as TextBoxContextMenuStrip;
                    if (strip != null)
                    {
                        strip.undo.Enabled = CanUndo;
                        strip.cut.Enabled = !string.IsNullOrEmpty(SelectedText);
                        strip.copy.Enabled = !string.IsNullOrEmpty(SelectedText);
                        strip.paste.Enabled = Clipboard.ContainsText();
                        strip.delete.Enabled = !string.IsNullOrEmpty(SelectedText);
                        strip.selectAll.Enabled = !string.IsNullOrEmpty(Text);
                    }
                }
            }

            private class TextBoxContextMenuStrip : MaterialContextMenuStrip
            {
                public readonly ToolStripItem undo = new MaterialToolStripMenuItem { Text = "Undo" };
                public readonly ToolStripItem seperator1 = new ToolStripSeparator();
                public readonly ToolStripItem cut = new MaterialToolStripMenuItem { Text = "Cut" };
                public readonly ToolStripItem copy = new MaterialToolStripMenuItem { Text = "Copy" };
                public readonly ToolStripItem paste = new MaterialToolStripMenuItem { Text = "Paste" };
                public readonly ToolStripItem delete = new MaterialToolStripMenuItem { Text = "Delete" };
                public readonly ToolStripItem seperator2 = new ToolStripSeparator();
                public readonly ToolStripItem selectAll = new MaterialToolStripMenuItem { Text = "Select All" };

                public TextBoxContextMenuStrip()
                {
                    Items.AddRange(new[]
                {
                    undo,
                    seperator1,
                    cut,
                    copy,
                    paste,
                    delete,
                    seperator2,
                    selectAll
                });
                }
            }
        }
    }

    #region ActionBarButtons

    [Designer(typeof(System.Windows.Forms.Design.ScrollableControlDesigner))]
    public class MaterialActionBarButton : Button, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public Color BackColor { get { return SkinManager.ColorScheme.PrimaryColor; } }

        private AnimationManager animationManager;
        private AnimationManager hoverAnimationManager;

        private ToolTip objToolTip;


        public Image Image { get; set; }

        public MaterialActionBarButton()
        {
            init();
        }

        public MaterialActionBarButton(String pName)
        {
            this.Name = pName;
            init();
        }

        public MaterialActionBarButton(Image pIcon)
        {

            this.Image = pIcon;

            init();
        }

        public MaterialActionBarButton(String pName, Image pIcon)
        {
            this.Name = pName;
            this.Image = pIcon;

            init();
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (objToolTip != null)
                    objToolTip.SetToolTip(this, Text);
            }
        }

        private void init()
        {
            objToolTip = new ToolTip();

            animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            hoverAnimationManager = new AnimationManager
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };

            hoverAnimationManager.OnAnimationProgress += sender => Invalidate();
            animationManager.OnAnimationProgress += sender => Invalidate();
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            Width = MaterialActionBar.ACTION_BAR_HEIGHT;
            Height = MaterialActionBar.ACTION_BAR_HEIGHT;
            Margin = new Padding(4, 6, 4, 6);
            Padding = new Padding(0);

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;

            MouseState = MouseState.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                hoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                hoverAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };
            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    MouseState = MouseState.DOWN;

                    animationManager.StartNewAnimation(AnimationDirection.In, args.Location);
                    Invalidate();
                }
            };
            MouseUp += (sender, args) =>
            {
                MouseState = MouseState.HOVER;

                Invalidate();
            };
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // base.OnPaint(e);
            g.Clear(SkinManager.ColorScheme.PrimaryColor);

            g.SetClip(DrawHelper.CreateCircle(0, 0, Width / 2));
            //Hover
            Color c = SkinManager.GetFlatButtonHoverBackgroundColor();
            using (Brush b = new SolidBrush(Color.FromArgb((int)(hoverAnimationManager.GetProgress() * c.A), c.RemoveAlpha())))
                g.FillEllipse(b, ClientRectangle);

            //Ripple
            if (animationManager.IsAnimating())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = animationManager.GetProgress(i);
                    var animationSource = animationManager.GetSource(i);

                    using (Brush rippleBrush = new SolidBrush(Color.FromArgb((int)(101 - (animationValue * 100)), Color.Black)))
                    {
                        var rippleSize = (int)(animationValue * Width * 2);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                    }
                }
                g.SmoothingMode = SmoothingMode.None;
            }
            else
            {

            }
            g.ResetClip();
            if (Image != null)
            {
                float fPosition = (float)(Width * 0.1);
                float fSize = (float)(Width * 0.8);
                e.Graphics.DrawImage(Image, new RectangleF(fPosition, fPosition, fSize, fSize));
            }

        }

    }

    public class ActionBarButtonCollection : CollectionBase
    {
        public delegate void CollectionChanged();
        public event CollectionChanged onCollectionChanged;

        public MaterialActionBarButton this[int index]
        {
            get { return (MaterialActionBarButton)List[index]; }
        }

        public void Add(MaterialActionBarButton pButton)
        {
            List.Add(pButton);
            onCollectionChanged();
        }

        public void Remove(MaterialActionBarButton pButton)
        {
            List.Remove(pButton);
            onCollectionChanged();
        }

        public bool Contains(Control pControl)
        {
            return List.Contains(pControl);
        }

    }

    public class MaterialActionButtonCollectionEditor : CollectionEditor
    {
        public MaterialActionButtonCollectionEditor(Type type)
            : base(type)
        {
        }

        protected override string GetDisplayText(object value)
        {
            MaterialActionBarButton item;
            item = (MaterialActionBarButton)value;

            return base.GetDisplayText(item.Name);
        }
    }

    #endregion
}

