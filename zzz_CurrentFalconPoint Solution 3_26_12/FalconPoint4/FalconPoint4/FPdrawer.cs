// ****************************************************************************
// $HeadURL $
// Owner: $Author: $
// $Revision: $
// $Id: $
// $LastChangedDate: $
// Description:  This class draws icons to the falconpoint map.
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fvw;

namespace FalconPoint4
{
    class FPdrawer
    {
        string iconLoc = null;

        public FPdrawer()
        {

            if (System.Environment.Is64BitOperatingSystem == true)
                iconLoc = "C:\\Program Files (x86)\\PFPS\\falcon\\data\\icons\\Localpnt\\NFZ.ico";
            else
                iconLoc = "C:\\Program Files\\PFPS\\falcon\\data\\icons\\Localpnt\\NFZ.ico";

        }
        

        public void CreatePoint(ILayer FP_point, int _layer, string _id, double _lat, double _long)
        {
            FP_point.DeleteAllObjects(_layer); // delete everything on this layer.. keeps us from having bread crumbs

            FP_point.AddIcon(_layer, iconLoc, _lat, _long, _id);

            FP_point.Refresh(-1);

        }


    }
}
