using System.Configuration;
using System.Linq;

namespace AFTestApp.Configs
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string GetConfig(string name)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(name))
            {
                return string.Empty;
            }

            var configvalue = ConfigurationManager.AppSettings[name];
            return configvalue;
        }
    }
}
