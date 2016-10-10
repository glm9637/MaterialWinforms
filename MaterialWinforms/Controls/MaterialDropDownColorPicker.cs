using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialWinforms.Controls
{
    public partial class MaterialDropDownColorPicker : DropDownControl,IMaterialControl
    {
 [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private MaterialColorPicker objColorControl;
        private Color _Color;
        private Rectangle ColorRect;
        public Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : Parent.BackColor; } set { } }
        public Color Color
        {
            get { return _Color; }
            set
            {
                _Color = value; objColorControl.Value = _Color;
                }
            }
            public MaterialDropDownColorPicker()
        {
            InitializeComponent();
            objColorControl = new MaterialColorPicker();
            Color = SkinManager.ColorScheme.AccentColor;
            objColorControl.onColorChanged += objDateControl_onDateChanged;
            InitializeDropDown(objColorControl);
        }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                ColorRect = new Rectangle();
                ColorRect.Location = new Point(1, 1);
                ColorRect.Size = new Size((int)(Width - 18), (int)(Height * 0.8));

                e.Graphics.FillRectangle(new SolidBrush(Color), ColorRect);
            }

        void objDateControl_onDateChanged(Color newColor)
        {
            Color = newColor;
            Invalidate();
        }
    }
}
