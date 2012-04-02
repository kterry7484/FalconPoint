using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

using FORMS = System.Windows.Forms;

namespace FalconPoint
{
    /// <summary> This class is the main required object to implement an Overlay 
    /// </summary>
    /// <remarks>
    /// <para>The Wizard set the ProgId to Namespace.PluginName as specified in the options, but left the code
    /// reflecting the default name of "FvOVerlay" for the overlay object.  This aligns the code classes
    /// to the interface names and is not exposed in the registry.  Until familiar with the object purposes,
    /// the actual class names can be left as default.
    /// </para>
    /// <para> The assemble DLL name will be created as FvOverlay.dll to match the DLL name specified in the Instruct.XML 
    /// falconview configuration file.  The DLL name of Assemblies generally is set to match the NameSpace.ClassName that they
    /// contain.  Some teams use Company.NameSpace.ClassName.Dll or Product.NameSpace.ClassName.DLL.
    /// </para>
    /// <para> For Coding clarity, the ClassName can remain FvOverlay internally, while the ProgId is explicitly
    /// set below with the ProgId Attribute.  The DLL name is set in the project options of the FvOverlay Project in the solution.
    /// It is recommended that the ProgId and DLL names be set specifically for your project.
    /// </para>
    /// </remarks>
    [ComVisible(true)]
    [Guid("9fee887b-8a78-4646-97c0-29bf468a50a5")]
    [ProgId("FalconPoint.FvOverlay")]
    public partial class FvOverlay : FalconViewOverlayLib.IFvOverlay, FvCommonDialogsLib.IFvInformationDialogCallback
    {
        #region Defined Values

        /// <summary> Coded value 
        /// </summary>
        const int FALSE = 0;

        /// <summary> Coded value 
        /// </summary>
        const int TRUE = 1;

        /// <summary> Coded value 
        /// </summary>
        /// <remarks> Use this return code to note that the plug-in "has handled"
        /// the call from FalconView.  FalconView will not query other plug-ins if
        /// they need to handle it -- i.e. the plug-in returned a tooltip.
        /// </remarks>
        const int SUCCESS = 0;

        /// <summary> Coded value 
        /// </summary>
        /// <remarks> Use this return code to note that the plug-in "has NOT handled"
        /// the call from FalconView.  FalconView will not query other plug-ins if
        /// they need to handle it -- i.e. the plug-in did NOT provide a tooltip so 
        /// other overlays need to be queried if they want to supply one.
        /// </remarks>
        const int FAILED = -1;

        #endregion



        #region Constructors

        /// <summary> Default constructor for FvOverlay class
        /// </summary>
        /// <remarks> <para>
        /// Be VERY careful when referring to other DLL's and or COM objects in constructor 
        /// or member initialization code.</para>
        /// <para>  
        /// If the "other" COM object, DLL, or Assembly is not present, is not or accessible, or
        /// cannot be created for any reason, FalconView will report this plugin with that error 
        /// (i.e. "not registered").  This occurs because Net throws the error before entrance INTO the 
        /// PlugIn's constructor.  The COM error will be passed on to our caller making it appear
        /// that the PlugIn and not a derivative reference caused the problem.</para>
        /// <para>
        /// Members which are initialized with COM objects, with types defined in other Assemblies, 
        /// or loads of other DLLs can all cause the same problem -- exceptions occuring before 
        /// our consructor.  Since the exception occurs before our constructor, it will not be 
        /// caught by the constructor's try~catch block.
        /// </para>
        /// <para>
        /// As a workaround for initialization, simply move the problematic code into an 
        /// initialization function.  The call to Initialize() can be wrapped with a try~catch 
        /// in the constructor to our plug-in.
        /// <note> Save the state of initialization for use when FalconView tries to use your object.
        /// </note>
        /// </para>
        /// </remarks>
        /// <example> 
        /// <para>
        /// This is "Bad" constructor code since it cannot catch the error.</para>
        /// <code lang="C#">
        /// try {
        ///    UnregisteredCOMObject theObj = new UnregisteredCOMObject();
        /// }
        /// catch (Exception ex)
        /// {
        ///    // If the COM object is in a DLL which is not registered or not loadable for any
        ///    // reason, .NET will throw an exception BEFORE ENTERING the FvOverlay constructor !!!
        ///    // We will never reach here!
        /// 
        ///    UseLessCall_SinceWeCantCatch();
        /// }
        /// </code>
        /// <para>
        /// This is "Good" constructor code since it will catch COM construction errors</para>
        /// <code lang="C#">
        /// try {
        ///    InitializeObject();      
        /// }
        /// catch (Exception ex)
        /// {
        ///    // Implement the several likely suspects with meaningful messages
        ///    // to the user 
        /// }
        /// finally {
        ///    // Use a generic catch and display the error in a finally to be sure
        ///    // your short list of suspects are not the only detected errors
        ///    if (!Initialized)
        ///       FORMS.MessageBox.Show("Unable to properly initialize the Landmark Editor");
        /// }
        /// </code>
        /// </example>
        public FvOverlay()
        {
            // Lab1 : Trace Location

            bool Initialized = false;

            try
            {
                InitializeObject();

                Initialized = true;
            }
            catch (Exception)
            {
                // While blank exception handlers are a bad idea, this is only 
                // meant to show the code struture by which construction errors
                // can be caught.  Adding a general "initialized" flag as a 
                // ready-to-use flag for your object is also a ood idea.  Or,
                // re-throw here a more meaningful error code to FalconView
            }
            finally
            {
                if (!Initialized)
                    FORMS.MessageBox.Show("Unable to properly initialize the " + OverlayFriendlyName);
            }

            ++objectCount;
            m_thisOverlayID = objectCount;

            Server test_server = new Server();
           
        }



