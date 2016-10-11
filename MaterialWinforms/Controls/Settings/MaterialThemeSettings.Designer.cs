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
            this.materialToggle1 = new MaterialWinforms.Controls.MaterialToggle();
            this.SuspendLayout();
            // 
            // materialToggle1
            // 
            this.materialToggle1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.materialToggle1.AutoSize = true;
            this.materialToggle1.Checked = true;
            this.materialToggle1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.materialToggle1.Depth = 0;
            this.materialToggle1.EllipseBorderColor = "#3b73d1";
            this.materialToggle1.EllipseColor = "#508ef5";
            this.materialToggle1.Location = new System.Drawing.Point(221, 18);
            this.materialToggle1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialToggle1.Name = "materialToggle1";
            this.materialToggle1.Size = new System.Drawing.Size(47, 19);
            this.materialToggle1.TabIndex = 0;
            this.materialToggle1.Text = "materialToggle1";
            this.materialToggle1.UseVisualStyleBackColor = true;
            this.materialToggle1.CheckedChanged += new System.EventHandler(this.materialToggle1_CheckedChanged);
            // 
            // MaterialThemeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialToggle1);
            this.Name = "MaterialThemeSettings";
            this.Size = new System.Drawing.Size(560, 550);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialToggle materialToggle1;
    }
}
