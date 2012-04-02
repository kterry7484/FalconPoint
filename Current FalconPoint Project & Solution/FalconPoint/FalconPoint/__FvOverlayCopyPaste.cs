using System;
using System.Collections.Generic;
using System.Text;

using FORMS = System.Windows.Forms;

namespace FalconPoint
{
    /// <summary> Interface provides clipboard operations to an overlay
    /// </summary>
    public partial class FvOverlay : FalconViewOverlayLib.IFvOverlayCopyPaste
    {
        #region Member Data


        #endregion


        #region IFvOverlayCopyPaste Members

        /// <summary> Return a flag if the user is allowed to "copy" to the clipboard
        /// </summary>
        /// <param name="pMapView"> the current map view
        /// </param>
        /// <returns>
        /// Return TRUE if data from the overlay's data can be copied to the "clipboard". The Edit | Copy menu item will be enabled. 
        /// </returns>
        /// <remarks>
        /// This should likely be restricted to when the user has something "selected" in the overlay
        /// </remarks>
        public int CanCopyToClipboard(FalconViewOverlayLib.IFvMapView pMapView)
        {
            return TRUE;
        }

        /// <summary> Return whether the given IDataObject can be dropped at the given screen coordinates in the view. 
        /// </summary>
        /// <param name="pMapView"> the current map view
        /// </param>
        /// <param name="x"> x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y"> y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="pDataObject"> an IDataObject which the user is attempting to "drop" on the map
        /// </param>
        /// <returns>
        /// Return TRUE if the given IDataObject can be dropped at the given screen coordinates in the view. 
        /// </returns>
        public int CanDropDataObject(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, object pDataObject)
        {
            return FALSE;
        }

        /// <summary> Return flag if data can be pasted from the "clipboard" to the current overlay.
        /// </summary>
        /// <param name="pMapView"> the current map view
        /// </param>
        /// <returns>
        /// Return TRUE if data can be pasted from the "clipboard" to the current overlay. The Edit | Paste men item will be enabled. 
        /// </returns>
        public int CanPasteFromClipboard(FalconViewOverlayLib.IFvMapView pMapView)
        {
            // Check if 
            //    - we are in EditMode if it applies and 
            //    - if out overlay can be seen (i.e. HideAbove)
            //    = The current file is not read Only

            if (m_OverlayHidden)
                return FALSE;

            if (FvOverlay.CurrentEditor != null)
                return (EditMode && m_OverlayIsReadOnly == FALSE) ? TRUE : FALSE;

            // The thought here is that overlays are not generally modified by the user
            // so pasting is not allowed.  But, this is up to the overlay design.
            return FALSE;
        }

        /// <summary> Instructs the overlay to copy the current selection to the clipboard
        /// </summary>
        /// <param name="pMapView">the current map view
        /// </param>
        /// <remarks>
        /// Copy data from the current overlay to the "clipboard" (e.g., handle the Edit | Copy menu item). 
        /// </remarks>
        public void CopyToClipboard(FalconViewOverlayLib.IFvMapView pMapView)
        {
            FORMS.Clipboard.SetText("'Copy' string from " + OverlayFriendlyName);
        }

        /// <summary> Drop the given IDataObject onto the overlay at the given screen coordinates in the view. 
        /// </summary>
        /// <param name="pMapView"> the current map view
        /// </param>
        /// <param name="x"> x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y"> y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="pDataObject"> an IDataObject which the user is attempting to "drop" on the map
        /// </param>
        public void DropDataObject(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, object pDataObject)
        {

        }

        /// <summary> Paste data from the "clipboard" to the current overlay. 
        /// </summary>
        /// <param name="pMapView"> the Current map view
        /// </param>
        /// <remarks>
        /// Note that the clipboard is overlay specific and does not necessarily mean the Windows clipboard. 
        /// </remarks>
        public void PasteFromClipboard(FalconViewOverlayLib.IFvMapView pMapView)
        {
            FORMS.IDataObject theClipBoardData = FORMS.Clipboard.GetDataObject();

            if (theClipBoardData.GetDataPresent(typeof(string)))
            {
                m_DummyItemName = (string)theClipBoardData.GetData(typeof(string));

                InvalidateImplementedViews();
            }
        }

        #endregion

    }
}
