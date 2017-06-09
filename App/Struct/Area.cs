using System;
using System.Collections.Generic;
using System.Xml;

namespace App
{
    public class Area
    {
        public int ZoneId { get; set; } = 0;

        public string Name { get; private set; } = string.Empty;
        public string Duty { get; private set; } = string.Empty;

        public Dictionary<int, string> FATEList { get; private set; } = new Dictionary<int, string>();

        public byte Tank { get; private set; } = 0;
        public byte Healer { get; private set; } = 0;
        public byte DPS { get; private set; } = 0;
        public bool PvP { get; private set; } = false;

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
        public Instance Instance
        {
            get
            {
                if (isDuty)
                {
                    return new Instance(Duty, Tank, Healer, DPS, PvP);
                }
                else
                {
                    return new Instance(string.Format("알 수 없는 임무 ({0})", ZoneId), 0, 0, 0);
                }
            }
        }

        public FATE GetFATE(int key)
        {
            if (FATEList.ContainsKey(key))
                return new FATE(key, FATEList[key]);
            else
                return new FATE(0, string.Format("알 수 없는 돌발임무 ({0})", key));
        }

        public Area()
        {
            // Null Area
        }

        public Area(XmlNode XE)
        {
            try
            {
                Duty = XE.FindAttribute("Duty");
                Name = XE.FindAttribute("Text");

                if (Name == string.Empty && isDuty)
                    Name = Duty;

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
                        if (XE.Attributes != null && XE.Attributes["PVP"] != null)
                            PvP = XmlConvert.ToBoolean(XE.FindAttribute("PVP"));
                        else
                            PvP = false;
                    }
                    catch
                    {
                        Tank = 0;
                        Healer = 0;
                        DPS = 0;
                        PvP = false;
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
