using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialWinforms.Animations;
using MaterialWinforms;
using System.Drawing.Drawing2D;

namespace MaterialWinforms.Controls.Settings
{
    public partial class SchemeCreator : Form
    {

        private AnimationManager objAnimationManager;
        private Point _Origin;
        private Brush FillBrush;
        private Brush BackBrush;
        private MaterialForm _BaseForm;
        private Primary _Primary;
        private Primary _PrimaryDark;
        private Primary _PrimaryLight;
        private Accent _Accent;
        private TextShade _Text;
        private const String _PrimaryKey = "Primary";
        private const String _AccentKey = "Accent";
        private const String _TextShadeKey = "TextShade";
        private Rectangle PrimaryRect;
        private Rectangle LightPrimaryRect;
        private Rectangle DarkPrimaryRect;
        private Rectangle AccentRect;
        private Rectangle TextShadeRect;
        private Rectangle FabRect;
        private int Zustand = 0;
        private bool _Close = false;
        private GraphicsPath CurrentHoveredPath;
        private int HoveredIndex;
        private bool FabHovered = false;
        private String SchemeName;

        private Dictionary<String, List<ColorRect>> ColorRectangles;

        private String _ActiveKey;

        private CurrentThemePreview objPreview;


        public SchemeCreator(Point Origin, MaterialForm BaseFormToOverlay)
        {
            BackBrush = Brushes.Magenta;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CurrentHoveredPath = new GraphicsPath();
            //set the backcolor and transparencykey on same color.
            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;
            InitializeComponent();
            FillBrush = new SolidBrush(MaterialSkinManager.Instance.GetApplicationBackgroundColor());
            _Origin = Origin;
            _BaseForm = BaseFormToOverlay;
            objAnimationManager = new AnimationManager()
            {
                Increment = 0.015,
                AnimationType = AnimationType.EaseInOut
            };
            DoubleBuffered = true;
            objAnimationManager.OnAnimationProgress += sender => Invalidate();
            objAnimationManager.OnAnimationFinished += objAnimationManager_OnAnimationFinished;
            Visible = false;
            _ActiveKey = _PrimaryKey;
            _Primary = Primary.Indigo500;
            _PrimaryDark = Primary.Indigo700;
            _PrimaryLight = Primary.Indigo100;
            _Accent = Accent.Pink200;
            _Text = TextShade.WHITE;
            BaseFormToOverlay.LocationChanged += BaseFormToOverlay_LocationChanged;
            initColorHints();
            Size = _BaseForm.Size;
            Location = _BaseForm.Location;

            MouseUp += SchemeCreator_MouseUp;

        }

        private void BaseFormToOverlay_LocationChanged(object sender, EventArgs e)
        {
            Location = _BaseForm.Location;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e .KeyCode == Keys.Escape)
            {
                _BaseForm.Activate();
                objAnimationManager.StartNewAnimation(AnimationDirection.Out);
            }

            base.OnKeyDown(e);
        }

