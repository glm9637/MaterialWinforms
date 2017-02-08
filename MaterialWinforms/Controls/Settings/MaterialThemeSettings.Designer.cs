namespace MaterialWinforms.Controls.Settings
{
    partial class MaterialThemeSettings
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Drawing.Drawing2D.GraphicsPath graphicsPath1 = new System.Drawing.Drawing2D.GraphicsPath();
            this.materialPanel1 = new MaterialWinforms.Controls.MaterialPanel();
            this.flowLayoutPanel1 = new MaterialWinforms.Controls.MaterialFlowLayoutPanel();
            this.materialLabel3 = new MaterialWinforms.Controls.MaterialLabel();
            this.materialDivider1 = new MaterialWinforms.Controls.MaterialDivider();
            this.materialLabel2 = new MaterialWinforms.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialWinforms.Controls.MaterialLabel();
            this.tgl_Theme = new MaterialWinforms.Controls.MaterialToggle();
            this.materialLabel4 = new MaterialWinforms.Controls.MaterialLabel();
            this.materialFloatingActionButton1 = new MaterialWinforms.Controls.MaterialFloatingActionButton();
            this.materialPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialPanel1
            // 
            this.materialPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialPanel1.AutoScroll = true;
            this.materialPanel1.Controls.Add(this.flowLayoutPanel1);
            this.materialPanel1.Controls.Add(this.materialLabel3);
            this.materialPanel1.Controls.Add(this.materialDivider1);
            this.materialPanel1.Depth = 0;
            this.materialPanel1.Location = new System.Drawing.Point(3, 98);
            this.materialPanel1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialPanel1.Name = "materialPanel1";
            this.materialPanel1.Size = new System.Drawing.Size(553, 319);
            this.materialPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Depth = 0;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 20);
            this.flowLayoutPanel1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(553, 299);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialLabel3.Location = new System.Drawing.Point(0, 1);
            this.materialLabel3.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(104, 19);
            this.materialLabel3.TabIndex = 1;
            this.materialLabel3.Text = "Farbschemata";
            // 
            // materialDivider1
            // 
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialDivider1.Location = new System.Drawing.Point(0, 0);
            this.materialDivider1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(553, 1);
            this.materialDivider1.TabIndex = 0;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialLabel2.Location = new System.Drawing.Point(158, 58);
            this.materialLabel2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(55, 19);
            this.materialLabel2.TabIndex = 5;
            this.materialLabel2.Text = "Dunkel";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialLabel1.Location = new System.Drawing.Point(30, 58);
            this.materialLabel1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(36, 19);
            this.materialLabel1.TabIndex = 4;
            this.materialLabel1.Text = "Hell";
            // 
            // tgl_Theme
            // 
            this.tgl_Theme.Depth = 0;
            this.tgl_Theme.EllipseBorderColor = "#3b73d1";
            this.tgl_Theme.EllipseColor = "#508ef5";
            this.tgl_Theme.Location = new System.Drawing.Point(91, 58);
            this.tgl_Theme.MouseState = MaterialWinforms.MouseState.HOVER;
            this.tgl_Theme.Name = "tgl_Theme";
            this.tgl_Theme.Size = new System.Drawing.Size(47, 19);
            this.tgl_Theme.TabIndex = 3;
            this.tgl_Theme.onAnimationFinished += new MaterialWinforms.Controls.MaterialToggle.AnimationFinished(this.tgl_Theme_onAnimationFinished);
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialLabel4.Location = new System.Drawing.Point(11, 13);
            this.materialLabel4.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(55, 19);
            this.materialLabel4.TabIndex = 3;
            this.materialLabel4.Text = "Thema";
            // 
            // materialFloatingActionButton1
            // 
            this.materialFloatingActionButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialFloatingActionButton1.Depth = 0;
            this.materialFloatingActionButton1.Elevation = 5;
            this.materialFloatingActionButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.materialFloatingActionButton1.Icon = null;
            this.materialFloatingActionButton1.Location = new System.Drawing.Point(490, 350);
            this.materialFloatingActionButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFloatingActionButton1.Name = "materialFloatingActionButton1";
            graphicsPath1.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialFloatingActionButton1.ShadowBorder = graphicsPath1;
            this.materialFloatingActionButton1.Size = new System.Drawing.Size(48, 48);
            this.materialFloatingActionButton1.TabIndex = 6;
            this.materialFloatingActionButton1.Text = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.UseVisualStyleBackColor = true;
            this.materialFloatingActionButton1.Click += new System.EventHandler(this.materialFloatingActionButton1_Click);
            // 
            // MaterialThemeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialFloatingActionButton1);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.tgl_Theme);
            this.Controls.Add(this.materialPanel1);
            this.Name = "MaterialThemeSettings";
            this.Size = new System.Drawing.Size(559, 417);
            this.materialPanel1.ResumeLayout(false);
            this.materialPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialPanel materialPanel1;
        private MaterialFlowLayoutPanel flowLayoutPanel1;
        private MaterialLabel materialLabel3;
        private MaterialDivider materialDivider1;
        private MaterialLabel materialLabel2;
        private MaterialLabel materialLabel1;
        private MaterialToggle tgl_Theme;
        private MaterialLabel materialLabel4;
        private MaterialFloatingActionButton materialFloatingActionButton1;
    }
}
