using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MaterialWinforms;
using MaterialWinforms.Controls;
using MaterialWinforms.Controls.Settings;

namespace MaterialWinformsExample
{
    public partial class MainForm : MaterialWinforms.Controls.MaterialForm
    {
        private readonly MaterialSkinManager MaterialWinformsManager;
        private ColorSchemePresetCollection objSchemes;
        public MainForm()
        {
            InitializeComponent();

            objSchemes = new ColorSchemePresetCollection();


            // Initialize MaterialWinformsManager
            MaterialWinformsManager = MaterialSkinManager.Instance;
            MaterialWinformsManager.AddFormToManage(this);

            // Add dummy data to the listview
            seedListView();

            materialLoadingFloatingActionButton1.startProgressAnimation();

            materialActionBar1.onSearched += materialActionBar1_onSearched;
            materialActionBar1.Invalidate();

            materialTimeline2.onTimeLineEntryClicked += materialTimeline1_onTimeLineEntryClicked;

            for (int i = 0; i < 10; i++)
            {
                MaterialTimeLineEntry objEntry = new MaterialTimeLineEntry
                {
                    UserName = "Melvin Fengelsfd",
                    Title = "Title Tahfjksdbgfquiwegjkqsgdfkjqwe"+i,
                    Time = DateTime.Now.AddDays(-i),
                    Text = "Test Text",
                    UserInitialien = "MF"
                };
                materialTimeline2.Entrys.Add(objEntry);
            }

        }

        void materialTimeline1_onTimeLineEntryClicked(MaterialTimeLineEntry sender, EventArgs e)
        {
            HeadsUp objTest = new HeadsUp();
            objTest.Titel = sender.Title;
            objTest.Text = sender.Text;
            MaterialFlatButton DismissHeadsUp = new MaterialFlatButton();
            DismissHeadsUp.Tag = objTest;
            DismissHeadsUp.Text = "Dismiss";
            DismissHeadsUp.Click += DismissHeadsUp_Click;
            objTest.Buttons.Add(DismissHeadsUp);
            objTest.Show();
        }

        void DismissHeadsUp_Click(object sender, EventArgs e)
        {
            ((HeadsUp)((MaterialFlatButton)sender).Tag).Close();
        }

        void materialActionBar1_onSearched(string pText)
        {
            materialTabControl1.TabPages.Add(new MaterialTabPage("Suchergebnisse"));
            materialTabControl1.SelectedIndex = materialTabControl1.TabCount-1;
        }

        private void seedListView()
        {
            //Define
            var data = new[]
	        {
		        new []{"Lollipop", "392", "0.2", "0"},
				new []{"KitKat", "518", "26.0", "7"},
				new []{"Ice cream sandwich", "237", "9.0", "4.3"},
				new []{"Jelly Bean", "375", "0.0", "0.0"},
				new []{"Honeycomb", "408", "3.2", "6.5"}
	        };

            //Add
            foreach (string[] version in data)
            {
                var item = new ListViewItem(version);
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            MaterialWinformsManager.Theme = MaterialWinformsManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;
        }

        private int colorSchemeIndex;
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 2) colorSchemeIndex = 0;

            MaterialWinformsManager.LoadColorSchemeFromPreset(objSchemes.get(colorSchemeIndex));


        }


        private void materialFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void materialSideDrawer3_onSideDrawerItemClicked(object sender, MaterialSideDrawer.SideDrawerEventArgs e)
        {
            if (materialTabControl1.TabPages.ContainsKey(e.getClickedItem())) {
                materialTabControl1.SelectTab(e.getClickedItem());
            }else{
            materialTabControl1.TabPages.Add(new MaterialTabPage(e.getClickedItem(),true));
            materialTabControl1.SelectedTab = materialTabControl1.TabPages[materialTabControl1.TabCount-1];
        }
        }


        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterialWinformsManager.LoadColorSchemeFromPreset(objSchemes.get("Indigo Pink"));
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterialWinformsManager.LoadColorSchemeFromPreset(objSchemes.get(1));
        }

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterialWinformsManager.LoadColorSchemeFromPreset(objSchemes.get(2));
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MaterialWinformsManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MaterialWinformsManager.Theme = MaterialSkinManager.Themes.DARK;
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            MaterialSettings objSettings = new MaterialSettings(this);
            objSettings.ShowThemeSettings = true;
            objSettings.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            MaterialDialog.Show(this, "test", "test", MaterialDialog.Buttons.OKCancel);
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            HeadsUp objTest = new HeadsUp();
            objTest.Titel = "Title";
            objTest.Text = "Description Text to show Something that happend";
            MaterialFlatButton DismissHeadsUp = new MaterialFlatButton();
            DismissHeadsUp.Tag = objTest;
            DismissHeadsUp.Text = "Dismiss";
            DismissHeadsUp.Click += DismissHeadsUp_Click;
            objTest.Buttons.Add(DismissHeadsUp);
            objTest.Show();
        }

        private void materialFlatButton6_Click(object sender, EventArgs e)
        {
            UserControl t = new UserControl();
            MaterialColorPicker mcp = new MaterialColorPicker();
            t.Size = mcp.Size;
            t.Controls.Add(mcp);
            MaterialDialog.Show(this, "Pick a Color", t,MaterialDialog.Buttons.OK);
        }

        private void materialFlatButton5_Click(object sender, EventArgs e)
        {
            MaterialDialog.Show(this, "Content", "Title", MaterialDialog.Buttons.OK, MaterialDialog.Icon.Shield);
        }

        private void ActionBarMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MaterialSettings objSettings = new MaterialSettings(this);
            objSettings.ShowThemeSettings = true;
            objSettings.Show();
            e.Cancel = true;
        }
    }
}
