using System;
using System.Collections.Generic;
using System.Text;
using fvw;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MAPENGINELib;
using System.Drawing;
using System.Windows.Forms;

namespace FalconPoint
{
    class Draw : ICallback, ILayerEditor3
    {
        private ILayer MyLayer = null;
        fvw.Map MyMap;
        int LayerHandle;
        int LineHandle;
        int IconHandle;
        long ToolbarHandle;

        

        [ComVisible(true)]

        public void FingDrawEllipse()
        {
            long result;
            Int32 result2;
            Draw callback = new Draw();
           // callback.parent = this;

            fvw.Map MyMap = new fvw.Map();
            result = MyMap.SetDispatchPtr(this);

            System.Diagnostics.Debug.WriteLine("mothafing " + result);

            IconHandle = -1;
            ToolbarHandle = -1;
            LineHandle = -1;
            LayerHandle = -1;

            MyLayer = new fvw.LayerClass();


                System.Diagnostics.Debug.WriteLine("whatcha think? " + result);


            result2 = MyLayer.RegisterWithMapServer("Sample Layer", 0, this);
            System.Diagnostics.Debug.WriteLine("whatcha think? " + result2);

        }

        public void GetHelpText(int layer_handle, int object_handle, ref string help_text)
        {
            throw new NotImplementedException();
        }

        public int GetInfoText(int layer_handle, int object_handle, ref string title_bar_txt, ref string dialog_txt)
        {
            throw new NotImplementedException();
        }

        public void GetMenuItems(int layer_handle, int object_handle, ref string menu_text)
        {
            throw new NotImplementedException();
        }

        public void GetTimeSpan(int layer_handle, ref DateTime begin, ref DateTime end)
        {
            throw new NotImplementedException();
        }

        public void GetToolTip(int layer_handle, int object_handle, ref string tool_tip)
        {
            throw new NotImplementedException();
        }

        public void OnDoubleClicked(int layer_handle, int object_handle, int fvw_parent_hWnd, double lat, double lon)
        {
            throw new NotImplementedException();
        }

        public void OnFalconViewExit(int layer_handle)
        {
            throw new NotImplementedException();
        }

        public void OnGeoCircleBounds(int click_id, double lat, double lon, double radius)
        {
            throw new NotImplementedException();
        }

        public void OnGeoCircleBoundsCanceled(int click_id)
        {
            throw new NotImplementedException();
        }

        public void OnGeoRectBounds(int click_id, double NW_lat, double NW_lon, double SE_lat, double SE_lon)
        {
            throw new NotImplementedException();
        }

        public void OnGeoRectBoundsCanceled(int click_id)
        {
            throw new NotImplementedException();
        }

