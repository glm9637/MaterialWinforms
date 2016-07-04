using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using MaterialWinforms.Animations;

namespace MaterialWinforms.Controls.Toolbar
{
    public class MaterialToolbar : ToolStrip, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public MaterialToolbar()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            Height = 48;
            Renderer = new MaterialToolStripRender();
        }


        internal class MaterialToolStripRender : ToolStripProfessionalRenderer, IMaterialControl
        {
            //Properties for managing the material design properties
            public int Depth { get; set; }
            public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
            public MouseState MouseState { get; set; }

            public MaterialToolStripRender()
            {
                RoundedEdges = true;
            }


            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                var g = e.Graphics;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                var itemRect = GetItemRect(e.Item);
                var textRect = new Rectangle(24, itemRect.Y, itemRect.Width - (24 + 16), itemRect.Height);
                g.DrawString(
                    e.Text,
                    SkinManager.ROBOTO_MEDIUM_10,
                    e.Item.Enabled ? SkinManager.ColorScheme.TextBrush : SkinManager.GetDisabledOrHintBrush(),
                    textRect,
                    new StringFormat { LineAlignment = StringAlignment.Center });
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                var g = e.Graphics;
                g.Clear(SkinManager.ColorScheme.PrimaryColor);

                //Draw background
                var itemRect = GetItemRect(e.Item);
                g.FillRectangle(e.Item.Selected && e.Item.Enabled ? SkinManager.ColorScheme.LightPrimaryBrush : new SolidBrush(SkinManager.ColorScheme.PrimaryColor), itemRect);

                //Ripple animation
                var toolStrip = e.ToolStrip as MaterialContextMenuStrip;
                if (toolStrip != null)
                {
                    var animationManager = toolStrip.animationManager;
                    var animationSource = toolStrip.animationSource;
                    if (toolStrip.animationManager.IsAnimating() && e.Item.Bounds.Contains(animationSource))
                    {
                        for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                        {
                            var animationValue = animationManager.GetProgress(i);
                            var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationValue * 50)), Color.Black));
                            var rippleSize = (int)(animationValue * itemRect.Width * 2.5);
                            g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, itemRect.Y - itemRect.Height, rippleSize, itemRect.Height * 3));
                        }
                    }
                }
            }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                e.Graphics.FillRectangle(SkinManager.ColorScheme.PrimaryBrush, e.AffectedBounds);
            }


            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
                //base.OnRenderImageMargin(e);
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                var g = e.Graphics;

                g.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), e.Item.Bounds);
                g.DrawLine(
                    new Pen(SkinManager.GetDividersColor()),
                    new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2),
                    new Point(e.Item.Bounds.Right, e.Item.Bounds.Height / 2));
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                var g = e.Graphics;

             /*   g.DrawRectangle(
                    new Pen(SkinManager.getCardsBrush()),
                    new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));*/
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                var g = e.Graphics;
                const int ARROW_SIZE = 4;

                var arrowMiddle = new Point(e.ArrowRectangle.X+2 + e.ArrowRectangle.Width / 2, e.ArrowRectangle.Y + e.ArrowRectangle.Height / 2);
                var arrowBrush = e.Item.Enabled ? SkinManager.ColorScheme.TextBrush : SkinManager.GetDisabledOrHintBrush();
                using (var arrowPath = new GraphicsPath())
                {
                    arrowPath.AddLines(
                        new[] { 
                        new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y - ARROW_SIZE), 
                        new Point(arrowMiddle.X, arrowMiddle.Y), 
                        new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y + ARROW_SIZE) });
                    arrowPath.CloseFigure();

                    g.FillPath(arrowBrush, arrowPath);
                }
            }

            private Rectangle GetItemRect(ToolStripItem item)
            {
                return new Rectangle(0, item.ContentRectangle.Y, item.ContentRectangle.Width + 4, item.ContentRectangle.Height);
            }
        }
    }
}
