using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FalconPoint
{
    /// <summary> Preference container used by the plug-in to show the overlay preferences
    /// on its own editor toolbar
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class PreferenceContainer : Form
    {
        /// <summary> The plug-in's actual preference control
        /// </summary>
        private FvOptionsCtl m_PrefsControl = new FvOptionsCtl();

        /// <summary> Constructor for the container
        /// </summary>
        /// <remarks>
        /// <para>The overlay's preference control is being programmatically added to a panel
        /// on the container.  The IDE will hold a copy of the DLL open containing the control 
        /// so there will be build issues if the control is added to the dialog via the VS IDE.</para>
        /// <para>Since we want to properly size the control to the container, a panel was added
        /// to the container that the control should fill.</para>
        /// </remarks>
        public PreferenceContainer()
        {
            InitializeComponent();

            panel1.Controls.Add(m_PrefsControl);
            m_PrefsControl.Visible = true;
            m_PrefsControl.Location = new Point(10, 10);

            m_PrefsControl.Width = panel1.Width - 20;
            m_PrefsControl.Height = panel1.Height - 20;

            // register for the control's OnModified event
            m_PrefsControl.OnModified += new FvOptionsCtl.ModifiedEventHandler(m_PrefsControl_OnModified);

            ApplyButton.Enabled = false;
        }

        /// <summary> The preference control must sign up for the options's control
        /// even to mimic the FalconView container's use of the control
        /// </summary>
        void m_PrefsControl_OnModified(object Sender)
        {
            ApplyButton.Enabled = true;
        }


        #region iFalconViewClientOptions Members

        /// <summary> Reflect this call in the container to the preference control
        /// </summary>
        /// <param name="Preferences">The preference string as retrieved from the 
        /// options control which has the seelcted preference values encoded in it
        /// </param>
        public int SetPreferences(string Preferences)
        {
            m_PrefsControl.SetPreferences(Preferences);
            ApplyButton.Enabled = false;

            return 0;
        }

        /// <summary> Reflect this call in the container to the preference control
        /// </summary>
        /// <param name="prefString">The preference string as retrieved from the 
        /// options control which has the seelcted preference values encoded in it
        /// </param>
        public string GetPreferences()
        {
            return m_PrefsControl.GetPreferences();
        }

        #endregion


        #region iFalconViewClientEvents Members

        /// <summary> Reflect this event from the preference control to our parent/owner
        /// if they want to sign up for the Apply button.
        /// </summary>
        private void FireOnModified()
        {
            if (OnModified != null)
                OnModified(this);
        }

        public delegate void ModifiedEventHandler(object Sender);

        /// <summary> Public event for callback registration
        /// </summary>
        public event ModifiedEventHandler OnModified;

        #endregion



        /// <summary> Notify clients of changes in the user selected preferences
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            FireOnModified();

            ApplyButton.Enabled = false;
        }

        /// <summary> Complete initialization of the dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Since the name of the dialog can change (development time), this reference
        /// to a member variable should not be done in the generated code of the form 
        /// as this may be lost by the resource editor.
        /// </remarks>
        private void PreferenceContainer_Load(object sender, EventArgs e)
        {
            this.Text = FvOverlay.OverlayFriendlyName + "Preferences";
        }
    }
}