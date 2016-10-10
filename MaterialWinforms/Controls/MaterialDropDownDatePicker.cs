using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace MaterialWinforms.Controls
{
    public partial class MaterialDropDownDatePicker : DropDownControl, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : Parent.BackColor; } set { } }


        private MaterialDatePicker objDateControl;
        private DateTime _Date;
            public DateTime Date {get { return _Date;}
                set { _Date = value; objDateControl.Date = _Date;
                Text = _Date.ToShortDateString();
                }
            }
        public MaterialDropDownDatePicker()
        {
            InitializeComponent();
            objDateControl = new MaterialDatePicker();
            Date = DateTime.Now;
            objDateControl.onDateChanged += objDateControl_onDateChanged;
            InitializeDropDown(objDateControl);
        }

        void objDateControl_onDateChanged(DateTime newDateTime)
        {
            _Date = newDateTime;
            Text = newDateTime.ToShortDateString();

        }

    }
}
