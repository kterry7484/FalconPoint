using System;
using System.Collections.Generic;
using System.Text;

namespace FalconPoint
{
    /// <summary> Interface provides file Open/Save support in the FalconView UI for an overlay
    /// </summary>
    public partial class FvOverlay : FalconViewOverlayLib.IFvOverlayPersistence
    {
        #region Member Data

        string m_Filename = "";

        //int m_OverlayDirty = FALSE;
        int m_OverlaySaved = FALSE;
        int m_OverlayIsReadOnly = FALSE;

        #endregion


        #region IFvOverlayPersistence Members

        /// <summary> The overlay has been created from scratch, usually from the user choosing File | New. 
        /// </summary>
        /// <remarks>
        /// The FileSpecification property should be initialized in this method to a temporary file name (e.g., trail1.gpb). 
        /// </remarks>
        public void FileNew()
        {
            //TODO: Implement a rotating filename!
            m_Filename = "DummyFile_000." + m_FileExt;
        }

        /// <summary> Open the overlay with the given file specification
        /// </summary>
        /// <param name="FileSpecification"> full pathname of the file to open
        /// </param>
        public void FileOpen(string FileSpecification)
        {
            //TODO: Read your data file
            m_Filename = FileSpecification;

            // In this example, the FileName is displayed on map "banner"
            InvalidateImplementedViews();
        }

        /// <summary> Save the overlay to the given file specification and format. 
        /// </summary>
        /// <param name="FileSpecification">  file specification where the file should be saved to 
        /// </param>
        /// <param name="nSaveFormat"> This corresponds to the selected filter index (1-based) 
        /// usually choosen from the Save As dialog. If the format is unspecified by the user, 
        /// then eSaveFileFormat will be zero. In this case, save to the overlay's default file format.
        /// </param>
        public void FileSaveAs(string FileSpecification, int nSaveFormat)
        {
            //TODO: Save the active file under a new name
            m_Filename = FileSpecification;

            // In this example, the FileName is displayed on map "banner"
            InvalidateImplementedViews();
        }

        /// <summary> Return the file specification of the overlay. 
        /// </summary>
        public string FileSpecification
        {
            get
            {
                return m_Filename;
            }
        }

        /// <summary> Return a flag as to whether the overlay has been saved or not.  
        /// TRUE if the file overlay has been persisted, FALSE otherwise. 
        /// </summary>
        public int HasBeenSaved
        {
            get
            {
                return m_OverlaySaved;
            }
            set
            {
                m_OverlaySaved = value;
            }
        }

        /// <summary> TRUE if the file overlay is dirty and needs to be saved, FALSE otherwise. 
        /// </summary>
        public int IsDirty
        {
            get
            {
                return m_OverlayDirty;
            }
            set
            {
                m_OverlayDirty = value;
            }
        }

        /// <summary> TRUE if the file overlay is a read-only file overlay, FALSE otherwise. 
        /// </summary>
        public int IsReadOnly
        {
            get
            {
                return m_OverlayIsReadOnly;
            }
            set
            {
                m_OverlayIsReadOnly = value;
            }
        }

        #endregion
    }
}
