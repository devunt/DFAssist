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

        public static string getXmlAttribute(this XmlAttributeCollection attr, string searchAttribute, bool stringtolower = true)
        {
            string findattr = searchAttribute;
            if (stringtolower)
                findattr = searchAttribute.ToLower();

            foreach(XmlAttribute attribute in attr)
            {
                if (attribute.Name == findattr)
                    return attribute.Value;
            }

            return null;
        }

        public static void Initializer()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Properties.Resources.ZoneList);

            foreach(XmlNode xn in doc.SelectNodes("/Data/Item"))
            {
                object[] item = new object[] { "", "", 0, null };
                if (xn.Attributes.getXmlAttribute("Text") != null)
                    item[0] = xn.Attributes.getXmlAttribute("Text");

                if (xn.Attributes.getXmlAttribute("Duty") != null)
                {
                    item[1] = xn.Attributes.getXmlAttribute("Duty");
                    item[2] = 1;
                    object[] dutyMembers;
                    XmlNode xnc = xn.SelectSingleNode("/Duty");
                    try
                    {
                        dutyMembers = new object[]
                        {
                            int.Parse(xnc.Attributes.getXmlAttribute("Tank")),
                            int.Parse(xnc.Attributes.getXmlAttribute("Healer")),
                            int.Parse(xnc.Attributes.getXmlAttribute("DPS")),
                        };
                    }
                    catch
                    {
                        dutyMembers = new object[] { 0, 0, 0 };
                    }
                    item[3] = dutyMembers;
                }

                try
                {
                    Zone.Add(ushort.Parse(xn.Attributes.getXmlAttribute("ZoneId")), item);
                }
                catch
                {

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

        public static string getZoneDutyname(ushort key)
        {
            if (!Initialized)
                Initializer();

            if (Zone.ContainsKey(key))
                return Zone[key].GetValue(1).ToString();
            else
                return "알수없는 임무";
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
    }
}
