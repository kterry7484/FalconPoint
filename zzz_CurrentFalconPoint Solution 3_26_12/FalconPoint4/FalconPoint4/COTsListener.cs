// ****************************************************************************
// $HeadURL $
// Owner: $Author: $
// $Revision: $
// $Id: $
// $LastChangedDate: $
// Description:  This class listens for incoming TCP/IP connections and if they are COT
//                 formated, then it will pullout lat, lon, time, stale?, etc. and send
//                  that data to the drawer to be drawn.
// ****************************************************************************

using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using fvw;
using System.Collections.Generic;

namespace FalconPoint4
{

    public class LayerList // used to keep track of UID and the layer that we put them on
    {
        public string cotID;
        public int Layer;

        public IList<LatLongList> FP_LatLonlist = new List<LatLongList>(); // list to keep track of all lat and longs assoc. with a particular cotID

        public class LatLongList // used to keep track of all lat and longs assoc. with a particular cotID
        {
            public double lat;
            public double lon;
            public DateTime time;
        }
    }


    class Haversine
    {
        public double Distance() // double lat1, double lat2, double lon1, double lon2
        {
            double lat1 = 34.713496;
            double lat2 = 34.713538;
            double lon1 = -86.685663;
            double lon2 = -86.688520;

            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            dist = dist * .8684;

            return dist;
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }

    class COTsListener
    {


        private TcpListener tcpListner;
        private Thread listenThread;
        public FPmain _FPmainAddr = null;

        public ILayer FP_point = null;
        int currentLayerHandle = 0;

        public IList<LayerList> FP_layerList = new List<LayerList>(); // used to keep track of UID and the layer that we put them on

        public LayerList temp_layerList = new LayerList(); // temp location for points before adding to FP_layerList

        public COTsListener(FPmain _FPmain)
        {
            _FPmainAddr = _FPmain; // used to get address of of FPmain for use in creating layers... gotta have address of "this" to create a layer

            this.tcpListner = new TcpListener(IPAddress.Any, 3000);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        private void ListenForClients()
        {
            this.tcpListner.Start();

            while (true)
            {
                TcpClient client = this.tcpListner.AcceptTcpClient();

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object client)
        {
            string messageString = null;

            double currentLat;
            double currentLon;
            string currentID;
            string currentTime;

            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clienStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clienStream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                {
                    break;
                }

                ASCIIEncoding encoder = new ASCIIEncoding();
                System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));

                messageString = encoder.GetString(message); 

                currentID = GetID(messageString);
                currentLat = GetLat(messageString);
                currentLon = GetLon(messageString);
                currentTime = GetTime(messageString);

                SendCordsToFP(currentID, currentLat, currentLon);
            }

            tcpClient.Close();
        }

        public void SendCordsToFP(string _cotID, double _lat, double _lon)
        {
            //Haversine newDistance = new Haversine();

            //double distance = newDistance.Distance();

            bool cotID_alreadyExists = false;

            for(int i=0; i<FP_layerList.Count; i++) // itterate through list to see if the current cotID has already been added to list
            {
                if (FP_layerList[i].cotID == _cotID) // if we already have this cotID in our list
                {
                    FPdrawer drawPT_instance = new FPdrawer(); // create a new instance of the drawer class
                    drawPT_instance.CreatePoint(FP_point, FP_layerList[i].Layer, _cotID, _lat, _lon); // if cot uid already exists in list, then use it's layer

                    LayerList.LatLongList temp_latLonList = new LayerList.LatLongList(); // temp list used to add lat and lon to our sub list... basically one main list holds LAYER and COTid and that list conists of another list that stores multiple lat, lon and times
                    temp_latLonList.lat = _lat;
                    temp_latLonList.lon = _lon;

                    FP_layerList[i].FP_LatLonlist.Add(temp_latLonList); // add the temp data to the main list
                    temp_latLonList = new LayerList.LatLongList(); // create a new temp list

                    cotID_alreadyExists = true; // used to keep us from jumping into the next if... prob could use else
                }
            }

            if(cotID_alreadyExists == false) // if we don't have this cotID in our list
            {
                CreateLayer(_cotID); // if we don't have this cotID, then we need to create a new layer
                FPdrawer drawPT_instance = new FPdrawer(); // create a new instance of the drawer class
                drawPT_instance.CreatePoint(FP_point, currentLayerHandle, _cotID, _lat, _lon); // use the newly created layer
            }
        }

