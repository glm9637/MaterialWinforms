using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MaterialWinforms.Controls
{
    public class MaterialTreeControl : TreeView, IMaterialControl
    {

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public new Font Font { get { return SkinManager.ROBOTO_MEDIUM_10; } }


        public new Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : typeof(IShadowedMaterialControl).IsAssignableFrom(Parent.GetType()) ? ((IMaterialControl)Parent).BackColor : Parent.BackColor; } set { } }
        private Pen ExpandButtonPen;

        public MaterialTreeControl()
        {
            this.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            base.BackColor = BackColor;
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (e.Node.Bounds.X != 0)
            {
                e.Graphics.FillRectangle(Brushes.Black, e.Bounds);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                if (ExpandButtonPen == null)
                {
                    ExpandButtonPen = new Pen(SkinManager.GetPrimaryTextBrush(), 1);
                }

                e.Graphics.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), new Rectangle(-2, e.Node.Bounds.Y - 2, Width + 4, e.Node.Bounds.Height + 4));

                Rectangle nodeRect = e.Node.Bounds;
                Rectangle ExpandIconRect = new Rectangle(e.Node.Bounds.X - e.Node.Bounds.Height * (CheckBoxes ? 2 : 1), e.Node.Bounds.Y + 2, e.Node.Bounds.Height - 4, e.Node.Bounds.Height - 4);
                if (e.Node.IsExpanded)
                {
                    PointF pntTopLeft, pntTopRight, pntBottom;
                    pntTopLeft = new PointF(ExpandIconRect.X + (float)(ExpandIconRect.Width * 0.2), ExpandIconRect.Y + (float)(ExpandIconRect.Height * 0.3));
                    pntTopRight = new PointF(ExpandIconRect.X + (float)(ExpandIconRect.Width * 0.8), ExpandIconRect.Y + (float)(ExpandIconRect.Height * 0.3));
                    pntBottom = new PointF(ExpandIconRect.X + (float)(ExpandIconRect.Width * 0.5), ExpandIconRect.Y + (float)(ExpandIconRect.Height * 0.6));
                    e.Graphics.DrawPolygon(ExpandButtonPen, new PointF[] {
                    pntBottom,pntTopLeft
                });

                    e.Graphics.DrawPolygon(ExpandButtonPen, new PointF[] {
                    pntBottom,pntTopRight
                });

                }
                else if (e.Node.GetNodeCount(false) > 0)
                {
                    PointF pntTop, pntRight, pntBottom;
                    pntTop = new PointF(ExpandIconRect.X + (float)(ExpandIconRect.Width * 0.5), ExpandIconRect.Y + (float)(ExpandIconRect.Height * 0.2));
                    pntRight = new PointF(ExpandIconRect.X + (float)(ExpandIconRect.Width * 0.8), ExpandIconRect.Y + (float)(ExpandIconRect.Height * 0.5));
                    pntBottom = new PointF(ExpandIconRect.X + (float)(ExpandIconRect.Width * 0.5), ExpandIconRect.Y + (float)(ExpandIconRect.Height * 0.8));
                    e.Graphics.DrawPolygon(ExpandButtonPen, new PointF[] {
                    pntBottom,pntRight
                });

                    e.Graphics.DrawPolygon(ExpandButtonPen, new PointF[] {
                    pntRight,pntTop
                });
                }


                Font nodeFont = SkinManager.ROBOTO_MEDIUM_10;
                Brush textBrush = SkinManager.GetPrimaryTextBrush();
                //to highlight the text when selected
                if ((e.State & TreeNodeStates.Focused) != 0)
                    e.Graphics.FillRectangle(SkinManager.GetFlatButtonHoverBackgroundBrush(), new Rectangle(0, e.Node.Bounds.Y, Width, e.Node.Bounds.Height));

                e.Graphics.DrawString(e.Node.Text, nodeFont, textBrush, NodeBounds(e.Node));
                DrawCheckbox(e);
            }
        }

        private void DrawCheckbox(DrawTreeNodeEventArgs e)
        {
            if (CheckBoxes == true)
            {
                Rectangle CheckBoxRect = new Rectangle(e.Node.Bounds.X - e.Node.Bounds.Height, e.Node.Bounds.Y + 2, e.Node.Bounds.Height - 4, e.Node.Bounds.Height - 4);

                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;


                using (var checkmarkPath = DrawHelper.CreateRoundRect(CheckBoxRect.X, CheckBoxRect.Y, CheckBoxRect.Width, CheckBoxRect.Height, 1f))
                {
                    SolidBrush brush2 = new SolidBrush(DrawHelper.BlendColor(Parent.BackColor, Enabled ? SkinManager.GetCheckboxOffColor() : SkinManager.GetCheckBoxOffDisabledColor(), 255));
                    Pen pen2 = new Pen(brush2.Color);
                    g.FillPath(brush2, checkmarkPath);
                    g.DrawPath(pen2, checkmarkPath);

                    g.FillRectangle(new SolidBrush(BackColor), CheckBoxRect);
                    g.DrawRectangle(new Pen(BackColor), CheckBoxRect.X + 2, CheckBoxRect.Y + 2, CheckBoxRect.Width - 1, CheckBoxRect.Height - 1);

                    var brush = new SolidBrush(Color.FromArgb(e.Node.Checked ? 255 : 0, Enabled ? SkinManager.ColorScheme.AccentColor : SkinManager.GetCheckBoxOffDisabledColor()));
                    var pen = new Pen(brush.Color);

                    brush2.Dispose();
                    pen2.Dispose();

                    if (Enabled)
                    {
                        g.FillPath(brush, checkmarkPath);
                        g.DrawPath(pen, checkmarkPath);
                    }
                    else if (e.Node.Checked)
                    {
                        g.SmoothingMode = SmoothingMode.None;
                        g.FillRectangle(brush, CheckBoxRect.X + 2, CheckBoxRect.Y + 2, CheckBoxRect.Width - 1, CheckBoxRect.Height - 1);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                    if (e.Node.Checked)
                    {
                        g.DrawImageUnscaledAndClipped(DrawCheckMarkBitmap(CheckBoxRect), CheckBoxRect);
                    }

                }
            }
        }

        private Bitmap DrawCheckMarkBitmap(Rectangle CheckBoxRect)
        {
            var checkMark = new Bitmap(CheckBoxRect.Width, CheckBoxRect.Height);
            var g = Graphics.FromImage(checkMark);
            PointF[] CHECKMARK_LINE = { new PointF((float)(CheckBoxRect.Width / 5), (float)(CheckBoxRect.Height / 1.875)), new PointF((float)(CheckBoxRect.Width / 2.16), (float)(CheckBoxRect.Height / 1.25)), new PointF((float)(CheckBoxRect.Width / 1.075), (float)(CheckBoxRect.Height / 3)) };
            // clear everything, transparent
            g.Clear(Color.Transparent);

            // draw the checkmark lines
            using (var pen = new Pen(Parent.BackColor, 2))
            {
                g.DrawLines(pen, CHECKMARK_LINE);
            }

            return checkMark;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
        }

        private Rectangle NodeBounds(TreeNode node)
        {
            // Set the return value to the normal node bounds.
            return new Rectangle(new Point(node.Bounds.X, node.Bounds.Y), Size.Ceiling(CreateGraphics().MeasureString(node.Text, SkinManager.ROBOTO_MEDIUM_10)));

        }

    }
}
