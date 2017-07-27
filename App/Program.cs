using System;
using System.Windows.Forms;

namespace App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
#if !DEBUG
            Sentry.Initialise();
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new MainForm();
            Application.Run(form);
        }
    }
}
