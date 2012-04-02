using System;
using System.Collections.Generic;
using System.Text;

namespace FalconPoint
{
    /// <summary> Private interface method to provides preference setting and retrieval on an "overlay" object
    /// </summary>
    public partial class FvOverlay
    {
        #region Overlay Preference Support

        /// <summary> Preferences are being passed to the object for decoding and use.
        /// <note>This is a required named method by FalconView</note>
        /// <note>The DispId MUST be 1</note>
        /// </summary>
        /// <param name="Preferences"> Encoded preference string
        /// </param>
        /// <remarks>
        /// The encoding of the string and preference values is entirely up to the
        /// plug-in developer.  Since the developer "owns" both the preference control
        /// and the overlay (both the encoding and use in both directions), any encoding
        /// which results in a valid string value is allowed.
        /// </remarks>
        public void SetPreferences(string Preferences)
        {
            PreferenceClass thePrefs = PreferenceClass.CreateFromString(Preferences);

            // TODO: Deconflict the need to set only "default" preferences or apply to one or more open files

            // Decode the preferences that we need now
            m_HideAbove = thePrefs.m_HideAbove;
            m_HideLabelsAbove = thePrefs.m_HideLabelsAbove;

            m_UseColor = thePrefs.m_UseColor;

            m_ShowBanner = thePrefs.m_ShowBanner;

            // If this is a file overlay, the file has been changed IF the user has applied the preferences
            // chosen in the overlay options dialog to this file.
            m_OverlayDirty = TRUE;



            // Persist the preference file
            // Persist the Common preferences here
            System.IO.StreamWriter stWriter = null;

            try
            {
                stWriter = new System.IO.StreamWriter(USER_DATA + m_DataDirectory + "\\PrefsFile.xml", false);
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(PreferenceClass));

                xmlSerializer.Serialize(stWriter.BaseStream, thePrefs);
            }
            catch
            {
                // Unable to write preference file!
            }
            finally
            {
                if (stWriter != null)
                {
                    stWriter.Close();
                    stWriter.Dispose();
                }
            }

            // Check if the overlay has been opened.  If so, invalidate it
            if (m_displayChangeNotifyEvents != null)
                InvalidateImplementedViews();
        }

        /// <summary> Preferences are being requested from the object to be encoded and 
        /// returned as a string value.  
        /// <note>This is a required named method by FalconView</note>
        /// <note>The DispId MUST be 2</note>
        /// </summary>
        /// <returns>
        /// Encoded preference string
        /// </returns>
        /// <remarks>
        /// The encoding of the string and preference values is entirely up to the
        /// plug-in developer.  Since the developer "owns" both the preference control
        /// and the overlay (both the encoding and use in both directions), any encoding
        /// which results in a valid string value is allowed.
        /// </remarks>
        public string GetPreferences()
        {
            PreferenceClass StoredPrefernces = null;
            System.IO.StreamReader stReader = null;

            try
            {
                string prefsFile = USER_DATA + m_DataDirectory + "\\PrefsFile.xml";

                if (System.IO.File.Exists(prefsFile))
                {
                    stReader = new System.IO.StreamReader(prefsFile);
                    System.Xml.Serialization.XmlSerializer xSerializer = new System.Xml.Serialization.XmlSerializer(typeof(PreferenceClass));

                    StoredPrefernces = (PreferenceClass)xSerializer.Deserialize(stReader);
                }

                if (StoredPrefernces == null)
                {
                    StoredPrefernces = new PreferenceClass();
                }
            }
            catch (Exception)
            {
                // If for any reason we can't read it, defaults will be taken from the PreferenceClass object
                StoredPrefernces = new PreferenceClass();
            }
            finally
            {
                if (stReader != null)
                {
                    stReader.Close();
                    stReader.Dispose();
                }
            }

            return StoredPrefernces.ToString();
        }

        #endregion
    }
}
