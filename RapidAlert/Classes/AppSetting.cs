using System.Runtime.InteropServices;
using System.Text;

namespace RapidAlert.Classes
{
    public class INIFile 
    {
        private string _filePath;
         
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
        string key,
        string val,
        string filePath);
 
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
        string key,
        string def,
        StringBuilder retVal,
        int size,
        string filePath);
        public INIFile(string filePath)
        {
            this._filePath = filePath;
        }
 
        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value , this._filePath);
        }
 
        public string Read(string section, string key)
        {
            var stringBuilder = new StringBuilder(255);
            var i = GetPrivateProfileString(section, key, "", stringBuilder, 255, this._filePath);
            return stringBuilder.ToString();
        }
         
        public string FilePath
        {
            get { return this._filePath; }
            set { this._filePath = value; }
        }
    }
}
