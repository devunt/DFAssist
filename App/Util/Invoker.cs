using System;
using System.Windows.Forms;

namespace App
{
    static class Invoker
    {
        internal static void Invoke(this Form form, Action task)
        {
            form.Invoke((MethodInvoker) delegate { task(); });
        }
    }
}