        /// <summary> A simple reference ID for use during debugging
        /// </summary>
        int m_thisOverlayID = 0;

        /// <summary> A simple reference count for use during debugging
        /// </summary>
        static int objectCount = 0;

        /// <summary> "Destructor" for visual use during debugging
        /// </summary>
        ~FvOverlay()
        {
            //TODO: remove "detructor"

            // this "destructor" is only here for visual object reference counting during debugging
            --objectCount;
        }



        /// <summary> Initialization method for the PlugIn
        /// </summary>
        /// <remarks>
        /// Due to the way .Net initializes types on object entry, if any types used in this method 
        /// cannot be found (i.e. CreateInstance would throw), an exception will be thrown PRIOR to 
        /// method entry.  This can be detrected in the caller (i.e. our constructor) and handled
        /// gracefully.  
        /// </remarks>
        void InitializeObject()
        {
            try
            {
                if (HD_DATA == string.Empty)
                {
                    Microsoft.Win32.RegistryKey FvwMainKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\pfps\FalconView\Main");

                    HD_DATA = (string)FvwMainKey.GetValue("HD_DATA", @"c:\pfps\falcon\data");
                    USER_DATA = (string)FvwMainKey.GetValue("USER_DATA", @"c:\pfps\data");

                    FvwMainKey.Close();
                }
            }
            catch (System.Security.SecurityException ex)
            {
                FORMS.MessageBox.Show(ex.ToString(), FvOverlay.OverlayFriendlyName + " initialization error 1!");
            }
            catch (System.ArgumentException ex)
            {
                FORMS.MessageBox.Show(ex.ToString(), FvOverlay.OverlayFriendlyName + " initialization error 2!");
            }

        }


        #endregion



        #region Member Data

        /// <summary> Displayed overlay name (i.e. menus)
        /// </summary>
        [ComVisible(false)]
        public readonly static string OverlayFriendlyName = "FalconPoint";

        /// <summary> Storage directory for static data files
        /// </summary>
        /// <remarks>
        /// HD_DATA is read from a registry key installed with FalconView for the proper
        /// location of static overlay data.  The location is not required of plug-ins, but
        /// strongly recommended. 
        /// </remarks>
        [ComVisible(false)]
        public static string HD_DATA = string.Empty;

        /// <summary> Storage directory for user created data files
        /// </summary>
        /// <remarks>
        /// USER_DATA is read from a registry key installed with FalconView for the proper
        /// location of static overlay data.  The location is not required of plug-ins, but
        /// strongly recommended. 
        /// </remarks>
        [ComVisible(false)]
        public static string USER_DATA;

        /// <summary> Subdirectory name our plug-in will use unde HD_DATA and USER_DATA
        /// </summary>
        const string m_DataDirectory = "\\FvOverlay";

        /// <summary> Our standard file extension for stored editor files
        /// </summary>
        // TODO: Verify if there is a 3 letter maximum or not
        const string m_FileExt = "xml";

        /// <summary> Flag on whether the overlay has been changed or not
        /// </summary>
        /// <remarks>
        /// This item is intended for use by the IFvOverlayPersistence piece of the overlay
        /// but is placed here for convenience of the wizard
        /// </remarks>
        // TODO: Move m_OverlayDirty flag definition back to the FVOverlayPersistence object or delete it as unnecessary
        int m_OverlayDirty = FALSE;

