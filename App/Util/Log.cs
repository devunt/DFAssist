using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace App
{
    internal static class Log
    {
        private static readonly Regex EscapePattern = new Regex(@"\{(.+?)\}");
        internal static MainForm Form { private get; set; }

        private static void Write(Color color, object format, params object[] args)
        {
            var formatted = format ?? "(null)";
            try
            {
                formatted = string.Format(formatted.ToString(), args);
            }
            catch (FormatException) { }

            var datetime = DateTime.Now.ToString("HH:mm:ss");
            var message = $"[{datetime}] {formatted}{Environment.NewLine}";

            Form.Invoke(() => 
            {
                Form.richTextBox_Log.SelectionStart = Form.richTextBox_Log.TextLength;
                Form.richTextBox_Log.SelectionLength = 0;

                Form.richTextBox_Log.SelectionColor = color;
                Form.richTextBox_Log.AppendText(message);
                Form.richTextBox_Log.SelectionColor = Form.richTextBox_Log.ForeColor;
            });
        }

        internal static void S(string key, params object[] args)
        {
            Write(Color.Green, Localization.GetText(key, args));
        }

        internal static void I(string key, params object[] args)
        {
            Write(Color.Black, Localization.GetText(key, args));
        }

        internal static void E(string key, params object[] args)
        {
            Write(Color.Red, Localization.GetText(key, args));
        }

        internal static void Ex(Exception ex, string key, params object[] args)
        {
#if DEBUG
            throw ex;
#else
            var format = Localization.GetText(key);

            var message = ex.Message;

            message = Escape(message);
            E($"{format}: {message}", args);
#endif
        }

        internal static void D(object format, params object[] args)
        {
            if (Settings.debugLog)
            {
                Write(Color.Gray, format, args);
            }
        }

        internal static void B(byte[] buffer)
        {
            var sb = new StringBuilder();
            sb.AppendLine();

            for (var i = 0; i < buffer.Length; i++)
            {
                if (i != 0)
                {
                    if (i % 16 == 0)
                    {
                        sb.AppendLine();
                    }
                    else if (i % 8 == 0)
                    {
                        sb.Append(' ', 2);
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }

                sb.Append(buffer[i].ToString("X2"));
            }

            D(sb.ToString());
        }

        private static string Escape(string line)
        {
            return EscapePattern.Replace(line, "{{$1}}");
        }
    }
}
