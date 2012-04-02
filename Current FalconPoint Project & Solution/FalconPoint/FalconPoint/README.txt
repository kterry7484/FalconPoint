General Information

=======================


Sections below:

   - Solution Comments

   - FalconView Plugin Manager



Please post/send corrections/Comments to
 
   - wiki.falconview.org

  or
 
   - daniel.longhurst@gtri.gatech.edu


Solution Comments
=======================


There are a some areas to watch to insure that your PlugIn will work out of the box.


1) Insure that the resource files are properly named in your solution and configuration

   files (instruct.xml and config.xml) before building your installer package (fvz).



2) Verify that the Assembly name has been changed to your project.  Unsigned assemblies

   are only name verified and can collide in user installations.  While not required by

   FalconView, your assemblies should be signed.



3) Make sure the Plugin Manager placed all your resources in the correct folders so your

   PlugIn can find them.  The configuration file(s) to VERIFY your entries in is:

      : HD_DATA\config\FvOverlayTypes.xml

      : HD_DATA\config\FvCustomInitializers.xml

      : HD_DATA\config\FvToolMenuItems.xml


   
   Most overlays will only need the FvOverlayTypes file.  Be VERY CAREFUL editing these files

   as they contain part of the FalconView configuration.



4) Watch %Appdata%\MissionPlanning\error_log.txt for errors related to your overlay when

   first starting it. 80000154 is E_CLASS_NOT_REGISTERED.  This can be caused by anything which

   causes the COM CreateInstance call to fail.  If you are registered, then the most likely
 
   causes are from DLLs, COM objects, or resources your PlugIn loads as part of its default
 
   constructor (i.e. member variables of the class which create COM objects, type references to

   other DLLs made IN your constructor, etc).





FalconView Plugin Manager

=========================


To create an installer file for FalconVIew, begin by editing the config.xml and instruct.xml files

in the project folder.  These instruct FalconView on how to install and use your Plugin.



Change instruct.XML to matche the project setup (i.e. AssemblyName).  

Edit config.xml to
match requested configuration.  For Example, the FvOverlayTypeDescriptor and the 
runtime file
descriptor objects are mutully exclusive buit both are included in the default config.xml file.

Other project items, like the custom initializer object, should be removed if not needed.
To 'remove' 
implementation from the config.xml, replace the guids in the file with 'GUID_NULL' 



Next, create a '.zip' file and add the following:

     * All DLL's from the project which will need to be installed
 
     * All Interop assemblies referenced by the project

     * All resource files (i.e. icons, bitmaps, etc.)


     * Config.XML and Instruct.XML

Rename the .zip file to *.fvz.

You may want to configure WinZip to open this file type as a 
FalconView Plugin Manager Archive.



Finally, Run the Plugin Manager to register your project with FalconView