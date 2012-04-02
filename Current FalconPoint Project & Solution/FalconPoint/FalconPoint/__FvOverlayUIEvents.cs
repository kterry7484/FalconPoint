using System;
using System.Collections.Generic;
using System.Text;

using FORMS = System.Windows.Forms;

namespace FalconPoint
{
    /// <summary> Interface provides the overlay the ability to react to "Key Presses"
    /// </summary>
    public partial class FvOverlay : FalconViewOverlayLib.IFvOverlayUIEvents
    {
        #region Member Data



        #endregion



        #region IFvOverlayUIEvents Members

        /// <summary> Occurs when a key is pressed.
        /// </summary>
        /// <param name="pMapView">current map view that the mouse event occured in
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="nChar">specifies the virtual key code of the given key. For a list of of standard virtual key codes, see Winuser.h
        /// </param>
        /// <param name="nRepCnt">repeat count (the number of times the keystroke is repeated as a result of the user holding down the key)
        /// </param>
        /// <param name="nFlags">specifies the scan code, key-transition code, previous key state, and context code
        /// </param>
        /// <returns>
        /// set to TRUE to indicate that the event was handled, FALSE otherwise
        /// </returns>
        /// <remarks>
        /// This event is routed to only the topmost overlay
        /// </remarks>
        public int KeyDown(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, int nChar, int nRepCnt, int nFlags)
        {
            return FALSE;
        }

        /// <summary> Occurs when a key is released.
        /// </summary>
        /// <param name="pMapView">current map view that the mouse event occured in
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="nChar">specifies the virtual key code of the given key. For a list of of standard virtual key codes, see Winuser.h
        /// </param>
        /// <param name="nRepCnt">repeat count (the number of times the keystroke is repeated as a result of the user holding down the key)
        /// </param>
        /// <param name="nFlags">specifies the scan code, key-transition code, previous key state, and context code
        /// </param>
        /// <returns>
        /// set to TRUE to indicate that the event was handled, FALSE otherwise
        /// </returns>
        /// <remarks>
        /// This event is routed to only the topmost overlay
        /// </remarks>
        public int KeyUp(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, int nChar, int nRepCnt, int nFlags)
        {
            return FALSE;
        }

        /// <summary> Occurs when the left mouse button is double clicked over the given view. 
        /// </summary>
        /// <param name="pMapView">current map view that the mouse event occured in
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="nFlags">indicates whether various virtual keys are down
        /// </param>
        /// <returns>
        /// set to TRUE to indicate that the event was handled, FALSE otherwise
        /// </returns>
        /// <remarks>
        /// This event is routed from the topmost overlay to the bottom most overlay until an overlay sets pbHandled to TRUE. 
        /// </remarks>
        public int MouseDoubleClick(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, int nFlags)
        {
            int retVal = FALSE;

            if (m_DummyItem.Contains(x, y))
            {
                retVal = TRUE;

                if (m_InfoDlg == null)
                    m_InfoDlg = new FvCommonDialogsLib.FvInformationDialogClass();

                m_InfoDlg.ShowDialog(0, "", FvOverlay.OverlayFriendlyName,
                                      "This is a sample Map Symbol",
                                      FvCommonDialogsLib.TextFormatEnum.TEXT_FORMAT_PLAIN_TEXT, this);
            }

            return retVal;
        }

        /// <summary> Occurs when the left mouse button is pressed while the mouse pointer is over the given view. 
        /// </summary>
        /// <param name="pMapView">current map view that the mouse event occured in
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="nFlags">indicates whether various virtual keys are down
        /// </param>
        /// <returns>
        /// set to TRUE to indicate that the event was handled, FALSE otherwise
        /// </returns>
        /// <remarks>
        /// This event is routed from the topmost overlay to the bottom most overlay until an overlay sets pbHandled to TRUE. 
        /// </remarks>
        public int MouseLeftButtonDown(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, int nFlags)
        {
            return FALSE;
        }

        /// <summary> Occurs when the left mouse button is released while the mouse point is over the given view.
        /// </summary>
        /// <param name="pMapView">current map view that the mouse event occured in
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="nFlags">indicates whether various virtual keys are down
        /// </param>
        /// <returns>
        /// set to TRUE to indicate that the event was handled, FALSE otherwise
        /// </returns>
        /// <remarks>
        /// This event is routed from the topmost overlay to the bottom most overlay until an overlay sets pbHandled to TRUE. 
        /// </remarks>
        public int MouseLeftButtonUp(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, int nFlags)
        {
            return FALSE;
        }

        /// <summary> Occurs when the mouse pointer is moved in the view
        /// </summary>
        /// <param name="pMapView">current map view that the mouse event occured in
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="nFlags">indicates whether various virtual keys are down
        /// </param>
        /// <returns>
        /// set to TRUE to indicate that the event was handled, FALSE otherwise
        /// </returns>
        /// <remarks>
        /// This event is routed from the topmost overlay to the bottom most overlay until an overlay sets pbHandled to TRUE. 
        /// </remarks>
        public int MouseMove(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, int nFlags)
        {
            int retVal = FALSE;

            if (m_DummyItem.Contains(x, y))
            {
                retVal = TRUE;

                pMapView.SetCursor(FORMS.Cursors.Arrow.Handle.ToInt32());
                pMapView.SetTooltipText(OverlayFriendlyName + " Sample map symbol (non-geo-referenced)");
            }

            return retVal;
        }

        /// <summary> Occurs when the right mouse button is pressed while the mouse pointer is over the given view
        /// </summary>
        /// <param name="pMapView">current map view that the mouse event occured in
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="nFlags">indicates whether various virtual keys are down
        /// </param>
        /// <returns>
        /// set to TRUE to indicate that the event was handled, FALSE otherwise
        /// </returns>
        /// <remarks>
        /// <para> This event is routed from the topmost overlay to the bottom most overlay until an overlay sets pbHandled to TRUE. 
        /// </para>
        /// <para> This event is NOT intended to replace right click menu handling.  However, this could be used to hijack the 
        /// right click menu during special edit modes.
        /// </para>
        /// </remarks>
        public int MouseRightButtonDown(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, int nFlags)
        {
            return FALSE;
        }

        /// <summary> Occurs when the user rotates the mouse wheel while the mouse pointer is over the given view
        /// </summary>
        /// <param name="pMapView">current map view that the mouse event occured in
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="nFlags">indicates whether various virtual keys are down
        /// </param>
        /// <returns>
        /// set to TRUE to indicate that the event was handled, FALSE otherwise
        /// </returns>
        /// <remarks>
        /// This event is routed only to the topmost overlay.
        /// </remarks>
        public int MouseWheel(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, int zDelta, int nFlags)
        {
            return FALSE;
        }

        #endregion
    }
}
