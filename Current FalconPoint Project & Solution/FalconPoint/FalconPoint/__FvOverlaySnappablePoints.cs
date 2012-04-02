using System;
using System.Collections.Generic;
using System.Text;

using DRAWING = System.Drawing;

namespace FalconPoint
{
    /// <summary> Interface allows an overlay to expose its "snappable" points to other editors
    /// </summary>
    public partial class FvOverlay : FalconViewOverlayLib.IFvOverlaySnappablePoints
    {
        #region Member Data

        #endregion



        #region IFvOverlaySnappablePoints Members

        /// <summary> Performs a "hit test" on the overlay for snappable points
        /// </summary>
        /// <param name="pMapView">current map view
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <returns>
        /// Returns TRUE if a snappable point exists in the given view at the given screen coordinates. 
        /// </returns>
        public int CanSnapTo(FalconViewOverlayLib.IFvMapView pMapView, int x, int y)
        {
            if (GetScaleDenominator((MAPENGINELib.ISettableMapProj)pMapView.CurrentMapProj) > m_HideAbove)
                return FALSE;

            if (m_DummyItem.Contains(x, y))
                return TRUE;

            return FALSE;
        }


        /// <summary> Populates the snap-to points list with one or more snappable points
        /// </summary>
        /// <param name="pMapView">current map view
        /// </param>
        /// <param name="x">x-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="y">y-coordinate of the cursor position relative to the upper-left corner of the view
        /// </param>
        /// <param name="pSnapToPointsList"> List of points that can be snapped to at the specified location.
        /// </param>
        /// <seealso cref="SNAPTOPOINTSLISTSERVERLib.SnapToPointsListClass"/>
        public void GetSnappablePoints(FalconViewOverlayLib.IFvMapView pMapView, int x, int y, object pSnapToPointsList)
        {
            if (m_DummyItem.Contains(x, y))
            {
                SNAPTOPOINTSLISTSERVERLib.ISnapToPointsList theItemList = (SNAPTOPOINTSLISTSERVERLib.ISnapToPointsList)pSnapToPointsList;
                SNAPTOPOINTSLISTSERVERLib.ISnapToPointsList2 theItemList2 = (SNAPTOPOINTSLISTSERVERLib.ISnapToPointsList2)pSnapToPointsList;

                MAPENGINELib.ISettableMapProj theMapProj = (MAPENGINELib.ISettableMapProj)pMapView.CurrentMapProj;

                double Lat, Lon;
                int result;
                theMapProj.surface_to_geo(m_DummyItem.X + (m_DummyItem.Width / 2), m_DummyItem.Y + (m_DummyItem.Height / 2),
                                          out Lat, out Lon, out result);

                if (result == SUCCESS)
                {
                    theItemList.m_dLatitude = Lat;
                    theItemList.m_dLongitude = Lon;

                    theItemList2.m_overlayDescGuid = this.OverlayDescGuid;
                    theItemList2.m_bstrOverlayName = OverlayFriendlyName;
                    theItemList2.m_bstrTooltip = "Dummy snap to point";
                    theItemList2.m_bstrKey = "";

                    theItemList.AddToList();
                }
            }
        }

        #endregion
    }
}
