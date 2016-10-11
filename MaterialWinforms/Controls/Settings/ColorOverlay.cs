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
        private Brush FillBrush;
        private Brush BackBrush;
        private bool close = false;
        private MaterialForm _BaseForm;
        public ColorOverlay(Point Origin,MaterialSkinManager.Themes Theme,MaterialForm BaseFormToOverlay)
        {
            BackBrush = Brushes.Magenta;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializeComponent();
            //set the backcolor and transparencykey on same color.
            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;
            _ThemeToApply = Theme;
            FillBrush = new SolidBrush(_ThemeToApply == MaterialSkinManager.Themes.DARK ? Color.FromArgb(255, 51, 51, 51) : Color.White);
            _Origin = PointToScreen(Origin);
            _BaseForm = BaseFormToOverlay;
            objAnimationManager = new AnimationManager()
            {
                Increment = 0.02,
                AnimationType = AnimationType.EaseInOut
            };
            DoubleBuffered = true;
            objAnimationManager.OnAnimationProgress += sender => Invalidate();
            objAnimationManager.OnAnimationFinished += objAnimationManager_OnAnimationFinished;
            Visible = false;
        }

        private void objAnimationManager_OnAnimationFinished(object sender)
        {
            if (close)
            {
                this.Close();
            }
            else
            {
                close = true;
                MaterialSkinManager.Instance.Theme = _ThemeToApply;
                objAnimationManager.AnimationType = AnimationType.EaseOut;
                objAnimationManager.SetProgress(0);
                Brush tmpBrush = FillBrush;
                FillBrush = BackBrush;
                BackBrush = tmpBrush;
                objAnimationManager.StartNewAnimation(AnimationDirection.In);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!Visible && objAnimationManager.GetProgress() >0)
            {
                Visible = true;
            }
            e.Graphics.FillRectangle(BackBrush, e.ClipRectangle);

            e.Graphics.FillEllipse(FillBrush, CalculateCurrentRect());
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //this.WindowState = FormWindowState.Maximized; 
            this.Location = _BaseForm.Location;
            this.Size = _BaseForm.Size;
            objAnimationManager.SetProgress(0);
            objAnimationManager.StartNewAnimation(AnimationDirection.In);
        }

        private void ChangeRevealSize()
        {
            if(Controls.Count>0)
            { 
            Rectangle objRect = CalculateCurrentRect();
            Controls[0].Location = objRect.Location;
            Controls[0].Region = new Region(DrawHelper.CreateCircle(objRect.X, objRect.Y, objRect.Width / 2));
            }
        }


        private Rectangle CalculateCurrentRect()
        {
            Rectangle objResult= new Rectangle();

            double xEdge = (Width / 2 >= _Origin.X ? Width : 0);
            double YEdge = (Height / 2 >= _Origin.Y ? Height : 0);


            double radiusMax = Math.Sqrt(Math.Pow(_Origin.X-xEdge, 2) + Math.Pow(_Origin.Y-YEdge, 2));
            radiusMax *= 2;
            double radius = radiusMax * objAnimationManager.GetProgress();
            double top = _Origin.Y- (radius / 2);
            double Left = _Origin.X - (radius / 2);

            objResult.Location = new Point((int)Left,(int)top);
            objResult.Size = new Size((int)radius, (int)radius);

            return objResult;
        }

    }
}
