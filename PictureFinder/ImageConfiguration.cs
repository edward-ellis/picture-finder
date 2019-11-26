using System;
using System.Collections.Generic;
using System.IO;

namespace PictureFinder
{
    public static class ImageConfiguration
    {
        public static int MinimumDimension
        {
            get { return 720; }
        }
        public static string MyPicturesPath
        {
            get
            {
                string myPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                string mySpotlightDataPath = Path.Combine(myPicturesPath, @"SpotlightImages");
                return mySpotlightDataPath;
            }
        }

        public static string SpotlightDataPath
        {
            get
            {
                string localApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string spotlightDataPath = Path.Combine(localApplicationDataPath, @"Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets");
                return spotlightDataPath;
            }
        }
    }
}
