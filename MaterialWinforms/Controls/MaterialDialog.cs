using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace MaterialWinforms.Controls
{
    public class MaterialDialog : MaterialForm
    {
        private const int CS_DROPSHADOW = 0x00020000;
        private FontManager _FontManager;
        private static MaterialDialog _msgBox;
        private Panel _plHeader = new Panel();
        private Panel _plFooter = new Panel();
        private Panel _plIcon = new Panel();
        private PictureBox _picIcon = new PictureBox();
        private FlowLayoutPanel _flpButtons = new FlowLayoutPanel();
        private Label _lblTitle;
        private Label _lblMessage;
        private List<MaterialFlatButton> _buttonCollection = new List<MaterialFlatButton>();
        private static DialogResult _buttonResult = new DialogResult();
        private static Timer _timer;
        

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool MessageBeep(uint type);

        private MaterialDialog(MaterialSkinManager pColor)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Width = 400;
            this.SkinManager.ColorScheme = pColor.ColorScheme;
            this.SkinManager.Theme = pColor.Theme;
            _FontManager = new FontManager();

            _lblTitle = new MaterialLabel();
            _lblTitle.Font = _FontManager.Roboto_Medium15;
            _lblTitle.Dock = DockStyle.Top;
            _lblTitle.Height = 50;

            _lblMessage = new MaterialLabel();
            _lblMessage.ForeColor = Color.DarkGray;
            _lblMessage.Font = _FontManager.Roboto_Medium10;
            _lblMessage.Dock = DockStyle.Fill;

            _flpButtons.FlowDirection = FlowDirection.RightToLeft;
            _flpButtons.Dock = DockStyle.Fill;

            _plHeader.Dock = DockStyle.Fill;
            _plHeader.Padding = new Padding(20);
            _plHeader.BackColor = pColor.GetCardsColor();
            _plHeader.Controls.Add(_lblMessage);
            _plHeader.Controls.Add(_lblTitle);

            _plFooter.Dock = DockStyle.Bottom;
            _plFooter.Padding = new Padding(20);
            _plFooter.BackColor = pColor.GetCardsColor();
            _plFooter.Height = 80;
            _plFooter.Controls.Add(_flpButtons);

            _picIcon.Width = 32;
            _picIcon.Height = 32;
            _picIcon.Location = new Point(30, 50);

            _plIcon.Dock = DockStyle.Left;
            _plIcon.Padding = new Padding(20);
            _plIcon.BackColor = pColor.GetCardsColor();
            _plIcon.Width = 70;
            _plIcon.Controls.Add(_picIcon);

            this.Controls.Add(_plHeader);
            this.Controls.Add(_plIcon);
            this.Controls.Add(_plFooter);
        }

        public static void Show(MaterialForm pForm,string message)
        {
            _msgBox = new MaterialDialog(pForm.SkinManager);
            _msgBox._lblMessage.Text = message;
            _msgBox.ShowDialog();
            MessageBeep(0);
        }

        public static void Show(MaterialForm pForm, string message, string title)
        {
            _msgBox = new MaterialDialog(pForm.SkinManager);
            _msgBox.SkinManager.ColorScheme = pForm.SkinManager.ColorScheme;
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox.Size = MaterialDialog.MessageSize(message);
            _msgBox.ShowDialog();
            MessageBeep(0);
        }

        public static DialogResult Show(MaterialForm pForm, string message, string title, Buttons buttons)
        {
            _msgBox = new MaterialDialog(pForm.SkinManager);
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox._plIcon.Hide();

            MaterialDialog.InitButtons(buttons);

            _msgBox.Size = MaterialDialog.MessageSize(message);
            _msgBox.ShowDialog();
            MessageBeep(0);
            return _buttonResult;
        }

        public static DialogResult Show(MaterialForm pForm, string message, string title, Buttons buttons, Icon icon)
        {
            _msgBox = new MaterialDialog(pForm.SkinManager);
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;

            MaterialDialog.InitButtons(buttons);
            MaterialDialog.InitIcon(icon);

            _msgBox.Size = MaterialDialog.MessageSize(message);
            _msgBox.ShowDialog();
            MessageBeep(0);
            return _buttonResult;
        }

        public static DialogResult Show(MaterialForm pForm, string message, string title, Buttons buttons, Icon icon, AnimateStyle style)
        {
            _msgBox = new MaterialDialog(pForm.SkinManager);
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox.Height = 0;

            MaterialDialog.InitButtons(buttons);
            MaterialDialog.InitIcon(icon);

            _timer = new Timer();
            Size formSize = MaterialDialog.MessageSize(message);

            switch (style)
            {
                case MaterialDialog.AnimateStyle.SlideDown:
                    _msgBox.Size = new Size(formSize.Width, 0);
                    _timer.Interval = 1;
                    _timer.Tag = new AnimateMsgBox(formSize, style);
                    break;

                case MaterialDialog.AnimateStyle.FadeIn:
                    _msgBox.Size = formSize;
                    _msgBox.Opacity = 0;
                    _timer.Interval = 20;
                    _timer.Tag = new AnimateMsgBox(formSize, style);
                    break;

                case MaterialDialog.AnimateStyle.ZoomIn:
                    _msgBox.Size = new Size(formSize.Width + 100, formSize.Height + 100);
                    _timer.Tag = new AnimateMsgBox(formSize, style);
                    _timer.Interval = 1;
                    break;
            }

            _timer.Tick += timer_Tick;
            _timer.Start();

            _msgBox.ShowDialog();
            MessageBeep(0);
            return _buttonResult;
        }

        static void timer_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            AnimateMsgBox animate = (AnimateMsgBox)timer.Tag;

            switch(animate.Style){
                case MaterialDialog.AnimateStyle.SlideDown:
                    if (_msgBox.Height < animate.FormSize.Height)
                    {
                        _msgBox.Height += 17;
                        _msgBox.Invalidate();
                    }
                    else
                    {
                        _timer.Stop();
                        _timer.Dispose();
                    }
                    break;

                case MaterialDialog.AnimateStyle.FadeIn:
                    if (_msgBox.Opacity < 1)
                    {
                        _msgBox.Opacity += 0.1;
                        _msgBox.Invalidate();
                    }
                    else
                    {
                        _timer.Stop();
                        _timer.Dispose();
                    }
                    break;

                case MaterialDialog.AnimateStyle.ZoomIn:
                    if (_msgBox.Width > animate.FormSize.Width )
                    {
                        _msgBox.Width -= 17;
                        _msgBox.Invalidate();
                    }
                    if (_msgBox.Height > animate.FormSize.Height)
                    {
                        _msgBox.Height -= 17;
                        _msgBox.Invalidate();
                    }
                    break;
            }
        }

        private static void InitButtons(Buttons buttons)
        {
            switch (buttons)
            {
                case MaterialDialog.Buttons.AbortRetryIgnore:
                    _msgBox.InitAbortRetryIgnoreButtons();
                    break;

                case MaterialDialog.Buttons.OK:
                    _msgBox.InitOKButton();
                    break;

                case MaterialDialog.Buttons.OKCancel:
                    _msgBox.InitOKCancelButtons();
                    break;

                case MaterialDialog.Buttons.RetryCancel:
                    _msgBox.InitRetryCancelButtons();
                    break;

                case MaterialDialog.Buttons.YesNo:
                    _msgBox.InitYesNoButtons();
                    break;

                case MaterialDialog.Buttons.YesNoCancel:
                    _msgBox.InitYesNoCancelButtons();
                    break;
            }

            foreach (Button btn in _msgBox._buttonCollection)
            {
                btn.Font = new FontManager().Roboto_Medium10;
                btn.Padding = new Padding(3);
                btn.FlatStyle = FlatStyle.Flat;
                btn.Height = 30;
                _msgBox._flpButtons.Controls.Add(btn);
            }
        }

        private static void InitIcon(Icon icon)
        {
            switch (icon)
            {
                case MaterialDialog.Icon.Application:
                    _msgBox._picIcon.Image = SystemIcons.Application.ToBitmap();
                    break;

                case MaterialDialog.Icon.Exclamation:
                    _msgBox._picIcon.Image = SystemIcons.Exclamation.ToBitmap();
                    break;

                case MaterialDialog.Icon.Error:
                    _msgBox._picIcon.Image = SystemIcons.Error.ToBitmap();
                    break;

                case MaterialDialog.Icon.Info:
                    _msgBox._picIcon.Image = SystemIcons.Information.ToBitmap();
                    break;

                case MaterialDialog.Icon.Question:
                    _msgBox._picIcon.Image = SystemIcons.Question.ToBitmap();
                    break;

                case MaterialDialog.Icon.Shield:
                    _msgBox._picIcon.Image = SystemIcons.Shield.ToBitmap();
                    break;

                case MaterialDialog.Icon.Warning:
                    _msgBox._picIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
            }
        }

        private void InitAbortRetryIgnoreButtons()
        {
            MaterialFlatButton btnAbort = new MaterialFlatButton();
            btnAbort.Text = "Abbrechen";
            btnAbort.Accent = true;
            btnAbort.Click += ButtonClick;

            MaterialFlatButton btnRetry = new MaterialFlatButton();
            btnRetry.Text = "Erneut versuchen";
            btnRetry.ForeColor = Color.DarkGray;
            btnRetry.Click += ButtonClick;

            MaterialFlatButton btnIgnore = new MaterialFlatButton();
            btnIgnore.Text = "Ignorieren";
            btnIgnore.ForeColor = Color.DarkGray;
            btnIgnore.Click += ButtonClick;

            this._buttonCollection.Add(btnIgnore);
            this._buttonCollection.Add(btnAbort);
            this._buttonCollection.Add(btnRetry);
            
        }

        private void InitOKButton()
        {
            MaterialFlatButton btnOK = new MaterialFlatButton();
            btnOK.Text = "OK";
            btnOK.Accent = true;
            btnOK.Click += ButtonClick;

            this._buttonCollection.Add(btnOK);
        }

        private void InitOKCancelButtons()
        {
            MaterialFlatButton btnOK = new MaterialFlatButton();
            btnOK.Text = "OK";
            btnOK.Accent = true;
            btnOK.Click += ButtonClick;

            MaterialFlatButton btnCancel = new MaterialFlatButton();
            btnCancel.Text = "Abbrechen";
            btnCancel.ForeColor = Color.DarkGray;
            btnCancel.Click += ButtonClick;

            this._buttonCollection.Add(btnOK);
            this._buttonCollection.Add(btnCancel);
           
        }

        private void InitRetryCancelButtons()
        {
            MaterialFlatButton btnRetry = new MaterialFlatButton();
            btnRetry.Text = "Erneut versuchen";
            btnRetry.Accent = true;
            btnRetry.Click += ButtonClick;

            MaterialFlatButton btnCancel = new MaterialFlatButton();
            btnCancel.Text = "Abbrechen";
            btnCancel.ForeColor = Color.DarkGray;
            btnCancel.Click += ButtonClick;

            this._buttonCollection.Add(btnRetry);
            this._buttonCollection.Add(btnCancel);
            
        }

        private void InitYesNoButtons()
        {
            MaterialFlatButton btnYes = new MaterialFlatButton();
            btnYes.Text = "Ja";
            btnYes.Accent = true;
            btnYes.Click += ButtonClick;

            MaterialFlatButton btnNo = new MaterialFlatButton();
            btnNo.Text = "Nein";
            btnNo.ForeColor = Color.DarkGray;
            btnNo.Click += ButtonClick;

            this._buttonCollection.Add(btnYes);
            this._buttonCollection.Add(btnNo);
            
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void InitYesNoCancelButtons()
        {
            MaterialFlatButton btnYes = new MaterialFlatButton();
            btnYes.Text = "Yes";
            btnYes.Accent = true;
            btnYes.Click += ButtonClick;

            MaterialFlatButton btnNo = new MaterialFlatButton();
            btnNo.Text = "No";
            btnNo.ForeColor = Color.DarkGray;
            btnNo.Click += ButtonClick;

            MaterialFlatButton btnCancel = new MaterialFlatButton();
            btnCancel.Text = "Cancel";
            btnCancel.ForeColor = Color.DarkGray;
            btnCancel.Click += ButtonClick;

            this._buttonCollection.Add(btnYes);
            this._buttonCollection.Add(btnNo);
            this._buttonCollection.Add(btnCancel);
           
        }

        private static void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Text)
            {
                case "Abort":
                    _buttonResult = DialogResult.Abort;
                    break;

                case "Retry":
                    _buttonResult = DialogResult.Retry;
                    break;

                case "Ignore":
                    _buttonResult = DialogResult.Ignore;
                    break;

                case "OK":
                    _buttonResult = DialogResult.OK;
                    break;

                case "Cancel":
                    _buttonResult = DialogResult.Cancel;
                    break;

                case "Yes":
                    _buttonResult = DialogResult.Yes;
                    break;

                case "No":
                    _buttonResult = DialogResult.No;
                    break;
            }

            _msgBox.Dispose();
        }

        private static Size MessageSize(string message)
        {
            Graphics g = _msgBox.CreateGraphics();
            int width=350;
            int height = 230;

            SizeF size = g.MeasureString(message, new FontManager().Roboto_Medium10);

            if (message.Length < 150)
            {
                if ((int)size.Width > 350)
                {
                    width = (int)size.Width;
                }
            }
            else
            {
                string[] groups = (from Match m in Regex.Matches(message, ".{1,180}") select m.Value).ToArray();
                int lines = groups.Length+1;
                width = 700;
                height += (int)(size.Height+10) * lines;
            }
            return new Size(width, height);
        }

       

  

        public enum Buttons
        {
            AbortRetryIgnore=1,
            OK=2,
            OKCancel=3,
            RetryCancel=4,
            YesNo=5,
            YesNoCancel=6
        }

        public enum Icon 
        {
            Application = 1,
            Exclamation = 2,
            Error = 3,
            Warning = 4,
            Info = 5,
            Question = 6,
            Shield = 7,
            Search = 8
        }

        public enum AnimateStyle
        {
            SlideDown=1,
            FadeIn= 2, 
            ZoomIn =3
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MaterialDialog
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MaterialDialog";
            this.ResumeLayout(false);

        }

    }

    class AnimateMsgBox
    {
        public Size FormSize;
        public MaterialDialog.AnimateStyle Style;

        public AnimateMsgBox(Size formSize, MaterialDialog.AnimateStyle style)
        {
            this.FormSize = formSize;
            this.Style = style;
        }
    }
}
