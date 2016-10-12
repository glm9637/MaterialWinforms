using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialWinforms.Controls.Settings
{
    public partial class MaterialThemeSettings : MaterialUserControl
    {
        private MaterialForm _BaseForm;
        private bool Ignore;
        private ColorSchemePresetCollection Presets;
        public MaterialThemeSettings(MaterialForm pBaseForm)
        {
            InitializeComponent();
            _BaseForm = pBaseForm;
            materialToggle1.Checked = SkinManager.Theme == MaterialSkinManager.Themes.DARK;
            Ignore = materialToggle1.Checked;
            Presets = new ColorSchemePresetCollection();
            foreach (ColorSchemePreset objPrest in Presets.List())
            {
                ThemePreview objPreview = new ThemePreview(objPrest);
                objPreview.Click += objPreview_Click;
                flowLayoutPanel1.Controls.Add(objPreview);
            }
        }

        void objPreview_Click(object sender, EventArgs e)
        {
            ThemePreview objPreview =(ThemePreview) sender;

            Point OverlayOrigin = new Point();
            OverlayOrigin = Cursor.Position;
            ColorOverlay objOverlay = new ColorOverlay(OverlayOrigin,objPreview.getColorSchemePreset(), _BaseForm);
            objOverlay.FormClosed += objOverlay_FormClosed;
            objOverlay.Show();

        }

        private void materialToggle1_onAnimationFinished()
        {
            if (Ignore)
            {
                Ignore = false;
                return;
            }
            Point OverlayOrigin = new Point();
            OverlayOrigin.X = materialToggle1.Checked ? materialToggle1.Right - materialToggle1.Height / 2 : materialToggle1.Left + materialToggle1.Height / 2;
            OverlayOrigin.Y = materialToggle1.Location.Y+materialToggle1.Height/3;
            ColorOverlay objOverlay = new ColorOverlay(PointToScreen(OverlayOrigin), (materialToggle1.Checked ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.LIGHT),_BaseForm);
            objOverlay.FormClosed += objOverlay_FormClosed;
            objOverlay.Show();
        }

        void objOverlay_FormClosed(object sender, FormClosedEventArgs e)
        {
            materialToggle1.Focus();
        }
    }

     class ThemePreview : Control,IMaterialControl
    {

        private string _Text;
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private ColorSchemePreset PreviewPreset;
        private Rectangle TopDark,TopDefault,Fab;
        private SolidBrush PrimaryDark, Primary, Accent,Text;
        public ThemePreview(ColorSchemePreset SchemeToPreview)
        {
            PreviewPreset = SchemeToPreview;
            Size = new Size(200, 100);
            TopDark = new Rectangle(0, 0, 200, 20);
            TopDefault = new Rectangle(0, TopDark.Bottom, 200,60);
            Fab = new Rectangle(Width - 60, TopDefault.Bottom - 20, 40, 40);
            PrimaryDark = new SolidBrush(((int)PreviewPreset.DarkPrimaryColor).ToColor());
            Primary = new SolidBrush(((int)PreviewPreset.PrimaryColor).ToColor());
            Accent = new SolidBrush(((int)PreviewPreset.AccentColor).ToColor());
            Text = new SolidBrush(((int)PreviewPreset.TextShade).ToColor());
        }

        public ColorSchemePreset getColorSchemePreset()
        {
            return PreviewPreset;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(SkinManager.GetApplicationBackgroundColor());
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawRectangle(new Pen(SkinManager.getCardsBrush()), ClientRectangle);
            g.FillRectangle(PrimaryDark, TopDark);
            g.FillRectangle(Primary, TopDefault);
            DrawHelper.drawShadow(g, DrawHelper.CreateCircle(Fab.X, Fab.Y, 20), 4, SkinManager.GetApplicationBackgroundColor());
            g.FillEllipse(Accent, Fab);

            g.DrawString(
                PreviewPreset.Name,
                 SkinManager.ROBOTO_REGULAR_11,
                 Text, TopDefault);
            
        }

        
    }

}
