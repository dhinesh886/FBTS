using System.Configuration;

namespace FBTS.Library.Common
{
    public static class ConfigurationReader<T>
    {
        public static T GetAppConfigurationValue(string argsConfigurationKey, T defaultValue)
        {
            T configValue = defaultValue;
            try
            {
                if (ConfigurationManager.AppSettings[argsConfigurationKey] != null)
                {
                    object obj = ConfigurationManager.AppSettings[argsConfigurationKey];
                    configValue = (T) obj;
                } 
            }
            catch
            {
                throw;
            }

            return configValue;
        }
        public static T GetAppConfigurationValue(string argsConfigurationKey)
        {
            T configValue = default(T);
            try
            {
                if (ConfigurationManager.AppSettings[argsConfigurationKey] != null)
                {
                    object obj = ConfigurationManager.AppSettings[argsConfigurationKey];
                    configValue = (T)obj;
                }
            }
            catch
            {
                throw;
            }

            return configValue;
        }
    }
}