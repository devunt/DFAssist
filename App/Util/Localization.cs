using System.Collections.Generic;
using Newtonsoft.Json;
using App.Properties;

namespace App
{
    internal class Localization
    {
        private static Dictionary<string, string> LocalizedMap;

        internal static void Initialize(string language)
        {
            string json;

            switch (language)
            {
                case "ko-kr":
                    json = Resources.Localization_KO_KR;
                    break;

                case "en-us":
                    json = Resources.Localization_EN_US;
                    break;

                default:
                    return;
            }

            LocalizedMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        internal static string GetText(string key, params object[] args)
        {
            if (!LocalizedMap.TryGetValue(key, out var value))
            {
                return $"<{key}>";
            }

            return string.Format(value, args);
        }
    }
}
