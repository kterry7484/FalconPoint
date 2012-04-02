using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FalconPoint
{
    /// <summary> Interface provides Context menu support for the plugin
    /// </summary>
    public partial class FvOverlay : FalconViewOverlayLib.IFvOverlayContextMenu
    {

        #region IFvOverlayContextMenu Members

        /// <summary> Append menu items to the given context menu
        /// </summary>
        /// <param name="pContextMenu">context menu object that menu items will be appended to
        /// </param>
        /// <param name="pMapView">current map view
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        public void AppendMenuItems(FalconViewOverlayLib.IFvContextMenu pContextMenu, FalconViewOverlayLib.IFvMapView pMapView, int x, int y)
        {
            if (m_DummyItem.Contains(x, y))
            {
                FVContextMenuItem theMenuItem = new FVContextMenuItem(this, OverlayFriendlyName + " items\\Dummy Item",
                                                                      FVContextMenuItem.MenuItemEntryType.SHOWINFO,
                                                                      null, true, false);

                pContextMenu.AppendMenuItem(theMenuItem);
            }


            // Overlays should directly access the member, reflection used for convenience in wizard
            FalconViewOverlayLib.IFvPlaybackEventsObserver thisOverlay = this as FalconViewOverlayLib.IFvPlaybackEventsObserver;
            if (thisOverlay != null)
            {
                System.Reflection.FieldInfo fi = thisOverlay.GetType().GetField("m_UsePlaybackTime");
                bool menuItemChecked = (bool)fi.GetValue(this);

                FVContextMenuItem theMenuItem = new FVContextMenuItem(this, OverlayFriendlyName + " - Use Playback Time",
                                                                      FVContextMenuItem.MenuItemEntryType.USEPLAYBACKTIME,
                                                                      null,
                                                                      true, menuItemChecked);

                pContextMenu.AppendMenuItem(theMenuItem);
            }
        }

        #endregion
    }
}
