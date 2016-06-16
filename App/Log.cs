using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace App
{
    class Log
    {
        internal static MainForm Form { get; set; }

        private static void Write(Color color, string format, params object[] args)
        {
            var datetime = DateTime.Now.ToString("HH:mm:ss");
            var formatted = string.Format(format, args);
            var message = string.Format("[{0}] {1}{2}", datetime, formatted, Environment.NewLine);

            Form.Invoke((MethodInvoker)delegate {
                Form.richTextBox_Log.SelectionStart = Form.richTextBox_Log.TextLength;
                Form.richTextBox_Log.SelectionLength = 0;

                Form.richTextBox_Log.SelectionColor = color;
                Form.richTextBox_Log.AppendText(message);
                Form.richTextBox_Log.SelectionColor = Form.richTextBox_Log.ForeColor;
            });
        }

        internal static void S(string format, params object[] args)
        {
            Write(Color.Green, format, args);
        }

        internal static void I(string format, params object[] args)
        {
            Write(Color.Black, format, args);
        }

        internal static void E(string format, params object[] args)
        {
            Write(Color.Red, format, args);
        }

        internal static void D(string format, params object[] args)
        {
            Write(Color.Gray, format, args);
        }
    }
}
