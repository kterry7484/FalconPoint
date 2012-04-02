using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace FalconPoint
{
    /// <summary> The deconfliction form is solely owned by the PlugIn.  There are no
    /// required interfaces or connections to FalconView.  So, the implementation 
    /// and design are entirely up to the designer.
    /// <para>
    /// In this case, we will fire events to apply preferences to each of the 
    /// overlays that are added to the control.
    /// </para>
    /// </summary>
    [ComVisible(false)]
    public partial class DeconflictionForm : Form
    {
        #region Member data

        /// <summary> Preference string to pass back to the client
        /// </summary>
        private PreferenceClass m_PrefsObject;

        /// <summary> Gets or Sets the preference string on the form
        /// </summary>
        public PreferenceClass PrefsObject
        {
            get
            {
                return m_PrefsObject;
            }
            set
            {
                m_PrefsObject = value;

                if (value != null)
                {
                    ApplyAllButton.Enabled = true;
                    ApplySelectedButton.Enabled = true;
                }
                else
                {
                    ApplyAllButton.Enabled = false;
                    ApplySelectedButton.Enabled = false;
                }

            }
        }

        #endregion



        #region Constructors

        /// <summary> Generic constructor for the form
        /// </summary>
        public DeconflictionForm()
        {
            InitializeComponent();

            // If you want the overlay defaults to be optionally set, Add this line back in
            //m_OverlayList.Items.Add(new OverlayListEntry(-1, "Set Defaults"));

            ApplyAllButton.Enabled = false;
            ApplySelectedButton.Enabled = false;
        }

        #endregion




        #region Exposed events

        /// <summary> Provided event handler for clients to the control
        /// </summary>
        /// <param name="LayerHandle"> Layer to apply the preferences to 
        /// </param>
        /// <param name="PrefString"> Preference string to apply to the layer
        /// </param>
        public delegate void ApplyPreferencesEventHandler(long LayerHandle, PreferenceClass PrefString);

        /// <summary> Public event for callback registration
        /// </summary>
        public event ApplyPreferencesEventHandler OnApplyPreferences;


        /// <summary> Notify the registered clients that an overlay should update preferences
        /// </summary>
        /// <param name="LayerHandle"></param>
        private void FireApplyPreferences(long LayerHandle)
        {
            if (OnApplyPreferences != null)
                OnApplyPreferences(LayerHandle, m_PrefsObject);
        }

        #endregion


        #region Support functions

        /// <summary> Clients should add overlays to our list
        /// </summary>
        /// <param name="LayerHandle"> LayerHandle of the added overlay
        /// </param>
        /// <param name="FileName"> Display name for the added overlay
        /// </param>
        public void AddOverlay(long LayerHandle, string FileName)
        {
            m_OverlayList.Items.Add(new OverlayListEntry(LayerHandle, FileName));
        }

        #endregion



        #region Message handlers

        private void ApplyAllButton_Click(object sender, EventArgs e)
        {
            foreach (OverlayListEntry theOverlay in m_OverlayList.Items)
                FireApplyPreferences(theOverlay.OverlayHandle);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ApplySelectedButton_Click(object sender, EventArgs e)
        {
            OverlayListEntry[] theItems = new OverlayListEntry[m_OverlayList.SelectedItems.Count];
            m_OverlayList.SelectedItems.CopyTo(theItems, 0);

            foreach (OverlayListEntry theOverlay in theItems)
            {
                FireApplyPreferences(theOverlay.OverlayHandle);

                m_OverlayList.Items.Remove(theOverlay);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }


    /// <summary> Simple container class to keep handles and names together
    /// </summary>
    [ComVisible(false)]
    class OverlayListEntry
    {
        /// <summary> Force construction with name and handle together
        /// </summary>
        /// <param name="Handle"> LayerHandle for the Overlay
        /// </param>
        /// <param name="Name"> Displayable name of the Overlay
        /// </param>
        public OverlayListEntry(long Handle, string Name)
        {
            OverlayHandle = Handle;
            OverlayName = Name;
        }

        /// <summary> We want the control to show the Overlay Name
        /// </summary>
        /// <returns>
        /// OverlayName
        /// </returns>
        public override string ToString()
        {
            return OverlayName;
        }

        /// <summary> The name of the overlay to display
        /// </summary>
        public string OverlayName;

        /// <summary>LayerHandle for the overlay
        /// </summary>
        public long OverlayHandle;
    }
}