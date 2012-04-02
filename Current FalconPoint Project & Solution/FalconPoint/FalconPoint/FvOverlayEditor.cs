using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FalconPoint
{
    /// <summary> Interface provides "ovarlay editor" support in FalconView, Only 1 is created
    /// </summary>
    /// <remarks>
    /// Required interface implemented by an overlay editor.  Only 1 editor will be created, while
    /// many overlay objects can be created.  Each overlay object will hold the preferences, data, 
    /// UI control, and rendering responsibiliy for its data.  The editor manages the "state" of the 
    /// editor, toolbar, and controls the editor UI portion
    /// </remarks>
    [ComVisible(true)]
    [Guid("7fcc14ef-c6a8-408e-bc27-aaccddf5ce9a")]
    [ProgId("FalconPoint.FvOverlayEditor")]
    public partial class FvOverlayEditor : FalconViewOverlayLib.IFvOverlayEditor
    {
        #region Defined Values

        /// <summary> Coded value 
        /// </summary>
        const int FALSE = 0;
        /// <summary> Coded value 
        /// </summary>
        const int TRUE = 1;

        /// <summary> Coded value 
        /// </summary>
        /// <remarks> Use this return code to note that the plug-in "has handled"
        /// the call from FalconView.  FalconView will not query other plug-ins if
        /// they need to handle it -- i.e. the plug-in returned a tooltip.
        /// </remarks>
        const int SUCCESS = 0;
        /// <summary> Coded value 
        /// </summary>
        /// <remarks> Use this return code to note that the plug-in "has NOT handled"
        /// the call from FalconView.  FalconView will not query other plug-ins if
        /// they need to handle it -- i.e. the plug-in did NOT provide a tooltip so 
        /// other overlays need to be queried if they want to supply one.
        /// </remarks>
        const int FAILED = -1;

        #endregion




        #region Member Data

        FvToolbarServerLib.IFvToolbar m_FvToolbar = null;

        int m_SelectionButton = -1;
        int m_DropPointsButton = -1;
        int m_QueryButton = -1;
        int m_EraseButton = -1;
        int m_HelpButton = -1;
        int m_OptionsButton = -1;


        /// <summary> enum encoding of our editors possible states.  
        /// </summary>
        /// <remarks>
        /// Editors will have various states of operation to defining the state machine
        /// of the editor.  Use an internal structure such as this internal enum to 
        /// keep track of the state
        /// </remarks>
        public enum EditorState
        {
            /// <summary> Edit mode set to basic "view" mode, allows selection of items </summary>
            VIEWDATA,
            /// <summary> User clicks will place new Landmarks on the map </summary>
            ADDPOINTS,
            /// <summary> User can drag out an AOI rectangle </summary>
            AOI_SELECT,
            /// <summary> Currently setting preferences for the active layer
            /// </summary>
            SET_PREFS,
            /// <summary> Deleting points,...etc
            /// </summary>
            DELETEMODE
        };

        #endregion



        #region Static Members

        /// <summary> provides overlays a reference to the to the "singleton" state member
        /// </summary>
        [ComVisible(false)]
        public static EditorState m_EditorState = EditorState.VIEWDATA;

        #endregion



        #region Support Methods

        /// <summary> Support function to set the toolbar button "states" in a single place
        /// according to the layer editor mode
        /// </summary>
        private void SetButtonStates()
        {
            if (m_FvToolbar != null)
            {
                // Reset only only the buttons who can have a "pressed" state

                m_FvToolbar.SetButtonPushed(m_SelectionButton, FALSE);
                m_FvToolbar.SetButtonPushed(m_DropPointsButton, FALSE);
                m_FvToolbar.SetButtonPushed(m_QueryButton, FALSE);
                m_FvToolbar.SetButtonPushed(m_EraseButton, FALSE);

                switch (m_EditorState)
                {
                    case EditorState.VIEWDATA:
                        {
                            m_FvToolbar.SetButtonPushed(m_SelectionButton, TRUE);
                            break;
                        }

                    case EditorState.ADDPOINTS:
                        {
                            m_FvToolbar.SetButtonPushed(m_DropPointsButton, TRUE);
                            break;
                        }

                    case EditorState.AOI_SELECT:
                        {
                            m_FvToolbar.SetButtonPushed(m_QueryButton, TRUE);
                            break;
                        }

                    // Lab5 : Adding a delete icon tool
                    case EditorState.DELETEMODE:
                        {
                            m_FvToolbar.SetButtonPushed(m_EraseButton, TRUE);
                            break;
                        }
                }
            }
        }

        #endregion



        #region IFvOverlayEditor Members

        /// <summary> Called when the editor mode is activated. 
        /// </summary>
        /// <param name="pMapView"> pMapView  the current map view 
        /// </param>
        public void ActivateEditor(FalconViewOverlayLib.IFvMapView pMapView)
        {
            FvOverlay.CurrentEditor = this;

            FvOverlay.EditMode = true;
        }



        /// <summary> Called when the user leaves this editor mode
        /// </summary>
        /// <param name="pMapView"> pMapView  the current map view 
        /// </param>
        public void DeactivateEditor(FalconViewOverlayLib.IFvMapView pMapView)
        {
            FvOverlay.CurrentEditor = null;

            FvOverlay.EditMode = false;
        }



        /// <summary> Returns a default cursor for the overlay. 
        /// </summary>
        public int DefaultCursor
        {
            get
            {
                // TODO: return different cursors based on the editor state
                return System.Windows.Forms.Cursors.Arrow.Handle.ToInt32();
            }
        }



        /// <summary>Called by the framework to set the editor's toolbar to set the pointer to an IFvToolbar (defined in FvToolbarServer.tlb) 
        /// </summary>
        /// <remarks> The given IFvToolbar has already been initialized, i.e., there is no need to call Initialize() again. 
        /// </remarks>
        public object EditorToolbar
        {
            set
            {
                m_FvToolbar = value as FvToolbarServerLib.IFvToolbar;

                m_SelectionButton = m_FvToolbar.AppendButtonFromIconFile(FvOverlay.HD_DATA + "\\FvOverlay\\FvOverlay.ICO", "Selection", "Select items on the map");
                m_FvToolbar.AppendSeparator();
                m_DropPointsButton = m_FvToolbar.AppendButtonFromIconFile(FvOverlay.HD_DATA + "\\FvOverlay\\FvOverlay.ICO", "Drop Points", "Place items on the map");
                m_QueryButton = m_FvToolbar.AppendButtonFromIconFile(FvOverlay.HD_DATA + "\\FvOverlay\\FvOverlay.ICO", "Query", "Select an Area of Interest");
                m_EraseButton = m_FvToolbar.AppendButtonFromIconFile(FvOverlay.HD_DATA + "\\FvOverlay\\FvOverlay.ICO", "Erase", "Erase items on the map");
                m_FvToolbar.AppendSeparator();
                m_HelpButton = m_FvToolbar.AppendButtonFromIconFile(FvOverlay.HD_DATA + "\\FvOverlay\\FvOverlay.ICO", "Help", FvOverlay.OverlayFriendlyName + " help");
                m_FvToolbar.AppendSeparator();
                m_OptionsButton = m_FvToolbar.AppendButtonFromIconFile(FvOverlay.HD_DATA + "\\FvOverlay\\FvOverlay.ICO", "Options", FvOverlay.OverlayFriendlyName + " options");

                m_FvToolbar.SetButtonEnabled(m_EraseButton, FALSE);
            }
        }



        /// <summary> Called whenever the user clicks on one of the toolbar buttons on the toolbar returned by ActivateEditor
        /// </summary>
        /// <param name="pMapView">the current map view
        /// </param>
        /// <param name="lButtonCommandId">lButtonCommandId  zero-based index of the button that was clicked 
        /// </param>
        /// <remarks>
        /// If a toolbar "object" is used rather than implemented on "this", then prefer the OnButtonPressed event 
        /// of the IFvToolbarEvents interface on that object
        /// </remarks>
        public void OnToolbarButtonClick(FalconViewOverlayLib.IFvMapView pMapView, int lButtonCommandId)
        {
            // TODO: Respond to the toolbar button press
            if (lButtonCommandId == m_SelectionButton)
            {
                m_EditorState = EditorState.VIEWDATA;
            }
            else if (lButtonCommandId == m_DropPointsButton)
            {
                m_EditorState = EditorState.ADDPOINTS;
            }
            else if (lButtonCommandId == m_QueryButton)
            {
                m_EditorState = EditorState.AOI_SELECT;
            }
            else if (lButtonCommandId == m_EraseButton)
            {
                m_EditorState = EditorState.DELETEMODE;
            }

            // Then Set the UI state of the toolbar
            SetButtonStates();
        }

        #endregion
    }

    public partial class FvOverlay
    {
        /// <summary> Reference to the associated editor object
        /// </summary>
        /// <remarks>
        /// The "singleton" Editor object is responsible for the management of the 
        /// multiple overlay objects, each of which is responsible for its own data. 
        /// Since the Editor owns the toolbar, then the overlay must hold the state
        /// of the editor obeject(s)
        /// </remarks>
        [ComVisible(false)]
        public static FalconViewOverlayLib.IFvOverlayEditor CurrentEditor = null;

    }

}
