using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    class Updater
    {
        internal static void CheckNewVersion(MainForm mainForm)
        {
            Task.Factory.StartNew(() =>
            {
                var resp = QueryGitHubReleases();
                if (resp == null)
                {
                    Log.E("새 업데이트 정보를 받아오지 못했습니다");
                    return;
                }

                try {
                    var api = JsonConvert.DeserializeObject<dynamic>(resp);

                    var latest = api.tag_name.ToObject<string>();
                    Log.I("현재 버전: {0}", Global.VERSION);
                    Log.I("최신 버전: {0}", latest);

                    if (Global.VERSION == latest)
                    {
                        Log.S("최신 버전을 이용중입니다");
                    }
                    else
                    {
                        Log.S("새로운 업데이트가 존재합니다");

                        mainForm.Invoke((MethodInvoker)delegate
                        {
                            mainForm.linkLabel_NewUpdate.Visible = true;
                            mainForm.Show();
                        });
                    }
                }
                catch (Exception ex)
                {
                    Log.Ex(ex, "업데이트 데이터 처리중 에러 발생");
                }
            });
        }

        static string QueryGitHubReleases()
        {
            var url = string.Format("https://api.github.com/repos/{0}/releases/latest", Global.GITHUB_REPO);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "DFA";
            request.Timeout = 10000;

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
            catch (Exception ex) {
                Log.Ex(ex, "업데이트 체크중 에러 발생");
            }

            return resp;
        }
    }
}
