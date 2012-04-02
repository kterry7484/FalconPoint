using System;
using System.Collections.Generic;
using System.Text;
using fvw;
using DRAWING = System.Drawing;
using MAPENGINELib;


namespace FalconPoint
{
    /// <summary> Interface provides Overlay drawing support for the plugin
    /// </summary>
    public partial class FvOverlay : FalconViewOverlayLib.IFvOverlayRenderer, fvw.IGPS
    {
        #region Member Data

        DRAWING.Font m_Font_Large = new DRAWING.Font(DRAWING.FontFamily.GenericSansSerif, 30);
        DRAWING.Font m_Font_Small = new DRAWING.Font(DRAWING.FontFamily.GenericSansSerif, 10);

        #endregion



        #region IFvOverlayRenderer Members

        /// <summary> Optional interface that can be implemented to support rendering your overlay.  
        /// OnDraw is called by the framework to render your overlay to a surface (screen, printer, or DIB). 
        /// </summary>
        /// <param name="pMapView">current map view that the overlay is being drawn to.  This is the 
        /// currently displayed map (which may not have been drawn).  Overlays should reference
        /// the CurrentMapProj from the IActiveMapProj as the map they will draw to.
        /// </param>
        /// <param name="pActiveMap">IActiveMapProj interface created by the map rendering engine
        /// </param>
        public void OnDraw(FalconViewOverlayLib.IFvMapView pMapView, object pActiveMap)
        {
            MAPENGINELib.IActiveMapProj theActiveMap = pActiveMap as MAPENGINELib.IActiveMapProj;

            MAPENGINELib.ISettableMapProj theSettableMapProj = null;
            MAPENGINELib.IGraphicsContext theGC = null;

            

            try
            {
                // Insure that the overlay can be seen

                theActiveMap.GetSettableMapProj(out theSettableMapProj);

                if (theSettableMapProj == null)
                    // can't draw yet without a map reference
                    return;

                double scaleDenominator = GetScaleDenominator(theSettableMapProj);

                m_OverlayHidden = (scaleDenominator > m_HideAbove);
                if (m_OverlayHidden)
                    return;
                


                // Some common items to retrieve,...
                double Lat, Lon;
                double LLLat, LLLon, URLat, URLon;
                int result;
                theSettableMapProj.actual_center_lat(out Lat);
                theSettableMapProj.actual_center_lon(out Lon);
                theSettableMapProj.get_vmap_bounds(out LLLat, out LLLon, out URLat, out URLon, out result, MAPENGINELib.VirtualSurfaceEnum.EQUALARC_VSURFACE);

                double rotation;
                theSettableMapProj.actual_rotation(out rotation);

                int zoom;
                theSettableMapProj.actual_zoom_percent(out zoom);



                // Draw the overlay according to user preferences

                theActiveMap.GetGraphicsContext(out theGC);

                if (theGC != null)
                {
                    int hdc, hAttribDC;
                    theGC.GetDC(out hdc, out hAttribDC);

                    DRAWING.Graphics mapGraphics = DRAWING.Graphics.FromHdc(new IntPtr(hdc), new IntPtr(hAttribDC));

                    bool isPrtDC;
                    theGC.IsPrinting(out isPrtDC);
                    if (isPrtDC)
                        mapGraphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;

                    DRAWING.SizeF strSize;

                    if (m_ShowBanner)
                    {
                        int width, height;
                        theSettableMapProj.get_surface_size(out width, out height, out result);

                        int centerX = width / 2;

                        string displayTitle = OverlayFriendlyName;

                        DRAWING.Rectangle theBannerRect = new DRAWING.Rectangle();
                        strSize = mapGraphics.MeasureString(displayTitle, m_Font_Large);
                        theBannerRect.X = centerX - (int)strSize.Width / 2 - 10;
                        theBannerRect.Y = 10;
                        theBannerRect.Width = (int)strSize.Width + 20;
                        theBannerRect.Height = (int)strSize.Height + 20;

                        // just an example of decluttering
                        if (scaleDenominator <= m_HideLabelsAbove)
                        {
                            mapGraphics.FillRectangle(DRAWING.Brushes.Aquamarine, theBannerRect);
                            mapGraphics.DrawRectangle(DRAWING.Pens.Blue, theBannerRect);
                        }

                        mapGraphics.DrawString(displayTitle, m_Font_Large, DRAWING.Brushes.Blue, centerX - (strSize.Width / 2), 15);

                        FalconViewOverlayLib.IFvOverlayPersistence theFileOverlay = this as FalconViewOverlayLib.IFvOverlayPersistence;
                        if (theFileOverlay != null)
                        {
                            // Draw the file name for the overlay (if we have one)
                            strSize = mapGraphics.MeasureString(theFileOverlay.FileSpecification, m_Font_Small);
                            mapGraphics.DrawString(theFileOverlay.FileSpecification, m_Font_Small, DRAWING.Brushes.Blue,
                                                    centerX - (strSize.Width / 2), 55);
                        }

                    }

                    

                    // Draw the data item
                    //DRAWING.SolidBrush theBrush = new DRAWING.SolidBrush(m_UseColor);
                    //mapGraphics.FillRectangle(theBrush, m_DummyItem);


                    //// Draw a title name for the item (new name can be pasted from the clipboard if support added)
                    //strSize = mapGraphics.MeasureString(m_DummyItemName, m_Font_Small);

                    //mapGraphics.DrawString(m_DummyItemName, m_Font_Small, DRAWING.Brushes.Blue,
                    //                        (m_DummyItem.X + m_DummyItem.Width / 2) - (strSize.Width / 2),
                    //                        (m_DummyItem.Y + m_DummyItem.Height * 0.8f) - (strSize.Height / 2));


                    //Austen: Working

                    fvw.Layer myLayer = new fvw.Layer();


                    //fvw.IGPS myGPS = new fvw.GPSClass();
                   // myGPS.AddPoint(GetConnectedHandle(), 46.5452, 29.2538, 0, 0, 0, 0, DateTime.Now);



                 //   Draw FingDraw = new Draw();

                  //  FingDraw.FingDrawEllipse();


                    DRAWING.SolidBrush lineBrush = new DRAWING.SolidBrush(DRAWING.Color.Red);

                    DRAWING.Point Point1 = new DRAWING.Point(200, 200);
                    DRAWING.Point Point2 = new DRAWING.Point(400, 400);

                    DRAWING.Pen Pen1 = new DRAWING.Pen(lineBrush, 3);

                    mapGraphics.DrawLine(Pen1, Point1, Point2);

                    //FalconViewOverlayLib.IFvOverlay curOverlay = this as FalconViewOverlayLib.IFvOverlay


                    // Draw something to exercise the time sensitive nature of the overlay
                    //
                    // This section draws the display time from the Playback dialog on the map 
                    // if the wizard implemented the IFvPlaybackEventsObserver interface on the overlay.
                    //
                    // IMPORTANT:: Overlays should use the m_DisplayTime member variable rather than 
                    // this "long" method of determining the current time on the overlay.  This is a 
                    // convenience for the wizard only
                    FalconViewOverlayLib.IFvPlaybackEventsObserver thisOverlay = this as FalconViewOverlayLib.IFvPlaybackEventsObserver;
                    if (thisOverlay != null)
                    {
                        DateTime theDisplayTime;

                        // TODO: Simplify this line
                        // bool UsePlaybackTime = m_TheOverlay.m_UsePlaybackTime;
                        System.Reflection.FieldInfo fi = thisOverlay.GetType().GetField("m_UsePlaybackTime");
                        bool UsePlaybackTime = (bool)fi.GetValue(this);

                        if (UsePlaybackTime)
                        {
                            fi = thisOverlay.GetType().GetField("m_DisplayTime");
                            theDisplayTime = (DateTime)fi.GetValue(this);
                        }
                        else
                        {
                            theDisplayTime = DateTime.Now;
                        }

                        string displayTime = theDisplayTime.ToShortTimeString();

                        strSize = mapGraphics.MeasureString(displayTime, m_Font_Small);

                        mapGraphics.DrawString(displayTime, m_Font_Small, DRAWING.Brushes.Blue,
                                                (m_DummyItem.X + m_DummyItem.Width / 2) - (strSize.Width / 2),
                                                (m_DummyItem.Y + m_DummyItem.Height / 5) - (strSize.Height / 2));
                    }

                }
            }
            catch (Exception)
            {
                // Block exceptions from being passed on to FalconView
            }
            finally
            {
                // Clean up any items aquired during the draw

                theActiveMap = null;
                theSettableMapProj = null;
                theGC = null;
            }
        }

        #endregion



        public int AddPoint(int gps_overlay_handle, double latitude, double longitude, double altitudeMeters, double true_course, double mag_course, double speed_knots, DateTime date_time)
        {
            throw new NotImplementedException();
        }

        public int Connect()
        {
            throw new NotImplementedException();
        }

        public int Disconnect()
        {
            throw new NotImplementedException();
        }

        public int GetConnectedHandle()
        {
            throw new NotImplementedException();
        }

        public int GetCurrentPoint(int gps_overlay_handle, ref double latitude, ref double longitude, ref double altitudeMeters, ref double true_course, ref double mag_course, ref double speed_knots, ref DateTime date_time)
        {
            throw new NotImplementedException();
        }

        public int RegisterForConnectDisconnect(int hWnd)
        {
            throw new NotImplementedException();
        }

        public int RegisterForCurrentPosition(int hWnd, int gps_overlay_handle)
        {
            throw new NotImplementedException();
        }

        public int UnRegisterForConnectDisconnect(int hWnd)
        {
            throw new NotImplementedException();
        }

        public int UnRegisterForCurrentPosition(int hWnd, int gps_overlay_handle)
        {
            throw new NotImplementedException();
        }
    }
}