        void SchemeCreator_MouseUp(object sender, MouseEventArgs e)
        {
            if (PrimaryRect.Contains(e.Location))
            {
                _ActiveKey = _PrimaryKey;
                Zustand = 0;
                HoveredIndex = -1;
            }
            if (LightPrimaryRect.Contains(e.Location))
            {
                _ActiveKey = _PrimaryKey;
                Zustand = 1;
                HoveredIndex = -1;
            }
            if (DarkPrimaryRect.Contains(e.Location))
            {
                _ActiveKey = _PrimaryKey;
                Zustand = 2;
                HoveredIndex = -1;
            }

            if (AccentRect.Contains(e.Location))
            {
                _ActiveKey = _AccentKey;
                Zustand = 3;
                HoveredIndex = -1;
            }
            if (TextShadeRect.Contains(e.Location))
            {
                _ActiveKey = _TextShadeKey;
                Zustand = 4;
                HoveredIndex = -1;
            }
            if (FabRect.Contains(e.Location))
            {
                MaterialUserControl objSaveControl = new MaterialUserControl();
                objSaveControl.Width = 400;
                objSaveControl.Padding = new Padding(10);
                MaterialSingleLineTextField objText = new MaterialSingleLineTextField();
                objText.Dock = DockStyle.Top;
                objText.Hint = "Geben sie einen Namen für das Farbschema ein:";
                objText.TextChanged += objText_TextChanged;
                objSaveControl.Height = 70;
                objSaveControl.Controls.Add(objText);
                if (MaterialDialog.Show("Neues Farbschema Speichern",objSaveControl, MaterialDialog.Buttons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    MaterialSkinManager.Instance.ColorSchemes.add(new ColorSchemePreset()
                    {
                        Name = SchemeName,
                        AccentColor = _Accent,
                        DarkPrimaryColor = _PrimaryDark,
                        LightPrimaryColor = _PrimaryLight,
                        PrimaryColor = _Primary,
                        TextShade = _Text
                    });

                }

                _BaseForm.Activate();
                objAnimationManager.StartNewAnimation(AnimationDirection.Out);
                
                return;
            }

            if (HoveredIndex >= 0)
            {
                switch (Zustand)
                {
                    case 0:
                        _Primary = (Primary)ColorRectangles[_ActiveKey][HoveredIndex].Tag;
                        break;
                    case 1:
                        _PrimaryLight = (Primary)ColorRectangles[_ActiveKey][HoveredIndex].Tag;
                        break;
                    case 2:
                        _PrimaryDark = (Primary)ColorRectangles[_ActiveKey][HoveredIndex].Tag;
                        break;
                    case 3:
                        _Accent = (Accent)ColorRectangles[_ActiveKey][HoveredIndex].Tag;
                        break;
                    case 4:
                        _Text = (TextShade)ColorRectangles[_ActiveKey][HoveredIndex].Tag;
                        break;
                }
                objPreview.setSchemeToPreview(new ColorScheme(_Primary, _PrimaryDark, _PrimaryLight, _Accent, _Text));
            }

            Invalidate();
        }

        void objText_TextChanged(object sender, EventArgs e)
        {
            SchemeName = ((MaterialSingleLineTextField.BaseTextBox)sender).Text;
        }

        private void objAnimationManager_OnAnimationFinished(object sender)
        {

            if (_Close)
            {
                Close();
            }
            else
            {
                _Close = true;
            }

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if(FabRect.Contains(e.Location))
            {
                HoveredIndex = -1;
                if(!FabHovered)
                {
                    FabHovered = true;
                    Invalidate();
                }

                return;
            }
            FabHovered = false;

            for (int i = 0; i < ColorRectangles[_ActiveKey].Count; i++)
            {
                if (ColorRectangles[_ActiveKey][i].Rect.Contains(e.Location))
                {
                    if (i != HoveredIndex)
                    {
                        HoveredIndex = i;
                        CurrentHoveredPath = new GraphicsPath();
                        CurrentHoveredPath.AddRectangle(new Rectangle(ColorRectangles[_ActiveKey][i].Rect.X - 3, ColorRectangles[_ActiveKey][i].Rect.Y - 3, ColorRectangles[_ActiveKey][i].Rect.Width, ColorRectangles[_ActiveKey][i].Rect.Height));
                        Invalidate();
                    }
                    return;
                }
            }
            if (HoveredIndex >= 0)
            {
                HoveredIndex = -1;
                CurrentHoveredPath = new GraphicsPath();
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!Visible && objAnimationManager.GetProgress() > 0)
            {
                Visible = true;
            }
            e.Graphics.FillRectangle(BackBrush, e.ClipRectangle);
            GraphicsPath Clip = new GraphicsPath();
            Clip.AddEllipse(CalculateCurrentRect());
            e.Graphics.SetClip(Clip);
            e.Graphics.Clear(MaterialSkinManager.Instance.GetApplicationBackgroundColor());
            e.Graphics.FillRectangle(new SolidBrush(((int)_Primary).ToColor()), PrimaryRect);
            e.Graphics.FillRectangle(new SolidBrush(((int)_PrimaryLight).ToColor()), LightPrimaryRect);
            e.Graphics.FillRectangle(new SolidBrush(((int)_PrimaryDark).ToColor()), DarkPrimaryRect);

            e.Graphics.FillRectangle(new SolidBrush(((int)_Accent).ToColor()), AccentRect);
            e.Graphics.FillRectangle(new SolidBrush(((int)_Text).ToColor()), TextShadeRect);

            GraphicsPath g;
            Color FillColor = Color.FromArgb(80, Color.White);
            switch (Zustand)
            {
                case 0:
                    g = new GraphicsPath();
                    g.AddRectangle(new Rectangle(PrimaryRect.X - 2, PrimaryRect.Y - 2, PrimaryRect.Width, PrimaryRect.Height));
                    DrawHelper.drawShadow(e.Graphics, g, 3, Color.Black);
                    e.Graphics.FillRectangle(new SolidBrush(((int)_Primary).ToColor()), PrimaryRect);
                    e.Graphics.FillRectangle(new SolidBrush(FillColor), PrimaryRect);
                    break;
                case 1:
                    g = new GraphicsPath();
                    g.AddRectangle(new Rectangle(LightPrimaryRect.X - 2, LightPrimaryRect.Y - 2, LightPrimaryRect.Width, LightPrimaryRect.Height));
                    DrawHelper.drawShadow(e.Graphics, g, 3, Color.Black);
                    e.Graphics.FillRectangle(new SolidBrush(((int)_PrimaryLight).ToColor()), LightPrimaryRect);
                    e.Graphics.FillRectangle(new SolidBrush(FillColor), LightPrimaryRect);
                    break;
                case 2:
                    g = new GraphicsPath();
                    g.AddRectangle(new Rectangle(DarkPrimaryRect.X - 2, DarkPrimaryRect.Y - 2, DarkPrimaryRect.Width, DarkPrimaryRect.Height));
                    g.AddRectangle(DarkPrimaryRect);
                    DrawHelper.drawShadow(e.Graphics, g, 3, Color.Black);
                    e.Graphics.FillRectangle(new SolidBrush(((int)_PrimaryDark).ToColor()), DarkPrimaryRect);
                    e.Graphics.FillRectangle(new SolidBrush(FillColor), DarkPrimaryRect);
                    break;
                case 3:
                    g = new GraphicsPath();
                    g.AddRectangle(new Rectangle(AccentRect.X - 2, AccentRect.Y - 2, AccentRect.Width, AccentRect.Height));
                    g.AddRectangle(AccentRect);
                    DrawHelper.drawShadow(e.Graphics, g, 3, Color.Black);
                    e.Graphics.FillRectangle(new SolidBrush(((int)_Accent).ToColor()), AccentRect);
                    e.Graphics.FillRectangle(new SolidBrush(FillColor), AccentRect);
                    break;
                case 4:
                    g = new GraphicsPath();
                    g.AddRectangle(new Rectangle(TextShadeRect.X - 2, TextShadeRect.Y - 2, TextShadeRect.Width, TextShadeRect.Height));
                    g.AddRectangle(TextShadeRect);
                    DrawHelper.drawShadow(e.Graphics, g, 3, Color.Black);
                    e.Graphics.FillRectangle(new SolidBrush(((int)_Text).ToColor()), TextShadeRect);
                    e.Graphics.FillRectangle(new SolidBrush(FillColor), TextShadeRect);
                    break;
            }

            foreach (ColorRect objRect in ColorRectangles[_ActiveKey])
            {
                e.Graphics.FillRectangle(new SolidBrush(objRect.Color), objRect.Rect);
            }

            if (HoveredIndex >= 0)
            {
                DrawHelper.drawShadow(e.Graphics, CurrentHoveredPath, 4, Color.Black);
                e.Graphics.FillRectangle(new SolidBrush(ColorRectangles[_ActiveKey][HoveredIndex].Color), ColorRectangles[_ActiveKey][HoveredIndex].Rect);
            }

            objPreview.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawHelper.drawShadow(e.Graphics, DrawHelper.CreateCircle(FabRect.X - 1, FabRect.Y - 1, 30), FabHovered?2:1, Color.Black);
            e.Graphics.FillEllipse(Brushes.HotPink, FabRect);
            Pen objLinePen = new Pen(Brushes.White, 3);
            e.Graphics.DrawLine(objLinePen, new Point(Convert.ToInt32(FabRect.Left + FabRect.Width * 0.2), Convert.ToInt32(FabRect.Top + FabRect.Height * 0.55)), new Point(Convert.ToInt32(FabRect.Left + FabRect.Width * 0.4), Convert.ToInt32(FabRect.Top + FabRect.Height * 0.80)));
            e.Graphics.DrawLine(objLinePen, new Point(Convert.ToInt32(FabRect.Left + FabRect.Width * 0.4), Convert.ToInt32(FabRect.Top + FabRect.Height * 0.80)), new Point(Convert.ToInt32(FabRect.Left + FabRect.Width * 0.8), Convert.ToInt32(FabRect.Top + FabRect.Height * 0.30)));

            e.Graphics.ResetClip();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Location = _BaseForm.Location;
            this.Size = _BaseForm.Size;
            objAnimationManager.SetProgress(0);
            _Origin = PointToClient(_Origin); ;
            objAnimationManager.StartNewAnimation(AnimationDirection.In);
        }

        private Rectangle CalculateCurrentRect()
        {
            Rectangle objResult = new Rectangle();

            double xEdge = (Width / 2 >= _Origin.X ? Width : 0);
            double YEdge = (Height / 2 >= _Origin.Y ? Height : 0);


            double radiusMax = Math.Sqrt(Math.Pow(_Origin.X - xEdge, 2) + Math.Pow(_Origin.Y - YEdge, 2));
            radiusMax *= 2;
            double radius = radiusMax * objAnimationManager.GetProgress();
            double top = _Origin.Y - (radius / 2);
            double Left = _Origin.X - (radius / 2);

            objResult.Location = new Point((int)Left, (int)top);
            objResult.Size = new Size((int)radius, (int)radius);

            return objResult;
        }

        private void initColorHints()
        {

            FabRect = new Rectangle(_BaseForm.Width - 90, _BaseForm.Height - 90, 60, 60);

            PrimaryRect = new Rectangle(10, 10, (int)(_BaseForm.Width / 2 - 10) / 3, (int)((_BaseForm.Height - 20) * 0.1));
            DarkPrimaryRect = new Rectangle(PrimaryRect.Right, 10, (int)(_BaseForm.Width / 2 - 10) / 3, (int)((_BaseForm.Height - 20) * 0.1));
            LightPrimaryRect = new Rectangle(DarkPrimaryRect.Right, 10, (int)(_BaseForm.Width / 2 - 10) / 3, (int)((_BaseForm.Height - 20) * 0.1));

            AccentRect = new Rectangle(10, PrimaryRect.Bottom, (int)(_BaseForm.Width / 2 - 10) / 2, (int)((_BaseForm.Height - 20) * 0.1));
            TextShadeRect = new Rectangle(AccentRect.Right, PrimaryRect.Bottom, (int)(_BaseForm.Width / 2 - 10) / 2, (int)((_BaseForm.Height - 20) * 0.1));

            objPreview = new CurrentThemePreview(new ColorScheme(_Primary, _PrimaryDark, _PrimaryLight, _Accent, _Text), new Point(LightPrimaryRect.Right + 10, 10), new Size((int)_BaseForm.Width / 2 - 20, Convert.ToInt32(_BaseForm.Height * 0.2 - 10)));



            ColorRectangles = new Dictionary<String, List<ColorRect>>();
            int x = 0;
            int y = 0;
            int BaseHeight = Convert.ToInt32(_BaseForm.Height * 0.2) + 10;

            int iWidht;
            int iHeight;
            int iMaxX;
            int iMaxY;

            List<Primary> primarys = new List<Primary>();
            primarys.Add(Primary.Red50);
            primarys.Add(Primary.Red100);
            primarys.Add(Primary.Red200);
            primarys.Add(Primary.Red300);
            primarys.Add(Primary.Red400);
            primarys.Add(Primary.Red500);
            primarys.Add(Primary.Red600);
            primarys.Add(Primary.Red700);
            primarys.Add(Primary.Red800);
            primarys.Add(Primary.Red900);

            primarys.Add(Primary.Pink50);
            primarys.Add(Primary.Pink100);
            primarys.Add(Primary.Pink200);
            primarys.Add(Primary.Pink300);
            primarys.Add(Primary.Pink400);
            primarys.Add(Primary.Pink500);
            primarys.Add(Primary.Pink600);
            primarys.Add(Primary.Pink700);
            primarys.Add(Primary.Pink800);
            primarys.Add(Primary.Pink900);

            primarys.Add(Primary.Purple50);
            primarys.Add(Primary.Purple100);
            primarys.Add(Primary.Purple200);
            primarys.Add(Primary.Purple300);
            primarys.Add(Primary.Purple400);
            primarys.Add(Primary.Purple500);
            primarys.Add(Primary.Purple600);
            primarys.Add(Primary.Purple700);
            primarys.Add(Primary.Purple800);
            primarys.Add(Primary.Purple900);

            primarys.Add(Primary.DeepPurple50);
            primarys.Add(Primary.DeepPurple100);
            primarys.Add(Primary.DeepPurple200);
            primarys.Add(Primary.DeepPurple300);
            primarys.Add(Primary.DeepPurple400);
            primarys.Add(Primary.DeepPurple500);
            primarys.Add(Primary.DeepPurple600);
            primarys.Add(Primary.DeepPurple700);
            primarys.Add(Primary.DeepPurple800);
            primarys.Add(Primary.DeepPurple900);

            primarys.Add(Primary.Indigo50);
            primarys.Add(Primary.Indigo100);
            primarys.Add(Primary.Indigo200);
            primarys.Add(Primary.Indigo300);
            primarys.Add(Primary.Indigo400);
            primarys.Add(Primary.Indigo500);
            primarys.Add(Primary.Indigo600);
            primarys.Add(Primary.Indigo700);
            primarys.Add(Primary.Indigo800);
            primarys.Add(Primary.Indigo900);

            primarys.Add(Primary.Blue50);
            primarys.Add(Primary.Blue100);
            primarys.Add(Primary.Blue200);
            primarys.Add(Primary.Blue300);
            primarys.Add(Primary.Blue400);
            primarys.Add(Primary.Blue500);
            primarys.Add(Primary.Blue600);
            primarys.Add(Primary.Blue700);
            primarys.Add(Primary.Blue800);
            primarys.Add(Primary.Blue900);

            primarys.Add(Primary.LightBlue50);
            primarys.Add(Primary.LightBlue100);
            primarys.Add(Primary.LightBlue200);
            primarys.Add(Primary.LightBlue300);
            primarys.Add(Primary.LightBlue400);
            primarys.Add(Primary.LightBlue500);
            primarys.Add(Primary.LightBlue600);
            primarys.Add(Primary.LightBlue700);
            primarys.Add(Primary.LightBlue800);
            primarys.Add(Primary.LightBlue900);

            primarys.Add(Primary.Cyan50);
            primarys.Add(Primary.Cyan100);
            primarys.Add(Primary.Cyan200);
            primarys.Add(Primary.Cyan300);
            primarys.Add(Primary.Cyan400);
            primarys.Add(Primary.Cyan500);
            primarys.Add(Primary.Cyan600);
            primarys.Add(Primary.Cyan700);
            primarys.Add(Primary.Cyan800);
            primarys.Add(Primary.Cyan900);

            primarys.Add(Primary.Teal50);
            primarys.Add(Primary.Teal100);
            primarys.Add(Primary.Teal200);
            primarys.Add(Primary.Teal300);
            primarys.Add(Primary.Teal400);
            primarys.Add(Primary.Teal500);
            primarys.Add(Primary.Teal600);
            primarys.Add(Primary.Teal700);
            primarys.Add(Primary.Teal800);
            primarys.Add(Primary.Teal900);

            primarys.Add(Primary.Green50);
            primarys.Add(Primary.Green100);
            primarys.Add(Primary.Green200);
            primarys.Add(Primary.Green300);
            primarys.Add(Primary.Green400);
            primarys.Add(Primary.Green500);
            primarys.Add(Primary.Green600);
            primarys.Add(Primary.Green700);
            primarys.Add(Primary.Green800);
            primarys.Add(Primary.Green900);

            primarys.Add(Primary.LightGreen50);
            primarys.Add(Primary.LightGreen100);
            primarys.Add(Primary.LightGreen200);
            primarys.Add(Primary.LightGreen300);
            primarys.Add(Primary.LightGreen400);
            primarys.Add(Primary.LightGreen500);
            primarys.Add(Primary.LightGreen600);
            primarys.Add(Primary.LightGreen700);
            primarys.Add(Primary.LightGreen800);
            primarys.Add(Primary.LightGreen900);

            primarys.Add(Primary.Lime50);
            primarys.Add(Primary.Lime100);
            primarys.Add(Primary.Lime200);
            primarys.Add(Primary.Lime300);
            primarys.Add(Primary.Lime400);
            primarys.Add(Primary.Lime500);
            primarys.Add(Primary.Lime600);
            primarys.Add(Primary.Lime700);
            primarys.Add(Primary.Lime800);
            primarys.Add(Primary.Lime900);

            primarys.Add(Primary.Yellow50);
            primarys.Add(Primary.Yellow100);
            primarys.Add(Primary.Yellow200);
            primarys.Add(Primary.Yellow300);
            primarys.Add(Primary.Yellow400);
            primarys.Add(Primary.Yellow500);
            primarys.Add(Primary.Yellow600);
            primarys.Add(Primary.Yellow700);
            primarys.Add(Primary.Yellow800);
            primarys.Add(Primary.Yellow900);

            primarys.Add(Primary.Amber50);
            primarys.Add(Primary.Amber100);
            primarys.Add(Primary.Amber200);
            primarys.Add(Primary.Amber300);
            primarys.Add(Primary.Amber400);
            primarys.Add(Primary.Amber500);
            primarys.Add(Primary.Amber600);
            primarys.Add(Primary.Amber700);
            primarys.Add(Primary.Amber800);
            primarys.Add(Primary.Amber900);

            primarys.Add(Primary.Orange50);
            primarys.Add(Primary.Orange100);
            primarys.Add(Primary.Orange200);
            primarys.Add(Primary.Orange300);
            primarys.Add(Primary.Orange400);
            primarys.Add(Primary.Orange500);
            primarys.Add(Primary.Orange600);
            primarys.Add(Primary.Orange700);
            primarys.Add(Primary.Orange800);
            primarys.Add(Primary.Orange900);

            primarys.Add(Primary.DeepOrange50);
            primarys.Add(Primary.DeepOrange100);
            primarys.Add(Primary.DeepOrange200);
            primarys.Add(Primary.DeepOrange300);
            primarys.Add(Primary.DeepOrange400);
            primarys.Add(Primary.DeepOrange500);
            primarys.Add(Primary.DeepOrange600);
            primarys.Add(Primary.DeepOrange700);
            primarys.Add(Primary.DeepOrange800);
            primarys.Add(Primary.DeepOrange900);

            primarys.Add(Primary.Brown50);
            primarys.Add(Primary.Brown100);
            primarys.Add(Primary.Brown200);
            primarys.Add(Primary.Brown300);
            primarys.Add(Primary.Brown400);
            primarys.Add(Primary.Brown500);
            primarys.Add(Primary.Brown600);
            primarys.Add(Primary.Brown700);
            primarys.Add(Primary.Brown800);
            primarys.Add(Primary.Brown900);

            primarys.Add(Primary.Grey50);
            primarys.Add(Primary.Grey100);
            primarys.Add(Primary.Grey200);
            primarys.Add(Primary.Grey300);
            primarys.Add(Primary.Grey400);
            primarys.Add(Primary.Grey500);
            primarys.Add(Primary.Grey600);
            primarys.Add(Primary.Grey700);
            primarys.Add(Primary.Grey800);
            primarys.Add(Primary.Grey900);

            primarys.Add(Primary.BlueGrey50);
            primarys.Add(Primary.BlueGrey100);
            primarys.Add(Primary.BlueGrey200);
            primarys.Add(Primary.BlueGrey300);
            primarys.Add(Primary.BlueGrey400);
            primarys.Add(Primary.BlueGrey500);
            primarys.Add(Primary.BlueGrey600);
            primarys.Add(Primary.BlueGrey700);
            primarys.Add(Primary.BlueGrey800);
            primarys.Add(Primary.BlueGrey900);

            primarys.Add(Primary.Black);
            primarys.Add(Primary.White);

            List<Accent> accent = new List<Accent>();
            accent.Add(Accent.Red100);
            accent.Add(Accent.Red200);
            accent.Add(Accent.Red400);
            accent.Add(Accent.Red700);

            accent.Add(Accent.Pink100);
            accent.Add(Accent.Pink200);
            accent.Add(Accent.Pink400);
            accent.Add(Accent.Pink700);

            accent.Add(Accent.Purple100);
            accent.Add(Accent.Purple200);
            accent.Add(Accent.Purple400);
            accent.Add(Accent.Purple700);

            accent.Add(Accent.DeepPurple100);
            accent.Add(Accent.DeepPurple200);
            accent.Add(Accent.DeepPurple400);
            accent.Add(Accent.DeepPurple700);

            accent.Add(Accent.Indigo100);
            accent.Add(Accent.Indigo200);
            accent.Add(Accent.Indigo400);
            accent.Add(Accent.Indigo700);

            accent.Add(Accent.Blue100);
            accent.Add(Accent.Blue200);
            accent.Add(Accent.Blue400);
            accent.Add(Accent.Blue700);

            accent.Add(Accent.LightBlue100);
            accent.Add(Accent.LightBlue200);
            accent.Add(Accent.LightBlue400);
            accent.Add(Accent.LightBlue700);

            accent.Add(Accent.Cyan100);
            accent.Add(Accent.Cyan200);
            accent.Add(Accent.Cyan400);
            accent.Add(Accent.Cyan700);

            accent.Add(Accent.Teal100);
            accent.Add(Accent.Teal200);
            accent.Add(Accent.Teal400);
            accent.Add(Accent.Teal700);

            accent.Add(Accent.Green100);
            accent.Add(Accent.Green200);
            accent.Add(Accent.Green400);
            accent.Add(Accent.Green700);

            accent.Add(Accent.LightGreen100);
            accent.Add(Accent.LightGreen200);
            accent.Add(Accent.LightGreen400);
            accent.Add(Accent.LightGreen700);

            accent.Add(Accent.Lime100);
            accent.Add(Accent.Lime200);
            accent.Add(Accent.Lime400);
            accent.Add(Accent.Lime700);

            accent.Add(Accent.Yellow100);
            accent.Add(Accent.Yellow200);
            accent.Add(Accent.Yellow400);
            accent.Add(Accent.Yellow700);

            accent.Add(Accent.Amber100);
            accent.Add(Accent.Amber200);
            accent.Add(Accent.Amber400);
            accent.Add(Accent.Amber700);

            accent.Add(Accent.Orange100);
            accent.Add(Accent.Orange200);
            accent.Add(Accent.Orange400);
            accent.Add(Accent.Orange700);

            accent.Add(Accent.DeepOrange100);
            accent.Add(Accent.DeepOrange200);
            accent.Add(Accent.DeepOrange400);
            accent.Add(Accent.DeepOrange700);

            accent.Add(Accent.Black);
            accent.Add(Accent.White);

            List<TextShade> textShade = new List<TextShade>();

            textShade.Add(TextShade.BLACK);
            textShade.Add(TextShade.WHITE);

            iMaxX = 20;
            iMaxY = 10;


            iWidht = _BaseForm.Width / iMaxX + 1;
            iHeight = (_BaseForm.Height - BaseHeight) / (iMaxY + 1);

            ColorRectangles.Add(_PrimaryKey, new List<ColorRect>());
            foreach (Primary objPrimary in primarys)
            {
                if (y == iMaxY)
                {
                    y = 0;
                    x++;
                }

                ColorRectangles[_PrimaryKey].Add(new ColorRect(new Rectangle(iWidht * x, iHeight * y + BaseHeight, iWidht, iHeight), ((int)objPrimary).ToColor(), objPrimary));
                y++;
            }



            iMaxX = 17;
            iMaxY = 4;
            x = 0;
            y = 0;


            iWidht = _BaseForm.Width / iMaxX + 1;
            iHeight = (_BaseForm.Height - BaseHeight) / (iMaxY + 1);

            ColorRectangles.Add(_AccentKey, new List<ColorRect>());
            foreach (Accent objAccent in accent)
            {
                if (y == iMaxY)
                {
                    y = 0;
                    x++;
                }

                ColorRectangles[_AccentKey].Add(new ColorRect(new Rectangle(iWidht * x, iHeight * y + BaseHeight, iWidht, iHeight), ((int)objAccent).ToColor(), objAccent));
                y++;
            }



            iMaxX = 2;
            iMaxY = 1;
            x = 0;
            y = 0;

            iWidht = _BaseForm.Width / iMaxX + 1;
            iHeight = (_BaseForm.Height - BaseHeight) / (iMaxY + 1);

            ColorRectangles.Add(_TextShadeKey, new List<ColorRect>());
            foreach (TextShade objTextShade in textShade)
            {
                if (y == iMaxY)
                {
                    y = 0;
                    x++;
                }

                ColorRectangles[_TextShadeKey].Add(new ColorRect(new Rectangle(iWidht * x, iHeight * y + BaseHeight, iWidht, iHeight), ((int)objTextShade).ToColor(), objTextShade));
                y++;
            }


        }

    }

