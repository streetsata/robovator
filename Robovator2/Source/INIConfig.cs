using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace HealthCheck
{
    public class INIConfig
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnString, int nSize, string lpFilename);

        [DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileStringW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFilename);

        private volatile Dictionary<String, Dictionary<String, String>> arrConfig = new Dictionary<string, Dictionary<string, string>>();
        private String iniFilePath = null;

        public INIConfig(String filePath)
        {
            if (String.IsNullOrEmpty(filePath))
                throw new FileNotFoundException("file name is undefined");
            iniFilePath = filePath;
            if (File.Exists(filePath))
                try
                {
                    foreach (String section in GetSections())
                    {
                        arrConfig.Add(section, new Dictionary<string, string>());
                        foreach (String key in GetKeys(section))
                            arrConfig[section].Add(key, GetValue(section, key, ""));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
        }

        public List<String> getKeys(String section)
        {
            List<String> RetVal = new List<string>();
            if (arrConfig.ContainsKey(section))
                foreach (KeyValuePair<String, String> keyValue in arrConfig[section])
                    RetVal.Add(keyValue.Key);
            return RetVal;
        }

        public List<String> getValues(String section)
        {
            List<String> RetVal = new List<string>();
            if (arrConfig.ContainsKey(section))
                foreach (KeyValuePair<String, String> keyValue in arrConfig[section])
                    RetVal.Add(keyValue.Value);
            return RetVal;
        }

        public bool containsSection(String section)
        {
            bool retVal = false;
            if (arrConfig.ContainsKey(section))
                retVal = true;

            return retVal;
        }

        public bool containsKey(String key)
        {
            bool retVal = false;
            foreach (String section in GetSections())
                if (this.arrConfig[section]["color_name"] == key)
                {
                    retVal = true;
                    break;
                }

            return retVal;
        }

        public Dictionary<String, String> this[String sectionName]
        {
            get
            {
                Dictionary<String, String> RetVal = new Dictionary<string, string>();
                if (arrConfig.ContainsKey(sectionName))
                    RetVal = arrConfig[sectionName];
                return RetVal;
            }
            set
            {
                if (arrConfig.ContainsKey(sectionName))
                {
                    arrConfig.Remove(sectionName);
                    arrConfig.Add(sectionName, value);
                    SaveIniFile();
                }
            }
        }

        public String this[String section, String key, String defaultValue = ""]
        {
            get
            {
                String RetVal = defaultValue;
                if (arrConfig.ContainsKey(section))
                    if (arrConfig[section].ContainsKey(key))
                        RetVal = arrConfig[section][key];
                return RetVal;
            }
            set
            {
                if (!arrConfig.ContainsKey(section))
                    arrConfig.Add(section, new Dictionary<string, string>());
                if (arrConfig.ContainsKey(key))
                    arrConfig.Remove(key);
                arrConfig[section].Add(key, value);
                this.SaveIniFile();
            }
        }

        private void SaveIniFile()
        {
            try
            {
                foreach (KeyValuePair<String, Dictionary<String, String>> section in arrConfig)
                    foreach (KeyValuePair<String, String> keyValue in section.Value)
                        this.WriteValue(section.Key, keyValue.Key, keyValue.Value);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        public List<string> GetSections()
        {
            string returnString = new string(' ', 65536);
            GetPrivateProfileString(null, null, null, returnString, 65536, this.iniFilePath);
            List<string> result = new List<string>(returnString.Split('\0'));
            result.RemoveRange(result.Count - 2, 2);
            return result;
        }

        private List<string> GetKeys(string section)
        {
            string returnString = new string(' ', 32768);
            GetPrivateProfileString(section, null, null, returnString, 32768, this.iniFilePath);
            List<string> result = new List<string>(returnString.Split('\0'));
            result.RemoveRange(result.Count - 2, 2);
            return result;
        }

        private string GetValue(string section, string key, string defaultValue)
        {
            string returnString = new string(' ', 1024);
            GetPrivateProfileString(section, key, defaultValue, returnString, 1024, this.iniFilePath);
            return returnString.Split('\0')[0];
        }

        private void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.iniFilePath);
        }
    }
}
