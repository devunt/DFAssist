using System.IO;

namespace App
{
    class Settings
    {
        private static INIFile iniFile;

        public static bool ShowOverlay { get; set; } = true;
        public static int OverlayX { get; set; } = Global.OVERLAY_XY_UNSET;
        public static int OverlayY { get; set; } = Global.OVERLAY_XY_UNSET;
        public static bool StartupShowMainForm { get; set; } = true;
        public static bool StartupCheckUpdate { get; set; } = true;

        private static void Init()
        {
        }

        public static void Load()
        {
            iniFile = new INIFile(Global.SETTINGS_FILEPATH);
            if (!File.Exists(Global.SETTINGS_FILEPATH))
            {
                Init();
                Save();
            }
            else
            {
                StartupShowMainForm = iniFile.ReadValue("startup", "show") == "1";
                StartupCheckUpdate = iniFile.ReadValue("startup", "update") == "1";
                ShowOverlay = iniFile.ReadValue("overlay", "show") == "1";
                OverlayX = int.Parse(iniFile.ReadValue("overlay", "x"));
                OverlayY = int.Parse(iniFile.ReadValue("overlay", "y"));
            }
        }

        public static void Save()
        {
            iniFile.WriteValue("startup", "show", StartupShowMainForm ? "1" : "0");
            iniFile.WriteValue("startup", "update", StartupCheckUpdate ? "1" : "0");
            iniFile.WriteValue("overlay", "show", ShowOverlay ? "1" : "0");
            iniFile.WriteValue("overlay", "x", OverlayX.ToString());
            iniFile.WriteValue("overlay", "y", OverlayY.ToString());
        }
    }
}
