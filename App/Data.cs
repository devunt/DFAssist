using System;
using System.Collections.Generic;
using App.Properties;
using Newtonsoft.Json;

namespace App
{
    public static class Data
    {
        public static bool Initialized { get; private set; } = false;
        public static decimal Version { get; private set; } = 0;
        public static string Language { get; private set; } = "";

        public static Dictionary<int, Area> Areas { get; private set; } = new Dictionary<int, Area>();
        public static Dictionary<int, Instance> Instances { get; private set; } = new Dictionary<int, Instance>();
        public static Dictionary<int, Roulette> Roulettes { get; private set; } = new Dictionary<int, Roulette>();
        public static Dictionary<int, FATE> FATEs { get; private set; } = new Dictionary<int, FATE>();

        internal static void Initialize(string language, MainForm mainForm)
        {
            string json;

            switch (language)
            {
                case "ko-kr":
                    json = Resources.Data_KO_KR;
                    break;

                case "en-us":
                    json = Resources.Data_EN_US;
                    break;

                case "fr-fr":
                    json = Resources.Data_FR_FR;
                    break;

                case "de-de":
                    json = Resources.Data_DE_DE;
                    break;

                case "ja-jp":
                    json = Resources.Data_JA_JP;
                    break;

                default:
                    return;
            }

            Fill(json, mainForm);
        }

        public static void Fill(string json, MainForm mainForm)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<GameData>(json);

                var version = data.Version;

                if (version > Version || Language != Settings.Language)
                {
                    var fates = new Dictionary<int, FATE>();
                    foreach (var area in data.Areas)
                    {
                        foreach (var fate in area.Value.FATEs)
                        {
                            fate.Value.Area = area.Value;
                            fates.Add(fate.Key, fate.Value);
                        }
                    }

                    Areas = data.Areas;
                    Instances = data.Instances;
                    Roulettes = data.Roulettes;
                    FATEs = fates;
                    Version = version;
                    Language = Settings.Language;

                    if (Initialized)
                    {
                        mainForm.Invoke(mainForm.refresh_Fates);
                        Log.S("l-data-updated", Version);
                    }

                    Initialized = true;
                }
                else
                {
                    Log.S("l-data-is-latest", Version);
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "l-data-error");
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
                // "Missing instance code"
            }

            return new Instance { Name = Localization.GetText("unknown-instance", code) };
        }

        internal static Roulette GetRoulette(int code)
        {
            if (Roulettes.TryGetValue(code, out var roulette))
            {
                return roulette;
            }

            if (code != 0)
            {
                // "Missing Roulette code"
            }

            return new Roulette { Name = Localization.GetText("unknown-roulette", code) };
        }

        internal static Area GetArea(int code)
        {
            if (Areas.TryGetValue(code, out var area))
            {
                return area;
            }

            if (code != 0)
            {
                // "Missing area code"
            }

            return new Area { Name = Localization.GetText("unknown-area", code) };
        }

        internal static FATE GetFATE(int code)
        {
            if (FATEs.ContainsKey(code))
            {
                return FATEs[code];
            }

            if (code != 0)
            {
                // "Missing FATE code"
            }

            return new FATE { Name = Localization.GetText("unknown-fate", code) };
        }
    }
}
