﻿using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace App
{
    internal class Updater
    {
        internal static void CheckNewVersion(MainForm mainForm)
        {
            Task.Factory.StartNew(() =>
            {
                var tempdir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Global.APPNAME, Global.UPDATE_TEMP_DIRPATH);
                var batchpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Global.APPNAME, "update.bat");

                if (Directory.Exists(tempdir))
                {
                    Directory.Delete(tempdir, true);
                }
                Directory.CreateDirectory(tempdir);

                var resp = WebApi.Request($"https://api.github.com/repos/{Global.GITHUB_REPO}/releases/latest");
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

                        string url = null;
                        foreach (var asset in api.assets)
                        {
                            if (asset.name == string.Format("DFAssist.{0}.zip", latest))
                            {
                                url = asset.browser_download_url;
                                break;
                            }
                        }

                        if (url == null)
                        {
                            Log.E("업데이트 파일을 찾을 수 없습니다");
                            return;
                        }

                        mainForm.Invoke(() =>
                        {
                            mainForm.Hide();
                            mainForm.overlayForm.Hide();
                        });

                        Task.Factory.StartNew(() =>
                        {
                            var updaterForm = new UpdaterForm();
                            updaterForm.SetVersion(latest);
                            updaterForm.ShowDialog();
                        });

                        Sentry.Report("Update started");

                        var stream = GetDownloadStreamByUrl(url);
                        using (var zip = ZipStorer.Open(stream, FileAccess.Read))
                        {
                            var dir = zip.ReadCentralDir();
                            foreach (var entry in dir)
                            {
                                if (entry.FilenameInZip == "README.txt")
                                {
                                    continue;
                                }
                                zip.ExtractFile(entry, Path.Combine(tempdir, entry.FilenameInZip));
                            }
                        }

                        var exepath = Process.GetCurrentProcess().MainModule.FileName;
                        var currentdir = Path.GetDirectoryName(exepath);

                        File.WriteAllText(batchpath,
                            "@echo off\r\n" +
                            "title DFAssist Updater\r\n" +
                            "echo Updating DFAssist...\r\n" +
                            "ping 127.0.0.1 -n 3 > nul\r\n" +
                            $"move /y \"{tempdir}\\*\" \"{currentdir}\" > nul\r\n" +
                            $"\"{exepath}\"\r\n" + 
                            "echo Running DFAssist...\r\n",
                        Encoding.Default);

                        var si = new ProcessStartInfo();
                        si.FileName = batchpath;
                        si.CreateNoWindow = true;
                        si.UseShellExecute = false;
                        si.WindowStyle = ProcessWindowStyle.Hidden;

                        Process.Start(si);
                        Settings.Updated = true;
                        Settings.Save();
                        Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    Log.Ex(ex, "업데이트 데이터 처리중 에러 발생");
                }

                try
                {
                    var json = WebApi.Request($"https://raw.githubusercontent.com/{Global.GITHUB_REPO}/master/App/Resources/GameData/ko.json");
                    Data.Initializer(json);
                }
                catch (Exception ex)
                {
                    Log.Ex(ex, "임무 데이터 업데이트중 에러 발생");
                }
            });
        }

        private static Stream GetDownloadStreamByUrl(string url)
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
