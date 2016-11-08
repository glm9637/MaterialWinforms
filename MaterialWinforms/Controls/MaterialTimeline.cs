using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialWinforms.Controls
{
    [Serializable]
    public partial class MaterialTimeline : Control, IMaterialControl
    {

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public delegate void TimeLineEntryClicked(MaterialTimeLineEntry sender, MouseEventArgs e);
        public event TimeLineEntryClicked onTimeLineEntryClicked;

        public Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : typeof(IMaterialControl).IsAssignableFrom(Parent.GetType()) ? ((IMaterialControl)Parent).BackColor : Parent.BackColor; } }

        private Boolean _AufsteigenSortieren;
        public Boolean AufsteigendSortieren { get { return _AufsteigenSortieren; } set { _AufsteigenSortieren = value; sortEntrys(); } }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<MaterialTimeLineEntry> Entrys { get; set; }

        public MaterialTimeline()
        {
            DoubleBuffered = true;
            InitializeComponent();
            Entrys = new ObservableCollection<MaterialTimeLineEntry>();
            Entrys.CollectionChanged += Entrys_CollectionChanged;
        }

        void Entrys_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            sortEntrys();
        }

        private void sortEntrys()
        {
            Entrys.CollectionChanged -= Entrys_CollectionChanged;

            foreach (MaterialTimeLineEntry objEntry in Entrys)
            {
                objEntry.SizeChanged -= EntrySizeChanged;
                objEntry.MouseClick -= objEntry_Click;
            }

            List<MaterialTimeLineEntry> objSorted = Entrys.OrderByDescending(Entry => Entry, new TimeLineComparer(_AufsteigenSortieren)).ToList<MaterialTimeLineEntry>();
            Entrys.Clear();
            Controls.Clear();
            int h = 0;
            int w = 0;
            foreach (MaterialTimeLineEntry objEntry in objSorted)
            {
                objEntry.Dock = DockStyle.Top;
                Controls.Add(objEntry);
                Entrys.Add(objEntry);
                objEntry.SizeChanged += EntrySizeChanged;
                objEntry.MouseClick += objEntry_Click;
                h += objEntry.Height;
                if (objEntry.Width > w)
                {
                    w = objEntry.Width;
                }
            }

            Entrys.CollectionChanged += Entrys_CollectionChanged;

            Size = new Size(w, h);
        }

        void objEntry_Click(object sender, MouseEventArgs e)
        {
            if(onTimeLineEntryClicked != null)
            {

                onTimeLineEntryClicked((MaterialTimeLineEntry)sender,e);
            }
        }


        private void EntrySizeChanged(object sender, EventArgs e)
        {

            int w = 0;
            int h = 0;
            foreach (MaterialTimeLineEntry objEntry in Entrys)
            {
                h += objEntry.Height;
                if (objEntry.Width > w)
                {
                    w = objEntry.Width;
                }
            }
            Size = new Size(w, h);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
        }
    }

    internal class TimeLineComparer : IComparer<MaterialTimeLineEntry>
    {
        private bool _AufsteigenSortieren;
        public TimeLineComparer(Boolean AufsteigenSortieren)
        {
            _AufsteigenSortieren = AufsteigenSortieren;
        }

        public int Compare(MaterialTimeLineEntry t1, MaterialTimeLineEntry t2)
        {
            if (t1.Time > t2.Time)
            {
                return _AufsteigenSortieren ? 1 : -1;
            }
            else if (t1.Time == t2.Time)
            {
                return 0;
            };
            return _AufsteigenSortieren ? -1 : 1;
        }
    }

}