        /// <summary> Object is used to call for invalidation (re-draw) on FalconVew's surface, base map, and vertical view window
        /// </summary>
        private FalconViewOverlayLib.IDisplayChangeNotifyEvents m_displayChangeNotifyEvents = null;

        /// <summary> Overlay Icon for overlay manager
        /// </summary>
        private string m_OverlayIconFile = "FvOverlay\\FvOverlay.ICO";

        /// <summary> Simple item to draw on the screen, a rectangle in screen space
        /// </summary>
        System.Drawing.Rectangle m_DummyItem = new System.Drawing.Rectangle(0, 0, 100, 100);

        /// <summary> "Name" of the DummyItem;
        /// </summary>
        string m_DummyItemName = "FalconPoint!!!";

        /// <summary> Simple color preference for drawing
        /// </summary>
        System.Drawing.Color m_UseColor = System.Drawing.Color.Beige;

        /// <summary> Simple color preference for drawing
        /// </summary>
        bool m_ShowBanner = true;

        /// <summary> FalconView assigned "GUID" uised in referencing this overlay type
        /// </summary>
        /// <remarks>
        /// Will match the "overlayDescGuid" entry in the overlay's config.xml file
        /// </remarks>
        private Guid m_OverlayDescGuid = Guid.Empty;

        /// <summary> Internal flag for when the overlay is visible
        /// </summary>
        private bool m_OverlayVisible = false;

        /// <summary> Flag as to whether the overlay was drawn in the last OnDraw call.
        /// </summary>
        private bool m_OverlayHidden = false;

        /// <summary> Internal flag for when to hide the overlay
        /// </summary>
        /// <remarks>
        /// Set to large value to prevent being hidden.  (i..e hide above never)
        /// </remarks>
        private int m_HideAbove = int.MaxValue;

        /// <summary> Internal flag for when to hide labels on the overlay
        /// </summary>
        /// <remarks>
        /// Set to large value to prevent being hidden.  (i..e hide above never)
        /// </remarks>
        private int m_HideLabelsAbove = int.MaxValue;

        /// <summary> Used in converting to and from detailed map scales
        /// </summary>
        /// <seealso cref="GetScaleDenominator"/>
        private MAPSCALEUTILSERVERLib.MapScaleUtilClass m_FvScaleUtils = null;


        /// <summary> Info dialog reference used by the overlay
        /// </summary>
        /// <remarks>
        /// <para> This dialog reference is used by all menu items which may be raised by the overlay
        /// </para>
        /// <para> The member is used by the FalconViewOverlayLib.IFvOverlayUIEvents implementation,
        /// the FVContextMenuItem implementations, and any other overlay operation which will be 
        /// using an information display dialog.
        /// </para>
        /// </remarks>
        [ComVisible(false)]
        public FvCommonDialogsLib.FvInformationDialogClass m_InfoDlg = null;



        /// <summary> provides overlays a reference to the to the "singleton" state member
        /// </summary>
        /// <remarks>
        /// Only used by the associated Editor object.  This property is only on the overlay
        /// object instead of the editor because of the wizard.  
        /// </remarks>
        [ComVisible(false)]
        public static bool EditMode = false;

        #endregion



        #region Support Methods

        /// <summary> InvalidateImplementedViews the Overlay in FalconView if we are registered
        /// </summary>
        public void InvalidateImplementedViews()
        {
            // TODO: Your overlay should know the needed invalidations at runtime, this wizard does not,...

            if (m_displayChangeNotifyEvents != null)
            {
                if (this as FalconViewOverlayLib.IFvOverlay != null)
                    m_displayChangeNotifyEvents.InvalidateOverlay();

                if (this as FalconViewOverlayLib.IFvOverlayVerticalViewRenderer != null)
                    m_displayChangeNotifyEvents.InvalidateVerticalDisplay();

                if (this as FalconViewOverlayLib.IFvOverlayBaseMapRenderer != null)
                    m_displayChangeNotifyEvents.InvalidateVerticalDisplay();
            }
        }

        /// <summary> Used to retrieve the current map scale 
        /// </summary>
        /// <param name="theSettableMapProj"> ISettableMapProj object for the relevant map view
        /// </param>
        /// <returns>
        /// The map's scale denominator (or equivelant for resolution scales) or 0 if there is an error.
        /// </returns>
        private double GetScaleDenominator(MAPENGINELib.ISettableMapProj theSettableMapProj)
        {
            if (theSettableMapProj == null)
                return 0;

            if (m_FvScaleUtils == null)
            {
                m_FvScaleUtils = new MAPSCALEUTILSERVERLib.MapScaleUtilClass();

                if (m_FvScaleUtils == null)
                    return 0;
            }

            double scaleDenominator = 0;
            MAPENGINELib.MapScaleUnitsEnum scaleUnits;
            theSettableMapProj.scale(out scaleDenominator, out scaleUnits);

            if (scaleUnits != MAPENGINELib.MapScaleUnitsEnum.MAP_SCALE_DENOMINATOR)
            {
                if (scaleUnits == MAPENGINELib.MapScaleUnitsEnum.MAP_SCALE_WORLD)
                {
                    scaleDenominator = 240000000;
                }
                else
                {
                    scaleDenominator = m_FvScaleUtils.ResolutionToScale(scaleDenominator, (MAPSCALEUTILSERVERLib.MapScaleUnitsEnum)scaleUnits);
                }
            }

            return scaleDenominator;
        }


