using System.Drawing;
using System.Windows.Forms;
using MaterialWinforms;
using MaterialWinforms.Controls;

namespace MaterialWinformsExample
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Drawing.Drawing2D.GraphicsPath graphicsPath1 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath graphicsPath2 = new System.Drawing.Drawing2D.GraphicsPath();
            this.materialFlatButton3 = new MaterialWinforms.Controls.MaterialFlatButton();
            this.materialFlatButton1 = new MaterialWinforms.Controls.MaterialFlatButton();
            this.materialButton1 = new MaterialWinforms.Controls.MaterialRaisedButton();
            this.materialRaisedButton1 = new MaterialWinforms.Controls.MaterialRaisedButton();
            this.materialFloatingActionButton1 = new MaterialWinforms.Controls.MaterialFloatingActionButton();
            this.materialFlatButton2 = new MaterialWinforms.Controls.MaterialFlatButton();
            this.materialContextMenuStrip1 = new MaterialWinforms.Controls.MaterialContextMenuStrip();
            this.tmi_Tabellen = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.tmi_UebersichtTabellen = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_Artikel = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_Struktur = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_Sichten = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.tmi_UebersichtSichten = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_Prozeduren = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.tmi_Uebersicht = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_Funktionen = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.tmi_UebersichtFunktionen = new System.Windows.Forms.ToolStripMenuItem();
            this.materialStyledTabControl1 = new MaterialWinforms.Controls.MaterialStyledTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.materialCard1 = new MaterialWinforms.Controls.MaterialCard();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.materialContextMenuStrip1.SuspendLayout();
            this.materialStyledTabControl1.SuspendLayout();
            this.materialCard1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialFlatButton3
            // 
            this.materialFlatButton3.Accent = false;
            this.materialFlatButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialFlatButton3.AutoSize = true;
            this.materialFlatButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton3.Depth = 0;
            this.materialFlatButton3.Enabled = false;
            this.materialFlatButton3.IconImage = null;
            this.materialFlatButton3.Location = new System.Drawing.Point(744, 790);
            this.materialFlatButton3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton3.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFlatButton3.Name = "materialFlatButton3";
            this.materialFlatButton3.Primary = false;
            this.materialFlatButton3.Selected = false;
            this.materialFlatButton3.Size = new System.Drawing.Size(75, 36);
            this.materialFlatButton3.TabIndex = 19;
            this.materialFlatButton3.Text = "DISABLED";
            this.materialFlatButton3.UseVisualStyleBackColor = true;
            // 
            // materialFlatButton1
            // 
            this.materialFlatButton1.Accent = false;
            this.materialFlatButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialFlatButton1.AutoSize = true;
            this.materialFlatButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton1.Depth = 0;
            this.materialFlatButton1.IconImage = null;
            this.materialFlatButton1.Location = new System.Drawing.Point(969, 790);
            this.materialFlatButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFlatButton1.Name = "materialFlatButton1";
            this.materialFlatButton1.Primary = true;
            this.materialFlatButton1.Selected = false;
            this.materialFlatButton1.Size = new System.Drawing.Size(71, 36);
            this.materialFlatButton1.TabIndex = 1;
            this.materialFlatButton1.Text = "Primary";
            this.materialFlatButton1.UseVisualStyleBackColor = true;
            // 
            // materialButton1
            // 
            this.materialButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.materialButton1.Depth = 0;
            this.materialButton1.Location = new System.Drawing.Point(720, 532);
            this.materialButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.Primary = true;
            this.materialButton1.Size = new System.Drawing.Size(135, 36);
            this.materialButton1.TabIndex = 0;
            this.materialButton1.Text = "Change Theme";
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Location = new System.Drawing.Point(873, 532);
            this.materialRaisedButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(179, 36);
            this.materialRaisedButton1.TabIndex = 21;
            this.materialRaisedButton1.Text = "Change color scheme";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // materialFloatingActionButton1
            // 
            this.materialFloatingActionButton1.BackColor = System.Drawing.Color.White;
            this.materialFloatingActionButton1.Breite = 42;
            this.materialFloatingActionButton1.Depth = 0;
            this.materialFloatingActionButton1.Elevation = 5;
            this.materialFloatingActionButton1.Hoehe = 42;
            this.materialFloatingActionButton1.Icon = global::MaterialWinformsExample.Properties.Resources.ic_action_action_search;
            this.materialFloatingActionButton1.Location = new System.Drawing.Point(1010, 714);
            this.materialFloatingActionButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFloatingActionButton1.Name = "materialFloatingActionButton1";
            graphicsPath1.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialFloatingActionButton1.ShadowBorder = graphicsPath1;
            this.materialFloatingActionButton1.Size = new System.Drawing.Size(42, 42);
            this.materialFloatingActionButton1.TabIndex = 20;
            this.materialFloatingActionButton1.Text = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.UseVisualStyleBackColor = false;
            // 
            // materialFlatButton2
            // 
            this.materialFlatButton2.Accent = false;
            this.materialFlatButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialFlatButton2.AutoSize = true;
            this.materialFlatButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton2.Depth = 0;
            this.materialFlatButton2.IconImage = global::MaterialWinformsExample.Properties.Resources.ic_launcher;
            this.materialFlatButton2.Image = global::MaterialWinformsExample.Properties.Resources.ic_launcher;
            this.materialFlatButton2.Location = new System.Drawing.Point(834, 790);
            this.materialFlatButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFlatButton2.Name = "materialFlatButton2";
            this.materialFlatButton2.Primary = false;
            this.materialFlatButton2.Selected = false;
            this.materialFlatButton2.Size = new System.Drawing.Size(127, 36);
            this.materialFlatButton2.TabIndex = 13;
            this.materialFlatButton2.Text = "Secondary";
            this.materialFlatButton2.UseVisualStyleBackColor = true;
            this.materialFlatButton2.Click += new System.EventHandler(this.materialFlatButton2_Click);
            // 
            // materialContextMenuStrip1
            // 
            this.materialContextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialContextMenuStrip1.Depth = 0;
            this.materialContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_Tabellen,
            this.tmi_Sichten,
            this.tmi_Prozeduren,
            this.tmi_Funktionen});
            this.materialContextMenuStrip1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialContextMenuStrip1.Name = "materialContextMenuStrip1";
            this.materialContextMenuStrip1.Size = new System.Drawing.Size(135, 124);
            // 
            // tmi_Tabellen
            // 
            this.tmi_Tabellen.AutoSize = false;
            this.tmi_Tabellen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_UebersichtTabellen,
            this.tmi_Artikel,
            this.tmi_Struktur});
            this.tmi_Tabellen.Name = "tmi_Tabellen";
            this.tmi_Tabellen.Size = new System.Drawing.Size(120, 30);
            this.tmi_Tabellen.Text = "Tabellen";
            // 
            // tmi_UebersichtTabellen
            // 
            this.tmi_UebersichtTabellen.Name = "tmi_UebersichtTabellen";
            this.tmi_UebersichtTabellen.Size = new System.Drawing.Size(124, 22);
            this.tmi_UebersichtTabellen.Text = "Übersicht";
            // 
            // tmi_Artikel
            // 
            this.tmi_Artikel.Name = "tmi_Artikel";
            this.tmi_Artikel.Size = new System.Drawing.Size(124, 22);
            this.tmi_Artikel.Text = "Artikel";
            // 
            // tmi_Struktur
            // 
            this.tmi_Struktur.Name = "tmi_Struktur";
            this.tmi_Struktur.Size = new System.Drawing.Size(124, 22);
            this.tmi_Struktur.Text = "Struktur";
            // 
            // tmi_Sichten
            // 
            this.tmi_Sichten.AutoSize = false;
            this.tmi_Sichten.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_UebersichtSichten});
            this.tmi_Sichten.Name = "tmi_Sichten";
            this.tmi_Sichten.Size = new System.Drawing.Size(120, 30);
            this.tmi_Sichten.Text = "Sichten";
            // 
            // tmi_UebersichtSichten
            // 
            this.tmi_UebersichtSichten.Name = "tmi_UebersichtSichten";
            this.tmi_UebersichtSichten.Size = new System.Drawing.Size(124, 22);
            this.tmi_UebersichtSichten.Text = "Übersicht";
            // 
            // tmi_Prozeduren
            // 
            this.tmi_Prozeduren.AutoSize = false;
            this.tmi_Prozeduren.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_Uebersicht});
            this.tmi_Prozeduren.Name = "tmi_Prozeduren";
            this.tmi_Prozeduren.Size = new System.Drawing.Size(120, 30);
            this.tmi_Prozeduren.Text = "Prozeduren";
            // 
            // tmi_Uebersicht
            // 
            this.tmi_Uebersicht.Name = "tmi_Uebersicht";
            this.tmi_Uebersicht.Size = new System.Drawing.Size(124, 22);
            this.tmi_Uebersicht.Text = "Übersicht";
            // 
            // tmi_Funktionen
            // 
            this.tmi_Funktionen.AutoSize = false;
            this.tmi_Funktionen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_UebersichtFunktionen});
            this.tmi_Funktionen.Name = "tmi_Funktionen";
            this.tmi_Funktionen.Size = new System.Drawing.Size(120, 30);
            this.tmi_Funktionen.Text = "Funktionen";
            // 
            // tmi_UebersichtFunktionen
            // 
            this.tmi_UebersichtFunktionen.Name = "tmi_UebersichtFunktionen";
            this.tmi_UebersichtFunktionen.Size = new System.Drawing.Size(124, 22);
            this.tmi_UebersichtFunktionen.Text = "Übersicht";
            // 
            // materialStyledTabControl1
            // 
            this.materialStyledTabControl1.Controls.Add(this.tabPage1);
            this.materialStyledTabControl1.Controls.Add(this.tabPage2);
            this.materialStyledTabControl1.Depth = 0;
            this.materialStyledTabControl1.ItemSize = new System.Drawing.Size(150, 40);
            this.materialStyledTabControl1.Location = new System.Drawing.Point(26, 71);
            this.materialStyledTabControl1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialStyledTabControl1.Name = "materialStyledTabControl1";
            this.materialStyledTabControl1.SelectedIndex = 0;
            this.materialStyledTabControl1.Size = new System.Drawing.Size(670, 100);
            this.materialStyledTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.materialStyledTabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(662, 52);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 44);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(662, 52);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // materialCard1
            // 
            this.materialCard1.BackColor = System.Drawing.Color.White;
            this.materialCard1.Controls.Add(this.materialStyledTabControl1);
            this.materialCard1.Depth = 0;
            this.materialCard1.Elevation = 5;
            this.materialCard1.Location = new System.Drawing.Point(123, 145);
            this.materialCard1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(5, 25, 5, 5);
            graphicsPath2.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialCard1.ShadowBorder = graphicsPath2;
            this.materialCard1.Size = new System.Drawing.Size(774, 292);
            this.materialCard1.TabIndex = 23;
            this.materialCard1.Title = null;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(186, 510);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(348, 100);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(340, 74);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(192, 74);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.ActionBarMenu = this.materialContextMenuStrip1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1086, 833);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.materialCard1);
            this.Controls.Add(this.materialFloatingActionButton1);
            this.Controls.Add(this.materialFlatButton3);
            this.Controls.Add(this.materialFlatButton2);
            this.Controls.Add(this.materialButton1);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.materialFlatButton1);
            this.Name = "MainForm";
            this.SideDrawer = this.materialContextMenuStrip1;
            this.Text = "MaterialWinforms Demo";
            this.Controls.SetChildIndex(this.materialFlatButton1, 0);
            this.Controls.SetChildIndex(this.materialRaisedButton1, 0);
            this.Controls.SetChildIndex(this.materialButton1, 0);
            this.Controls.SetChildIndex(this.materialFlatButton2, 0);
            this.Controls.SetChildIndex(this.materialFlatButton3, 0);
            this.Controls.SetChildIndex(this.materialFloatingActionButton1, 0);
            this.Controls.SetChildIndex(this.materialCard1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.materialContextMenuStrip1.ResumeLayout(false);
            this.materialStyledTabControl1.ResumeLayout(false);
            this.materialCard1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialWinforms.Controls.MaterialRaisedButton materialButton1;
        private MaterialWinforms.Controls.MaterialFlatButton materialFlatButton1;
        private MaterialFlatButton materialFlatButton2;
        private MaterialRaisedButton materialRaisedButton1;
        private MaterialFlatButton materialFlatButton3;
        private MaterialFloatingActionButton materialFloatingActionButton1;
        private MaterialContextMenuStrip materialContextMenuStrip1;
        private MaterialToolStripMenuItem tmi_Tabellen;
        private ToolStripMenuItem tmi_UebersichtTabellen;
        private ToolStripMenuItem tmi_Artikel;
        private ToolStripMenuItem tmi_Struktur;
        private MaterialToolStripMenuItem tmi_Sichten;
        private ToolStripMenuItem tmi_UebersichtSichten;
        private MaterialToolStripMenuItem tmi_Prozeduren;
        private ToolStripMenuItem tmi_Uebersicht;
        private MaterialToolStripMenuItem tmi_Funktionen;
        private ToolStripMenuItem tmi_UebersichtFunktionen;
        private MaterialStyledTabControl materialStyledTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private MaterialCard materialCard1;
        private TabControl tabControl1;
        private TabPage tabPage3;
        private TabPage tabPage4;
    }
}