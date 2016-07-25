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

        public static Dictionary<int, ZoneItem> Zone { get; set; } = new Dictionary<int, ZoneItem>();

        public static void Initializer()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Properties.Resources.ZoneList);

            foreach(XmlNode xn in doc.SelectNodes("/Data/Item"))
            {
                ZoneItem zone = new ZoneItem(xn);
                if (!Zone.ContainsKey(zone.ZoneId))
                    Zone.Add(zone.ZoneId, zone);
            }
            Initialized = true;
        }

        public static string getZoneString(int key)
        {
            if (!Initialized)
                Initializer();

            if (Zone.ContainsKey(key))
                return Zone[key].Text;
            else
                return "알수없는 지역";
        }

        public static string getZoneString(string hexa)
        {
            return getZoneString(hexa.getHexValue());
        }

        public static string getZoneDutyname(int key)
        {
            if (!Initialized)
                Initializer();

            if (Zone.ContainsKey(key))
                return Zone[key].Duty;
            else
                return "알수없는 임무";
        }

        public static string getZoneDutyname(string hexa)
        {
            return getZoneDutyname(hexa.getHexValue());
        }

        public static bool getZoneIsDuty(int key)
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

        public static bool getZoneIsDuty(string hexa)
        {
            return getZoneIsDuty(hexa.getHexValue());
        }

        public static ushort getHexValue(this string hexa)
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
