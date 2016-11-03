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

        public new Color BackColor
        {
            get
            {
                return SkinManager.GetApplicationBackgroundColor();
            }
        }

        private String _Titel;
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
        public String Text
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

        public ObservableCollection<MaterialFlatButton> Buttons {get;set;}

        private MaterialLabel TitelLabel, TextLabel;


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
            TextLabel = new MaterialLabel();
            ButtonPanel = new FlowLayoutPanel();
            ButtonPanel.FlowDirection = FlowDirection.RightToLeft;
            ButtonPanel.Height = 40;
            ButtonPanel.Dock = DockStyle.Bottom;
            ButtonPanel.BackColor = BackColor;
            Controls.Add(ButtonPanel);
            TitelLabel.Location = new Point(20, 10);
            TitelLabel.Click += TitelLabel_Click;
            TextLabel.Location = new Point(20, TitelLabel.Bottom + 5);
            TextLabel.AutoSize = true;
            TextLabel.Resize += TextLabel_Resize;
            TextLabel.MaximumSize = new Size(Width, Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * 0.2));
            Controls.Add(TitelLabel);
            Controls.Add(TextLabel);
            Buttons = new ObservableCollection<MaterialFlatButton>();
            Buttons.CollectionChanged += Buttons_CollectionChanged;
        }

        void Buttons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ButtonPanel.Controls.Clear();
            ButtonPanel.Controls.AddRange(Buttons.ToArray());
        }

        void TextLabel_Resize(object sender, EventArgs e)
        {
            Height = TextLabel.Bottom + 10 + ButtonPanel.Height;
            StartHeight = Height;
        }

        void TitelLabel_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Location = new Point(Convert.ToInt32(Width * 0.75), -StartHeight);
            _AnimationManager.StartNewAnimation(AnimationDirection.In);
        }

        void _AnimationManager_OnAnimationProgress(object sender)
        {
            Location = new Point(Location.X, -StartHeight + Convert.ToInt32((StartHeight * _AnimationManager.GetProgress())));
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !Schliessen;
            if (!Schliessen)
            {
                _AnimationManager.OnAnimationFinished += _AnimationManager_OnAnimationFinished;
                _AnimationManager.StartNewAnimation(AnimationDirection.Out);
            }
            base.OnClosing(e);
        }

        void _AnimationManager_OnAnimationFinished(object sender)
        {
            Schliessen = true;
            Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HeadsUp
            // 
            this.ClientSize = new System.Drawing.Size(347, 300);
            this.Name = "HeadsUp";
            this.ResumeLayout(false);

        }
    }
}
