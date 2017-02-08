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
        

        public new Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : typeof(IShadowedMaterialControl).IsAssignableFrom(Parent.GetType()) ? ((IMaterialControl)Parent).BackColor : Parent.BackColor; } }
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
            
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (ExpandButtonPen == null)
            {
                ExpandButtonPen = new Pen(SkinManager.ColorScheme.TextBrush, 1);
            }
            
            e.Graphics.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), new Rectangle(-2, e.Node.Bounds.Y-2, Width+4, e.Node.Bounds.Height+4));

            Rectangle nodeRect = e.Node.Bounds;
            Rectangle ExpandIconRect = new Rectangle(e.Node.Bounds.X-e.Node.Bounds.Height, e.Node.Bounds.Y + 2, e.Node.Bounds.Height - 4, e.Node.Bounds.Height - 4);
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
            Brush textBrush = SkinManager.ColorScheme.TextBrush;
            //to highlight the text when selected
            if ((e.State & TreeNodeStates.Focused) != 0)
                e.Graphics.FillRectangle(SkinManager.GetFlatButtonHoverBackgroundBrush(), new Rectangle(0, e.Node.Bounds.Y, Width, e.Node.Bounds.Height));

            e.Graphics.DrawString(e.Node.Text, nodeFont, textBrush, NodeBounds(e.Node));

        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(SkinManager.GetApplicationBackgroundColor());
        }

        protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
        {
            base.OnBeforeCollapse(e);
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
