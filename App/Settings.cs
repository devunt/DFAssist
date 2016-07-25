using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App
{
    class Settings
    {
        private static INIFile iniFile;

        public static bool ShowOverlay { get; set; } = true;
        public static int OverlayX { get; set; } = Global.OVERLAY_XY_UNSET;
        public static int OverlayY { get; set; } = Global.OVERLAY_XY_UNSET;
        public static bool StartupShowMainForm { get; set; } = true;
        public static bool CheckUpdate { get; set; } = true;
        public static bool AutoUpdate { get; set; } = true;
        public static bool TwitterEnabled { get; set; } = false;
        public static bool AutoOverlayHide { get; set; } = false;
        public static string TwitterAccount { get; set; } = "";
        public static HashSet<int> FATEs { get; set; } = new HashSet<int>();

        private static void Init()
        {
        }

        public static void Load()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Global.APPNAME, Global.SETTINGS_FILEPATH);

            iniFile = new INIFile(path);
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                Init();
                Save();
            }
            else
            {
                StartupShowMainForm = iniFile.ReadValue("startup", "show") != "0";
                CheckUpdate = iniFile.ReadValue("startup", "update") != "0";
                AutoUpdate = iniFile.ReadValue("startup", "autoupdate") != "0";
                ShowOverlay = iniFile.ReadValue("overlay", "show") != "0";
                OverlayX = int.Parse(iniFile.ReadValue("overlay", "x"));
                OverlayY = int.Parse(iniFile.ReadValue("overlay", "y"));
                TwitterEnabled = iniFile.ReadValue("notification", "twitter") == "1";
                TwitterAccount = iniFile.ReadValue("notification", "twitteraccount");
                AutoOverlayHide = iniFile.ReadValue("overlay", "autohide") != "0";

                string fates = iniFile.ReadValue("fate", "fates");
                if (!string.IsNullOrEmpty(fates))
                {
                    FATEs = new HashSet<int>(from x in fates.Split(',') select int.Parse(x));
                }
            }
        }

        public static void Save()
        {
            iniFile.WriteValue("startup", "show", StartupShowMainForm ? "1" : "0");
            iniFile.WriteValue("startup", "update", CheckUpdate ? "1" : "0");
            iniFile.WriteValue("startup", "autoupdate", AutoUpdate ? "1" : "0");
            iniFile.WriteValue("overlay", "show", ShowOverlay ? "1" : "0");
            iniFile.WriteValue("overlay", "autohide", AutoOverlayHide ? "1" : "0");
            iniFile.WriteValue("overlay", "x", OverlayX.ToString());
            iniFile.WriteValue("overlay", "y", OverlayY.ToString());
            iniFile.WriteValue("notification", "twitter", TwitterEnabled ? "1" : "0");
            iniFile.WriteValue("notification", "twitteraccount", TwitterAccount);
            iniFile.WriteValue("fate", "fates", string.Join(",", FATEs));
        }
    }
}
