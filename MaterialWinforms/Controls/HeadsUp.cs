using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MaterialWinforms.Animations;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialWinforms.Controls
{
    public class HeadsUp : MaterialForm
    {
        private AnimationManager _AnimationManager;
        private bool Schliessen = false;
        private int StartHeight;
        private FlowLayoutPanel ButtonPanel;
        private bool CloseAnimation = false;
        private int StartX;

        public new Color BackColor
        {
            get
            {
                return SkinManager.GetApplicationBackgroundColor();
            }
        }

        private String _Titel;
        /// <summary>
        /// The Titel of the Form
        /// </summary>
        public String Titel
        {
            get
            {
                return _Titel;
            }
            set
            {
                _Titel = value;
                TitelLabel.Text = _Titel;
                TextLabel.Location = new Point(TextLabel.Location.X, TitelLabel.Bottom + 8);
            }
        }

        
        private String _Text;
        /// <summary>
        /// The Text which gets displayed as the Content
        /// </summary>
        public new String Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                TextLabel.Text = _Text;
            }
        }

        /// <summary>
        /// The Collection for the Buttons
        /// </summary>
        public ObservableCollection<MaterialFlatButton> Buttons {get;set;}

        /// <summary>
        /// The Content Labels
        /// </summary>
        private MaterialLabel TitelLabel, TextLabel;

        /// <summary>
        /// Constructer Setting up the Layout
        /// </summary>
        public HeadsUp()
        {
            SkinManager.AddFormToManage(this);
            TopMost = true;
            ShowInTaskbar = false;
            Width = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width * 0.4);
            _AnimationManager = new AnimationManager();
            _AnimationManager.AnimationType = AnimationType.EaseOut;
            _AnimationManager.Increment = 0.03;
            _AnimationManager.OnAnimationProgress += _AnimationManager_OnAnimationProgress;
            TitelLabel = new MaterialLabel();
            TitelLabel.AutoSize = true;
            TextLabel = new MaterialLabel();
            ButtonPanel = new FlowLayoutPanel();
            ButtonPanel.FlowDirection = FlowDirection.RightToLeft;
            ButtonPanel.Height = 40;
            //ButtonPanel.AutoScroll = true;
            ButtonPanel.Dock = DockStyle.Bottom;
            //ButtonPanel.BackColor = BackColor;
            Controls.Add(ButtonPanel);
            TitelLabel.Location = new Point(20, 10);
            TextLabel.Location = new Point(20, TitelLabel.Bottom + 5);
            TextLabel.AutoSize = true;
            TextLabel.Resize += TextLabel_Resize;
            TextLabel.MaximumSize = new Size(Width, Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * 0.2));
            Controls.Add(TitelLabel);
            Controls.Add(TextLabel);
            Buttons = new ObservableCollection<MaterialFlatButton>();
            Buttons.CollectionChanged += Buttons_CollectionChanged;
        }

        /// <summary>
        /// Sets up The Buttons
        /// </summary>
        void Buttons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ButtonPanel.SuspendLayout();
            ButtonPanel.Controls.Clear();
            if (Buttons.Count > 0) { 
            ButtonPanel.Controls.AddRange(Buttons.ToArray());
            //    ButtonPanel.Height = Buttons.First().Height;
            }
            ButtonPanel.ResumeLayout();
        }

        /// <summary>
        /// Corrects the Size and Location after the Content changes
        /// </summary>
        void TextLabel_Resize(object sender, EventArgs e)
        {
            Height = TextLabel.Bottom + 10 + ButtonPanel.Height;
            StartHeight = Height;
        }

        /// <summary>
        /// Sets up the Starting Location and starts the Animation
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Location = new Point(Convert.ToInt32((Screen.PrimaryScreen.Bounds.Width-Width)/2), -StartHeight);
            StartX = Location.X;
            _AnimationManager.StartNewAnimation(AnimationDirection.In);
        }

        /// <summary>
        /// Animates the Form slides
        /// </summary>
        void _AnimationManager_OnAnimationProgress(object sender)
        {
            if (CloseAnimation)
            {
                Opacity =  _AnimationManager.GetProgress();
                Location = new Point(StartX+Convert.ToInt32((Screen.PrimaryScreen.Bounds.Width-StartX-Width)*(1-_AnimationManager.GetProgress())), Location.Y);
            }
            else
            {
                Location = new Point(Location.X, -StartHeight + Convert.ToInt32((StartHeight * _AnimationManager.GetProgress())));
            }
        }

        /// <summary>
        /// Ovverides the Paint to create the solid colored backcolor
        /// </summary>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
        }

        /// <summary>
        /// Overrides the Closing Event to Animate the Slide Out
        /// </summary>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !Schliessen;
            if (!Schliessen)
            {
                CloseAnimation = true;
                _AnimationManager.Increment = 0.06;
                _AnimationManager.OnAnimationFinished += _AnimationManager_OnAnimationFinished;
                _AnimationManager.StartNewAnimation(AnimationDirection.Out);
            }
            base.OnClosing(e);
        }

        /// <summary>
        /// Closes the Form after the pull out animation
        /// </summary>
        void _AnimationManager_OnAnimationFinished(object sender)
        {
            Schliessen = true;
            Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(347, 300);
            this.Name = "HeadsUp";
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Prevents the Form from beeing dragged
        /// </summary>
        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }

    }
}