    class ColorRect
    {
        public Rectangle Rect;
        public Color Color;
        public Object Tag;

        public ColorRect(Rectangle pRect, Color pColor, Object pTag)
        {
            Rect = pRect;
            Color = pColor;
            Tag = pTag;
        }
    }

    class CurrentThemePreview
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private ColorScheme PreviewPreset;
        private Rectangle TopDark, TopDefault, Fab;
        private SolidBrush PrimaryDark, Primary, Accent, Text;
        private int Width;
        private Size Size;
        public CurrentThemePreview(ColorScheme SchemeToPreview, Point Location, Size pSize)
        {
            Size = pSize; //new Size(200, 110);
            TopDark = new Rectangle(Location.X + 0, Location.Y + 0, Size.Width, 20);
            TopDefault = new Rectangle(Location.X + 0, TopDark.Bottom, Size.Width, Size.Height - TopDark.Height - 40);
            Fab = new Rectangle(Location.X + Size.Width - 60, Location.Y + TopDefault.Bottom - 20, 40, 40);
            setSchemeToPreview(SchemeToPreview);
        }


        public void setSchemeToPreview(ColorScheme SchemeToPreview)
        {
            PreviewPreset = SchemeToPreview;
            PrimaryDark = new SolidBrush(PreviewPreset.DarkPrimaryColor);
            Primary = new SolidBrush(PreviewPreset.PrimaryColor);
            Accent = new SolidBrush(PreviewPreset.AccentColor);
            Text = new SolidBrush(PreviewPreset.TextColor);
        }

        public void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsPath objPath = new GraphicsPath();
            g.FillRectangle(PrimaryDark, TopDark);
            g.FillRectangle(Primary, TopDefault);
            DrawHelper.drawShadow(g, DrawHelper.CreateCircle(Fab.X - 1, Fab.Y - 1, 20), 2, Color.Black);
            g.FillEllipse(Accent, Fab);
            g.DrawString(
                "Sample Text", SkinManager.ROBOTO_MEDIUM_10, Text, TopDefault.Location);
        }


    }


}
