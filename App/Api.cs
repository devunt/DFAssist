using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App
{
    static class WebApi
    {
        internal static void Tweet(string format, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                var message = string.Format(format, args);
                var url = string.Format("{0}/tweet?u={1}&m={2}&h={3}", Global.API_ENDPOINT,
                    Settings.TwitterAccount, HttpUtility.UrlEncode(message), GetMD5Hash(message));

                var resp = Request(url);
                if (resp == null)
                {
                    Log.E("트윗 발송중 에러 발생");
                }
                else if (resp == "1")
                {
                    Log.E("트윗 발송 실패");
                }
                else if (resp == "0")
                {
                    Log.S("트윗을 발송했습니다");
                }
            });
        }

        internal static string Request(string urlfmt, params object[] args)
        {
            try
            {
                var url = string.Format(urlfmt, args);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "DFA";
                request.Timeout = 10000;
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var encoding = Encoding.GetEncoding(response.CharacterSet);

                    using (var responseStream = response.GetResponseStream())
                    using (var reader = new StreamReader(responseStream, encoding))
                        return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Log.Ex(ex, "웹 요청중 에러 발생");
            }

            return null;
        }

        private static string GetMD5Hash(string text)
        {
            if ((text == null) || (text.Length == 0))
            {
                return null;
            }

            byte[] textToHash = Encoding.UTF8.GetBytes(text);
            byte[] result;
            using (MD5 md5 = new MD5CryptoServiceProvider())
                result = md5.ComputeHash(textToHash);

            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }
}
