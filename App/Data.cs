using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace App
{
    public static class Data
    {
        public static bool Initialized = false;
        public static decimal Version { get; set; } = 0;
        public static Dictionary<int, Area> Areas { get; set; }
        public static Dictionary<int, FATE> FATEs { get; set; }

        public static void Initializer()
        {
            Initializer(Properties.Resources.ZoneList);
        }

        public static void Initializer(string xml)
        {
            try
            {

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                var vxn = doc.SelectSingleNode("/Data/Version");
                var version = decimal.Parse(vxn.InnerText);

                if (version > Version)
                {
                    var areas = new Dictionary<int, Area>();
                    var fates = new Dictionary<int, FATE>();

                    foreach (XmlNode xn in doc.SelectNodes("/Data/Item"))
                    {
                        Area zone = new Area(xn);
                        if (!areas.ContainsKey(zone.ZoneId))
                        {
                            areas.Add(zone.ZoneId, zone);
                            foreach (KeyValuePair<int, string> fate in zone.FATEList)
                            {
                                fates.Add(fate.Key, new FATE(zone.ZoneId, fate.Value));
                            }
                        }
                    }

                    Areas = areas;
                    FATEs = fates;
                    Version = version;
                    Initialized = true;

                    Log.S("임무 데이터가 {0} 버전으로 갱신되었습니다.", Version);
                }
                else
                {
                    Log.S("최신 임무 데이터 (버전 {0})를 이용중입니다.", Version);
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "임무 데이터를 처리하던 중 문제 발생");
            }
        }

        public static string GetAreaName(int key)
        {
            if (Areas.ContainsKey(key))
            {
                return Areas[key].Name;
            }
            else
            {
                if (key != 0)
                {
                    var @event = new SentryEvent("Missing area code");
                    @event.Level = ErrorLevel.Warning;
                    @event.Tags["code"] = key.ToString();
                    Sentry.ReportAsync(@event);
                }
                return string.Format("알 수 없는 지역 ({0})", key);
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

            if (code != 0)
            {
                var @event = new SentryEvent("Missing instance code");
                @event.Level = ErrorLevel.Warning;
                @event.Tags["code"] = code.ToString();
                Sentry.ReportAsync(@event);
            }

            return new Instance(string.Format("알 수 없는 임무 ({0})", code), 0, 0, 0);
        }

        internal static FATE GetFATE(int code)
        {
            if (FATEs.ContainsKey(code))
            {
                return FATEs[code];
            }

            if (code != 0)
            {
                var @event = new SentryEvent("Missing FATE code");
                @event.Level = ErrorLevel.Warning;
                @event.Tags["code"] = code.ToString();
                Sentry.ReportAsync(@event);
            }

            return new FATE(0, string.Format("알 수 없는 돌발임무 ({0})", code));
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
