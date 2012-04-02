using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FalconPoint
{
    /// <summary> "ActiveX" Preference control for the PlugIn
    /// </summary>
    /// <remarks>
    /// <para>To create an ActiveX usable control in .Net, base the control off of 
    /// System.UserControl, implement the iFalconViewClientOptions members, and
    /// expose the iFalconViewClientEvents events.</para>
    /// <para>Verification of dialog settings must be done at data entry time.  
    /// There is no validation step for the dialog as a whole.</para>
    /// <note> 
    /// The names of the interfaces are not important, only the method/event names 
    /// </note>
    /// </remarks>
    [ComVisible(true)]
    [ProgId("FalconPoint.Preferences")]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("326e8780-24b0-431e-8fd8-73e2cf525b72")]
    public partial class FvOptionsCtl : UserControl, FalconViewOverlayLib.IFvOverlayPropertyPage
    {
        [DllImport("user32.dll")]
        private static extern void SetParent(IntPtr child, IntPtr newParent);

        #region Controls

        private ComboBox m_HideAboveCB;
        private Label label2;
        private ComboBox m_HideLabelsAboveCB;
        private CheckBox m_OverlayBannerCk;
        private RadioButton RBBlueIcons;
        private RadioButton RBRedIcons;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;

        #endregion


        #region Contructor

        /// <summary> Default constructor
        /// </summary>
        /// <remarks> 
        /// An uninitialized control does not need to display reasonable values.
        /// A preference string will always be requested from the plug-in when 
        /// the control is initialized.  Have one source only for setting 
        /// default preference values.
        /// </remarks>
        public FvOptionsCtl()
        {
            InitializeComponent();

        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.m_HideAboveCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_HideLabelsAboveCB = new System.Windows.Forms.ComboBox();
            this.m_OverlayBannerCk = new System.Windows.Forms.CheckBox();
            this.RBBlueIcons = new System.Windows.Forms.RadioButton();
            this.RBRedIcons = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hide Overlay Above";
            // 
            // m_HideAboveCB
            // 
            this.m_HideAboveCB.FormattingEnabled = true;
            this.m_HideAboveCB.Items.AddRange(new object[] {
            "never",
            "1:10M",
            "1:5M",
            "1:2M",
            "1:1M",
            "1:500 K",
            "1:250 K",
            "1:100 K"});
            this.m_HideAboveCB.Location = new System.Drawing.Point(115, 13);
            this.m_HideAboveCB.Name = "m_HideAboveCB";
            this.m_HideAboveCB.Size = new System.Drawing.Size(121, 21);
            this.m_HideAboveCB.TabIndex = 2;
            this.m_HideAboveCB.SelectedIndexChanged += new System.EventHandler(this.m_HideAboveCB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Hide Labels Above";
            // 
            // m_HideLabelsAboveCB
            // 
            this.m_HideLabelsAboveCB.FormattingEnabled = true;
            this.m_HideLabelsAboveCB.Items.AddRange(new object[] {
            "never",
            "1:10M",
            "1:5M",
            "1:2M",
            "1:1M",
            "1:500 K",
            "1:250 K",
            "1:100 K"});
            this.m_HideLabelsAboveCB.Location = new System.Drawing.Point(115, 40);
            this.m_HideLabelsAboveCB.Name = "m_HideLabelsAboveCB";
            this.m_HideLabelsAboveCB.Size = new System.Drawing.Size(121, 21);
            this.m_HideLabelsAboveCB.TabIndex = 2;
            this.m_HideLabelsAboveCB.SelectedIndexChanged += new System.EventHandler(this.m_HideLabelsAboveCB_SelectedIndexChanged);
            // 
            // m_OverlayBannerCk
            // 
            this.m_OverlayBannerCk.AutoSize = true;
            this.m_OverlayBannerCk.Location = new System.Drawing.Point(22, 85);
            this.m_OverlayBannerCk.Name = "m_OverlayBannerCk";
            this.m_OverlayBannerCk.Size = new System.Drawing.Size(126, 17);
            this.m_OverlayBannerCk.TabIndex = 3;
            this.m_OverlayBannerCk.Text = "Show overlay banner";
            this.m_OverlayBannerCk.UseVisualStyleBackColor = true;
            this.m_OverlayBannerCk.CheckedChanged += new System.EventHandler(this.m_OverlayBannerCk_CheckedChanged);
            // 
            // RBBlueIcons
            // 
            this.RBBlueIcons.AutoSize = true;
            this.RBBlueIcons.Location = new System.Drawing.Point(9, 12);
            this.RBBlueIcons.Name = "RBBlueIcons";
            this.RBBlueIcons.Size = new System.Drawing.Size(75, 17);
            this.RBBlueIcons.TabIndex = 4;
            this.RBBlueIcons.TabStop = true;
            this.RBBlueIcons.Text = "Blue Icons";
            this.RBBlueIcons.UseVisualStyleBackColor = true;
            this.RBBlueIcons.CheckedChanged += new System.EventHandler(this.RBIconsColors_CheckedChanged);
            // 
            // RBRedIcons
            // 
            this.RBRedIcons.AutoSize = true;
            this.RBRedIcons.Location = new System.Drawing.Point(102, 12);
            this.RBRedIcons.Name = "RBRedIcons";
            this.RBRedIcons.Size = new System.Drawing.Size(74, 17);
            this.RBRedIcons.TabIndex = 5;
            this.RBRedIcons.TabStop = true;
            this.RBRedIcons.Text = "Red Icons";
            this.RBRedIcons.UseVisualStyleBackColor = true;
            this.RBRedIcons.CheckedChanged += new System.EventHandler(this.RBIconsColors_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_HideLabelsAboveCB);
            this.groupBox1.Controls.Add(this.m_HideAboveCB);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 73);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Common Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RBRedIcons);
            this.groupBox2.Controls.Add(this.RBBlueIcons);
            this.groupBox2.Location = new System.Drawing.Point(13, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 44);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Specific Settings";
            // 
            // FvOptionsCtl
            // 
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_OverlayBannerCk);
            this.Name = "FvOptionsCtl";
            this.Size = new System.Drawing.Size(410, 266);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FvOptionsCtl_HelpRequested);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        #region Event support

        /// <summary> Notify registered objects our content has changed.  
        /// </summary>
        /// <remarks>
        /// FalconView : If this event is not fired, the control's settings
        /// (the modified preference string) will not be retrieved by 
        /// the framework overlay options control.
        /// .
        /// JMPS : When fired, JMPS will retrieve the current preference string
        /// to decide whether to light the "Apply" button.  In this case, the 
        /// dialog can return to a "clean" state.
        /// </remarks>
        private void FireOnModified()
        {
            if (OnModified != null)
                OnModified(this);

            if (m_PropertyPageNotifyEvents != null)
            {
                m_PropertyPageNotifyEvents.OnPropertyPageModified();
            }
        }

        public delegate void ModifiedEventHandler(object Sender);

        /// <summary> Public event for callback registration
        /// </summary>
        public event ModifiedEventHandler OnModified;

        #endregion


        #region Support Members

        /// <summary> Called on the control to pass in preference strings. 
        /// </summary>
        /// <param name="Preferences"> Preference string to decode into the 
        /// preference control's controls
        /// </param>
        /// <returns> SUCCESS / FAIL
        /// </returns>
        public int SetPreferences(string Preferences)
        {
            PreferenceClass thePrefs = PreferenceClass.CreateFromString(Preferences);

            m_OverlayBannerCk.Checked = thePrefs.m_ShowBanner;

            SetSelectedItem(thePrefs.m_HideAbove, m_HideAboveCB);
            SetSelectedItem(thePrefs.m_HideLabelsAbove, m_HideLabelsAboveCB);

            RBBlueIcons.Checked = thePrefs.m_UseBlueIcons;
            RBRedIcons.Checked = !RBBlueIcons.Checked;

            return 0;
        }

        private static void SetSelectedItem(int scaleFactor, System.Windows.Forms.ComboBox theBox)
        {
            if (scaleFactor > 10000000)
                theBox.SelectedIndex = 0;
            else if (scaleFactor > 5000000)
                theBox.SelectedIndex = 1;
            else if (scaleFactor > 2000000)
                theBox.SelectedIndex = 2;
            else if (scaleFactor > 1000000)
                theBox.SelectedIndex = 3;
            else if (scaleFactor > 500000)
                theBox.SelectedIndex = 4;
            else if (scaleFactor > 250000)
                theBox.SelectedIndex = 5;
            else if (scaleFactor > 100000)
                theBox.SelectedIndex = 6;
            else
                theBox.SelectedIndex = 7;
        }

        delegate int SetPreferenceDelegate(string Preferences);

        /// <summary> Return the users preferences packed in to a string
        /// </summary>
        /// <returns> 
        /// Encoded preference string
        /// </returns>
        public string GetPreferences()
        {
            PreferenceClass thePrefs = new PreferenceClass();

            thePrefs.m_ShowBanner = m_OverlayBannerCk.Checked;

            thePrefs.m_HideAbove = GetScaleFactor(m_HideAboveCB);

            thePrefs.m_HideLabelsAbove = GetScaleFactor(m_HideLabelsAboveCB);

            thePrefs.m_UseBlueIcons = RBBlueIcons.Checked;

            return thePrefs.ToString();
        }

        private static int GetScaleFactor(System.Windows.Forms.ComboBox theBox)
        {
            int scaleFactor;
            if (theBox.SelectedIndex == 0)
                scaleFactor = int.MaxValue;
            else if (theBox.SelectedIndex == 1)
                scaleFactor = 10000000;
            else if (theBox.SelectedIndex == 2)
                scaleFactor = 5000000;
            else if (theBox.SelectedIndex == 3)
                scaleFactor = 2000000;
            else if (theBox.SelectedIndex == 4)
                scaleFactor = 1000000;
            else if (theBox.SelectedIndex == 5)
                scaleFactor = 500000;
            else if (theBox.SelectedIndex == 6)
                scaleFactor = 250000;
            else
                scaleFactor = 100000;
            return scaleFactor;
        }

        #endregion


        #region Dialog Message Handlers

        private void m_HideAboveCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireOnModified();
        }

        private void m_HideLabelsAboveCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireOnModified();
        }

        private void m_OverlayBannerCk_CheckedChanged(object sender, EventArgs e)
        {
            FireOnModified();
        }

        private void RBIconsColors_CheckedChanged(object sender, EventArgs e)
        {
            FireOnModified();
        }

        #endregion



        // New style overlay preference handlers
        #region IFvOverlayPropertyPage Members

        FalconViewOverlayLib.IFvMapView m_MapView = null;

        FalconViewOverlayLib.IMapChangeNotifyEvents m_MapChangeNotifyEvents = null;
        FalconViewOverlayLib.IDisplayChangeNotifyEvents m_DisplayChangeNotifyEvents = null;
        FalconViewOverlayLib.IPropertyPageNotifyEvents m_PropertyPageNotifyEvents = null;

        FvOverlay m_TheOverlay = null;

        private void FvOptionsCtl_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

            if (m_PropertyPageNotifyEvents != null)
                m_PropertyPageNotifyEvents.OnPropertyPageHelp();
        }

        #endregion



        #region IFvOverlayPropertyPage Members

        /// <summary> Called when the page has been marked dirty in the Overlay Options Dialog and the 
        /// user has pressed Apply/OK on the dialog
        /// </summary>
        public void OnApply()
        {
            PreferenceClass thePrefs = new PreferenceClass();

            thePrefs.m_HideAbove = GetScaleFactor(m_HideAboveCB);
            thePrefs.m_HideLabelsAbove = GetScaleFactor(m_HideLabelsAboveCB);

            thePrefs.m_ShowBanner = m_OverlayBannerCk.Checked;

            thePrefs.m_UseBlueIcons = RBBlueIcons.Checked;

            if (m_TheOverlay != null)
                m_TheOverlay.SetPreferences(thePrefs.ToString());
        }

        /// <summary> Called when the property page is created by the overlay options dialog. 
        /// </summary>
        /// <param name="propertyPageGuid"> uid specifed in the property pages config file
        /// </param>
        /// <param name="pMapView"> The Current Map View object
        /// </param>
        /// <param name="hWndParent"> the HWND of the parent window in the overlay options 
        /// dialog box. The property page should be created as a child of this window
        /// </param>
        /// <param name="pEvents"> can implement IMapChangeNotifyEvents, IDisplayChangeNotifyEvents, and IFvPropertyPageEvents
        /// </param>
        public void OnCreate(Guid propertyPageGuid, FalconViewOverlayLib.IFvMapView pMapView, int hWndParent, object pEvents)
        {
            // The GUID of the property page that is being requested.  i.e. "this" provides several configurations of the 
            // property control (itself).

            // Save aside the callback objects for use
            m_MapView = pMapView;
            m_MapChangeNotifyEvents = pEvents as FalconViewOverlayLib.IMapChangeNotifyEvents;
            m_DisplayChangeNotifyEvents = pEvents as FalconViewOverlayLib.IDisplayChangeNotifyEvents;

            m_PropertyPageNotifyEvents = pEvents as FalconViewOverlayLib.IPropertyPageNotifyEvents;

            // Critical step to get the draw notification
            SetParent(Handle, new IntPtr(hWndParent));

            int OverlayOnTop = m_MapView.OverlayManager.SelectByOverlayDescGuid(new Guid("f9109e69-faa5-4144-9b74-aaf6fd933d7d"));

            if (OverlayOnTop == 1)
            {
                m_TheOverlay = m_MapView.OverlayManager.CurrentOverlay as FvOverlay;
            }
            else
            {
                do
                {
                    int result = m_MapView.OverlayManager.MoveNext();
                    if (result == 0)
                        break;

                    m_TheOverlay = m_MapView.OverlayManager.CurrentOverlay as FvOverlay;
                } while (m_TheOverlay == null);
            }

            PreferenceClass thePrefs = null;

            if (m_TheOverlay != null)
                thePrefs = PreferenceClass.CreateFromString(m_TheOverlay.GetPreferences());
            else
                thePrefs = new PreferenceClass();

            SetSelectedItem(thePrefs.m_HideAbove, m_HideAboveCB);
            SetSelectedItem(thePrefs.m_HideLabelsAbove, m_HideLabelsAboveCB);

            m_OverlayBannerCk.Checked = thePrefs.m_ShowBanner;

            RBBlueIcons.Checked = thePrefs.m_UseBlueIcons;
            RBRedIcons.Checked = !thePrefs.m_UseBlueIcons;
        }

        #endregion
    }
}
