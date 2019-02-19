using AFTestApp.Configs;

namespace AFTestApp.Data
{
    public class AfTestAppContextFactory : IAfTestAppContextFactory
    {
        private readonly IConfigurationProvider _configurationProvider;
        private const string DatabaseFileConfigName = "DatabaseFaleName";

        public AfTestAppContextFactory(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public AfTestAppContext CreateContext()
        {
            var databaseFaleName = _configurationProvider.GetConfig(DatabaseFileConfigName);
            AfTestAppContext.CreateDatabaseFile(databaseFaleName);
            var connection = AfTestAppContext.CreateConnection(databaseFaleName);
            var context = new AfTestAppContext(connection);
            return context;
        }
    }
}
