using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesUtility
{
    public static class Configuration
    {
        private static readonly Dictionary<string, string> AppSettingCache = new Dictionary<string, string>();

        private static readonly Dictionary<string, string> FiltersCache = new Dictionary<string, string>();

        static Configuration()
        {
            // Store application settings
            string[] keys = ConfigurationManager.AppSettings.AllKeys;

            foreach (string key in keys)
                if (key.Contains("filter"))
                    FiltersCache.Add(key, ConfigurationManager.AppSettings[key]);
                else
                    AppSettingCache.Add(key, ConfigurationManager.AppSettings[key]);
        }
        
        public static string GetAppSetting(string key)
        {
            string result;
            
            return AppSettingCache.TryGetValue(key, out result) ? result : null;
        }

        public static string GetFilters(string key)
        {
            string result;

            return FiltersCache.TryGetValue(key, out result) ? result : null;
        }
    }
}
