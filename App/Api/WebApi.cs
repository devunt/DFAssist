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
    internal static class WebApi
    {
        internal static string Request(string urlfmt, params object[] args)
        {
            try
            {
                var url = string.Format(urlfmt, args);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "DFA";
                request.Timeout = 10000;
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.DefaultConnectionLimit = 9999;
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
                Log.Ex(ex, "l-web-request-failed");
            }

            return null;
        }

        private static string GetMD5Hash(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var textToHash = Encoding.UTF8.GetBytes(text);
            byte[] result;
            using (MD5 md5 = new MD5CryptoServiceProvider())
                result = md5.ComputeHash(textToHash);

            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }
}
