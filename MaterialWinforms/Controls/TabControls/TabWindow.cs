using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialWinforms.Controls
{
    public class TabWindow: MaterialForm
    {
        private MaterialTabPage TabPage;
        private MaterialTabSelector BaseTabControl;
        private MaterialTabControl Root;
        private Rectangle ReturnButtonBounds;
        private RetButtonState ReturnButtonState;
        private bool Closable;
        private bool allowClose;

        public TabWindow(MaterialTabPage tabPage,ref MaterialTabSelector baseTab)
        {
            TabPage = tabPage;
            Text = TabPage.Text;
            Root = new MaterialTabControl();
            Root.TabPages.Add(TabPage);
            Root.Dock= System.Windows.Forms.DockStyle.Fill;
            BaseTabControl = baseTab;
            Closable = tabPage.Closable;
            Size = TabPage.Size;
            Controls.Add(Root);
            allowClose = false;
        }

        protected enum RetButtonState
        {
            ReturnButtonDown,
            ReturnButtonOver,
            None
        }

        protected override void UpdateButtons(MouseEventArgs e, bool up = false)
        {
            base.UpdateButtons(e, up);
            RetButtonState oldState = ReturnButtonState;
            if (e.Button == MouseButtons.Left && !up)
            {

                if (ReturnButtonBounds.Contains(e.Location))
                {
                    ReturnButtonState = RetButtonState.ReturnButtonDown;
                   
                }
                else
                    ReturnButtonState = RetButtonState.None;
            }
            else
            {
                if (ReturnButtonBounds.Contains(e.Location))
                {
                    ReturnButtonState = RetButtonState.ReturnButtonOver;

                    if (oldState == RetButtonState.ReturnButtonDown)
                        Return();
                }

                else ReturnButtonState = RetButtonState.None;
            }

            if (oldState != ReturnButtonState) Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ReturnButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) -  4 * STATUS_BAR_BUTTON_WIDTH , 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);

        }

        private void Return()
        {
            Root.TabPages.Remove(TabPage);
            BaseTabControl.BaseTabControl.TabPages.Add(TabPage);
            BaseTabControl.Invalidate();
            allowClose = true;
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = !allowClose;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if (!Closable)
            {
                g.FillRectangle(SkinManager.ColorScheme.DarkPrimaryBrush, xButtonBounds);
            }

            var downBrush = SkinManager.GetFlatButtonPressedBackgroundBrush();
            if (ReturnButtonState == RetButtonState.ReturnButtonOver )
                g.FillRectangle(downBrush, ReturnButtonBounds);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var DrawerButtonPen = new Pen(SkinManager.ACTION_BAR_TEXT_SECONDARY(), 2))
            {
                g.DrawLine(
                   DrawerButtonPen,
                   ReturnButtonBounds.X + (int)(ReturnButtonBounds.Width * (0.75)),
                   ReturnButtonBounds.Y + (int)(ReturnButtonBounds.Height * (0.5)),
                   ReturnButtonBounds.X + (int)(ReturnButtonBounds.Width * (0.25)),
                   ReturnButtonBounds.Y + (int)(ReturnButtonBounds.Height * (0.5)));
                g.DrawLine(
                   DrawerButtonPen,
                   ReturnButtonBounds.X + (int)(ReturnButtonBounds.Width * (0.5)),
                   ReturnButtonBounds.Y + (int)(ReturnButtonBounds.Height * (0.3)),
                   ReturnButtonBounds.X + (int)(ReturnButtonBounds.Width * (0.25)),
                   ReturnButtonBounds.Y + (int)(ReturnButtonBounds.Height * (0.5)));
                g.DrawLine(
                  DrawerButtonPen,
                  ReturnButtonBounds.X + (int)(ReturnButtonBounds.Width * 0.5),
                  ReturnButtonBounds.Y + (int)(ReturnButtonBounds.Height * 0.7),
                  ReturnButtonBounds.X + (int)(ReturnButtonBounds.Width * 0.25),
                  ReturnButtonBounds.Y + (int)(ReturnButtonBounds.Height * 0.5));
            }
        }
    }
}
