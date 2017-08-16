using System.Windows.Forms;

namespace App
{
    public class LocalizableLabel : Label
    {
        public void SetLocalizedText(string key, params object[] args)
        {
            Text = Localization.GetText(key, args);
        }
    }
}
