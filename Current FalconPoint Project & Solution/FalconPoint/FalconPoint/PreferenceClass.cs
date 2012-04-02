using System;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

using DRAWING = System.Drawing;

namespace FalconPoint
{
    /// <summary> A simple class for holding preferences and serializing them.
    /// </summary>
    /// <remarks>
    /// Use .Net Serialization to store this as a preference file.  
    /// </remarks>
    /// <remarks>
    /// Individual items should be added to the class with default value initializers.
    /// </remarks>
    [Serializable]
    [ComVisible(false)]
    public class PreferenceClass
    {
        #region Member Data

        /// <summary> Hide above value for the Layer
        /// </summary>
        /// <remarks>
        /// Use a high value for "hide above = never" or the scale denominator
        /// </remarks>
        [XmlAttribute]
        public Int32 m_HideAbove = Int32.MaxValue;

        /// <summary> Hide above value for the Layer
        /// </summary>
        /// <remarks>
        /// Use a high value for "hide above = never" or the scale denominator
        /// </remarks>
        [XmlAttribute]
        public Int32 m_HideLabelsAbove = Int32.MaxValue;

        /// <summary> Show Banner for the Overlay 
        /// </summary>
        /// <remarks>
        /// This is not really intended for operational use.  It is only a 
        /// placeholder for performing ILayerEditor3 level GDI draw calls
        /// </remarks>
        [XmlAttribute]
        public bool m_ShowBanner = false;

        /// <summary> Flag setting to "use the blue icons"
        /// </summary>
        /// <remarks>
        /// This is an example of a common overlay preference.
        /// </remarks>
        [XmlAttribute]
        public bool m_UseBlueIcons = false;


        /// <summary> Color to use when Drawing
        /// </summary>
        /// <remarks>
        /// Just an example use of preferences
        /// </remarks>
        [XmlIgnore]
        public DRAWING.Color m_UseColor = System.Drawing.Color.Beige;

        #endregion



        #region Constructor

        /// <summary> Default constructor for the preference class
        /// </summary>
        public PreferenceClass()
        {
        }

        #endregion



        #region Support Methods

        /// <summary> Set preferences on the object
        /// </summary>
        /// <param name="thePrefstring"> XML serialized string version of this class
        /// </param>
        public static PreferenceClass CreateFromString(string thePrefstring)
        {
            PreferenceClass StoredPrefernces = null;
            System.IO.MemoryStream theMemStream = null;
            System.IO.StreamReader stReader = null;

            try
            {
                System.Text.ASCIIEncoding byteEncoding = new System.Text.ASCIIEncoding();
                theMemStream = new System.IO.MemoryStream(byteEncoding.GetBytes(thePrefstring));
                stReader = new System.IO.StreamReader(theMemStream);
                System.Xml.Serialization.XmlSerializer xSerializer = new System.Xml.Serialization.XmlSerializer(typeof(PreferenceClass));

                StoredPrefernces = (PreferenceClass)xSerializer.Deserialize(stReader);
            }
            catch
            {
            }
            finally
            {
                if (stReader != null)
                {
                    stReader.Close();
                    stReader.Dispose();
                }

                if (theMemStream != null)
                    theMemStream.Close();
            }

            return StoredPrefernces;
        }


        /// <summary> Set preferences on the object
        /// </summary>
        /// <returns>XML serialized string version of this class
        /// </returns>
        public override string ToString()
        {
            string thePrefString = String.Empty;
            System.IO.MemoryStream theMemStream = null;

            try
            {
                theMemStream = new System.IO.MemoryStream();
                System.Xml.Serialization.XmlSerializer xSerializer = new System.Xml.Serialization.XmlSerializer(typeof(PreferenceClass));
                xSerializer.Serialize(theMemStream, this);

                byte[] buffer = theMemStream.GetBuffer();
                thePrefString = buffer.ToString();

                char[] cbuffer = new char[buffer.Length];
                buffer.CopyTo(cbuffer, 0);
                thePrefString = new string(cbuffer);
            }
            catch
            {
            }
            finally
            {
                if (theMemStream != null)
                    theMemStream.Close();
            }

            return thePrefString;
        }

        #endregion
    }
}