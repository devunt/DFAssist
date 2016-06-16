using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace App
{
    class Global
    {
        public const string VERSION = "v20160617.1-v64";

        public const string DLL_PATH = @".\dfhelper.dll";
        public const string SETTINGS_FILEPATH = @".\dfh.ini";

        public const string GITHUB_REPO = @"devunt/DFHelper";

        public static readonly Color ACCENT_COLOR = Color.Red;
        public const int BLINK_COUNT = 100;
        public const int BLINK_INTERVAL = 200;

        public const int OVERLAY_XY_UNSET = 65535;
    }
}
