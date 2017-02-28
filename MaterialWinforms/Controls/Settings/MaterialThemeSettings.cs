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
        private MaterialSettings _Parent;
        private bool Ignore;
        private ColorSchemePresetCollection Presets;
        public MaterialThemeSettings(MaterialForm pBaseForm, MaterialSettings pSettings)
        {
            InitializeComponent();
            _Parent = pSettings;
            _BaseForm = pBaseForm;
            tgl_Theme.Checked = SkinManager.Theme == MaterialSkinManager.Themes.DARK;
            Ignore = tgl_Theme.Checked;
            foreach (ColorSchemePreset objPrest in SkinManager.ColorSchemes.List())
            {
                ThemePreview objPreview = new ThemePreview(objPrest);
                objPreview.Click += objPreview_Click;
                flowLayoutPanel1.Controls.Add(objPreview);
            }
            Bitmap bmp = new Bitmap(materialFloatingActionButton1.Width, materialFloatingActionButton1.Height);
            Graphics g = Graphics.FromImage(bmp);
            Pen p = new Pen(Brushes.White, 6);
            g.DrawLine(p, new Point(0, bmp.Height / 2), new Point(bmp.Width, bmp.Height / 2));
            g.DrawLine(p, new Point(bmp.Width / 2, 0), new Point(bmp.Width / 2, bmp.Height));
            materialFloatingActionButton1.Icon = bmp;
        }

        void objPreview_Click(object sender, EventArgs e)
        {
            ThemePreview objPreview =(ThemePreview) sender;

            Point OverlayOrigin = new Point();
            OverlayOrigin = Cursor.Position;
            ColorOverlay objOverlay = new ColorOverlay(OverlayOrigin,objPreview.getColorSchemePreset(), _BaseForm,_Parent);
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
            ColorOverlay objOverlay = new ColorOverlay(PointToScreen(OverlayOrigin), (tgl_Theme.Checked ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.LIGHT), _BaseForm,_Parent);
            objOverlay.FormClosed += objOverlay_FormClosed;
            objOverlay.Show();
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Point OverlayOrigin = new Point();
            OverlayOrigin = Cursor.Position;
            SchemeCreator objScheme = new SchemeCreator(OverlayOrigin, _BaseForm);
            objScheme.FormClosed += newColorScheme;
            objScheme.Show();
        }

        private void newColorScheme(object sender, FormClosedEventArgs e)
        
        {
           
            flowLayoutPanel1.Controls.Clear();
            foreach (ColorSchemePreset objPrest in SkinManager.ColorSchemes.List())
            {
                ThemePreview objPreview = new ThemePreview(objPrest);
                objPreview.Click += objPreview_Click;
                flowLayoutPanel1.Controls.Add(objPreview);
            }
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

            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
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
