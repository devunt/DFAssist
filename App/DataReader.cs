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

        // 0: 지역이름 1: 임무이름 2:지역타입(0=필드/1=던전,토벌) 3:탱힐딜 계수 또는 돌발임무 목록
        public static Dictionary<ushort, object[]> Zone { get; set; } = new Dictionary<ushort, object[]>();

        public static void Initializer()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Properties.Resources.ZoneList);

            foreach(XmlNode xn in doc.SelectNodes("/Data/Item"))
            {
                object[] item = new object[] { "", "", 0, null };
                if ((xn as XmlElement).GetAttribute("Text") != string.Empty)
                    item[0] = (xn as XmlElement).GetAttribute("Text");

                if ((xn as XmlElement).GetAttribute("Duty") != string.Empty)
                {
                    item[1] = (xn as XmlElement).GetAttribute("Duty");
                    item[2] = 1;
                }

                try
                {
                    //Log.E(xn.Attributes.getXmlAttribute("zoneid"));
                    ushort us = (xn as XmlElement).GetAttribute("ZoneId").getHexValue();
                    //Log.E(us.ToString());
                    Zone.Add(us, item);
                }
                catch(Exception ex)
                {
                    Log.E(ex.Message);
                }
            }

            Initialized = true;
        }

        public static string getZoneString(ushort key)
        {
            if (!Initialized)
                Initializer();

            if (Zone.ContainsKey(key))
                return Zone[key].GetValue(0).ToString();
            else
                return "알수없는 지역";
        }

        public static string getZoneString(string hexa)
        {
            return getZoneString(hexa.getHexValue());
        }

        public static string getZoneDutyname(ushort key)
        {
            if (!Initialized)
                Initializer();

            if (Zone.ContainsKey(key))
                return Zone[key].GetValue(1).ToString();
            else
                return "알수없는 임무";
        }

        public static string getZoneDutyname(string hexa)
        {
            return getZoneDutyname(hexa.getHexValue());
        }

        public static bool getZoneIsDuty(ushort key)
        {
            if (!Initialized)
                Initializer();

            if (Zone.ContainsKey(key))
                if (int.Parse(Zone[key].GetValue(2).ToString()) == 1)
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
    }
}
