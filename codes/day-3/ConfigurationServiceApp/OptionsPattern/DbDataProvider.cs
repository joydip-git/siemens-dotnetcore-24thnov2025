using Microsoft.Extensions.Options;

namespace OptionsPattern
{
    public class DbDataProvider : IDataProvider
    {
        private readonly string _dbSettings;
        public DbDataProvider(IOptions<DbSetting> dbSettingOptions)
        {
            _dbSettings = string.IsNullOrEmpty(dbSettingOptions.Value.FilePath) ? "default path" : dbSettingOptions.Value.FilePath;
        }
        public string GetData()
        {
            //use _dbSettings connect to db and fetch data
            return "db data from " + _dbSettings;
        }
    }
}
