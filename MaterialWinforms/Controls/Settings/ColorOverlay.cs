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

namespace MaterialWinforms.Controls.Settings
{
    public partial class ColorOverlay : Form
    {

        private AnimationManager objAnimationManager;
        private Point _Origin;
        private MaterialSkinManager.Themes _ThemeToApply;
        private ColorSchemePreset _ColorSchemeToApply;
        private Brush FillBrush;
        private MaterialForm _BaseForm;
        private bool applyTheme;
        private Bitmap Original;
        private Bitmap Final;
        private MaterialSettings _SettingsDialog;
        private Boolean _StyleWurdeGesetzt = false;
        private Pen _ColorSchemePen;

        public ColorOverlay(Point Origin, MaterialSkinManager.Themes Theme, MaterialForm BaseFormToOverlay, MaterialSettings pSettingsDialog)
        {

            _SettingsDialog = pSettingsDialog;
            _BaseForm = BaseFormToOverlay;
            _ThemeToApply = Theme;
            _Origin = Origin;
            applyTheme = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializeComponent();
            objAnimationManager = new AnimationManager()
            {
                Increment = 0.015,
                AnimationType = AnimationType.EaseInOut
            };
            DoubleBuffered = true;
            objAnimationManager.OnAnimationProgress += sender => Invalidate();
            objAnimationManager.OnAnimationFinished += objAnimationManager_OnAnimationFinished;
        }

        private void GenerateOriginalBitmap()
        {
            Original = _SettingsDialog.CreateImage();
        }

        public ColorOverlay(Point Origin, ColorSchemePreset Theme, MaterialForm BaseFormToOverlay, MaterialSettings pSettingsDialog)
        {

            _SettingsDialog = pSettingsDialog;
            _ColorSchemeToApply = Theme;
            _Origin = Origin;
            _BaseForm = BaseFormToOverlay;
            GenerateOriginalBitmap();
            BackgroundImage = Original;
            applyTheme = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializeComponent();
            objAnimationManager = new AnimationManager()
            {
                Increment = 0.015,
                AnimationType = AnimationType.EaseInOut
            };
            DoubleBuffered = true;
            objAnimationManager.OnAnimationProgress += sender => Invalidate();
            objAnimationManager.OnAnimationFinished += objAnimationManager_OnAnimationFinished;
            _ColorSchemePen = new Pen(new SolidBrush(((int)_ColorSchemeToApply.PrimaryColor).ToColor()), 25);
        }

        private void objAnimationManager_OnAnimationFinished(object sender)
        {
            Close();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Opacity = 1;
            if(Final == null)
            {
                if(!_StyleWurdeGesetzt)
                {
                    if(applyTheme)
                    {
                        MaterialSkinManager.Instance.Theme = _ThemeToApply;
                    }
                    else
                    {
                        MaterialSkinManager.Instance.LoadColorSchemeFromPreset(_ColorSchemeToApply);
                    }
                    _StyleWurdeGesetzt = true;
                    return;
                }
                else
                {
                    Final = _SettingsDialog.CreateImage();
                    FillBrush = new TextureBrush(Final);
                }
                
            }
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle CurrentRect = CalculateCurrentRect();
            e.Graphics.FillEllipse(FillBrush,CurrentRect);

            if(!applyTheme)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawEllipse(_ColorSchemePen, CurrentRect);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Opacity = 0;
            base.OnLoad(e);
            this.Location = _BaseForm.Location;
            this.Size = _BaseForm.Size;

            GenerateOriginalBitmap();
            BackgroundImage = Original;
            TopMost = true;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
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

    }
}
