using SharpRaven;
using SharpRaven.Data;
using System;
using System.Windows.Forms;

namespace App
{
    static class Program
    {
#if !DEBUG
        internal static RavenClient ravenClient;
#endif

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if !DEBUG
            ravenClient = new RavenClient(Global.RAVEN_DSN);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm form = new MainForm();
            Application.Run(form);
        }

#if !DEBUG
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                MessageBox.Show(string.Format("알 수 없는 오류가 발생해 프로그램을 종료합니다.\n\n에러: {0}", exception.Message), "에러 발생", MessageBoxButtons.OK, MessageBoxIcon.Error);

                var @event = new SentryEvent(exception);
                @event.Level = ErrorLevel.Fatal;
                @event.Tags.Add("version", Global.VERSION);

                ravenClient.Capture(@event);
            }
            catch { }
        }
#endif
    }
}
