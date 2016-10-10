using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Drawing;

namespace MaterialWinforms.Controls
{
    [Designer(typeof(MaterialTabControlDesigner))]
    public class MaterialTabControl : TabControl, IMaterialControl
    {

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : Parent.BackColor; } }

        private bool _TabsAreClosable;
        public bool TabsAreClosable
        {
            get
            {
                return _TabsAreClosable;
            }
            set
            {
                _TabsAreClosable = true;
            }
        }

        public MaterialTabControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);
        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }


        [Editor(typeof(MaterialTabPageCollectionEditor), typeof(UITypeEditor))]
        public new TabPageCollection TabPages
        {
            get
            {
                return base.TabPages;
            }
        }

        internal class MaterialTabPageCollectionEditor : CollectionEditor
        {
            protected override CollectionEditor.CollectionForm
            CreateCollectionForm()
            {
                CollectionForm baseForm = base.CreateCollectionForm();
                baseForm.Text = "TabPage Collection Editor";
                return baseForm;
            }

            public MaterialTabPageCollectionEditor(System.Type type)
                : base(type)
            {
            }
            protected override Type CreateCollectionItemType()
            {
                return typeof(MaterialTabPage);
            }
            protected override Type[] CreateNewItemTypes()
            {
                return new Type[] { typeof(MaterialTabPage) };
            }

        }

    }

    [Designer(typeof(System.Windows.Forms.Design.ScrollableControlDesigner))]
    public class MaterialTabPage : TabPage, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public Color BackColor { get { return Parent == null ? SkinManager.GetApplicationBackgroundColor() : typeof(IMaterialControl).IsAssignableFrom(Parent.GetType()) ? ((IMaterialControl)Parent).BackColor : Parent.BackColor; } }

        [Category("Behavior")]
        public Boolean Closable { get; set; }

        public MaterialTabPage()
        {

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        public MaterialTabPage(string pText)
        {
            this.Name = pText;
            this.Text = pText;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        public MaterialTabPage(string pText, bool isClosable)
        {
            this.Name = pText;
            this.Text = pText;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Closable = isClosable;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(SkinManager.GetApplicationBackgroundColor());
            foreach (Control objChild in Controls)
            {
                if (typeof(IShadowedMaterialControl).IsAssignableFrom(objChild.GetType()))
                {
                    IShadowedMaterialControl objCurrent = (IShadowedMaterialControl)objChild;
                    DrawHelper.drawShadow(e.Graphics, objCurrent.ShadowBorder, objCurrent.Elevation, SkinManager.GetApplicationBackgroundColor());

                }
            }
        }
    }


    internal class MaterialTabControlDesigner :
    System.Windows.Forms.Design.ParentControlDesigner
    {

        #region Private Instance Variables

        private DesignerVerbCollection m_verbs = new
        DesignerVerbCollection();
        private IDesignerHost m_DesignerHost;
        private ISelectionService m_SelectionService;

        #endregion

        public MaterialTabControlDesigner()
            : base()
        {
            DesignerVerb verb1 = new DesignerVerb("Add Tab", new
            EventHandler(OnAddPage));
            DesignerVerb verb2 = new DesignerVerb("Remove Tab", new
            EventHandler(OnRemovePage));
            m_verbs.AddRange(new DesignerVerb[] { verb1, verb2 });
        }

        #region Properties

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (m_verbs.Count == 2)
                {
                    MaterialTabControl MyControl = (MaterialTabControl)Control;
                    if (MyControl.TabCount == 0)
                    {
                        m_verbs[1].Enabled = true;
                    }
                    else
                    {
                        m_verbs[1].Enabled = false;
                    }
                }
                return m_verbs;
            }
        }

        public IDesignerHost DesignerHost
        {
            get
            {
                if (m_DesignerHost == null)
                    m_DesignerHost =
                    (IDesignerHost)(GetService(typeof(IDesignerHost)));

                return m_DesignerHost;
            }
        }

        public ISelectionService SelectionService
        {
            get
            {
                if (m_SelectionService == null)
                    m_SelectionService =
                    (ISelectionService)(this.GetService(typeof(ISelectionService)));
                return m_SelectionService;
            }
        }

        #endregion

        void OnAddPage(Object sender, EventArgs e)
        {
            MaterialTabControl ParentControl = (MaterialTabControl)Control;
            System.Windows.Forms.Control.ControlCollection oldTabs =
            ParentControl.Controls;

            RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

            System.Windows.Forms.TabPage P =
            (System.Windows.Forms.TabPage)(DesignerHost.CreateComponent(typeof(MaterialTabPage)));
            P.Text = P.Name;
            ParentControl.TabPages.Add(P);

            RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"],
            oldTabs, ParentControl.TabPages);
            ParentControl.SelectedTab = P;

            SetVerbs();

        }

        void OnRemovePage(Object sender, EventArgs e)
        {
            MaterialTabControl ParentControl = (MaterialTabControl)Control;
            System.Windows.Forms.Control.ControlCollection oldTabs =
            ParentControl.Controls;

            if (ParentControl.SelectedIndex < 0) return;

            RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

            DesignerHost.DestroyComponent(ParentControl.TabPages[ParentControl.SelectedIndex]);

            RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"],
            oldTabs, ParentControl.TabPages);

            SelectionService.SetSelectedComponents(new IComponent[] {
ParentControl }, SelectionTypes.Auto);

            SetVerbs();

        }

        private void SetVerbs()
        {
            MaterialTabControl ParentControl = (MaterialTabControl)Control;

            switch (ParentControl.TabPages.Count)
            {
                case 0:
                    Verbs[1].Enabled = false;
                    break;
                default:
                    Verbs[1].Enabled = true;
                    break;
            }
        }

        private const int WM_NCHITTEST = 0x84;

        private const int HTTRANSPARENT = -1;
        private const int HTCLIENT = 1;

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
            {
                //select tabcontrol when Tabcontrol clicked outside of
                if (m.Result.ToInt32() == HTTRANSPARENT)
                    m.Result = (IntPtr)HTCLIENT;
            }

        }

        private enum TabControlHitTest
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }

        private const int TCM_HITTEST = 0x130D;

        private struct TCHITTESTINFO
        {
            public System.Drawing.Point pt;
            public TabControlHitTest flags;
        }

        protected override bool GetHitTest(System.Drawing.Point point)
        {
            if (this.SelectionService.PrimarySelection == this.Control)
            {
                TCHITTESTINFO hti = new TCHITTESTINFO();

                hti.pt = this.Control.PointToClient(point);
                hti.flags = 0;

                System.Windows.Forms.Message m = new
                System.Windows.Forms.Message();
                m.HWnd = this.Control.Handle;
                m.Msg = TCM_HITTEST;

                IntPtr lparam =
                System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(hti));
                System.Runtime.InteropServices.Marshal.StructureToPtr(hti,
                lparam, false);
                m.LParam = lparam;

                base.WndProc(ref m);
                System.Runtime.InteropServices.Marshal.FreeHGlobal(lparam);

                if (m.Result.ToInt32() != -1)
                    return hti.flags != TabControlHitTest.TCHT_NOWHERE;

            }

            return false;
        }

        protected override void
        OnPaintAdornments(System.Windows.Forms.PaintEventArgs pe)
        {
            //Don't want DrawGrid dots.
        }

        //Fix the AllSizable selectionrule on DockStyle.Fill
        public override System.Windows.Forms.Design.SelectionRules
        SelectionRules
        {
            get
            {
                if (Control.Dock == System.Windows.Forms.DockStyle.Fill)
                    return
                    System.Windows.Forms.Design.SelectionRules.Visible;
                return base.SelectionRules;
            }
        }
    }
}
