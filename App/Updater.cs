using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
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
                var temppath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Global.UPDATE_TEMP_FILEPATH);
                File.Delete(temppath);

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

                    if (decimal.Parse(Global.VERSION.Substring(1)) >= decimal.Parse(latest.Substring(1)))
                    {
                        Log.S("최신 버전을 이용중입니다");
                    }
                    else
                    {
                        Log.S("새로운 업데이트가 존재합니다");

                        if (Settings.StartupAutoUpdate)
                        {
                            string url = null;
                            foreach (var asset in api.assets)
                            {
                                if (asset.name == string.Format("DFAssist.{0}.zip", latest))
                                {
                                    url = asset.browser_download_url;
                                }
                            }

                            if (url == null)
                            {
                                Log.E("업데이트 파일을 찾을 수 없습니다");
                                return;
                            }

                            mainForm.Invoke((MethodInvoker)delegate
                            {
                                mainForm.Hide();
                                mainForm.overlayForm.Hide();
                                mainForm.WindowState = FormWindowState.Minimized;
                            });

                            Task.Factory.StartNew(() =>
                            {
                                var updaterForm = new UpdaterForm();
                                updaterForm.SetVersion(latest);
                                updaterForm.ShowDialog();
                            });

                            var stream = GetDownloadStreamByUrl(url);

                            var exepath = Process.GetCurrentProcess().MainModule.FileName;
                            File.Move(exepath, temppath);

                            using (ZipStorer zip = ZipStorer.Open(stream, FileAccess.Read))
                            {
                                List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();
                                foreach (ZipStorer.ZipFileEntry entry in dir)
                                {
                                    zip.ExtractFile(entry, Path.Combine(Path.GetDirectoryName(exepath), entry.FilenameInZip));
                                }
                            }

                            Process.Start(new ProcessStartInfo(exepath));
                            Application.Exit();
                        }
                        else
                        {
                            mainForm.Invoke((MethodInvoker)delegate
                            {
                                mainForm.linkLabel_NewUpdate.Visible = true;
                                mainForm.Show();
                            });
                        }
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
            try
            {
                var url = string.Format("https://api.github.com/repos/{0}/releases/latest", Global.GITHUB_REPO);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "DFA";
                request.Timeout = 10000;

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var encoding = Encoding.GetEncoding(response.CharacterSet);

                    using (var responseStream = response.GetResponseStream())
                    using (var reader = new StreamReader(responseStream, encoding))
                        return reader.ReadToEnd();
                }
            }
            catch (Exception ex) {
                Log.Ex(ex, "업데이트 체크중 에러 발생");
            }

            return null;
        }

        static Stream GetDownloadStreamByUrl(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "DFA";
                request.Timeout = 20000;

                var response = (HttpWebResponse)request.GetResponse();
                var stream = new MemoryStream();

                response.GetResponseStream().CopyTo(stream);
                return stream;
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "업데이트 데이터 받는 중 에러 발생");
            }

            return null;
        }
    }
}
