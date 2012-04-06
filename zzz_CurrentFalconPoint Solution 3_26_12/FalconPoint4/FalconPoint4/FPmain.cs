// ****************************************************************************
// $HeadURL $
// Owner: $Author: $
// $Revision: $
// $Id: $
// $LastChangedDate: $
// Description:  This class is the main class called by FalconView.  It is starts 
//                  COTs listener class
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using fvw;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MAPENGINELib;
using System.Drawing;
using System.Windows.Forms;

namespace FalconPoint4
{
    [Guid("CC9BE44B-9E3F-4100-9EC5-9C33E53B9917")]

    public class FPmain : ILayerEditor3, ICallback
    {
        
        #region // Ilayer editor 3

        public void CanAddPixmapsToBaseMap(out int bCanAddPixmapsToBaseMap)
        {
            bCanAddPixmapsToBaseMap = 0;
        }

        public int CanDropOLEDataObject(int layer_handle, int lCursorX, int lCursorY, object pSettableMapProj, object pDataObject)
        {
            return 0;
        }

        public void CancelDrag(int layer_handle, int object_handle)
        {

        }

        public void SetPreferenceString(IntPtr a)
        {

        }

        public void CancelDragEx(int layer_handle, object pSettableMapProj, int hDC)
        {

        }

        public void DisableProjectionUI(int layer_handle, ref short disable_proj)
        {

        }

        public void DisableRotationUI(int layer_handle, ref short disable_rotation)
        {

        }

        public void GetDefaultCursor(ref int hcur)
        {

        }

        public void GetDefaultDirectory(int layer_handle, ref string default_dir)
        {

        }

        public void GetDefaultExtension(int layer_handle, ref string default_extension)
        {
            default_extension = "";
        }

        public void GetDefaultFilter(int layer_handle, ref string default_filter)
        {

        }

        public void GetDispatchPtr(ref object dispatch_pointer)
        {
            dispatch_pointer = this;
            COTsListener listen = new COTsListener(this);
        }

        public void GetEditorName(ref string editor_name)
        {
            editor_name = "FalconPoint Overlay";
        }

        public void GetEditorStrings(ref string menu_text, ref string file_type_text)
        {
            menu_text = "FalconPoint Overlay";
            file_type_text = "FalconPoint Overlay";
        }

        public void GetEditorToolbarButton(ref string button_filename)
        {
            // TODO add custom button
            button_filename = "";
        }

        public void GetIconName(ref string icon_name)
        {
            // TODO add custom button
            icon_name = "";
        }

        public void GetNextNewFileName(ref string next_new_filename)
        {

        }

        public void GetPreferenceString(ref string preference_str)
        {

        }

        public void GetPropertiesProgID(ref string properties_ProgID)
        {
            properties_ProgID = "";
        }

        public int IsReadOnly(int layer_handle)
        {
            return 1;
        }

        public int IsSelectionLocked(int layer_handle)
        {
            return 0;
        }

        public void OnAddMenuItems(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, ref string pMenuItems)
        {

        }

        public void OnDoubleClickEx(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, int lCursorFlags)
        {
        }

        public void OnDrag(int layer_handle, int object_handle, double lat, double lon, int flags, ref int hcur, ref string tooltip, ref string helptext)
        {

        }

        public void OnDragEx(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, int lCursorFlags, ref int hcur, ref string tooltip, ref string helptext)
        {

        }

        public void OnDraw(int layer_handle, object pActiveMapProj, int bDrawBeforeLayerObjects)
        {


        }

        public void OnDrawToBaseMap(int layer_handle, object pActiveMapProj)
        {
        }

        public void OnDrop(int layer_handle, int object_handle, double lat, double lon, int flags)
        {

        }

        public void OnDropEx(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, int lCursorFlags)
        {

        }

        public void OnGetSnapToPoints(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, object pSnapToPoints)
        {

        }

        public void OnKeyDown(int layer_handle, int character, int flags, ref int result)
        {
            MessageBox.Show(character.ToString());
        }

        public void OnKeyUp(int layer_handle, int character, int flags, ref int result)
        {

        }

        public void OnNewLayer(int layer_handle)
        {

        }

        public void OnPropertiesHelp()
        {

        }

        public int OnTestSnapTo(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY)
        {
            return 0;
        }

        public void OpenFile(int layer_handle, string filename)
        {

        }

        public void PasteOLEDataObject(int layer_handle, int lCursorX, int lCursorY, object pSettableMapProj, object pDataObject)
        {

        }

        public int PreSave(int layer_handle)
        {
            return 0;
        }

        public void RequiresEqualArc(int layer_handle, ref short requires)
        {

        }

        public void RequiresNorthUp(int layer_handle, ref short requires)
        {

        }

        public void Save(int layer_handle, string file_spec)
        {

        }

        public void Selected(int layer_handle, int object_handle, double lat, double lon, int flags, ref short drag, ref int hcur, ref string tooltip, ref string helptext, ref int return_val)
        {
 
        }

        public void SelectedEx(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, int lCursorFlags, ref short drag, ref int hcur, ref string tooltip, ref string helptext, ref int return_val)
        {

        }

        public void SetEditOn(short edit)
        {

        }


        public void TestSelected(int layer_handle, int object_handle, double lat, double lon, int flags, ref int hcur, ref string tooltip, ref string helptext, ref int return_val)
        {

        }

        public void TestSelectedEx(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, int lCursorFlags, ref int hcur, ref string tooltip, ref string helptext, ref int lReturnVal)
        {

        }
        #endregion // end ilayer editor 3

        #region // Icallback


        public void GetHelpText(int layer_handle, int object_handle, ref string help_text)
        {

        }

        public int GetInfoText(int layer_handle, int object_handle, ref string title_bar_txt, ref string dialog_txt)
        {
            return 0;
        }

        public void GetMenuItems(int layer_handle, int object_handle, ref string menu_text)
        {

        }

        public void GetTimeSpan(int layer_handle, ref DateTime begin, ref DateTime end)
        {

        }

        public void GetToolTip(int layer_handle, int object_handle, ref string tool_tip)
        {

        }

        public void OnDoubleClicked(int layer_handle, int object_handle, int fvw_parent_hWnd, double lat, double lon)
        {

        }

        public void OnFalconViewExit(int layer_handle)
        {

        }

        public void OnGeoCircleBounds(int click_id, double lat, double lon, double radius)
        {

        }

        public void OnGeoCircleBoundsCanceled(int click_id)
        {

        }

        public void OnGeoRectBounds(int click_id, double NW_lat, double NW_lon, double SE_lat, double SE_lon)
        {

        }

        public void OnGeoRectBoundsCanceled(int click_id)
        {

        }

        public void OnMouseClick(int click_id, double latitude, double longitude)
        {

        }

        public void OnMouseClickCanceled(int click_id)
        {

        }

        public void OnOverlayClose(int layer_handle)
        {

        }

        public void OnPreClose(int layer_handle, ref int cancel)
        {

        }

        public int OnSelected(int layer_handle, int object_handle, int fv_parent_hWnd, double latitude, double longitude)
        {
            MessageBox.Show(object_handle.ToString());  
           return 0;
        }

        public void OnSnapToInfo(int click_id, double lat, double lon, int point_type, string key_text)
        {

        }

        public void OnSnapToInfoCanceled(int click_id)
        {

        }

        public void OnToolbarButtonPressed(int toolbar_id, int button_number)
        {

        }

        public void SetCurrentViewTime(int layer_handle, DateTime date)
        {

        }

        #endregion // end Icallback


    }
}
