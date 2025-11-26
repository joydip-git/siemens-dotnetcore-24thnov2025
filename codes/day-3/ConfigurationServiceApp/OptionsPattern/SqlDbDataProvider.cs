using Microsoft.Extensions.Options;

namespace OptionsPattern
{
    public class SqlDbDataProvider : IDataProvider
    {
        private readonly string _dbSettings;
        public SqlDbDataProvider(IOptions<DbSetting> dbSettingOptions)
        {
            _dbSettings = string.IsNullOrEmpty(dbSettingOptions.Value.SqlDbPath) ? "default path" : dbSettingOptions.Value.SqlDbPath;
        }
        public string GetData()
        {
            //use _dbSettings connect to db and fetch data
            return "db data from " + _dbSettings;
        }
    }
}
