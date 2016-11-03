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

            for (int i = 0; i < 5; i++)
            {
                MaterialTimeLineEntry objEntry = new MaterialTimeLineEntry
                {
                    UserName = "Melvin Fengelsfd",
                    Title = "Title "+i,
                    Time = DateTime.Now.AddDays(-i),
                    Text = "TestText \r\nTefgsdfgsfdgsdfgsdfvklrenghiobönkqwbnroighfdnkbvgqrejbgvklndcvmbqöwreogkihsadkghfeöklt\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest\r\nTest"
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
            String strMessage;
            strMessage = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi. Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi. Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum. Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,  At accusam aliquyam diam diam dolore dolores duo eirmod eos erat, et nonumy sed tempor et et invidunt justo labore Stet clita ea et gubergren, kasd magna no rebum. sanctus sea sed takimata ut vero voluptua. est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat.  Consetetur sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
            UserControl t = new UserControl();
            MaterialColorPicker mcp = new MaterialColorPicker();
            t.Size = mcp.Size;
            t.Controls.Add(mcp);
            MaterialDialog.Show(this, "Mit Titel", t);
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




    }
}