        public void OnMouseClick(int click_id, double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public void OnMouseClickCanceled(int click_id)
        {
            throw new NotImplementedException();
        }

        public void OnOverlayClose(int layer_handle)
        {
            throw new NotImplementedException();
        }

        public void OnPreClose(int layer_handle, ref int cancel)
        {
            throw new NotImplementedException();
        }

        public int OnSelected(int layer_handle, int object_handle, int fv_parent_hWnd, double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public void OnSnapToInfo(int click_id, double lat, double lon, int point_type, string key_text)
        {
            throw new NotImplementedException();
        }

        public void OnSnapToInfoCanceled(int click_id)
        {
            throw new NotImplementedException();
        }

        public void OnToolbarButtonPressed(int toolbar_id, int button_number)
        {
            throw new NotImplementedException();
        }

        public void SetCurrentViewTime(int layer_handle, DateTime date)
        {
            throw new NotImplementedException();
        }

        public void CanAddPixmapsToBaseMap(out int bCanAddPixmapsToBaseMap)
        {
            throw new NotImplementedException();
        }

        public int CanDropOLEDataObject(int layer_handle, int lCursorX, int lCursorY, object pSettableMapProj, object pDataObject)
        {
            throw new NotImplementedException();
        }

        public void CancelDrag(int layer_handle, int object_handle)
        {
            throw new NotImplementedException();
        }

        public void CancelDragEx(int layer_handle, object pSettableMapProj, int hDC)
        {
            throw new NotImplementedException();
        }

        public void DisableProjectionUI(int layer_handle, ref short disable_proj)
        {
            throw new NotImplementedException();
        }

        public void DisableRotationUI(int layer_handle, ref short disable_rotation)
        {
            throw new NotImplementedException();
        }

        public void GetDefaultCursor(ref int hcur)
        {
            throw new NotImplementedException();
        }

        public void GetDefaultDirectory(int layer_handle, ref string default_dir)
        {
            throw new NotImplementedException();
        }

        public void GetDefaultExtension(int layer_handle, ref string default_extension)
        {
            throw new NotImplementedException();
        }

        public void GetDefaultFilter(int layer_handle, ref string default_filter)
        {
            throw new NotImplementedException();
        }

        public void GetDispatchPtr(ref object dispatch_pointer)
        {
            throw new NotImplementedException();
        }

        public void GetEditorName(ref string editor_name)
        {
            throw new NotImplementedException();
        }

        public void GetEditorStrings(ref string menu_text, ref string file_type_text)
        {
            throw new NotImplementedException();
        }

        public void GetEditorToolbarButton(ref string button_filename)
        {
            throw new NotImplementedException();
        }

        public void GetIconName(ref string icon_name)
        {
            throw new NotImplementedException();
        }

        public void GetNextNewFileName(ref string next_new_filename)
        {
            throw new NotImplementedException();
        }

        public void GetPreferenceString(ref string preference_str)
        {
            throw new NotImplementedException();
        }

        public void GetPropertiesProgID(ref string properties_ProgID)
        {
            throw new NotImplementedException();
        }

        public int IsReadOnly(int layer_handle)
        {
            throw new NotImplementedException();
        }

        public int IsSelectionLocked(int layer_handle)
        {
            throw new NotImplementedException();
        }

        public void OnAddMenuItems(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, ref string pMenuItems)
        {
            throw new NotImplementedException();
        }

        public void OnDoubleClickEx(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, int lCursorFlags)
        {
            throw new NotImplementedException();
        }

        public void OnDrag(int layer_handle, int object_handle, double lat, double lon, int flags, ref int hcur, ref string tooltip, ref string helptext)
        {
            throw new NotImplementedException();
        }

        public void OnDragEx(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, int lCursorFlags, ref int hcur, ref string tooltip, ref string helptext)
        {
            throw new NotImplementedException();
        }

        public void OnDraw(int layer_handle, object pActiveMapProj, int bDrawBeforeLayerObjects)
        {
            throw new NotImplementedException();
        }

        public void OnDrawToBaseMap(int layer_handle, object pActiveMapProj)
        {
            throw new NotImplementedException();
        }

        public void OnDrop(int layer_handle, int object_handle, double lat, double lon, int flags)
        {
            throw new NotImplementedException();
        }

        public void OnDropEx(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, int lCursorFlags)
        {
            throw new NotImplementedException();
        }

        public void OnGetSnapToPoints(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, object pSnapToPoints)
        {
            throw new NotImplementedException();
        }

        public void OnKeyDown(int layer_handle, int character, int flags, ref int result)
        {
            throw new NotImplementedException();
        }

        public void OnKeyUp(int layer_handle, int character, int flags, ref int result)
        {
            throw new NotImplementedException();
        }

        public void OnNewLayer(int layer_handle)
        {
            throw new NotImplementedException();
        }

        public void OnPropertiesHelp()
        {
            throw new NotImplementedException();
        }

        public int OnTestSnapTo(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY)
        {
            throw new NotImplementedException();
        }

        public void OpenFile(int layer_handle, string filename)
        {
            throw new NotImplementedException();
        }

        public void PasteOLEDataObject(int layer_handle, int lCursorX, int lCursorY, object pSettableMapProj, object pDataObject)
        {
            throw new NotImplementedException();
        }

        public int PreSave(int layer_handle)
        {
            throw new NotImplementedException();
        }

        public void RequiresEqualArc(int layer_handle, ref short requires)
        {
            throw new NotImplementedException();
        }

        public void RequiresNorthUp(int layer_handle, ref short requires)
        {
            throw new NotImplementedException();
        }

        public void Save(int layer_handle, string file_spec)
        {
            throw new NotImplementedException();
        }

        public void Selected(int layer_handle, int object_handle, double lat, double lon, int flags, ref short drag, ref int hcur, ref string tooltip, ref string helptext, ref int return_val)
        {
            throw new NotImplementedException();
        }

        public void SelectedEx(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, int lCursorFlags, ref short drag, ref int hcur, ref string tooltip, ref string helptext, ref int return_val)
        {
            throw new NotImplementedException();
        }

        public void SetEditOn(short edit)
        {
            throw new NotImplementedException();
        }

        public void SetPreferenceString(IntPtr preference_str)
        {
            throw new NotImplementedException();
        }

        public void TestSelected(int layer_handle, int object_handle, double lat, double lon, int flags, ref int hcur, ref string tooltip, ref string helptext, ref int return_val)
        {
            throw new NotImplementedException();
        }

        public void TestSelectedEx(int layer_handle, object pSettableMapProj, int hDC, int lCursorX, int lCursorY, int lCursorFlags, ref int hcur, ref string tooltip, ref string helptext, ref int lReturnVal)
        {
            throw new NotImplementedException();
        }
    }
}
