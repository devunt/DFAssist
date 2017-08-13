using System;
using System.Collections.Generic;
using App.Properties;
using Newtonsoft.Json;
using SharpRaven.Data;

namespace App
{
    public static class Data
    {
        public static bool Initialized { get; private set; } = false;
        public static decimal Version { get; private set; } = 0;

        public static Dictionary<int, Area> Areas { get; private set; } = new Dictionary<int, Area>();
        public static Dictionary<int, Instance> Instances { get; private set; } = new Dictionary<int, Instance>();
        public static Dictionary<int, Roulette> Roulettes { get; private set; } = new Dictionary<int, Roulette>();
        public static Dictionary<int, Roulette> RoulettesGS { get; private set; } = new Dictionary<int, Roulette>();
        public static Dictionary<int, FATE> FATEs { get; private set; } = new Dictionary<int, FATE>();

        public static void Initializer()
        {
            if (Settings.lang == 0)
                Initializer(Resources.GameData_Korean);
            else if (Settings.lang == 1)
                Initializer(Resources.GameData_English);
        }

        public static void Initializer(string json)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<GameData>(json);

                var version = data.Version;

                if (version > Version)
                {
                    var fates = new Dictionary<int, FATE>();
                    foreach (var area in data.Areas)
                    {
                        foreach (var fate in area.Value.FATEs)
                        {
                            fates.Add(fate.Key, fate.Value);
                        }
                    }

                    Areas = data.Areas;
                    Instances = data.Instances;
                    Roulettes = data.Roulettes;
                    RoulettesGS = data.RoulettesGS;
                    FATEs = fates;
                    Version = version;

                    if (Initialized)
                    {
                        Log.S("임무 데이터가 {0} 버전으로 갱신되었습니다.", Version);
                    }

                    Initialized = true;
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

        internal static Instance GetInstance(int code)
        {
            if (Instances.TryGetValue(code, out var instance))
            {
                return instance;
            }

            if (code != 0)
            {
                var @event = new SentryEvent("Missing instance code");
                @event.Level = ErrorLevel.Warning;
                @event.Tags["code"] = code.ToString();
                Sentry.ReportAsync(@event);
            }

            return new Instance { Name = $"알 수 없는 임무 ({code})" };
        }

        internal static Roulette GetRoulette(int code, bool isGlobal = false)
        {
            if (Roulettes.TryGetValue(code, out var roulette) && isGlobal == false)
            {
                return roulette;
            }
            else if (RoulettesGS.TryGetValue(code, out var rouletteGS) && isGlobal == true)
            {
                return rouletteGS;
            }

            if (code != 0)
            {
                var @event = new SentryEvent("Missing Roulette code");
                @event.Level = ErrorLevel.Warning;
                @event.Tags["code"] = code.ToString();
                Sentry.ReportAsync(@event);
            }

            return new Roulette { Name = $"알 수 없는 무작위 임무 ({code})" };
        }

        internal static Area GetArea(int code)
        {
            if (Areas.TryGetValue(code, out var area))
            {
                return area;
            }

            if (code != 0)
            {
                var @event = new SentryEvent("Missing area code");
                @event.Level = ErrorLevel.Warning;
                @event.Tags["code"] = code.ToString();
                Sentry.ReportAsync(@event);
            }

            return new Area { Name = $"알 수 없는 지역 ({code})" };
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

            return new FATE { Name = $"알 수 없는 돌발 ({code})" };
        }
    }
}
