using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialWinforms.Controls
{


    public class MaterialToggle : CheckBox
    {
        #region Variables

        Timer AnimationTimer = new Timer { Interval = 1 };
        GraphicsPath RoundedRectangle;

        string EllipseBG = "#508ef5";
        string EllipseBorder = "#3b73d1";

        Color EllipseBackColor;
        Color EllipseBorderBackColor;

        Color EnabledUnCheckedColor = ColorTranslator.FromHtml("#bcbfc4");
        Color EnabledUnCheckedEllipseBorderColor = ColorTranslator.FromHtml("#a9acb0");

        Color DisabledEllipseBackColor = ColorTranslator.FromHtml("#c3c4c6");
        Color DisabledEllipseBorderBackColor = ColorTranslator.FromHtml("#90949a");

        int PointAnimationNum = 4;
        bool AnimationRunning = false;

        #endregion
        #region  Properties

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : typeof(IShadowedMaterialControl).IsAssignableFrom(Parent.GetType()) ? ((IMaterialControl)Parent).BackColor : Parent.BackColor; } }

        [Category("Appearance")]
        public string EllipseColor
        {
            get { return EllipseBG; }
            set
            {
                EllipseBG = value;
                Invalidate();
            }
        }
        [Category("Appearance")]
        public string EllipseBorderColor
        {
            get { return EllipseBorder; }
            set
            {
                EllipseBorder = value;
                Invalidate();
            }
        }

        #endregion
        #region Events

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AnimationTimer.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            Height = 19; Width = 47;

            RoundedRectangle = new GraphicsPath();
            int radius = 10;

            RoundedRectangle.AddArc(11, 4, radius - 1, radius, 180, 90);
            RoundedRectangle.AddArc(Width - 21, 4, radius - 1, radius, -90, 90);
            RoundedRectangle.AddArc(Width - 21, Height - 15, radius - 1, radius, 0, 90);
            RoundedRectangle.AddArc(11, Height - 15, radius - 1, radius, 90, 90);

            RoundedRectangle.CloseAllFigures();
            Invalidate();
        }

        public delegate void AnimationFinished();
        public event AnimationFinished onAnimationFinished;

        #endregion
        public MaterialToggle()
        {
            Height = 19; Width = 47; DoubleBuffered = true;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var G = pevent.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.Clear(BackColor);

            EllipseBackColor = ColorTranslator.FromHtml(EllipseBG);
            EllipseBorderBackColor = ColorTranslator.FromHtml(EllipseBorder);

            G.FillPath(new SolidBrush(Color.FromArgb(115, Enabled ? Checked ? SkinManager.ColorScheme.AccentColor : EnabledUnCheckedColor : EnabledUnCheckedColor)), RoundedRectangle);
            G.DrawPath(new Pen(Color.FromArgb(50, Enabled ? Checked ? SkinManager.ColorScheme.AccentColor : EnabledUnCheckedColor : EnabledUnCheckedColor)), RoundedRectangle);

            G.FillEllipse(new SolidBrush(Enabled ? Checked ? SkinManager.ColorScheme.AccentColor : Color.White : DisabledEllipseBackColor), PointAnimationNum, 0, 18, 18);
            G.DrawEllipse(new Pen(Enabled ? Checked ? SkinManager.ColorScheme.AccentColor : EnabledUnCheckedEllipseBorderColor : DisabledEllipseBorderBackColor), PointAnimationNum, 0, 18, 18);
        }

        void AnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (PointAnimationNum < 24)
                {
                    PointAnimationNum += 1;
                    AnimationRunning = true;
                    this.Invalidate();
                }
                else if (AnimationRunning)
                {
                    if (onAnimationFinished != null)
                    {
                        onAnimationFinished();
                    }
                    AnimationRunning = false;
                }
            }
            else
            {


                if (PointAnimationNum > 4)
                {
                    PointAnimationNum -= 1;
                    AnimationRunning = true;
                    this.Invalidate();
                }
                else if(AnimationRunning)
                {
                    if (onAnimationFinished != null)
                    {
                        onAnimationFinished();
                    }
                    AnimationRunning = false;
                }
            }
        }
    }
}

