using System.IO;
using System.Reflection;


namespace Automation_RWassist.Configuration
{
    public static class ConfigSetting
    {
        private static readonly ConfigurationReader configReader = new ConfigurationReader(
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Tests\TestData", Constants.TestConfigFile));

        private static readonly ConfigurationReader envConfigReader = new ConfigurationReader(
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.EnvConfigFile));

        public static string ClientURL
        {
            get { return envConfigReader.GetConfiguration("ClientURL")?.ToString(); }
        }




      
    }
}
