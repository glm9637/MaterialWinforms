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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Drawing.Drawing2D.GraphicsPath graphicsPath5 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath graphicsPath6 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath graphicsPath7 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath graphicsPath8 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Knoten0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Knoten1");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Knoten3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Knoten5");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Knoten6");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Knoten4", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Knoten2", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Knoten0");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Knoten2");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Knoten0", new System.Windows.Forms.TreeNode[] {
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Knoten1");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Knoten3", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Knoten4");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Knoten1", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Knoten2");
            System.Drawing.Drawing2D.GraphicsPath graphicsPath10 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath graphicsPath11 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath graphicsPath12 = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath graphicsPath9 = new System.Drawing.Drawing2D.GraphicsPath();
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
            this.testToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eafToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sadfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sadfToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.asdfToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.asdfToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.asToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.qToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fgvsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.fToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.asdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sdToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.qToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.weToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.sdToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.vToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.sdToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.qvgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.materialTabControl1 = new MaterialWinforms.Controls.MaterialTabControl();
            this.mtp_Picker = new MaterialWinforms.Controls.MaterialTabPage();
            this.materialAvatarView1 = new MaterialWinforms.Controls.MaterialAvatarView();
            this.materialDropDownColorPicker1 = new MaterialWinforms.Controls.MaterialDropDownColorPicker();
            this.materialDropDownDatePicker1 = new MaterialWinforms.Controls.MaterialDropDownDatePicker();
            this.materialColorPicker1 = new MaterialWinforms.Controls.MaterialColorPicker();
            this.materialDivider1 = new MaterialWinforms.Controls.MaterialDivider();
            this.materialDatePicker1 = new MaterialWinforms.Controls.MaterialDatePicker();
            this.materialTabPage1 = new MaterialWinforms.Controls.MaterialTabPage();
            this.materialBreadCrumbToolbar1 = new MaterialWinforms.Controls.MaterialBreadCrumbToolbar();
            this.materialTextBox1 = new MaterialWinforms.Controls.MaterialTextBox();
            this.materialCard3 = new MaterialWinforms.Controls.MaterialCard();
            this.materialLabel2 = new MaterialWinforms.Controls.MaterialLabel();
            this.materialSingleLineTextField1 = new MaterialWinforms.Controls.MaterialSingleLineTextField();
            this.materialComboBox1 = new MaterialWinforms.Controls.MaterialComboBox();
            this.materialProgressBar1 = new MaterialWinforms.Controls.MaterialProgressBar();
            this.materialLoadingFloatingActionButton1 = new MaterialWinforms.Controls.MaterialLoadingFloatingActionButton();
            this.materialLabel1 = new MaterialWinforms.Controls.MaterialLabel();
            this.materialFolderInput1 = new MaterialWinforms.Controls.MaterialFolderInput();
            this.materialFileInput1 = new MaterialWinforms.Controls.MaterialFileInput();
            this.materialCard2 = new MaterialWinforms.Controls.MaterialCard();
            this.materialToggle3 = new MaterialWinforms.Controls.MaterialToggle();
            this.materialToggle4 = new MaterialWinforms.Controls.MaterialToggle();
            this.materialToggle2 = new MaterialWinforms.Controls.MaterialToggle();
            this.materialToggle1 = new MaterialWinforms.Controls.MaterialToggle();
            this.materialCheckBox3 = new MaterialWinforms.Controls.MaterialCheckBox();
            this.materialCheckBox4 = new MaterialWinforms.Controls.MaterialCheckBox();
            this.materialCheckBox2 = new MaterialWinforms.Controls.MaterialCheckBox();
            this.materialCheckBox1 = new MaterialWinforms.Controls.MaterialCheckBox();
            this.materialCard1 = new MaterialWinforms.Controls.MaterialCard();
            this.materialSlider2 = new MaterialWinforms.Controls.MaterialSlider();
            this.materialSlider1 = new MaterialWinforms.Controls.MaterialSlider();
            this.materialTabPage3 = new MaterialWinforms.Controls.MaterialTabPage();
            this.materialPanel1 = new MaterialWinforms.Controls.MaterialPanel();
            this.materialTimeline2 = new MaterialWinforms.Controls.MaterialTimeline();
            this.materialTabPage2 = new MaterialWinforms.Controls.MaterialTabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.materialTreeControl1 = new MaterialWinforms.Controls.MaterialTreeControl();
            this.materialFlatButton6 = new MaterialWinforms.Controls.MaterialFlatButton();
            this.materialFlatButton5 = new MaterialWinforms.Controls.MaterialFlatButton();
            this.materialFlatButton4 = new MaterialWinforms.Controls.MaterialFlatButton();
            this.materialTabPage5 = new MaterialWinforms.Controls.MaterialTabPage();
            this.mcm_ComboBox1 = new MaterialWinforms.Controls.MaterialContextMenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wert3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.materialActionBar1 = new MaterialWinforms.Controls.MaterialActionBar();
            this.materialActionBarButton1 = new MaterialWinforms.Controls.MaterialActionBarButton();
            this.materialActionBarButton2 = new MaterialWinforms.Controls.MaterialActionBarButton();
            this.ActionBarMenu = new MaterialWinforms.Controls.MaterialContextMenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.materialTabSelectorClosable1 = new MaterialWinforms.Controls.MaterialTabSelector();
            this.materialSideDrawer1 = new MaterialWinforms.Controls.MaterialSideDrawer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.materialRaisedButton2 = new MaterialWinforms.Controls.MaterialRaisedButton();
            this.SideDrawerList.SuspendLayout();
            this.materialTabControl1.SuspendLayout();
            this.mtp_Picker.SuspendLayout();
            this.materialTabPage1.SuspendLayout();
            this.materialCard3.SuspendLayout();
            this.materialCard2.SuspendLayout();
            this.materialCard1.SuspendLayout();
            this.materialTabPage3.SuspendLayout();
            this.materialPanel1.SuspendLayout();
            this.materialTabPage2.SuspendLayout();
            this.mcm_ComboBox1.SuspendLayout();
            this.materialActionBar1.SuspendLayout();
            this.ActionBarMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialFlatButton3
            // 
            this.materialFlatButton3.Accent = false;
            this.materialFlatButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialFlatButton3.AutoSize = true;
            this.materialFlatButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton3.Capitalized = true;
            this.materialFlatButton3.Depth = 0;
            this.materialFlatButton3.Enabled = false;
            this.materialFlatButton3.IconImage = null;
            this.materialFlatButton3.Location = new System.Drawing.Point(482, 517);
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
            this.materialFlatButton1.Capitalized = true;
            this.materialFlatButton1.Depth = 0;
            this.materialFlatButton1.IconImage = null;
            this.materialFlatButton1.Location = new System.Drawing.Point(700, 517);
            this.materialFlatButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFlatButton1.Name = "materialFlatButton1";
            this.materialFlatButton1.Primary = true;
            this.materialFlatButton1.Selected = false;
            this.materialFlatButton1.Size = new System.Drawing.Size(71, 36);
            this.materialFlatButton1.TabIndex = 1;
            this.materialFlatButton1.Text = "Primary";
            this.materialFlatButton1.UseVisualStyleBackColor = true;
            this.materialFlatButton1.Click += new System.EventHandler(this.materialFlatButton1_Click);
            // 
            // materialButton1
            // 
            this.materialButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.materialButton1.Depth = 0;
            this.materialButton1.Elevation = 5;
            this.materialButton1.Location = new System.Drawing.Point(650, 650);
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
            this.materialRaisedButton1.Location = new System.Drawing.Point(791, 650);
            this.materialRaisedButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = false;
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
            this.materialFloatingActionButton1.Breite = 42;
            this.materialFloatingActionButton1.Depth = 0;
            this.materialFloatingActionButton1.Elevation = 5;
            this.materialFloatingActionButton1.Hoehe = 42;
            this.materialFloatingActionButton1.Icon = global::MaterialWinformsExample.Properties.Resources.ic_action_action_search;
            this.materialFloatingActionButton1.Location = new System.Drawing.Point(222, 644);
            this.materialFloatingActionButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFloatingActionButton1.Name = "materialFloatingActionButton1";
            graphicsPath3.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialFloatingActionButton1.ShadowBorder = graphicsPath3;
            this.materialFloatingActionButton1.Size = new System.Drawing.Size(42, 42);
            this.materialFloatingActionButton1.TabIndex = 20;
            this.materialFloatingActionButton1.Text = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.UseVisualStyleBackColor = false;
            this.materialFloatingActionButton1.Click += new System.EventHandler(this.materialFloatingActionButton1_Click);
            // 
            // materialFlatButton2
            // 
            this.materialFlatButton2.Accent = false;
            this.materialFlatButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialFlatButton2.AutoSize = true;
            this.materialFlatButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton2.Capitalized = true;
            this.materialFlatButton2.Depth = 0;
            this.materialFlatButton2.IconImage = global::MaterialWinformsExample.Properties.Resources.ic_action_action_search;
            this.materialFlatButton2.Image = global::MaterialWinformsExample.Properties.Resources.ic_action_action_search;
            this.materialFlatButton2.Location = new System.Drawing.Point(565, 517);
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
            this.SideDrawerList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.SideDrawerList.Depth = 0;
            this.SideDrawerList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_Datenbank1,
            this.tmi_Datenbank2,
            this.testToolStripMenuItem1,
            this.eafToolStripMenuItem,
            this.asdfToolStripMenuItem,
            this.sadfToolStripMenuItem,
            this.sadfToolStripMenuItem1,
            this.asdfToolStripMenuItem1,
            this.asdfToolStripMenuItem2,
            this.asToolStripMenuItem,
            this.vdToolStripMenuItem,
            this.asToolStripMenuItem1,
            this.dToolStripMenuItem,
            this.vToolStripMenuItem,
            this.aToolStripMenuItem,
            this.sdToolStripMenuItem,
            this.vToolStripMenuItem1,
            this.qToolStripMenuItem,
            this.weToolStripMenuItem,
            this.qToolStripMenuItem1,
            this.fgvsToolStripMenuItem,
            this.dToolStripMenuItem1,
            this.fToolStripMenuItem,
            this.asToolStripMenuItem2,
            this.dToolStripMenuItem2,
            this.fToolStripMenuItem1,
            this.asdToolStripMenuItem,
            this.vToolStripMenuItem2,
            this.aToolStripMenuItem1,
            this.sdToolStripMenuItem1,
            this.fToolStripMenuItem2,
            this.qToolStripMenuItem2,
            this.weToolStripMenuItem1,
            this.fToolStripMenuItem3,
            this.aToolStripMenuItem2,
            this.sdToolStripMenuItem2,
            this.vToolStripMenuItem3,
            this.aToolStripMenuItem3,
            this.sdToolStripMenuItem3,
            this.qvgToolStripMenuItem,
            this.eToolStripMenuItem,
            this.rToolStripMenuItem,
            this.bgToolStripMenuItem,
            this.sfdToolStripMenuItem});
            this.SideDrawerList.MouseState = MaterialWinforms.MouseState.HOVER;
            this.SideDrawerList.Name = "materialContextMenuStrip1";
            this.SideDrawerList.Size = new System.Drawing.Size(141, 988);
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
            // testToolStripMenuItem1
            // 
            this.testToolStripMenuItem1.Name = "testToolStripMenuItem1";
            this.testToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.testToolStripMenuItem1.Text = "Test";
            // 
            // eafToolStripMenuItem
            // 
            this.eafToolStripMenuItem.Name = "eafToolStripMenuItem";
            this.eafToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.eafToolStripMenuItem.Text = "eaf";
            // 
            // asdfToolStripMenuItem
            // 
            this.asdfToolStripMenuItem.Name = "asdfToolStripMenuItem";
            this.asdfToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.asdfToolStripMenuItem.Text = "asdf";
            // 
            // sadfToolStripMenuItem
            // 
            this.sadfToolStripMenuItem.Name = "sadfToolStripMenuItem";
            this.sadfToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.sadfToolStripMenuItem.Text = "sadf";
            // 
            // sadfToolStripMenuItem1
            // 
            this.sadfToolStripMenuItem1.Name = "sadfToolStripMenuItem1";
            this.sadfToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.sadfToolStripMenuItem1.Text = "sadf";
            // 
            // asdfToolStripMenuItem1
            // 
            this.asdfToolStripMenuItem1.Name = "asdfToolStripMenuItem1";
            this.asdfToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.asdfToolStripMenuItem1.Text = "asdf";
            // 
            // asdfToolStripMenuItem2
            // 
            this.asdfToolStripMenuItem2.Name = "asdfToolStripMenuItem2";
            this.asdfToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.asdfToolStripMenuItem2.Text = "asdf";
            // 
            // asToolStripMenuItem
            // 
            this.asToolStripMenuItem.Name = "asToolStripMenuItem";
            this.asToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.asToolStripMenuItem.Text = "as";
            // 
            // vdToolStripMenuItem
            // 
            this.vdToolStripMenuItem.Name = "vdToolStripMenuItem";
            this.vdToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.vdToolStripMenuItem.Text = "vd";
            // 
            // asToolStripMenuItem1
            // 
            this.asToolStripMenuItem1.Name = "asToolStripMenuItem1";
            this.asToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.asToolStripMenuItem1.Text = "as";
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.dToolStripMenuItem.Text = "d";
            // 
            // vToolStripMenuItem
            // 
            this.vToolStripMenuItem.Name = "vToolStripMenuItem";
            this.vToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.vToolStripMenuItem.Text = "v";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.aToolStripMenuItem.Text = "a";
            // 
            // sdToolStripMenuItem
            // 
            this.sdToolStripMenuItem.Name = "sdToolStripMenuItem";
            this.sdToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.sdToolStripMenuItem.Text = "sd";
            // 
            // vToolStripMenuItem1
            // 
            this.vToolStripMenuItem1.Name = "vToolStripMenuItem1";
            this.vToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.vToolStripMenuItem1.Text = "v";
            // 
            // qToolStripMenuItem
            // 
            this.qToolStripMenuItem.Name = "qToolStripMenuItem";
            this.qToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.qToolStripMenuItem.Text = "q";
            // 
            // weToolStripMenuItem
            // 
            this.weToolStripMenuItem.Name = "weToolStripMenuItem";
            this.weToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.weToolStripMenuItem.Text = "we";
            // 
            // qToolStripMenuItem1
            // 
            this.qToolStripMenuItem1.Name = "qToolStripMenuItem1";
            this.qToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.qToolStripMenuItem1.Text = "q";
            // 
            // fgvsToolStripMenuItem
            // 
            this.fgvsToolStripMenuItem.Name = "fgvsToolStripMenuItem";
            this.fgvsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.fgvsToolStripMenuItem.Text = "fgvs";
            // 
            // dToolStripMenuItem1
            // 
            this.dToolStripMenuItem1.Name = "dToolStripMenuItem1";
            this.dToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.dToolStripMenuItem1.Text = "d";
            // 
            // fToolStripMenuItem
            // 
            this.fToolStripMenuItem.Name = "fToolStripMenuItem";
            this.fToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.fToolStripMenuItem.Text = "f";
            // 
            // asToolStripMenuItem2
            // 
            this.asToolStripMenuItem2.Name = "asToolStripMenuItem2";
            this.asToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.asToolStripMenuItem2.Text = "as";
            // 
            // dToolStripMenuItem2
            // 
            this.dToolStripMenuItem2.Name = "dToolStripMenuItem2";
            this.dToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.dToolStripMenuItem2.Text = "d";
            // 
            // fToolStripMenuItem1
            // 
            this.fToolStripMenuItem1.Name = "fToolStripMenuItem1";
            this.fToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.fToolStripMenuItem1.Text = "f";
            // 
            // asdToolStripMenuItem
            // 
            this.asdToolStripMenuItem.Name = "asdToolStripMenuItem";
            this.asdToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.asdToolStripMenuItem.Text = "asd";
            // 
            // vToolStripMenuItem2
            // 
            this.vToolStripMenuItem2.Name = "vToolStripMenuItem2";
            this.vToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.vToolStripMenuItem2.Text = "v";
            // 
            // aToolStripMenuItem1
            // 
            this.aToolStripMenuItem1.Name = "aToolStripMenuItem1";
            this.aToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.aToolStripMenuItem1.Text = "a";
            // 
            // sdToolStripMenuItem1
            // 
            this.sdToolStripMenuItem1.Name = "sdToolStripMenuItem1";
            this.sdToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.sdToolStripMenuItem1.Text = "sd";
            // 
            // fToolStripMenuItem2
            // 
            this.fToolStripMenuItem2.Name = "fToolStripMenuItem2";
            this.fToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.fToolStripMenuItem2.Text = "f";
            // 
            // qToolStripMenuItem2
            // 
            this.qToolStripMenuItem2.Name = "qToolStripMenuItem2";
            this.qToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.qToolStripMenuItem2.Text = "q";
            // 
            // weToolStripMenuItem1
            // 
            this.weToolStripMenuItem1.Name = "weToolStripMenuItem1";
            this.weToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.weToolStripMenuItem1.Text = "we";
            // 
            // fToolStripMenuItem3
            // 
            this.fToolStripMenuItem3.Name = "fToolStripMenuItem3";
            this.fToolStripMenuItem3.Size = new System.Drawing.Size(140, 22);
            this.fToolStripMenuItem3.Text = "f";
            // 
            // aToolStripMenuItem2
            // 
            this.aToolStripMenuItem2.Name = "aToolStripMenuItem2";
            this.aToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.aToolStripMenuItem2.Text = "a";
            // 
            // sdToolStripMenuItem2
            // 
            this.sdToolStripMenuItem2.Name = "sdToolStripMenuItem2";
            this.sdToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.sdToolStripMenuItem2.Text = "sd";
            // 
            // vToolStripMenuItem3
            // 
            this.vToolStripMenuItem3.Name = "vToolStripMenuItem3";
            this.vToolStripMenuItem3.Size = new System.Drawing.Size(140, 22);
            this.vToolStripMenuItem3.Text = "v";
            // 
            // aToolStripMenuItem3
            // 
            this.aToolStripMenuItem3.Name = "aToolStripMenuItem3";
            this.aToolStripMenuItem3.Size = new System.Drawing.Size(140, 22);
            this.aToolStripMenuItem3.Text = "a";
            // 
            // sdToolStripMenuItem3
            // 
            this.sdToolStripMenuItem3.Name = "sdToolStripMenuItem3";
            this.sdToolStripMenuItem3.Size = new System.Drawing.Size(140, 22);
            this.sdToolStripMenuItem3.Text = "sd";
            // 
            // qvgToolStripMenuItem
            // 
            this.qvgToolStripMenuItem.Name = "qvgToolStripMenuItem";
            this.qvgToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.qvgToolStripMenuItem.Text = "qvg";
            // 
            // eToolStripMenuItem
            // 
            this.eToolStripMenuItem.Name = "eToolStripMenuItem";
            this.eToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.eToolStripMenuItem.Text = "e";
            // 
            // rToolStripMenuItem
            // 
            this.rToolStripMenuItem.Name = "rToolStripMenuItem";
            this.rToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.rToolStripMenuItem.Text = "r";
            // 
            // bgToolStripMenuItem
            // 
            this.bgToolStripMenuItem.Name = "bgToolStripMenuItem";
            this.bgToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.bgToolStripMenuItem.Text = "bg";
            // 
            // sfdToolStripMenuItem
            // 
            this.sfdToolStripMenuItem.Name = "sfdToolStripMenuItem";
            this.sfdToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.sfdToolStripMenuItem.Text = "sfd";
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
            this.materialTabControl1.Controls.Add(this.mtp_Picker);
            this.materialTabControl1.Controls.Add(this.materialTabPage1);
            this.materialTabControl1.Controls.Add(this.materialTabPage3);
            this.materialTabControl1.Controls.Add(this.materialTabPage2);
            this.materialTabControl1.Controls.Add(this.materialTabPage5);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTabControl1.Location = new System.Drawing.Point(210, 107);
            this.materialTabControl1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.Padding = new System.Drawing.Point(15, 3);
            this.materialTabControl1.SelectedIndex = 1;
            this.materialTabControl1.Size = new System.Drawing.Size(783, 581);
            this.materialTabControl1.TabIndex = 24;
            this.materialTabControl1.TabsAreClosable = true;
            // 
            // mtp_Picker
            // 
            this.mtp_Picker.Closable = true;
            this.mtp_Picker.Controls.Add(this.materialAvatarView1);
            this.mtp_Picker.Controls.Add(this.materialDropDownColorPicker1);
            this.mtp_Picker.Controls.Add(this.materialDropDownDatePicker1);
            this.mtp_Picker.Controls.Add(this.materialColorPicker1);
            this.mtp_Picker.Controls.Add(this.materialDivider1);
            this.mtp_Picker.Controls.Add(this.materialFlatButton3);
            this.mtp_Picker.Controls.Add(this.materialDatePicker1);
            this.mtp_Picker.Controls.Add(this.materialFlatButton2);
            this.mtp_Picker.Controls.Add(this.materialFlatButton1);
            this.mtp_Picker.Depth = 0;
            this.mtp_Picker.Location = new System.Drawing.Point(4, 22);
            this.mtp_Picker.MouseState = MaterialWinforms.MouseState.HOVER;
            this.mtp_Picker.Name = "mtp_Picker";
            this.mtp_Picker.Size = new System.Drawing.Size(775, 555);
            this.mtp_Picker.TabIndex = 1;
            this.mtp_Picker.Text = "Pickers";
            // 
            // materialAvatarView1
            // 
            this.materialAvatarView1.Avatar = null;
            this.materialAvatarView1.AvatarLetter = "MF";
            this.materialAvatarView1.Depth = 0;
            this.materialAvatarView1.Elevation = 5;
            this.materialAvatarView1.Location = new System.Drawing.Point(498, 169);
            this.materialAvatarView1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialAvatarView1.Name = "materialAvatarView1";
            graphicsPath4.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialAvatarView1.ShadowBorder = graphicsPath4;
            this.materialAvatarView1.Size = new System.Drawing.Size(277, 277);
            this.materialAvatarView1.TabIndex = 34;
            this.materialAvatarView1.Text = "materialAvatarView1";
            // 
            // materialDropDownColorPicker1
            // 
            this.materialDropDownColorPicker1.AnchorSize = new System.Drawing.Size(88, 21);
            this.materialDropDownColorPicker1.BackColor = System.Drawing.SystemColors.Control;
            this.materialDropDownColorPicker1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(64)))), ((int)(((byte)(129)))));
            this.materialDropDownColorPicker1.Depth = 0;
            this.materialDropDownColorPicker1.DockSide = MaterialWinforms.Controls.DropDownControl.eDockSide.Left;
            this.materialDropDownColorPicker1.Location = new System.Drawing.Point(574, 75);
            this.materialDropDownColorPicker1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialDropDownColorPicker1.Name = "materialDropDownColorPicker1";
            this.materialDropDownColorPicker1.Size = new System.Drawing.Size(88, 21);
            this.materialDropDownColorPicker1.TabIndex = 4;
            // 
            // materialDropDownDatePicker1
            // 
            this.materialDropDownDatePicker1.AnchorSize = new System.Drawing.Size(181, 21);
            this.materialDropDownDatePicker1.BackColor = System.Drawing.SystemColors.Control;
            this.materialDropDownDatePicker1.Date = new System.DateTime(2016, 7, 25, 14, 22, 14, 444);
            this.materialDropDownDatePicker1.Depth = 0;
            this.materialDropDownDatePicker1.DockSide = MaterialWinforms.Controls.DropDownControl.eDockSide.Left;
            this.materialDropDownDatePicker1.Location = new System.Drawing.Point(574, 47);
            this.materialDropDownDatePicker1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialDropDownDatePicker1.Name = "materialDropDownDatePicker1";
            this.materialDropDownDatePicker1.Size = new System.Drawing.Size(181, 21);
            this.materialDropDownDatePicker1.TabIndex = 3;
            // 
            // materialColorPicker1
            // 
            this.materialColorPicker1.Depth = 0;
            this.materialColorPicker1.Location = new System.Drawing.Point(12, 3);
            this.materialColorPicker1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialColorPicker1.Name = "materialColorPicker1";
            this.materialColorPicker1.Size = new System.Drawing.Size(250, 425);
            this.materialColorPicker1.TabIndex = 2;
            this.materialColorPicker1.Text = "materialColorPicker1";
            this.materialColorPicker1.Value = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(64)))), ((int)(((byte)(129)))));
            // 
            // materialDivider1
            // 
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(8, 513);
            this.materialDivider1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(813, 1);
            this.materialDivider1.TabIndex = 33;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // materialDatePicker1
            // 
            this.materialDatePicker1.Date = new System.DateTime(2016, 7, 25, 14, 22, 14, 457);
            this.materialDatePicker1.Depth = 0;
            this.materialDatePicker1.Location = new System.Drawing.Point(268, 3);
            this.materialDatePicker1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialDatePicker1.Name = "materialDatePicker1";
            this.materialDatePicker1.Size = new System.Drawing.Size(250, 425);
            this.materialDatePicker1.TabIndex = 1;
            this.materialDatePicker1.Text = "materialDatePicker1";
            // 
            // materialTabPage1
            // 
            this.materialTabPage1.Closable = true;
            this.materialTabPage1.Controls.Add(this.materialBreadCrumbToolbar1);
            this.materialTabPage1.Controls.Add(this.materialTextBox1);
            this.materialTabPage1.Controls.Add(this.materialCard3);
            this.materialTabPage1.Controls.Add(this.materialSingleLineTextField1);
            this.materialTabPage1.Controls.Add(this.materialComboBox1);
            this.materialTabPage1.Controls.Add(this.materialProgressBar1);
            this.materialTabPage1.Controls.Add(this.materialLoadingFloatingActionButton1);
            this.materialTabPage1.Controls.Add(this.materialLabel1);
            this.materialTabPage1.Controls.Add(this.materialFolderInput1);
            this.materialTabPage1.Controls.Add(this.materialFileInput1);
            this.materialTabPage1.Controls.Add(this.materialCard2);
            this.materialTabPage1.Controls.Add(this.materialCard1);
            this.materialTabPage1.Depth = 0;
            this.materialTabPage1.Location = new System.Drawing.Point(4, 22);
            this.materialTabPage1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTabPage1.Name = "materialTabPage1";
            this.materialTabPage1.Size = new System.Drawing.Size(775, 555);
            this.materialTabPage1.TabIndex = 0;
            this.materialTabPage1.Text = "Controls";
            // 
            // materialBreadCrumbToolbar1
            // 
            this.materialBreadCrumbToolbar1.Depth = 0;
            this.materialBreadCrumbToolbar1.Items.Add(((MaterialWinforms.Controls.BreadCrumbItem)(resources.GetObject("materialBreadCrumbToolbar1.Items"))));
            this.materialBreadCrumbToolbar1.Items.Add(((MaterialWinforms.Controls.BreadCrumbItem)(resources.GetObject("materialBreadCrumbToolbar1.Items1"))));
            this.materialBreadCrumbToolbar1.Items.Add(((MaterialWinforms.Controls.BreadCrumbItem)(resources.GetObject("materialBreadCrumbToolbar1.Items2"))));
            this.materialBreadCrumbToolbar1.Items.Add(((MaterialWinforms.Controls.BreadCrumbItem)(resources.GetObject("materialBreadCrumbToolbar1.Items3"))));
            this.materialBreadCrumbToolbar1.Location = new System.Drawing.Point(26, 4);
            this.materialBreadCrumbToolbar1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialBreadCrumbToolbar1.Name = "materialBreadCrumbToolbar1";
            this.materialBreadCrumbToolbar1.Padding = new System.Windows.Forms.Padding(5);
            this.materialBreadCrumbToolbar1.Size = new System.Drawing.Size(730, 23);
            this.materialBreadCrumbToolbar1.TabIndex = 48;
            this.materialBreadCrumbToolbar1.Text = "materialBreadCrumbToolbar1";
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.Font = new System.Drawing.Font("Roboto", 10F);
            this.materialTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialTextBox1.Hint = "Testhint";
            this.materialTextBox1.Location = new System.Drawing.Point(552, 218);
            this.materialTextBox1.MaxLength = 32767;
            this.materialTextBox1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.ReadOnly = false;
            this.materialTextBox1.SelectedText = "";
            this.materialTextBox1.SelectionLength = 0;
            this.materialTextBox1.SelectionStart = 0;
            this.materialTextBox1.Size = new System.Drawing.Size(204, 134);
            this.materialTextBox1.TabIndex = 47;
            this.materialTextBox1.TabStop = false;
            // 
            // materialCard3
            // 
            this.materialCard3.Controls.Add(this.materialLabel2);
            this.materialCard3.Depth = 0;
            this.materialCard3.Elevation = 5;
            this.materialCard3.LargeTitle = false;
            this.materialCard3.Location = new System.Drawing.Point(274, 347);
            this.materialCard3.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialCard3.Name = "materialCard3";
            this.materialCard3.Padding = new System.Windows.Forms.Padding(5, 25, 5, 5);
            graphicsPath5.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialCard3.ShadowBorder = graphicsPath5;
            this.materialCard3.Size = new System.Drawing.Size(259, 54);
            this.materialCard3.TabIndex = 46;
            this.materialCard3.Title = null;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialLabel2.Location = new System.Drawing.Point(41, 17);
            this.materialLabel2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(108, 19);
            this.materialLabel2.TabIndex = 0;
            this.materialLabel2.Text = "materialLabel2";
            // 
            // materialSingleLineTextField1
            // 
            this.materialSingleLineTextField1.Depth = 0;
            this.materialSingleLineTextField1.Enabled = false;
            this.materialSingleLineTextField1.Hint = "materialSingleLineTextField1";
            this.materialSingleLineTextField1.Location = new System.Drawing.Point(320, 286);
            this.materialSingleLineTextField1.MaxLength = 32767;
            this.materialSingleLineTextField1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialSingleLineTextField1.Name = "materialSingleLineTextField1";
            this.materialSingleLineTextField1.PasswordChar = '\0';
            this.materialSingleLineTextField1.ReadOnly = false;
            this.materialSingleLineTextField1.SelectedText = "";
            this.materialSingleLineTextField1.SelectionLength = 0;
            this.materialSingleLineTextField1.SelectionStart = 0;
            this.materialSingleLineTextField1.Size = new System.Drawing.Size(213, 38);
            this.materialSingleLineTextField1.TabIndex = 44;
            this.materialSingleLineTextField1.TabStop = false;
            this.materialSingleLineTextField1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialSingleLineTextField1.UseSystemPasswordChar = false;
            // 
            // materialComboBox1
            // 
            this.materialComboBox1.AllowDrop = true;
            this.materialComboBox1.Depth = 0;
            this.materialComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.materialComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.materialComboBox1.Items.AddRange(new object[] {
            "MaterialComboBox1",
            "Test",
            "Item",
            "TestItem"});
            this.materialComboBox1.Location = new System.Drawing.Point(320, 218);
            this.materialComboBox1.MaxLength = 32767;
            this.materialComboBox1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialComboBox1.Name = "materialComboBox1";
            this.materialComboBox1.Size = new System.Drawing.Size(213, 21);
            this.materialComboBox1.TabIndex = 43;
            // 
            // materialProgressBar1
            // 
            this.materialProgressBar1.Depth = 0;
            this.materialProgressBar1.InvertedProgressBar = false;
            this.materialProgressBar1.Location = new System.Drawing.Point(26, 443);
            this.materialProgressBar1.Maximum = 1;
            this.materialProgressBar1.Minimum = 0;
            this.materialProgressBar1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialProgressBar1.Name = "materialProgressBar1";
            this.materialProgressBar1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.materialProgressBar1.Size = new System.Drawing.Size(769, 5);
            this.materialProgressBar1.Step = 0;
            this.materialProgressBar1.Style = MaterialWinforms.Controls.MaterialProgressBar.ProgressStyle.Indeterminate;
            this.materialProgressBar1.TabIndex = 42;
            this.materialProgressBar1.Value = 1;
            // 
            // materialLoadingFloatingActionButton1
            // 
            this.materialLoadingFloatingActionButton1.Depth = 0;
            this.materialLoadingFloatingActionButton1.Elevation = 5;
            this.materialLoadingFloatingActionButton1.Icon = null;
            this.materialLoadingFloatingActionButton1.Location = new System.Drawing.Point(38, 379);
            this.materialLoadingFloatingActionButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialLoadingFloatingActionButton1.Name = "materialLoadingFloatingActionButton1";
            graphicsPath6.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialLoadingFloatingActionButton1.ShadowBorder = graphicsPath6;
            this.materialLoadingFloatingActionButton1.Size = new System.Drawing.Size(48, 48);
            this.materialLoadingFloatingActionButton1.TabIndex = 41;
            this.materialLoadingFloatingActionButton1.Text = "materialLoadingFloatingActionButton1";
            this.materialLoadingFloatingActionButton1.UseVisualStyleBackColor = false;
            this.materialLoadingFloatingActionButton1.Click += new System.EventHandler(this.materialLoadingFloatingActionButton1_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialLabel1.Location = new System.Drawing.Point(26, 347);
            this.materialLabel1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(108, 19);
            this.materialLabel1.TabIndex = 40;
            this.materialLabel1.Text = "materialLabel1";
            // 
            // materialFolderInput1
            // 
            this.materialFolderInput1.Depth = 0;
            this.materialFolderInput1.Filter = "All Files (*.*)|*.*";
            this.materialFolderInput1.FocusedColor = "#508ef5";
            this.materialFolderInput1.FontColor = "#999999";
            this.materialFolderInput1.Hint = "materialFolderInput1";
            this.materialFolderInput1.IsEnabled = true;
            this.materialFolderInput1.Location = new System.Drawing.Point(26, 286);
            this.materialFolderInput1.MaxLength = 32767;
            this.materialFolderInput1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFolderInput1.Name = "materialFolderInput1";
            this.materialFolderInput1.ReadOnly = false;
            this.materialFolderInput1.Size = new System.Drawing.Size(172, 38);
            this.materialFolderInput1.TabIndex = 39;
            this.materialFolderInput1.Text = "materialFolderInput1";
            this.materialFolderInput1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialFolderInput1.UseSystemPasswordChar = false;
            // 
            // materialFileInput1
            // 
            this.materialFileInput1.Depth = 0;
            this.materialFileInput1.Filter = "All Files (*.*)|*.*";
            this.materialFileInput1.FocusedColor = "#508ef5";
            this.materialFileInput1.FontColor = "#999999";
            this.materialFileInput1.Hint = "materialFileInput1";
            this.materialFileInput1.IsEnabled = true;
            this.materialFileInput1.Location = new System.Drawing.Point(26, 218);
            this.materialFileInput1.MaxLength = 32767;
            this.materialFileInput1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFileInput1.Name = "materialFileInput1";
            this.materialFileInput1.ReadOnly = false;
            this.materialFileInput1.Size = new System.Drawing.Size(172, 38);
            this.materialFileInput1.TabIndex = 38;
            this.materialFileInput1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialFileInput1.UseSystemPasswordChar = false;
            // 
            // materialCard2
            // 
            this.materialCard2.Controls.Add(this.materialToggle3);
            this.materialCard2.Controls.Add(this.materialToggle4);
            this.materialCard2.Controls.Add(this.materialToggle2);
            this.materialCard2.Controls.Add(this.materialToggle1);
            this.materialCard2.Controls.Add(this.materialCheckBox3);
            this.materialCard2.Controls.Add(this.materialCheckBox4);
            this.materialCard2.Controls.Add(this.materialCheckBox2);
            this.materialCard2.Controls.Add(this.materialCheckBox1);
            this.materialCard2.Depth = 0;
            this.materialCard2.Elevation = 10;
            this.materialCard2.LargeTitle = false;
            this.materialCard2.Location = new System.Drawing.Point(387, 33);
            this.materialCard2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialCard2.Name = "materialCard2";
            this.materialCard2.Padding = new System.Windows.Forms.Padding(5, 25, 5, 5);
            graphicsPath7.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialCard2.ShadowBorder = graphicsPath7;
            this.materialCard2.Size = new System.Drawing.Size(349, 161);
            this.materialCard2.TabIndex = 37;
            this.materialCard2.Title = "Toggle";
            // 
            // materialToggle3
            // 
            this.materialToggle3.AutoSize = true;
            this.materialToggle3.Checked = true;
            this.materialToggle3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.materialToggle3.Depth = 0;
            this.materialToggle3.EllipseBorderColor = "#3b73d1";
            this.materialToggle3.EllipseColor = "#508ef5";
            this.materialToggle3.Enabled = false;
            this.materialToggle3.Location = new System.Drawing.Point(75, 134);
            this.materialToggle3.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialToggle3.Name = "materialToggle3";
            this.materialToggle3.Size = new System.Drawing.Size(47, 19);
            this.materialToggle3.TabIndex = 7;
            this.materialToggle3.Text = "materialToggle3";
            this.materialToggle3.UseVisualStyleBackColor = true;
            // 
            // materialToggle4
            // 
            this.materialToggle4.AutoSize = true;
            this.materialToggle4.Depth = 0;
            this.materialToggle4.EllipseBorderColor = "#3b73d1";
            this.materialToggle4.EllipseColor = "#508ef5";
            this.materialToggle4.Enabled = false;
            this.materialToggle4.Location = new System.Drawing.Point(75, 95);
            this.materialToggle4.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialToggle4.Name = "materialToggle4";
            this.materialToggle4.Size = new System.Drawing.Size(47, 19);
            this.materialToggle4.TabIndex = 6;
            this.materialToggle4.Text = "materialToggle4";
            this.materialToggle4.UseVisualStyleBackColor = true;
            // 
            // materialToggle2
            // 
            this.materialToggle2.AutoSize = true;
            this.materialToggle2.Checked = true;
            this.materialToggle2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.materialToggle2.Depth = 0;
            this.materialToggle2.EllipseBorderColor = "#3b73d1";
            this.materialToggle2.EllipseColor = "#508ef5";
            this.materialToggle2.Location = new System.Drawing.Point(9, 134);
            this.materialToggle2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialToggle2.Name = "materialToggle2";
            this.materialToggle2.Size = new System.Drawing.Size(47, 19);
            this.materialToggle2.TabIndex = 5;
            this.materialToggle2.Text = "materialToggle2";
            this.materialToggle2.UseVisualStyleBackColor = true;
            // 
            // materialToggle1
            // 
            this.materialToggle1.AutoSize = true;
            this.materialToggle1.Depth = 0;
            this.materialToggle1.EllipseBorderColor = "#3b73d1";
            this.materialToggle1.EllipseColor = "#508ef5";
            this.materialToggle1.Location = new System.Drawing.Point(9, 95);
            this.materialToggle1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialToggle1.Name = "materialToggle1";
            this.materialToggle1.Size = new System.Drawing.Size(47, 19);
            this.materialToggle1.TabIndex = 4;
            this.materialToggle1.Text = "materialToggle1";
            this.materialToggle1.UseVisualStyleBackColor = true;
            // 
            // materialCheckBox3
            // 
            this.materialCheckBox3.AutoSize = true;
            this.materialCheckBox3.Checked = true;
            this.materialCheckBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.materialCheckBox3.Depth = 0;
            this.materialCheckBox3.Enabled = false;
            this.materialCheckBox3.Font = new System.Drawing.Font("Roboto", 10F);
            this.materialCheckBox3.Location = new System.Drawing.Point(170, 59);
            this.materialCheckBox3.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckBox3.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckBox3.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialCheckBox3.Name = "materialCheckBox3";
            this.materialCheckBox3.Ripple = true;
            this.materialCheckBox3.Size = new System.Drawing.Size(150, 30);
            this.materialCheckBox3.TabIndex = 3;
            this.materialCheckBox3.Text = "materialCheckBox3";
            this.materialCheckBox3.UseVisualStyleBackColor = true;
            // 
            // materialCheckBox4
            // 
            this.materialCheckBox4.AutoSize = true;
            this.materialCheckBox4.Depth = 0;
            this.materialCheckBox4.Enabled = false;
            this.materialCheckBox4.Font = new System.Drawing.Font("Roboto", 10F);
            this.materialCheckBox4.Location = new System.Drawing.Point(170, 29);
            this.materialCheckBox4.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckBox4.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckBox4.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialCheckBox4.Name = "materialCheckBox4";
            this.materialCheckBox4.Ripple = true;
            this.materialCheckBox4.Size = new System.Drawing.Size(150, 30);
            this.materialCheckBox4.TabIndex = 2;
            this.materialCheckBox4.Text = "materialCheckBox4";
            this.materialCheckBox4.UseVisualStyleBackColor = true;
            // 
            // materialCheckBox2
            // 
            this.materialCheckBox2.AutoSize = true;
            this.materialCheckBox2.Checked = true;
            this.materialCheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.materialCheckBox2.Depth = 0;
            this.materialCheckBox2.Font = new System.Drawing.Font("Roboto", 10F);
            this.materialCheckBox2.Location = new System.Drawing.Point(9, 59);
            this.materialCheckBox2.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckBox2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckBox2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialCheckBox2.Name = "materialCheckBox2";
            this.materialCheckBox2.Ripple = true;
            this.materialCheckBox2.Size = new System.Drawing.Size(150, 30);
            this.materialCheckBox2.TabIndex = 1;
            this.materialCheckBox2.Text = "materialCheckBox2";
            this.materialCheckBox2.UseVisualStyleBackColor = true;
            // 
            // materialCheckBox1
            // 
            this.materialCheckBox1.AutoSize = true;
            this.materialCheckBox1.Depth = 0;
            this.materialCheckBox1.Font = new System.Drawing.Font("Roboto", 10F);
            this.materialCheckBox1.Location = new System.Drawing.Point(9, 29);
            this.materialCheckBox1.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckBox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckBox1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialCheckBox1.Name = "materialCheckBox1";
            this.materialCheckBox1.Ripple = true;
            this.materialCheckBox1.Size = new System.Drawing.Size(150, 30);
            this.materialCheckBox1.TabIndex = 0;
            this.materialCheckBox1.Text = "materialCheckBox1";
            this.materialCheckBox1.UseVisualStyleBackColor = true;
            // 
            // materialCard1
            // 
            this.materialCard1.Controls.Add(this.materialSlider2);
            this.materialCard1.Controls.Add(this.materialSlider1);
            this.materialCard1.Depth = 0;
            this.materialCard1.Elevation = 10;
            this.materialCard1.LargeTitle = false;
            this.materialCard1.Location = new System.Drawing.Point(26, 33);
            this.materialCard1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(5, 25, 5, 5);
            graphicsPath8.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialCard1.ShadowBorder = graphicsPath8;
            this.materialCard1.Size = new System.Drawing.Size(315, 161);
            this.materialCard1.TabIndex = 36;
            this.materialCard1.Title = "Slider";
            // 
            // materialSlider2
            // 
            this.materialSlider2.Depth = 0;
            this.materialSlider2.Enabled = false;
            this.materialSlider2.Location = new System.Drawing.Point(26, 42);
            this.materialSlider2.MaxValue = 100;
            this.materialSlider2.MinValue = 0;
            this.materialSlider2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialSlider2.Name = "materialSlider2";
            this.materialSlider2.Size = new System.Drawing.Size(250, 40);
            this.materialSlider2.TabIndex = 35;
            this.materialSlider2.Text = "materialSlider2";
            this.materialSlider2.Value = 50;
            // 
            // materialSlider1
            // 
            this.materialSlider1.Depth = 0;
            this.materialSlider1.Location = new System.Drawing.Point(26, 95);
            this.materialSlider1.MaxValue = 100;
            this.materialSlider1.MinValue = 0;
            this.materialSlider1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialSlider1.Name = "materialSlider1";
            this.materialSlider1.Size = new System.Drawing.Size(250, 40);
            this.materialSlider1.TabIndex = 34;
            this.materialSlider1.Text = "materialSlider1";
            this.materialSlider1.Value = 50;
            // 
            // materialTabPage3
            // 
            this.materialTabPage3.Closable = true;
            this.materialTabPage3.Controls.Add(this.materialPanel1);
            this.materialTabPage3.Depth = 0;
            this.materialTabPage3.Location = new System.Drawing.Point(4, 22);
            this.materialTabPage3.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTabPage3.Name = "materialTabPage3";
            this.materialTabPage3.Size = new System.Drawing.Size(775, 555);
            this.materialTabPage3.TabIndex = 2;
            this.materialTabPage3.Text = "Timeline";
            // 
            // materialPanel1
            // 
            this.materialPanel1.AutoScroll = true;
            this.materialPanel1.Controls.Add(this.materialTimeline2);
            this.materialPanel1.Depth = 0;
            this.materialPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialPanel1.Location = new System.Drawing.Point(0, 0);
            this.materialPanel1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialPanel1.Name = "materialPanel1";
            this.materialPanel1.Size = new System.Drawing.Size(775, 555);
            this.materialPanel1.TabIndex = 1;
            this.materialPanel1.Text = "materialPanel1";
            // 
            // materialTimeline2
            // 
            this.materialTimeline2.AufsteigendSortieren = false;
            this.materialTimeline2.Depth = 0;
            this.materialTimeline2.Location = new System.Drawing.Point(6, 3);
            this.materialTimeline2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTimeline2.Name = "materialTimeline2";
            this.materialTimeline2.Size = new System.Drawing.Size(954, 508);
            this.materialTimeline2.TabIndex = 3;
            this.materialTimeline2.Text = "materialTimeline2";
            // 
            // materialTabPage2
            // 
            this.materialTabPage2.Closable = true;
            this.materialTabPage2.Controls.Add(this.materialRaisedButton2);
            this.materialTabPage2.Controls.Add(this.treeView1);
            this.materialTabPage2.Controls.Add(this.materialTreeControl1);
            this.materialTabPage2.Controls.Add(this.materialFlatButton6);
            this.materialTabPage2.Controls.Add(this.materialFlatButton5);
            this.materialTabPage2.Controls.Add(this.materialFlatButton4);
            this.materialTabPage2.Depth = 0;
            this.materialTabPage2.Location = new System.Drawing.Point(4, 22);
            this.materialTabPage2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTabPage2.Name = "materialTabPage2";
            this.materialTabPage2.Size = new System.Drawing.Size(775, 555);
            this.materialTabPage2.TabIndex = 3;
            this.materialTabPage2.Text = "Dialogs and windows";
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(308, 211);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Knoten0";
            treeNode1.Text = "Knoten0";
            treeNode2.Name = "Knoten1";
            treeNode2.Text = "Knoten1";
            treeNode3.Name = "Knoten3";
            treeNode3.Text = "Knoten3";
            treeNode4.Name = "Knoten5";
            treeNode4.Text = "Knoten5";
            treeNode5.Name = "Knoten6";
            treeNode5.Text = "Knoten6";
            treeNode6.Name = "Knoten4";
            treeNode6.Text = "Knoten4";
            treeNode7.Name = "Knoten2";
            treeNode7.Text = "Knoten2";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode7});
            this.treeView1.Size = new System.Drawing.Size(208, 184);
            this.treeView1.TabIndex = 4;
            // 
            // materialTreeControl1
            // 
            this.materialTreeControl1.BackColor = System.Drawing.SystemColors.Control;
            this.materialTreeControl1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTreeControl1.CheckBoxes = true;
            this.materialTreeControl1.Depth = 0;
            this.materialTreeControl1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.materialTreeControl1.Location = new System.Drawing.Point(47, 211);
            this.materialTreeControl1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTreeControl1.Name = "materialTreeControl1";
            treeNode8.Name = "Knoten0";
            treeNode8.Text = "Knoten0";
            treeNode9.Name = "Knoten2";
            treeNode9.Text = "Knoten2";
            treeNode10.Name = "Knoten0";
            treeNode10.Text = "Knoten0";
            treeNode11.Name = "Knoten1";
            treeNode11.Text = "Knoten1";
            treeNode12.Name = "Knoten3";
            treeNode12.Text = "Knoten3";
            treeNode13.Name = "Knoten4";
            treeNode13.Text = "Knoten4";
            treeNode14.Name = "Knoten1";
            treeNode14.Text = "Knoten1";
            treeNode15.Name = "Knoten2";
            treeNode15.Text = "Knoten2";
            this.materialTreeControl1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode14,
            treeNode15});
            this.materialTreeControl1.Size = new System.Drawing.Size(221, 298);
            this.materialTreeControl1.TabIndex = 3;
            // 
            // materialFlatButton6
            // 
            this.materialFlatButton6.Accent = false;
            this.materialFlatButton6.AutoSize = true;
            this.materialFlatButton6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton6.Capitalized = true;
            this.materialFlatButton6.Depth = 0;
            this.materialFlatButton6.IconImage = null;
            this.materialFlatButton6.Location = new System.Drawing.Point(13, 102);
            this.materialFlatButton6.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton6.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFlatButton6.Name = "materialFlatButton6";
            this.materialFlatButton6.Primary = false;
            this.materialFlatButton6.Selected = false;
            this.materialFlatButton6.Size = new System.Drawing.Size(120, 36);
            this.materialFlatButton6.TabIndex = 2;
            this.materialFlatButton6.Text = "Custom dialog";
            this.materialFlatButton6.UseVisualStyleBackColor = true;
            this.materialFlatButton6.Click += new System.EventHandler(this.materialFlatButton6_Click);
            // 
            // materialFlatButton5
            // 
            this.materialFlatButton5.Accent = false;
            this.materialFlatButton5.AutoSize = true;
            this.materialFlatButton5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton5.Capitalized = true;
            this.materialFlatButton5.Depth = 0;
            this.materialFlatButton5.IconImage = null;
            this.materialFlatButton5.Location = new System.Drawing.Point(13, 54);
            this.materialFlatButton5.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton5.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFlatButton5.Name = "materialFlatButton5";
            this.materialFlatButton5.Primary = false;
            this.materialFlatButton5.Selected = false;
            this.materialFlatButton5.Size = new System.Drawing.Size(60, 36);
            this.materialFlatButton5.TabIndex = 1;
            this.materialFlatButton5.Text = "Dialog";
            this.materialFlatButton5.UseVisualStyleBackColor = true;
            this.materialFlatButton5.Click += new System.EventHandler(this.materialFlatButton5_Click);
            // 
            // materialFlatButton4
            // 
            this.materialFlatButton4.Accent = false;
            this.materialFlatButton4.AutoSize = true;
            this.materialFlatButton4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton4.Capitalized = true;
            this.materialFlatButton4.Depth = 0;
            this.materialFlatButton4.IconImage = null;
            this.materialFlatButton4.Location = new System.Drawing.Point(13, 6);
            this.materialFlatButton4.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton4.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialFlatButton4.Name = "materialFlatButton4";
            this.materialFlatButton4.Primary = false;
            this.materialFlatButton4.Selected = false;
            this.materialFlatButton4.Size = new System.Drawing.Size(173, 36);
            this.materialFlatButton4.TabIndex = 0;
            this.materialFlatButton4.Text = "Heads up notification";
            this.materialFlatButton4.UseVisualStyleBackColor = true;
            this.materialFlatButton4.Click += new System.EventHandler(this.materialFlatButton4_Click);
            // 
            // materialTabPage5
            // 
            this.materialTabPage5.Closable = true;
            this.materialTabPage5.Depth = 0;
            this.materialTabPage5.Location = new System.Drawing.Point(4, 22);
            this.materialTabPage5.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTabPage5.Name = "materialTabPage5";
            this.materialTabPage5.Size = new System.Drawing.Size(775, 555);
            this.materialTabPage5.TabIndex = 4;
            this.materialTabPage5.Text = "spImportExportArtikelStücklisteHofmeier";
            // 
            // mcm_ComboBox1
            // 
            this.mcm_ComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.mcm_ComboBox1.Depth = 0;
            this.mcm_ComboBox1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.test2ToolStripMenuItem,
            this.wert3ToolStripMenuItem,
            this.toolStripMenuItem4});
            this.mcm_ComboBox1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.mcm_ComboBox1.Name = "mcm_ComboBox1";
            this.mcm_ComboBox1.Size = new System.Drawing.Size(106, 92);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // test2ToolStripMenuItem
            // 
            this.test2ToolStripMenuItem.Name = "test2ToolStripMenuItem";
            this.test2ToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.test2ToolStripMenuItem.Text = "Test2";
            // 
            // wert3ToolStripMenuItem
            // 
            this.wert3ToolStripMenuItem.Name = "wert3ToolStripMenuItem";
            this.wert3ToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.wert3ToolStripMenuItem.Text = "Wert3";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItem4.Text = "42";
            // 
            // materialActionBar1
            // 
            this.materialActionBar1.ActionBarButtons.Add(this.materialActionBarButton1);
            this.materialActionBar1.ActionBarButtons.Add(this.materialActionBarButton2);
            this.materialActionBar1.ActionBarMenu = this.ActionBarMenu;
            this.materialActionBar1.Controls.Add(this.materialActionBarButton1);
            this.materialActionBar1.Controls.Add(this.materialActionBarButton2);
            this.materialActionBar1.Depth = 0;
            this.materialActionBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialActionBar1.Elevation = 10;
            this.materialActionBar1.IntegratedSearchBar = true;
            this.materialActionBar1.Location = new System.Drawing.Point(0, 24);
            this.materialActionBar1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialActionBar1.Name = "materialActionBar1";
            this.materialActionBar1.SearchBarFilterIcon = true;
            graphicsPath10.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialActionBar1.ShadowBorder = graphicsPath10;
            this.materialActionBar1.Size = new System.Drawing.Size(993, 42);
            this.materialActionBar1.TabIndex = 26;
            // 
            // materialActionBarButton1
            // 
            this.materialActionBarButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialActionBarButton1.Depth = 0;
            this.materialActionBarButton1.Image = global::MaterialWinformsExample.Properties.Resources.ic_action_action_search;
            this.materialActionBarButton1.Location = new System.Drawing.Point(806, 0);
            this.materialActionBarButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialActionBarButton1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialActionBarButton1.Name = "materialActionBarButton1";
            this.materialActionBarButton1.Size = new System.Drawing.Size(42, 42);
            this.materialActionBarButton1.TabIndex = 0;
            this.materialActionBarButton1.Text = "materialActionBarButton1";
            // 
            // materialActionBarButton2
            // 
            this.materialActionBarButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialActionBarButton2.Depth = 0;
            this.materialActionBarButton2.Image = global::MaterialWinformsExample.Properties.Resources.ic_launcher;
            this.materialActionBarButton2.Location = new System.Drawing.Point(848, 0);
            this.materialActionBarButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialActionBarButton2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialActionBarButton2.Name = "materialActionBarButton2";
            this.materialActionBarButton2.Size = new System.Drawing.Size(42, 42);
            this.materialActionBarButton2.TabIndex = 0;
            this.materialActionBarButton2.Text = "materialActionBarButton2";
            // 
            // ActionBarMenu
            // 
            this.ActionBarMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.ActionBarMenu.Depth = 0;
            this.ActionBarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.ActionBarMenu.MouseState = MaterialWinforms.MouseState.HOVER;
            this.ActionBarMenu.Name = "materialContextMenuStrip2";
            this.ActionBarMenu.Size = new System.Drawing.Size(146, 26);
            this.ActionBarMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ActionBarMenu_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem1.Text = "Einstellungen";
            // 
            // materialTabSelectorClosable1
            // 
            this.materialTabSelectorClosable1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelectorClosable1.CenterTabs = false;
            this.materialTabSelectorClosable1.Depth = 0;
            this.materialTabSelectorClosable1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialTabSelectorClosable1.Elevation = 10;
            this.materialTabSelectorClosable1.Location = new System.Drawing.Point(0, 66);
            this.materialTabSelectorClosable1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.materialTabSelectorClosable1.MaxTabWidht = 0;
            this.materialTabSelectorClosable1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialTabSelectorClosable1.Name = "materialTabSelectorClosable1";
            graphicsPath11.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialTabSelectorClosable1.ShadowBorder = graphicsPath11;
            this.materialTabSelectorClosable1.Size = new System.Drawing.Size(993, 35);
            this.materialTabSelectorClosable1.TabIndex = 32;
            this.materialTabSelectorClosable1.TabPadding = 24;
            this.materialTabSelectorClosable1.Text = "materialTabSelectorClosable1";
            // 
            // materialSideDrawer1
            // 
            this.materialSideDrawer1.AutoScroll = true;
            this.materialSideDrawer1.Depth = 0;
            this.materialSideDrawer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.materialSideDrawer1.Elevation = 10;
            this.materialSideDrawer1.HiddenOnStart = false;
            this.materialSideDrawer1.HideSideDrawer = false;
            this.materialSideDrawer1.Location = new System.Drawing.Point(0, 101);
            this.materialSideDrawer1.MaximumSize = new System.Drawing.Size(210, 10000);
            this.materialSideDrawer1.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialSideDrawer1.Name = "materialSideDrawer1";
            this.materialSideDrawer1.SelectOnClick = false;
            graphicsPath12.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialSideDrawer1.ShadowBorder = graphicsPath12;
            this.materialSideDrawer1.SideDrawer = this.SideDrawerList;
            this.materialSideDrawer1.SideDrawerFixiert = false;
            this.materialSideDrawer1.SideDrawerUnterActionBar = false;
            this.materialSideDrawer1.Size = new System.Drawing.Size(210, 587);
            this.materialSideDrawer1.TabIndex = 34;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(210, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 581);
            this.panel1.TabIndex = 35;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(210, 101);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(783, 6);
            this.panel2.TabIndex = 36;
            // 
            // materialRaisedButton2
            // 
            this.materialRaisedButton2.Depth = 0;
            this.materialRaisedButton2.Elevation = 5;
            this.materialRaisedButton2.Location = new System.Drawing.Point(354, 483);
            this.materialRaisedButton2.MouseState = MaterialWinforms.MouseState.HOVER;
            this.materialRaisedButton2.Name = "materialRaisedButton2";
            this.materialRaisedButton2.Primary = true;
            graphicsPath9.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            this.materialRaisedButton2.ShadowBorder = graphicsPath9;
            this.materialRaisedButton2.Size = new System.Drawing.Size(117, 47);
            this.materialRaisedButton2.TabIndex = 5;
            this.materialRaisedButton2.Text = "materialRaisedButton2";
            this.materialRaisedButton2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.ActionBar = this.materialActionBar1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(993, 688);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.materialFloatingActionButton1);
            this.Controls.Add(this.materialButton1);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.materialSideDrawer1);
            this.Controls.Add(this.materialTabSelectorClosable1);
            this.Controls.Add(this.materialActionBar1);
            this.Name = "MainForm";
            this.SideDrawer = this.materialSideDrawer1;
            this.Text = "Datenbank Dokumentation";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SideDrawerList.ResumeLayout(false);
            this.materialTabControl1.ResumeLayout(false);
            this.mtp_Picker.ResumeLayout(false);
            this.mtp_Picker.PerformLayout();
            this.materialTabPage1.ResumeLayout(false);
            this.materialTabPage1.PerformLayout();
            this.materialCard3.ResumeLayout(false);
            this.materialCard3.PerformLayout();
            this.materialCard2.ResumeLayout(false);
            this.materialCard2.PerformLayout();
            this.materialCard1.ResumeLayout(false);
            this.materialTabPage3.ResumeLayout(false);
            this.materialPanel1.ResumeLayout(false);
            this.materialTabPage2.ResumeLayout(false);
            this.materialTabPage2.PerformLayout();
            this.mcm_ComboBox1.ResumeLayout(false);
            this.materialActionBar1.ResumeLayout(false);
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
        private MaterialTabSelector materialTabSelectorClosable1;
        private MaterialContextMenuStrip ActionBarMenu;
        private MaterialTabPage materialTabPage1;
        private MaterialToolStripMenuItem tmi_Datenbank2;
        private MaterialToolStripMenuItem tmi_Sichten;
        private MaterialToolStripMenuItem tmi_Funktionen;
        private MaterialToolStripMenuItem tmi_Prozeduren;
        private MaterialToolStripMenuItem tmi_Trigger;
        private MaterialToolStripMenuItem tmi_Tabellen;
        private MaterialSlider materialSlider1;
        private MaterialSlider materialSlider2;
        private MaterialCard materialCard2;
        private MaterialToggle materialToggle3;
        private MaterialToggle materialToggle4;
        private MaterialToggle materialToggle2;
        private MaterialToggle materialToggle1;
        private MaterialCheckBox materialCheckBox3;
        private MaterialCheckBox materialCheckBox4;
        private MaterialCheckBox materialCheckBox2;
        private MaterialCheckBox materialCheckBox1;
        private MaterialCard materialCard1;
        private MaterialTabPage mtp_Picker;
        private MaterialDivider materialDivider1;
        private MaterialProgressBar materialProgressBar1;
        private MaterialLoadingFloatingActionButton materialLoadingFloatingActionButton1;
        private MaterialLabel materialLabel1;
        private MaterialFolderInput materialFolderInput1;
        private MaterialFileInput materialFileInput1;
        private MaterialComboBox materialComboBox1;
        private MaterialContextMenuStrip mcm_ComboBox1;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripMenuItem test2ToolStripMenuItem;
        private ToolStripMenuItem wert3ToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem4;
        private MaterialSingleLineTextField materialSingleLineTextField1;
        private MaterialDatePicker materialDatePicker1;
        private MaterialColorPicker materialColorPicker1;
        private MaterialDropDownDatePicker materialDropDownDatePicker1;
        private MaterialDropDownColorPicker materialDropDownColorPicker1;
        private MaterialCard materialCard3;
        private MaterialLabel materialLabel2;
        private MaterialTextBox materialTextBox1;
        private MaterialActionBarButton materialActionBarButton1;
        private MaterialActionBarButton materialActionBarButton2;
        private ToolStripMenuItem testToolStripMenuItem1;
        private ToolStripMenuItem eafToolStripMenuItem;
        private ToolStripMenuItem asdfToolStripMenuItem;
        private ToolStripMenuItem sadfToolStripMenuItem;
        private ToolStripMenuItem sadfToolStripMenuItem1;
        private ToolStripMenuItem asdfToolStripMenuItem1;
        private ToolStripMenuItem asdfToolStripMenuItem2;
        private ToolStripMenuItem asToolStripMenuItem;
        private ToolStripMenuItem vdToolStripMenuItem;
        private ToolStripMenuItem asToolStripMenuItem1;
        private ToolStripMenuItem dToolStripMenuItem;
        private ToolStripMenuItem vToolStripMenuItem;
        private ToolStripMenuItem aToolStripMenuItem;
        private ToolStripMenuItem sdToolStripMenuItem;
        private ToolStripMenuItem vToolStripMenuItem1;
        private ToolStripMenuItem qToolStripMenuItem;
        private ToolStripMenuItem weToolStripMenuItem;
        private ToolStripMenuItem qToolStripMenuItem1;
        private ToolStripMenuItem fgvsToolStripMenuItem;
        private ToolStripMenuItem dToolStripMenuItem1;
        private ToolStripMenuItem fToolStripMenuItem;
        private ToolStripMenuItem asToolStripMenuItem2;
        private ToolStripMenuItem dToolStripMenuItem2;
        private ToolStripMenuItem fToolStripMenuItem1;
        private ToolStripMenuItem asdToolStripMenuItem;
        private ToolStripMenuItem vToolStripMenuItem2;
        private ToolStripMenuItem aToolStripMenuItem1;
        private ToolStripMenuItem sdToolStripMenuItem1;
        private ToolStripMenuItem fToolStripMenuItem2;
        private ToolStripMenuItem qToolStripMenuItem2;
        private ToolStripMenuItem weToolStripMenuItem1;
        private ToolStripMenuItem fToolStripMenuItem3;
        private ToolStripMenuItem aToolStripMenuItem2;
        private ToolStripMenuItem sdToolStripMenuItem2;
        private ToolStripMenuItem vToolStripMenuItem3;
        private ToolStripMenuItem aToolStripMenuItem3;
        private ToolStripMenuItem sdToolStripMenuItem3;
        private ToolStripMenuItem qvgToolStripMenuItem;
        private ToolStripMenuItem eToolStripMenuItem;
        private ToolStripMenuItem rToolStripMenuItem;
        private ToolStripMenuItem bgToolStripMenuItem;
        private ToolStripMenuItem sfdToolStripMenuItem;
        private MaterialTabPage materialTabPage3;
        private MaterialPanel materialPanel1;
        private MaterialTimeline materialTimeline2;
        private MaterialSideDrawer materialSideDrawer1;
        private Panel panel1;
        private Panel panel2;
        private MaterialBreadCrumbToolbar materialBreadCrumbToolbar1;
        private MaterialTabPage materialTabPage2;
        private MaterialFlatButton materialFlatButton6;
        private MaterialFlatButton materialFlatButton5;
        private MaterialFlatButton materialFlatButton4;
        private ToolStripMenuItem toolStripMenuItem1;
        private MaterialTreeControl materialTreeControl1;
        private TreeView treeView1;
        private MaterialAvatarView materialAvatarView1;
        private MaterialTabPage materialTabPage5;
        private MaterialRaisedButton materialRaisedButton2;
    }
}