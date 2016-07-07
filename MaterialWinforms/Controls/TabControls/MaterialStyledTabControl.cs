using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace MaterialWinforms.Controls
{
    public class MaterialStyledTabControl : System.Windows.Forms.TabControl,IMaterialControl
    {

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private Rectangle xButtonBounds;
        public MaterialStyledTabControl()
            : base()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            this.ItemSize = new Size(150, 40);
            xButtonBounds = new Rectangle((ItemSize.Width - SkinManager.FORM_PADDING / 2) - 24, (ItemSize.Height-24)/2, 24, 24);
            this.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            MouseDown += MaterialStyledTabControl_MouseDown;
        }

        void MaterialStyledTabControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                Rectangle r = GetTabRect(i);
                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.X+xButtonBounds.X, r.Y + xButtonBounds.Y, xButtonBounds.Width, xButtonBounds.Height);
                if (closeButton.Contains(e.Location))
                {
                        TabPages.RemoveAt(i);
                        break;
                    }
                }
            
        }

        

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            // Hintergrund Zeichnen
            e.Graphics.Clear(Parent.BackColor);
            e.Graphics.FillRectangle(SkinManager.ColorScheme.DarkPrimaryBrush, 0, 0, Width, ItemSize.Height+5);
            

            // Tabs Zeichnen
            for (int i = 0; i < base.TabPages.Count; i++)
            {
                if (i != this.SelectedIndex)
                {
                    PaintTab(i, e.Graphics);
                }
            }

            // Gewähltes Tab Zeichnen
            this.PaintSelectedTab(e.Graphics);
        }

        private void PaintTab(int Index, Graphics e)
        {
            // Rechteck des Tabs
            Rectangle TabRect = base.GetTabRect(Index);
            // Abgedunkelte Farbe, für die Line

            GraphicsPath Border = DrawHelper.CreateTopCornerRoundRect(TabRect, 2);

            e.FillPath(SkinManager.ColorScheme.PrimaryBrush, Border);

            
            e.DrawString(TabPages[Index].Text, SkinManager.ROBOTO_MEDIUM_11, SkinManager.ACTION_BAR_TEXT_BRUSH, TabRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            using (var formButtonsPen = new Pen(SkinManager.ACTION_BAR_TEXT_SECONDARY, 2))
            {
                e.DrawLine(
                            formButtonsPen,
                            TabRect.X+xButtonBounds.X + (int)(xButtonBounds.Width * 0.33),
                            TabRect.Y + xButtonBounds.Y + (int)(xButtonBounds.Height * 0.33),
                            TabRect.X + xButtonBounds.X + (int)(xButtonBounds.Width * 0.66),
                            TabRect.Y + xButtonBounds.Y + (int)(xButtonBounds.Height * 0.66)
                       );

                e.DrawLine(
                    formButtonsPen,
                    TabRect.X + xButtonBounds.X + (int)(xButtonBounds.Width * 0.66),
                    TabRect.Y + xButtonBounds.Y + (int)(xButtonBounds.Height * 0.33),
                    TabRect.X + xButtonBounds.X + (int)(xButtonBounds.Width * 0.33),
                    TabRect.Y + xButtonBounds.Y + (int)(xButtonBounds.Height * 0.66));
            }
        }
        private void PaintSelectedTab(Graphics e)
        {
            if (this.SelectedIndex > -1)
            {
                // Rechteck, des Gewählten Tab
                Rectangle TabRect = base.GetTabRect(this.SelectedIndex);
                // Abgedunkelte Farbe für Line
                GraphicsPath Border = DrawHelper.CreateTopCornerRoundRect(TabRect, 4);
                e.FillPath(SkinManager.ColorScheme.LightPrimaryBrush, Border);

                GraphicsPath Shadow = DrawHelper.CreateTopCornerRoundRectWithoutBottom(TabRect.X - 2, TabRect.Y - 2, TabRect.Width + 4, TabRect.Height +2, 4);

                DrawHelper.drawShadow(e, Shadow, 4, SkinManager.ColorScheme.DarkPrimaryColor);

                e.DrawString(TabPages[SelectedIndex].Text, SkinManager.ROBOTO_MEDIUM_11, SkinManager.ACTION_BAR_TEXT_BRUSH, TabRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                using (var formButtonsPen = new Pen(SkinManager.ACTION_BAR_TEXT_SECONDARY, 2))
                {
                    e.DrawLine(
                            formButtonsPen,
                            TabRect.X + xButtonBounds.X + (int)(xButtonBounds.Width * 0.33),
                            TabRect.Y + xButtonBounds.Y + (int)(xButtonBounds.Height * 0.33),
                            TabRect.X + xButtonBounds.X + (int)(xButtonBounds.Width * 0.66),
                            TabRect.Y + xButtonBounds.Y + (int)(xButtonBounds.Height * 0.66)
                       );

                    e.DrawLine(
                        formButtonsPen,
                        TabRect.X + xButtonBounds.X + (int)(xButtonBounds.Width * 0.66),
                        TabRect.Y + xButtonBounds.Y + (int)(xButtonBounds.Height * 0.33),
                        TabRect.X + xButtonBounds.X + (int)(xButtonBounds.Width * 0.33),
                        TabRect.Y + xButtonBounds.Y + (int)(xButtonBounds.Height * 0.66));
                }
            }
        }
    }

}