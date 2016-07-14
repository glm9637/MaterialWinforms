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
        private FlowLayoutPanel _flpButtons = new FlowLayoutPanel();
        private List<MaterialFlatButton> _buttonCollection = new List<MaterialFlatButton>();
        private static DialogResult _buttonResult = new DialogResult();
        private Panel pnl_Top;
        private MaterialLabel lbl_Message;
        private Label lbl_Title;
        private Panel pnl_Footer;
        private Panel pnl_Message;
        private static Timer _timer;
        

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool MessageBeep(uint type);

        private MaterialDialog(MaterialSkinManager pColor)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = pColor.GetCardsColor();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Width = 400;
            this.SkinManager.ColorScheme = pColor.ColorScheme;
            this.SkinManager.Theme = pColor.Theme;
            _FontManager = new FontManager();
         

            InitializeComponent();

            

            _flpButtons.FlowDirection = FlowDirection.RightToLeft;
            _flpButtons.Dock = DockStyle.Fill;
            pnl_Footer.Controls.Add(_flpButtons);
            lbl_Title.ForeColor = SkinManager.ColorScheme.AccentColor;
        }

        public static void Show(MaterialForm pForm,string message)
        {
            _msgBox = new MaterialDialog(pForm.SkinManager);
            _msgBox.lbl_Message.Text = message;
            _msgBox.ShowDialog();
            MessageBeep(0);
        }

        public static void Show(MaterialForm pForm, string message, string title)
        {
            _msgBox = new MaterialDialog(pForm.SkinManager);
            _msgBox.SkinManager.ColorScheme = pForm.SkinManager.ColorScheme;
            _msgBox.lbl_Message.Text = message;
            _msgBox.lbl_Title.Text = title;
            _msgBox.ShowDialog();
            MessageBeep(0);
        }

        public static DialogResult Show(MaterialForm pForm, string message, string title, Buttons buttons)
        {
            _msgBox = new MaterialDialog(pForm.SkinManager);
            _msgBox.lbl_Message.Text = message;
            _msgBox.lbl_Title.Text = title;

            MaterialDialog.InitButtons(buttons);

            _msgBox.ShowDialog();
            MessageBeep(0);
            return _buttonResult;
        }

        public static DialogResult Show(MaterialForm pForm, string message, string title, Buttons buttons, Icon icon)
        {
            _msgBox = new MaterialDialog(pForm.SkinManager);
            _msgBox.lbl_Message.Text = message;
            _msgBox.lbl_Title.Text = title;

            MaterialDialog.InitButtons(buttons);
            MaterialDialog.InitIcon(icon);
            _msgBox.ShowDialog();
            MessageBeep(0);
            return _buttonResult;
        }

        public static DialogResult Show(MaterialForm pForm, string message, string title, Buttons buttons, Icon icon, AnimateStyle style)
        {
            _msgBox = new MaterialDialog(pForm.SkinManager);
            _msgBox.lbl_Message.Text = message;
            _msgBox.lbl_Title.Text = title;
            _msgBox.Height = 0;

            MaterialDialog.InitButtons(buttons);
            MaterialDialog.InitIcon(icon);

            _timer = new Timer();
            Size formSize = _msgBox.Size;

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
                    _msgBox.lbl_Title.ForeColor= ((int)Accent.Green200).ToColor();
                    break;

                case MaterialDialog.Icon.Exclamation:
                    _msgBox.lbl_Title.ForeColor = ((int)Accent.Red200).ToColor();
                    break;

                case MaterialDialog.Icon.Error:
                    _msgBox.lbl_Title.ForeColor = ((int)Accent.Red200).ToColor();
                    break;

                case MaterialDialog.Icon.Info:
                    _msgBox.lbl_Title.ForeColor = ((int)Accent.Blue200).ToColor();
                    break;

                case MaterialDialog.Icon.Question:
                    _msgBox.lbl_Title.ForeColor = ((int)Accent.Blue200).ToColor();
                    break;

                case MaterialDialog.Icon.Shield:
                    _msgBox.lbl_Title.ForeColor = ((int)Accent.Yellow200).ToColor();
                    break;

                case MaterialDialog.Icon.Warning:
                    _msgBox.lbl_Title.ForeColor = ((int)Accent.Orange200).ToColor();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialDialog));
            this.pnl_Top = new System.Windows.Forms.Panel();
            this.pnl_Message = new System.Windows.Forms.Panel();
            this.lbl_Message = new MaterialWinforms.Controls.MaterialLabel();
            this.lbl_Title = new Label();
            this.pnl_Footer = new System.Windows.Forms.Panel();
            this.pnl_Top.SuspendLayout();
            this.pnl_Message.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Top
            // 
            this.pnl_Top.Controls.Add(this.pnl_Message);
            this.pnl_Top.Controls.Add(this.lbl_Title);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.Padding = new System.Windows.Forms.Padding(10);
            this.pnl_Top.Size = new System.Drawing.Size(350, 250);
            this.pnl_Top.TabIndex = 1;
            // 
            // pnl_Message
            // 
            this.pnl_Message.AutoScroll = true;
            this.pnl_Message.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_Message.Controls.Add(this.lbl_Message);
            this.pnl_Message.Location = new System.Drawing.Point(12, 41);
            this.pnl_Message.MinimumSize = new System.Drawing.Size(330, 200);
            this.pnl_Message.Name = "pnl_Message";
            this.pnl_Message.Padding = new System.Windows.Forms.Padding(5);
            this.pnl_Message.Size = new System.Drawing.Size(330, 200);
            this.pnl_Message.TabIndex = 2;
            this.pnl_Message.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Message_Paint);
            // 
            // lbl_Message
            // 
            this.lbl_Message.AutoSize = true;
            this.lbl_Message.Depth = 0;
            this.lbl_Message.Font = new System.Drawing.Font("Roboto", 11F);
            this.lbl_Message.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_Message.Location = new System.Drawing.Point(0, 0);
            this.lbl_Message.MaximumSize = new System.Drawing.Size(330, 5000);
            this.lbl_Message.MouseState = MaterialWinforms.MouseState.HOVER;
            this.lbl_Message.Name = "lbl_Message";
            this.lbl_Message.Size = new System.Drawing.Size(330, 5000);
            this.lbl_Message.TabIndex = 1;
            this.lbl_Message.Text = resources.GetString("lbl_Message.Text");
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Title.Font = SkinManager.ROBOTO_MEDIUM_12;
            this.lbl_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_Title.Location = new System.Drawing.Point(10, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(108, 19);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "materialLabel1";
            // 
            // pnl_Footer
            // 
            this.pnl_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Footer.Location = new System.Drawing.Point(0, 250);
            this.pnl_Footer.Name = "pnl_Footer";
            this.pnl_Footer.Size = new System.Drawing.Size(350, 50);
            this.pnl_Footer.TabIndex = 2;
            // 
            // MaterialDialog
            // 
            this.ClientSize = new System.Drawing.Size(350, 300);
            this.Controls.Add(this.pnl_Footer);
            this.Controls.Add(this.pnl_Top);
            this.Name = "MaterialDialog";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Controls.SetChildIndex(this.pnl_Top, 0);
            this.Controls.SetChildIndex(this.pnl_Footer, 0);
            this.pnl_Top.ResumeLayout(false);
            this.pnl_Top.PerformLayout();
            this.pnl_Message.ResumeLayout(false);
            this.pnl_Message.PerformLayout();
            this.ResumeLayout(false);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SkinManager.GetCardsColor());
        }

        private void pnl_Message_Paint(object sender, PaintEventArgs e)
        {

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
