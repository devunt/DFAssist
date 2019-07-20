using System.Windows.Forms;

namespace App
{
    internal static class LMessageBox
    {
        internal static DialogResult I(string bodyKey,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return Show(bodyKey, "ui-msgbox-title-info", buttons, MessageBoxIcon.Information, defaultButton);
        }

        internal static DialogResult W(string bodyKey,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return Show(bodyKey, "ui-msgbox-title-warning", buttons, MessageBoxIcon.Warning, defaultButton);
        }

        internal static DialogResult E(string bodyKey,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return Show(bodyKey, "ui-msgbox-title-error", buttons, MessageBoxIcon.Error, defaultButton);
        }

        internal static DialogResult Dialog(
            string body, string title,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(body, title, buttons, icon, defaultButton);
        }

        private static DialogResult Show(
            string bodyKey, string titleKey,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            var body = Localization.GetText(bodyKey);
            var title = Localization.GetText(titleKey);

            return MessageBox.Show(body, title, buttons, icon, defaultButton);
        }
    }
}