        public double GetLat(string _message)
        {
            string _rtnString = _message.TrimEnd('\0');
            int wheresLat = _rtnString.IndexOf("lat=") + 1;
            int whereFirstQuote = _rtnString.IndexOf("\"", wheresLat);
            int wheresLastQuote = _rtnString.IndexOf("\"", whereFirstQuote + 1);

            _rtnString = _rtnString.Remove(wheresLastQuote, (_rtnString.Length - wheresLastQuote)); // get rid of everything to the right of what we want

            _rtnString = _rtnString.Remove(0, whereFirstQuote + 1);

            return Convert.ToDouble(_rtnString);
        }

        public double GetLon(string _message)
        {
            string _rtnString = _message.TrimEnd('\0');
            int wheresLon = _rtnString.IndexOf("lon=") + 1;
            int whereFirstQuote = _rtnString.IndexOf("\"", wheresLon);
            int wheresLastQuote = _rtnString.IndexOf("\"", whereFirstQuote + 1);

            _rtnString = _rtnString.Remove(wheresLastQuote, (_rtnString.Length - wheresLastQuote)); // get rid of everything to the right of what we want

            _rtnString = _rtnString.Remove(0, whereFirstQuote + 1);

            return Convert.ToDouble(_rtnString);
        }

        public string GetID(string _message)
        {
            string _rtnString = _message.TrimEnd('\0');
            int wheresUID = _rtnString.IndexOf("uid=") + 1;
            int whereFirstQuote = _rtnString.IndexOf("\"", wheresUID);
            int wheresLastQuote = _rtnString.IndexOf("\"", whereFirstQuote + 1);

            _rtnString = _rtnString.Remove(wheresLastQuote, (_rtnString.Length - wheresLastQuote)); // get rid of everything to the right of what we want

            _rtnString = _rtnString.Remove(0, whereFirstQuote + 1);

            return _rtnString;
        }

        public string GetTime(string _message)
        {
            string _rtnString = _message.TrimEnd('\0');
            int wheresTime = _rtnString.IndexOf("time=") + 1;
            int whereFirstQuote = _rtnString.IndexOf("\"", wheresTime);
            int wheresLastQuote = _rtnString.IndexOf("\"", whereFirstQuote + 1);

            _rtnString = _rtnString.Remove(wheresLastQuote, (_rtnString.Length - wheresLastQuote)); // get rid stuff to the right
            _rtnString = _rtnString.TrimEnd('Z'); // trim the z off the end

            _rtnString = _rtnString.Remove(0, whereFirstQuote + 1);

            int wheresFirstColon = _rtnString.IndexOf(':'); // finds the : in the time string

            _rtnString = _rtnString.Remove(0, wheresFirstColon + 1); // remove everything up to the first colon.. gives us a time like 07:06

            return _rtnString;
        }

        public int CreateLayer(string _cotID) // create a new layer, return the handle, and add info to list
        {
            int result = 50; // result is used for debugging... i used 50 so that i could tell that -1, 0, or 1 was the output... nothing special about 50
            FP_point = new LayerClass();

            result = FP_point.RegisterWithMapServer("falconpoint", 0, _FPmainAddr); // result is used for debugging
            currentLayerHandle = FP_point.CreateLayer("FP layer");

            System.Diagnostics.Debug.WriteLine("registered with map server result = " + currentLayerHandle); // used for debugging... shows registration result in output window

            temp_layerList.Layer = currentLayerHandle; // temp list item
            temp_layerList.cotID = _cotID; // temp list item

            FP_layerList.Add(temp_layerList); // add temp items to real list

            temp_layerList = new LayerList(); // create a new temp list... otherwise we would keep writing over the old list

            return currentLayerHandle; // return the newly created layer handle ... should be something like 102, 103..

        }
    }
}

