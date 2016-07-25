using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace App
{
    public static class DataStorage
    {
        public static bool Initialized = false;

        public static Dictionary<int, Area> Zone { get; set; } = new Dictionary<int, Area>();
        public static Dictionary<int, FATE> FATEs { get; set; } = new Dictionary<int, FATE>();

        public static void Initializer()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Properties.Resources.ZoneList);

            foreach(XmlNode xn in doc.SelectNodes("/Data/Item"))
            {
                Area zone = new Area(xn);
                if (!Zone.ContainsKey(zone.ZoneId))
                {
                    Zone.Add(zone.ZoneId, zone);
                    foreach (KeyValuePair<int, string> fate in zone.FATEList)
                        FATEs.Add(fate.Key, new FATE(zone.ZoneId, fate.Value));
                }
            }
            Initialized = true;
        }

        public static string GetZoneString(int key)
        {
            if (!Initialized)
                Initializer();

            if (Zone.ContainsKey(key))
                return Zone[key].Text;
            else
                return "알수없는 지역";
        }

        public static string GetZoneString(string hexa)
        {
            return GetZoneString(hexa.GetHexValue());
        }

        public static string GetZoneDutyname(int key)
        {
            if (!Initialized)
                Initializer();

            if (Zone.ContainsKey(key))
                return Zone[key].Duty;
            else
                return "알수없는 임무";
        }

        public static string GetZoneDutyname(string hexa)
        {
            return GetZoneDutyname(hexa.GetHexValue());
        }

        public static bool GetZoneIsDuty(int key)
        {
            if (!Initialized)
                Initializer();

            if (Zone.ContainsKey(key))
                if (Zone[key].isDuty)
                    return true;
                else
                    return false;
            else
                return false;
        }

        public static bool GetZoneIsDuty(string hexa)
        {
            return GetZoneIsDuty(hexa.GetHexValue());
        }

        public static ushort GetHexValue(this string hexa)
        {
            return ushort.Parse(hexa.ToUpper(), System.Globalization.NumberStyles.HexNumber);
        }

        public static string FindAttribute(this XmlNode xn, string findAttr, bool IgnoreCase = true)
        {
            try
            {
                string FindKey = findAttr;

                if (IgnoreCase)
                    FindKey = findAttr.ToLower();

                foreach (XmlAttribute xa in xn.Attributes)
                {
                    string attr = xa.Name;

                    if (IgnoreCase)
                        attr = attr.ToLower();

                    if (attr == FindKey)
                        return xa.Value;
                }
            }
            catch
            {
                Log.E("요소 찾기 오류");
            }

            return string.Empty;
        }
    }
}
