using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace App
{
    class Updater
    {
        internal static bool CheckNewVersion()
        {
            var resp = QueryGitHubReleases();
            if (resp == null)
            {
                Log.E("새 업데이트 정보를 받아오지 못했습니다");
                return false;
            }

            Regex pattern = new Regex("\"tag_name\":\"(v.+?)\",");
            Match m = pattern.Match(resp);
            if (!m.Success)
            {
                Log.E("새 업데이트 정보에 문제가 있습니다");
                return false;
            }

            string latest = m.Groups[1].Value;
            Log.I("현재 버전: {0}", Global.VERSION);
            Log.I("최신 버전: {0}", latest);

            if (Global.VERSION == latest)
            {
                Log.S("최신 버전을 이용중입니다");
                return false;
            }
            else
            {
                Log.S("새로운 업데이트가 존재합니다");
                return true;
            }
        }

        static string QueryGitHubReleases()
        {
            var url = string.Format("https://api.github.com/repos/{0}/releases/latest", Global.GITHUB_REPO);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "DFA";
            request.Timeout = 1000;

            string resp = null;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var encoding = Encoding.GetEncoding(response.CharacterSet);

                    using (var responseStream = response.GetResponseStream())
                    using (var reader = new StreamReader(responseStream, encoding))
                        resp = reader.ReadToEnd();
                }
            }
            catch (WebException) { }

            return resp;
        }
    }
}
