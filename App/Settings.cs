using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App
{
    internal class Settings
    {
        private static INIFile iniFile;

        public static string Language { get; set; } = "ko-kr";
        public static bool ShowOverlay { get; set; } = true;
        public static int OverlayX { get; set; } = Global.OVERLAY_XY_UNSET;
        public static int OverlayY { get; set; } = Global.OVERLAY_XY_UNSET;
        public static bool ShowAnnouncement { get; set; } = true;
        public static bool StartupShowMainForm { get; set; } = true;
        public static bool TwitterEnabled { get; set; } = false;
        public static bool AutoOverlayHide { get; set; } = true;
        public static bool FlashWindow { get; set; } = true;
        public static bool CheatRoulette { get; set; } = false;
        public static string TwitterAccount { get; set; } = "";
        public static bool Updated { get; set; } = true;
        public static bool PlaySound { get; set; } = false;
        public static string SoundLocation { get; set; } = "";
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
                ShowOverlay = iniFile.ReadValue("overlay", "show") != "0";
                AutoOverlayHide = iniFile.ReadValue("overlay", "autohide") != "0";
                OverlayX = int.Parse(iniFile.ReadValue("overlay", "x") ?? "0");
                OverlayY = int.Parse(iniFile.ReadValue("overlay", "y") ?? "0");
                ShowAnnouncement = iniFile.ReadValue("overlay", "announcement") != "0";
                TwitterEnabled = iniFile.ReadValue("notification", "twitter") == "1";
                TwitterAccount = iniFile.ReadValue("notification", "twitteraccount") ?? "";
                FlashWindow = iniFile.ReadValue("notification", "flashwindow") != "0";
                // CheatRoulette = iniFile.ReadValue("misc", "cheatroulette") == "1";
                CheatRoulette = false; // 악용 방지를 위한 강제 비활성화
                Language = iniFile.ReadValue("misc", "language") ?? "ko-kr";
                Updated = iniFile.ReadValue("internal", "updated") != "0";
                PlaySound = iniFile.ReadValue("notification","playsound") != "0";
                SoundLocation = iniFile.ReadValue("notification", "soundlocation") ?? "";

                var fates = iniFile.ReadValue("fate", "fates");
                if (!string.IsNullOrEmpty(fates))
                {
                    FATEs = new HashSet<int>(from x in fates.Split(',') select int.Parse(x));
                }
            }
        }

        public static void Save()
        {
            iniFile.WriteValue("startup", "show", StartupShowMainForm ? "1" : "0");
            iniFile.WriteValue("overlay", "show", ShowOverlay ? "1" : "0");
            iniFile.WriteValue("overlay", "autohide", AutoOverlayHide ? "1" : "0");
            iniFile.WriteValue("overlay", "x", OverlayX.ToString());
            iniFile.WriteValue("overlay", "y", OverlayY.ToString());
            iniFile.WriteValue("overlay", "announcement", ShowAnnouncement ? "1" : "0");
            iniFile.WriteValue("notification", "twitter", TwitterEnabled ? "1" : "0");
            iniFile.WriteValue("notification", "twitteraccount", TwitterAccount);
            iniFile.WriteValue("notification", "flashwindow", FlashWindow ? "1" : "0");
            iniFile.WriteValue("misc", "cheatroulette", CheatRoulette ? "1" : "0");
            iniFile.WriteValue("misc", "language", Language);
            iniFile.WriteValue("fate", "fates", string.Join(",", FATEs));
            iniFile.WriteValue("internal", "updated", Updated ? "1" : "0");
            iniFile.WriteValue("notification", "playsound", PlaySound ? "1" : "0");
            iniFile.WriteValue("notification", "soundlocation", SoundLocation);
        }
    }
}