        #endregion



        #region IFvOverlay Members

        /// <summary> Initialize the overlay for use
        /// </summary>
        /// <remarks>
        /// The read-only property OverlayDescGuid should be set by this method. 
        /// FalconView will check the OverlayDescGuid property after Initialize 
        /// returns to verify that the property was set as expected. 
        /// </remarks>
        /// <param name="OverlayDescGuid"></param>
        public void Initialize(Guid OverlayDescGuid)
        {
            m_OverlayDescGuid = OverlayDescGuid;

            IsOverlayVisible = TRUE;

            string defaultDirectory = USER_DATA + m_DataDirectory;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(defaultDirectory);
            if (!di.Exists)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(defaultDirectory);
                }
                catch (Exception)
                {
                    // Handle this for good coding!
                }
            }


            // TODO: Load the default preferences for the overlay
            SetPreferences(GetPreferences());
        }

        /// <summary> Hides/Shows the overlay
        /// </summary>
        /// <remarks>
        /// return TRUE if the overlay is visible, FALSE otherwise. 
        /// </remarks>
        public int IsOverlayVisible
        {
            get
            {
                return (m_OverlayVisible) ? TRUE : FALSE;
            }
            set
            {
                m_OverlayVisible = (value == TRUE);
            }
        }

        /// <summary> return the overlay "type" GUID implemented by this obeject.
        /// </summary>
        /// <remarks>
        /// This should match the setting in config.xml"
        /// </remarks>
        public Guid OverlayDescGuid
        {
            get
            {
                return m_OverlayDescGuid;
            }
        }

        /// <summary> Overlay is given a callback to FalconView for use in clearing UI display elements
        /// </summary>
        /// <remarks>
        /// Cache this object for the lifetime of the overlay.
        /// </remarks>
        public object OverlayEventSink
        {
            set
            {
                m_displayChangeNotifyEvents = value as FalconViewOverlayLib.IDisplayChangeNotifyEvents;
            }
        }

        /// <summary> Called when the overlay is being termininated. 
        /// </summary>
        /// <param name="bCanAbortTermination">TRUE if this overlay can abort the termination, 
        /// FALSE otherwise (e.g., FalconView is shutting down)
        /// </param>
        /// <returns>
        /// return TRUE to abort the termination, FALSE otherwise. 
        /// Note that the return value can be ignored by FalconView depending 
        /// on the value of bCanAbortTermination.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The overlay will remain opened, in the case that bCanAbortTermination is TRUE, 
        /// if pbAbortTermination is set to a non-zero value.</para>
        /// <para>
        /// If bCanAbortTermination is FALSE then the value passed 
        /// back in pbAbortTerminate is ignored. </para>
        /// </remarks>
        public int Terminate(int bCanAbortTermination)
        {
            if (bCanAbortTermination == TRUE)
            {
                // Optionally block closing of the overlay here.  For instance, when a critical 
                // user operation hasnot been completed, like the UAV being controlled has not 
                // landed or been handed off to another ground station.
                return FALSE;
            }
            else
            {
                return FALSE;
            }
        }

        #endregion IFvOverlay


        #region IFvInformationDialogCallback Members

        /// <summary> Callback to the owner of the dialog that control has been taken away
        /// </summary>
        /// <remarks>
        /// as an example, for "selection" type operations where info is displayed for a 
        /// MapSymbol and the item was highlighted on the map while information on that item 
        /// was displayed, this call signals the overlay that the highlight may be removed.
        /// </remarks>
        public void InfoDialogOwnerChanged()
        {
            // Check for an erroneous call
            if (m_InfoDlg == null)
                return;

            if (m_InfoDlg.IsDialogActive == 1)
            {
                // Dialog is still open, but not owned by us anymore

                // Likely, the only action here is to remove any Map Symbol highlights from the map
            }
            else
            {
                // dioalog has been closed
            }

            m_InfoDlg = null;
        }

        #endregion

    }
}
