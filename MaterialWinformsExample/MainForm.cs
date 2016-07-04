using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MaterialWinforms;
using MaterialWinforms.Controls;

namespace MaterialWinformsExample
{
    public partial class MainForm : MaterialWinforms.Controls.MaterialForm
    {
        private readonly MaterialSkinManager MaterialWinformsManager;
        public MainForm()
        {
            InitializeComponent();

            // Initialize MaterialWinformsManager
            MaterialWinformsManager = MaterialSkinManager.Instance;
            MaterialWinformsManager.AddFormToManage(this);
            MaterialWinformsManager.Theme = MaterialSkinManager.Themes.LIGHT;
            MaterialWinformsManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            // Add dummy data to the listview
            seedListView();
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
                materialListView1.Items.Add(item);
               
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

            //These are just example color schemes
            switch (colorSchemeIndex)
            {
                case 0:
                    MaterialWinformsManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;
                case 1:
                    MaterialWinformsManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
                    break;
                case 2:
                    MaterialWinformsManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                    break;
            }
        }

        private void materialSingleLineTextField3_Click(object sender, EventArgs e)
        {
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            MaterialDialog.Show(this, "Ein Toller Dialog", "Mit Titel", MaterialDialog.Buttons.OKCancel);
        }



        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = materialComboBox1;
        }

        private void materialToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialToggle1.Checked)
            {
                materialLoadingFloatingActionButton1.startProgressAnimation();
            }
            else
            {
                materialLoadingFloatingActionButton1.ProgressFinished();
            }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            materialLoadingFloatingActionButton1.resetProgressAnimation();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

    }
}
