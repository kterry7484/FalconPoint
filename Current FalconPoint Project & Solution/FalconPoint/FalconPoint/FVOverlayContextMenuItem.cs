using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FalconPoint
{
    /// <summary> "MY" menu item object which will be handed to FalconView for use
    /// in the context menu.  This class implements the IFvContextMenuItem interface
    /// but can support private interfaces and carry other data as well.
    /// </summary>
    /// <remarks>
    /// This implementation is not directly creatable from COM, due to [ComVisible(false)] 
    /// but it can be passed as a COM object.  This is not a required behavior.
    /// </remarks>
    [ComVisible(false)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    class FVContextMenuItem : FalconViewOverlayLib.IFvContextMenuItem
    {
        #region Defined Values

        /// <summary> Coded value 
        /// </summary>
        const int FALSE = 0;
        /// <summary> Coded value 
        /// </summary>
        const int TRUE = 1;

        #endregion



        #region Member Data

        /// <summary> Internal classifications for menu item "types"
        /// </summary>
        public enum MenuItemEntryType { EMPTY, SHOWINFO, USEPLAYBACKTIME };

        /// <summary> Internal classification of the menu item "type" or action to be performed
        /// </summary>
        private MenuItemEntryType m_MenuItemType = MenuItemEntryType.EMPTY;

        /// <summary> Data item reference to be acted upon (if any)
        /// </summary>
        private object m_MyDataItem = null;

        /// <summary> refernce to the owning overlay
        /// </summary>
        /// <remarks>
        /// The lifetime of "this" object is only up to the selection or closing of the menu
        /// </remarks>
        private FvOverlay m_TheOverlay = null;

        #endregion



        #region Constructor

        /// <summary> Additional constructor for convenience in creating menu items
        /// </summary>
        /// <param name="Name"> Name of the menu item (display string)
        /// </param>
        /// <param name="Enabled"> Flag for whether the menu item should be "enabled" in the menu
        /// </param>
        /// <param name="Checked"> Flag for whether the menu item should have a check mark next to it in the menu
        /// </param>
        public FVContextMenuItem(FvOverlay theOverlay, string Name, MenuItemEntryType MenuItemType, object theObject,
                                 bool Enabled, bool Checked)
        {
            m_MenuItemName = Name;
            m_MenuItemEnabled = Enabled;
            m_MenuItemChecked = Checked;

            m_TheOverlay = theOverlay;
            m_MenuItemType = MenuItemType;
            m_MyDataItem = theObject;
        }

        #endregion


        #region IFvContextMenuItem Members

        /// <summary> Flag for whether the menu item should have a check mark next to it in the menu
        /// </summary>
        public int MenuItemChecked
        {
            get
            {
                return (m_MenuItemChecked) ? TRUE : FALSE;
            }
        }
        protected bool m_MenuItemChecked = false;

        /// <summary> Flag for whether the menu item should be "enabled" in the menu
        /// </summary>
        public int MenuItemEnabled
        {
            get
            {
                return (m_MenuItemEnabled) ? TRUE : FALSE;
            }
        }
        protected bool m_MenuItemEnabled = false;

        /// <summary> Name of the menu item (display string)
        /// </summary>
        public string MenuItemName
        {
            get
            {
                return m_MenuItemName;
            }
        }
        protected string m_MenuItemName = "default_name";

        /// <summary> This method is called when the menu items is selected in the context menu
        /// </summary>
        /// <remarks>
        /// While the lifetime of this object is short, "action" for the menu item may be 
        /// contained in this object so long as ownership of persisted items (i.e. the info dialog 
        /// has been raised) is passed back to the overlay.
        /// </remarks>
        public void MenuItemSelected()
        {
            switch (m_MenuItemType)
            {
                case MenuItemEntryType.SHOWINFO:
                    {
                        if (m_TheOverlay.m_InfoDlg == null)
                            m_TheOverlay.m_InfoDlg = new FvCommonDialogsLib.FvInformationDialogClass();

                        m_TheOverlay.m_InfoDlg.ShowDialog(0, "", FvOverlay.OverlayFriendlyName,
                                                "This is a sample Map Symbol",
                                                FvCommonDialogsLib.TextFormatEnum.TEXT_FORMAT_PLAIN_TEXT, m_TheOverlay);

                        break;
                    }

                case MenuItemEntryType.USEPLAYBACKTIME:
                    {
                        // Overlays should directly access the member, reflection used for convenience in wizard
                        FalconViewOverlayLib.IFvPlaybackEventsObserver thisOverlay = m_TheOverlay as FalconViewOverlayLib.IFvPlaybackEventsObserver;
                        if (thisOverlay != null)
                        {
                            //TODO: Simplify this line
                            // m_TheOverlay.m_UsePlaybackTime = !m_TheOverlay.m_UsePlaybackTime;
                            System.Reflection.FieldInfo fi = thisOverlay.GetType().GetField("m_UsePlaybackTime");
                            fi.SetValue(thisOverlay, !(bool)fi.GetValue(thisOverlay));

                            // Force a refresh of the overlay
                            m_TheOverlay.InvalidateImplementedViews();
                        }

                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        #endregion
    }
}
