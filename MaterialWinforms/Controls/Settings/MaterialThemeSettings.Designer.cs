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
            this.materialToggle1 = new MaterialWinforms.Controls.MaterialToggle();
            this.materialPanel1 = new MaterialWinforms.Controls.MaterialPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.materialLabel3 = new MaterialWinforms.Controls.MaterialLabel();
            this.materialDivider1 = new MaterialWinforms.Controls.MaterialDivider();
            this.materialCard1 = new MaterialWinforms.Controls.MaterialCard();
            this.materialLabel2 = new MaterialWinforms.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialWinforms.Controls.MaterialLabel();
            this.materialPanel1.SuspendLayout();
            this.materialCard1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialToggle1
            // 
            this.materialToggle1.Depth = 0;
            this.materialToggle1.EllipseBorderColor = "#3b73d1";
            this.materialToggle1.EllipseColor = "#508ef5";
            this.materialToggle1.Location = new System.Drawing.Point(65, 35);
            this.materialToggle1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialToggle1.Name = "materialToggle1";
            this.materialToggle1.Size = new System.Drawing.Size(47, 19);
            this.materialToggle1.TabIndex = 0;
            this.materialToggle1.onAnimationFinished += materialToggle1_onAnimationFinished;
            // 
            // materialPanel1
            // 
            this.materialPanel1.Controls.Add(this.flowLayoutPanel1);
            this.materialPanel1.Controls.Add(this.materialLabel3);
            this.materialPanel1.Controls.Add(this.materialDivider1);
            this.materialPanel1.Depth = 0;
            this.materialPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.materialPanel1.Location = new System.Drawing.Point(0, 91);
            this.materialPanel1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialPanel1.Name = "materialPanel1";
            this.materialPanel1.Size = new System.Drawing.Size(387, 451);
            this.materialPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(387, 416);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialLabel3.Location = new System.Drawing.Point(8, 13);
            this.materialLabel3.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(104, 19);
            this.materialLabel3.TabIndex = 1;
            this.materialLabel3.Text = "Farbschemata";
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(0, 0);
            this.materialDivider1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(387, 1);
            this.materialDivider1.TabIndex = 0;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // materialCard1
            // 
            this.materialCard1.Controls.Add(this.materialLabel2);
            this.materialCard1.Controls.Add(this.materialLabel1);
            this.materialCard1.Controls.Add(this.materialToggle1);
            this.materialCard1.Depth = 0;
            this.materialCard1.Elevation = 5;
            this.materialCard1.Location = new System.Drawing.Point(12, 13);
            this.materialCard1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(5, 25, 5, 5);
            graphicsPath1.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialCard1.ShadowBorder = graphicsPath1;
            this.materialCard1.Size = new System.Drawing.Size(200, 64);
            this.materialCard1.TabIndex = 2;
            this.materialCard1.Title = "Theme";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialLabel2.Location = new System.Drawing.Point(137, 33);
            this.materialLabel2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(55, 19);
            this.materialLabel2.TabIndex = 2;
            this.materialLabel2.Text = "Dunkel";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialLabel1.Location = new System.Drawing.Point(9, 33);
            this.materialLabel1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(36, 19);
            this.materialLabel1.TabIndex = 1;
            this.materialLabel1.Text = "Hell";
            // 
            // MaterialThemeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialCard1);
            this.Controls.Add(this.materialPanel1);
            this.Name = "MaterialThemeSettings";
            this.Size = new System.Drawing.Size(387, 542);
            this.materialPanel1.ResumeLayout(false);
            this.materialPanel1.PerformLayout();
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialToggle materialToggle1;
        private MaterialPanel materialPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MaterialLabel materialLabel3;
        private MaterialDivider materialDivider1;
        private MaterialCard materialCard1;
        private MaterialLabel materialLabel2;
        private MaterialLabel materialLabel1;
    }
}
