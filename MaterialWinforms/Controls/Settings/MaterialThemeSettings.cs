using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            tgl_Theme.Checked = SkinManager.Theme == MaterialSkinManager.Themes.DARK;
            Ignore = tgl_Theme.Checked;
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


        void objOverlay_FormClosed(object sender, FormClosedEventArgs e)
        {
            tgl_Theme.Focus();
        }

        private void tgl_Theme_onAnimationFinished()
        {
            if (Ignore)
            {
                Ignore = false;
                return;
            }
            Point OverlayOrigin = new Point();
            OverlayOrigin.X = tgl_Theme.Checked ? tgl_Theme.Right - tgl_Theme.Height / 2 : tgl_Theme.Left + tgl_Theme.Height / 2;
            OverlayOrigin.Y = tgl_Theme.Location.Y + tgl_Theme.Height / 3;
            ColorOverlay objOverlay = new ColorOverlay(PointToScreen(OverlayOrigin), (tgl_Theme.Checked ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.LIGHT), _BaseForm);
            objOverlay.FormClosed += objOverlay_FormClosed;
            objOverlay.Show();
        }
    }

     class ThemePreview : Control,IMaterialControl
    {
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
            Size = new Size(200, 110);
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
            GraphicsPath objPath = new GraphicsPath();
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.Clear(SkinManager.GetApplicationBackgroundColor());
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillRectangle(PrimaryDark, TopDark);
            g.FillRectangle(Primary, TopDefault);
            DrawHelper.drawShadow(g, DrawHelper.CreateCircle(Fab.X-1, Fab.Y-1, 20), 2, Color.Black);
            g.FillEllipse(Accent, Fab);

            g.DrawString(
                PreviewPreset.Name,
                 SkinManager.ROBOTO_REGULAR_11,
                 Text, TopDefault);
            g.ResetClip();
            
        }

        
    }

}
