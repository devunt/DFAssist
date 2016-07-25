using System;
using System.Collections.Generic;
using System.Xml;

namespace App
{
    public class ZoneItem
    {
        public int ZoneId { get; set; } = 0;

        public string Text { get; private set; } = string.Empty;
        public string Duty { get; private set; } = string.Empty;

        public Dictionary<int, string> FATEList { get; private set; } = new Dictionary<int, string>();

        public byte Tank { get; private set; } = 0;
        public byte Healer { get; private set; } = 0;
        public byte DPS { get; private set; } = 0;
        
        public ushort uZoneId
        {
            get
            {
                return (ushort)ZoneId;
            }
        }

        public bool isDuty
        {
            get
            {
                if (Duty != "")
                    return true;
                else
                    return false;
            }
        }

        public ZoneItem(XmlNode XE)
        {
            try
            {
                Duty = XE.FindAttribute("Duty");
                Text = XE.FindAttribute("Text");

                if (Text == string.Empty && isDuty)
                    Text = Duty;

                string ZoneIdVal = XE.FindAttribute("ZoneId").ToUpper();

                ZoneId = int.Parse(ZoneIdVal, System.Globalization.NumberStyles.HexNumber);

                if (isDuty)
                {
                    XmlNode CXE = XE.FirstChild;
                    try
                    {
                        Tank = byte.Parse(CXE.FindAttribute("Tank"));
                        Healer = byte.Parse(CXE.FindAttribute("Healer"));
                        DPS = byte.Parse(CXE.FindAttribute("DPS"));
                    }
                    catch
                    {
                        Tank = 0;
                        Healer = 0;
                        DPS = 0;
                    }
                }
                else
                {
                    foreach(XmlNode xn in XE.ChildNodes)
                    {
                        if (xn.Name == "FATE")
                        {
                            try
                            {
                                int key = int.Parse(xn.FindAttribute("Id"));
                                if (!FATEList.ContainsKey(key))
                                    FATEList.Add(key, xn.FindAttribute("Name"));
                            }
                            catch(Exception ex)
                            {
                                Log.E("돌발임무 추가 오류 : {0}", ex.Message);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log.E("객체 생성 오류 : {0}", ex.GetBaseException().ToString());
            }
        }
    }
}
