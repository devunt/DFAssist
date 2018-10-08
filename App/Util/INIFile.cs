using System.Runtime.InteropServices;
using System.Text;

namespace App
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    public class INIFile
    {
        public string path;

        private static class NativeMethods
        {
            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            public static extern uint WritePrivateProfileString(string section,
                string key, string val, string filePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            public static extern int GetPrivateProfileString(string section,
                     string key, string def, StringBuilder retVal,
                int size, string filePath);
        }

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public INIFile(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void WriteValue(string Section, string Key, string Value)
        {
            NativeMethods.WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string ReadValue(string Section, string Key)
        {
            var temp = new StringBuilder(8192);
            var i = NativeMethods.GetPrivateProfileString(Section, Key, "", temp,
                                            8192, this.path);
            if (i == 0)
            {
                return null;
            }

            return temp.ToString();
        }
    }
}

// code from http://www.codeproject.com/Articles/1966/An-INI-file-handling-class-using-C