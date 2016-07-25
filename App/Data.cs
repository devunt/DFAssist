using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace App
{
    public static class Data
    {
        public static bool Initialized = false;
        public static Dictionary<int, Area> Areas { get; set; } = new Dictionary<int, Area>();
        public static Dictionary<int, FATE> FATEs { get; set; } = new Dictionary<int, FATE>();

        public static void Initializer()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Properties.Resources.ZoneList);

            foreach (XmlNode xn in doc.SelectNodes("/Data/Item"))
            {
                Area zone = new Area(xn);
                if (!Areas.ContainsKey(zone.ZoneId))
                {
                    Areas.Add(zone.ZoneId, zone);
                    foreach (KeyValuePair<int, string> fate in zone.FATEList)
                    {
                        FATEs.Add(fate.Key, new FATE(zone.ZoneId, fate.Value));
                    }
                }
            }

            Initialized = true;
        }

        public static string GetAreaName(int key)
        {
            if (Areas.ContainsKey(key))
            {
                return Areas[key].Name;
            }
            else
            {
                return "알 수 없는 지역";
            }
        }

        public static bool GetIsDuty(int key)
        {
            if (Areas.ContainsKey(key))
            {
                return Areas[key].isDuty;
            }
            else
            {
                return false;
            }
        }

        public static string FindAttribute(this XmlNode xn, string findAttr, bool IgnoreCase = true)
        {
            try
            {
                string FindKey = findAttr;

                if (IgnoreCase)
                {
                    FindKey = findAttr.ToLower();
                }

                foreach (XmlAttribute xa in xn.Attributes)
                {
                    string attr = xa.Name;

                    if (IgnoreCase)
                    {
                        attr = attr.ToLower();
                    }

                    if (attr == FindKey)
                    {
                        return xa.Value;
                    }
                }
            }
            catch
            {
                Log.E("요소 찾기 오류");
            }

            return string.Empty;
        }

        internal static Instance GetInstance(int code)
        {
            if (Areas.ContainsKey(code))
            {
                return Areas[code].Instance;
            }

            return new Instance("알 수 없는 임무", 0, 0, 0);
        }

        internal static FATE GetFATE(int code)
        {
            if (FATEs.ContainsKey(code))
            {
                return FATEs[code];
            }

            return new FATE(0, "알 수 없는 돌발임무");
        }

        internal static Area GetArea(int code)
        {
            if (Areas.ContainsKey(code))
            {
                return Areas[code];
            }

            return new Area();
        }

        internal static List<KeyValuePair<int, FATE>> GetFATEs()
        {
            return FATEs.ToList();
        }
    }
}
