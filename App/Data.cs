using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class Data
    {
        public static Dictionary<int, string> WarpMeasure = new Dictionary<int, string>()
        {
            {1, "NPC/사물" },
            {2, "" },
            {3, "지역 이동" },
            {4, "텔레포" },
            {5, "" },
            {6, "" },
            {7, "데존" },
            {8, "" },
            {9, "" },
            {10, "" },
            {11, "임무 입장" },
            {12, "임무 퇴장" }
        };

        internal static Instance GetInstance(int code)
        {
            if (DataStorage.Zone.ContainsKey(code))
            {
                return DataStorage.Zone[code].Instance;
            }

            return new Instance("알 수 없는 임무", 0, 0, 0);
        }

        internal static FATE GetFATE(int code)
        {
            if (DataStorage.FATEs.ContainsKey(code))
            {
                return DataStorage.FATEs[code];
            }

            return new FATE(0, "알 수 없는 돌발임무");
        }

        internal static Area GetZone(int code)
        {
            if (DataStorage.Zone.ContainsKey(code))
            {
                return DataStorage.Zone[code];
            }

            return new Area();
        }

        internal static List<KeyValuePair<int, FATE>> GetFATEs()
        {
            return DataStorage.FATEs.ToList();
        }
    }
}
