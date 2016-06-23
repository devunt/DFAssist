using System.Drawing;

namespace App
{
    class Global
    {
        public const string VERSION = "v20160620.1";

        public const string UPDATE_TEMP_FILEPATH = @"DFAssist/.DFAssist.old";
        public const string APPNAME = "DFAssist";
        public const string SETTINGS_FILEPATH = @"config.ini";

        public const string GITHUB_REPO = @"devunt/DFAssist";
        public const string RAVEN_DSN = @"http://1ef7c7a5d0004eaea1815b200c2db6ba:a531662520b5493685a1789e0760e3ec@s.devunt.kr/4";

        public static readonly Color ACCENT_COLOR = Color.Red;
        public const int BLINK_COUNT = 100;
        public const int BLINK_INTERVAL = 200;

        public const int OVERLAY_XY_UNSET = 65535;
    }
}
