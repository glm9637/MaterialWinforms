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
            System.Drawing.Drawing2D.GraphicsPath graphicsPath3 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath graphicsPath4 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath graphicsPath5 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath graphicsPath6 = new System.Drawing.Drawing2D.GraphicsPath();
            this.materialFlatButton3 = new MaterialWinforms.Controls.MaterialFlatButton();
            this.materialFlatButton1 = new MaterialWinforms.Controls.MaterialFlatButton();
            this.materialButton1 = new MaterialWinforms.Controls.MaterialRaisedButton();
            this.materialRaisedButton1 = new MaterialWinforms.Controls.MaterialRaisedButton();
            this.materialFloatingActionButton1 = new MaterialWinforms.Controls.MaterialFloatingActionButton();
            this.materialFlatButton2 = new MaterialWinforms.Controls.MaterialFlatButton();
            this.SideDrawerList = new MaterialWinforms.Controls.MaterialContextMenuStrip();
            this.tmi_Datenbank1 = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.tmi_UebersichtTabellen = new System.Windows.Forms.ToolStripMenuItem();
            this.sichtenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prozedurenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.funktionenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_Datenbank2 = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.tmi_Tabellen = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.tmi_Sichten = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.tmi_Funktionen = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.tmi_Prozeduren = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.tmi_Trigger = new MaterialWinforms.Controls.MaterialToolStripMenuItem();
            this.materialSideDrawer3 = new MaterialWinforms.Controls.MaterialSideDrawer();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.materialTabControl1 = new MaterialWinforms.Controls.MaterialTabControl();
            this.materialTabPage1 = new MaterialWinforms.Controls.MaterialTabPage();
            this.materialActionBar1 = new MaterialWinforms.Controls.MaterialActionBar();
            this.ActionBarMenu = new MaterialWinforms.Controls.MaterialContextMenuStrip();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.materialTabSelectorClosable1 = new MaterialWinforms.Controls.MaterialTabSelectorClosable();
            this.materialSearchButton1 = new MaterialWinforms.Controls.MaterialSearchButton();
            this.materialSlider1 = new MaterialWinforms.Controls.MaterialSlider();
            this.SideDrawerList.SuspendLayout();
            this.materialTabControl1.SuspendLayout();
            this.materialTabPage1.SuspendLayout();
            this.ActionBarMenu.SuspendLayout();
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
            this.materialFlatButton3.Location = new System.Drawing.Point(386, 71);
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
            this.materialFlatButton1.Location = new System.Drawing.Point(527, 71);
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
            this.materialButton1.Elevation = 5;
            this.materialButton1.Location = new System.Drawing.Point(437, 599);
            this.materialButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.Primary = true;
            graphicsPath1.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialButton1.ShadowBorder = graphicsPath1;
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
            this.materialRaisedButton1.Elevation = 5;
            this.materialRaisedButton1.Location = new System.Drawing.Point(437, 533);
            this.materialRaisedButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            graphicsPath2.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialRaisedButton1.ShadowBorder = graphicsPath2;
            this.materialRaisedButton1.Size = new System.Drawing.Size(179, 36);
            this.materialRaisedButton1.TabIndex = 21;
            this.materialRaisedButton1.Text = "Change color scheme";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // materialFloatingActionButton1
            // 
            this.materialFloatingActionButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.materialFloatingActionButton1.Breite = 42;
            this.materialFloatingActionButton1.Depth = 0;
            this.materialFloatingActionButton1.Elevation = 5;
            this.materialFloatingActionButton1.Hoehe = 42;
            this.materialFloatingActionButton1.Icon = global::MaterialWinformsExample.Properties.Resources.ic_action_action_search;
            this.materialFloatingActionButton1.Location = new System.Drawing.Point(672, 599);
            this.materialFloatingActionButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFloatingActionButton1.Name = "materialFloatingActionButton1";
            graphicsPath3.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialFloatingActionButton1.ShadowBorder = graphicsPath3;
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
            this.materialFlatButton2.IconImage = global::MaterialWinformsExample.Properties.Resources.ic_action_action_search;
            this.materialFlatButton2.Image = global::MaterialWinformsExample.Properties.Resources.ic_action_action_search;
            this.materialFlatButton2.Location = new System.Drawing.Point(213, 71);
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
            // SideDrawerList
            // 
            this.SideDrawerList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SideDrawerList.Depth = 0;
            this.SideDrawerList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_Datenbank1,
            this.tmi_Datenbank2});
            this.SideDrawerList.MouseState = MaterialWinforms.MouseState.HOVER;
            this.SideDrawerList.Name = "materialContextMenuStrip1";
            this.SideDrawerList.Size = new System.Drawing.Size(141, 64);
            // 
            // tmi_Datenbank1
            // 
            this.tmi_Datenbank1.AutoSize = false;
            this.tmi_Datenbank1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_UebersichtTabellen,
            this.sichtenToolStripMenuItem,
            this.prozedurenToolStripMenuItem,
            this.funktionenToolStripMenuItem,
            this.triggerToolStripMenuItem});
            this.tmi_Datenbank1.Name = "tmi_Datenbank1";
            this.tmi_Datenbank1.Size = new System.Drawing.Size(120, 30);
            this.tmi_Datenbank1.Text = "Datenbank 1";
            // 
            // tmi_UebersichtTabellen
            // 
            this.tmi_UebersichtTabellen.Name = "tmi_UebersichtTabellen";
            this.tmi_UebersichtTabellen.Size = new System.Drawing.Size(134, 22);
            this.tmi_UebersichtTabellen.Text = "Tabellen";
            // 
            // sichtenToolStripMenuItem
            // 
            this.sichtenToolStripMenuItem.Name = "sichtenToolStripMenuItem";
            this.sichtenToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.sichtenToolStripMenuItem.Text = "Sichten";
            // 
            // prozedurenToolStripMenuItem
            // 
            this.prozedurenToolStripMenuItem.Name = "prozedurenToolStripMenuItem";
            this.prozedurenToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.prozedurenToolStripMenuItem.Text = "Prozeduren";
            // 
            // funktionenToolStripMenuItem
            // 
            this.funktionenToolStripMenuItem.Name = "funktionenToolStripMenuItem";
            this.funktionenToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.funktionenToolStripMenuItem.Text = "Funktionen";
            // 
            // triggerToolStripMenuItem
            // 
            this.triggerToolStripMenuItem.Name = "triggerToolStripMenuItem";
            this.triggerToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.triggerToolStripMenuItem.Text = "Trigger";
            // 
            // tmi_Datenbank2
            // 
            this.tmi_Datenbank2.AutoSize = false;
            this.tmi_Datenbank2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_Tabellen,
            this.tmi_Sichten,
            this.tmi_Funktionen,
            this.tmi_Prozeduren,
            this.tmi_Trigger});
            this.tmi_Datenbank2.Name = "tmi_Datenbank2";
            this.tmi_Datenbank2.Size = new System.Drawing.Size(120, 30);
            this.tmi_Datenbank2.Text = "Datenbank2";
            // 
            // tmi_Tabellen
            // 
            this.tmi_Tabellen.AutoSize = false;
            this.tmi_Tabellen.Name = "tmi_Tabellen";
            this.tmi_Tabellen.Size = new System.Drawing.Size(120, 30);
            this.tmi_Tabellen.Text = "Tabellen";
            // 
            // tmi_Sichten
            // 
            this.tmi_Sichten.AutoSize = false;
            this.tmi_Sichten.Name = "tmi_Sichten";
            this.tmi_Sichten.Size = new System.Drawing.Size(120, 30);
            this.tmi_Sichten.Text = "Sichten";
            // 
            // tmi_Funktionen
            // 
            this.tmi_Funktionen.AutoSize = false;
            this.tmi_Funktionen.Name = "tmi_Funktionen";
            this.tmi_Funktionen.Size = new System.Drawing.Size(120, 30);
            this.tmi_Funktionen.Text = "Funktionen";
            // 
            // tmi_Prozeduren
            // 
            this.tmi_Prozeduren.AutoSize = false;
            this.tmi_Prozeduren.Name = "tmi_Prozeduren";
            this.tmi_Prozeduren.Size = new System.Drawing.Size(120, 30);
            this.tmi_Prozeduren.Text = "Prozeduren";
            // 
            // tmi_Trigger
            // 
            this.tmi_Trigger.AutoSize = false;
            this.tmi_Trigger.Name = "tmi_Trigger";
            this.tmi_Trigger.Size = new System.Drawing.Size(120, 30);
            this.tmi_Trigger.Text = "Trigger";
            // 
            // materialSideDrawer3
            // 
            this.materialSideDrawer3.AutoScroll = true;
            this.materialSideDrawer3.Depth = 0;
            this.materialSideDrawer3.Dock = System.Windows.Forms.DockStyle.Left;
            this.materialSideDrawer3.Elevation = 10;
            this.materialSideDrawer3.HideSideDrawer = false;
            this.materialSideDrawer3.Location = new System.Drawing.Point(0, 64);
            this.materialSideDrawer3.MaximumSize = new System.Drawing.Size(200, 653);
            this.materialSideDrawer3.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialSideDrawer3.Name = "materialSideDrawer3";
            graphicsPath4.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialSideDrawer3.ShadowBorder = graphicsPath4;
            this.materialSideDrawer3.SideDrawer = this.SideDrawerList;
            this.materialSideDrawer3.SideDrawerFixiert = true;
            this.materialSideDrawer3.SideDrawerUnterActionBar = false;
            this.materialSideDrawer3.Size = new System.Drawing.Size(200, 629);
            this.materialSideDrawer3.TabIndex = 29;
            this.materialSideDrawer3.onSideDrawerItemClicked += new MaterialWinforms.Controls.MaterialSideDrawer.SideDrawerEventHandler(this.materialSideDrawer3_onSideDrawerItemClicked);
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
            // materialTabControl1
            // 
            this.materialTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabControl1.Controls.Add(this.materialTabPage1);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(206, 254);
            this.materialTabControl1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.Padding = new System.Drawing.Point(15, 3);
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(705, 201);
            this.materialTabControl1.TabIndex = 24;
            this.materialTabControl1.TabsAreClosable = true;
            // 
            // materialTabPage1
            // 
            this.materialTabPage1.Controls.Add(this.materialFlatButton2);
            this.materialTabPage1.Controls.Add(this.materialFlatButton3);
            this.materialTabPage1.Controls.Add(this.materialFlatButton1);
            this.materialTabPage1.Depth = 0;
            this.materialTabPage1.Location = new System.Drawing.Point(4, 22);
            this.materialTabPage1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTabPage1.Name = "materialTabPage1";
            this.materialTabPage1.Size = new System.Drawing.Size(697, 175);
            this.materialTabPage1.TabIndex = 0;
            this.materialTabPage1.Text = "materialTabPage1";
            // 
            // materialActionBar1
            // 
            this.materialActionBar1.ActionBarMenu = this.ActionBarMenu;
            this.materialActionBar1.Depth = 0;
            this.materialActionBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialActionBar1.Elevation = 10;
            this.materialActionBar1.IntegratedSearchBar = true;
            this.materialActionBar1.Location = new System.Drawing.Point(0, 24);
            this.materialActionBar1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialActionBar1.Name = "materialActionBar1";
            graphicsPath5.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialActionBar1.ShadowBorder = graphicsPath5;
            this.materialActionBar1.Size = new System.Drawing.Size(914, 40);
            this.materialActionBar1.TabIndex = 26;
            this.materialActionBar1.Text = "materialActionBar1";
            // 
            // ActionBarMenu
            // 
            this.ActionBarMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ActionBarMenu.Depth = 0;
            this.ActionBarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripMenuItem,
            this.themeToolStripMenuItem1});
            this.ActionBarMenu.MouseState = MaterialWinforms.MouseState.HOVER;
            this.ActionBarMenu.Name = "materialContextMenuStrip2";
            this.ActionBarMenu.Size = new System.Drawing.Size(112, 48);
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blueToolStripMenuItem,
            this.greenToolStripMenuItem,
            this.standardToolStripMenuItem});
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.themeToolStripMenuItem.Text = "Style";
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.blueToolStripMenuItem.Tag = "1";
            this.blueToolStripMenuItem.Text = "Blue";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.blueToolStripMenuItem_Click);
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.greenToolStripMenuItem.Tag = "2";
            this.greenToolStripMenuItem.Text = "Blue Gray";
            this.greenToolStripMenuItem.Click += new System.EventHandler(this.greenToolStripMenuItem_Click);
            // 
            // standardToolStripMenuItem
            // 
            this.standardToolStripMenuItem.Name = "standardToolStripMenuItem";
            this.standardToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.standardToolStripMenuItem.Tag = "3";
            this.standardToolStripMenuItem.Text = "Green";
            this.standardToolStripMenuItem.Click += new System.EventHandler(this.standardToolStripMenuItem_Click);
            // 
            // themeToolStripMenuItem1
            // 
            this.themeToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.themeToolStripMenuItem1.Name = "themeToolStripMenuItem1";
            this.themeToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.themeToolStripMenuItem1.Text = "Theme";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(101, 22);
            this.toolStripMenuItem1.Tag = "4";
            this.toolStripMenuItem1.Text = "Light";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(101, 22);
            this.toolStripMenuItem2.Tag = "5";
            this.toolStripMenuItem2.Text = "Dark";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // materialTabSelectorClosable1
            // 
            this.materialTabSelectorClosable1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelectorClosable1.CenterTabs = true;
            this.materialTabSelectorClosable1.Depth = 0;
            this.materialTabSelectorClosable1.Elevation = 0;
            this.materialTabSelectorClosable1.Location = new System.Drawing.Point(219, 103);
            this.materialTabSelectorClosable1.Margin = new System.Windows.Forms.Padding(0);
            this.materialTabSelectorClosable1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTabSelectorClosable1.Name = "materialTabSelectorClosable1";
            graphicsPath6.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialTabSelectorClosable1.ShadowBorder = graphicsPath6;
            this.materialTabSelectorClosable1.Size = new System.Drawing.Size(614, 40);
            this.materialTabSelectorClosable1.TabIndex = 32;
            this.materialTabSelectorClosable1.Text = "materialTabSelectorClosable1";
            // 
            // materialSearchButton1
            // 
            this.materialSearchButton1.Depth = 0;
            this.materialSearchButton1.Hint = "Suchbegriff eingeben...";
            this.materialSearchButton1.Location = new System.Drawing.Point(859, 210);
            this.materialSearchButton1.MaxLength = 32767;
            this.materialSearchButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialSearchButton1.Name = "materialSearchButton1";
            this.materialSearchButton1.Size = new System.Drawing.Size(25, 38);
            this.materialSearchButton1.TabIndex = 33;
            this.materialSearchButton1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // materialSlider1
            // 
            this.materialSlider1.Depth = 0;
            this.materialSlider1.Location = new System.Drawing.Point(285, 642);
            this.materialSlider1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialSlider1.Name = "materialSlider1";
            this.materialSlider1.Size = new System.Drawing.Size(140, 18);
            this.materialSlider1.TabIndex = 34;
            this.materialSlider1.Text = "materialSlider1";
            // 
            // MainForm
            // 
            this.ActionBar = this.materialActionBar1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(914, 693);
            this.Controls.Add(this.materialSideDrawer3);
            this.Controls.Add(this.materialSlider1);
            this.Controls.Add(this.materialTabSelectorClosable1);
            this.Controls.Add(this.materialSearchButton1);
            this.Controls.Add(this.materialActionBar1);
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.materialFloatingActionButton1);
            this.Controls.Add(this.materialButton1);
            this.Controls.Add(this.materialRaisedButton1);
            this.Name = "MainForm";
            this.SideDrawer = this.materialSideDrawer3;
            this.Text = "Datenbank Dokumentation";
            this.SideDrawerList.ResumeLayout(false);
            this.materialTabControl1.ResumeLayout(false);
            this.materialTabPage1.ResumeLayout(false);
            this.materialTabPage1.PerformLayout();
            this.ActionBarMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialWinforms.Controls.MaterialRaisedButton materialButton1;
        private MaterialWinforms.Controls.MaterialFlatButton materialFlatButton1;
        private MaterialFlatButton materialFlatButton2;
        private MaterialRaisedButton materialRaisedButton1;
        private MaterialFlatButton materialFlatButton3;
        private MaterialFloatingActionButton materialFloatingActionButton1;
        private MaterialContextMenuStrip SideDrawerList;
        private MaterialToolStripMenuItem tmi_Datenbank1;
        private ToolStripMenuItem tmi_UebersichtTabellen;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private MaterialTabControl materialTabControl1;
        private ToolStripMenuItem sichtenToolStripMenuItem;
        private ToolStripMenuItem prozedurenToolStripMenuItem;
        private ToolStripMenuItem funktionenToolStripMenuItem;
        private ToolStripMenuItem triggerToolStripMenuItem;
        private MaterialActionBar materialActionBar1;
        private MaterialSideDrawer materialSideDrawer3;
        private MaterialTabSelectorClosable materialTabSelectorClosable1;
        private MaterialContextMenuStrip ActionBarMenu;
        private ToolStripMenuItem themeToolStripMenuItem;
        private ToolStripMenuItem blueToolStripMenuItem;
        private ToolStripMenuItem greenToolStripMenuItem;
        private ToolStripMenuItem standardToolStripMenuItem;
        private ToolStripMenuItem themeToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private MaterialTabPage materialTabPage1;
        private MaterialSearchButton materialSearchButton1;
        private MaterialToolStripMenuItem tmi_Datenbank2;
        private MaterialToolStripMenuItem tmi_Sichten;
        private MaterialToolStripMenuItem tmi_Funktionen;
        private MaterialToolStripMenuItem tmi_Prozeduren;
        private MaterialToolStripMenuItem tmi_Trigger;
        private MaterialToolStripMenuItem tmi_Tabellen;
        private MaterialSlider materialSlider1;
    }
}