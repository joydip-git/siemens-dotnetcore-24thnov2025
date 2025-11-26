using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsPattern
{
    public class OracleDbDataProvider : IDataProvider
    {
        private readonly string _constr;
        private readonly ILogger<OracleDbDataProvider> _logger;
        private readonly IConfiguration _configuration;

        public OracleDbDataProvider(IOptions<DbSetting> settings, ILogger<OracleDbDataProvider> logger, IConfiguration configuration)
        {
            DbSetting setting = settings.Value;
            _constr = setting.OrclDbPath ?? "default path";
            _logger = logger;
            _configuration = configuration;
        }
        public string GetData()
        {
            _logger.LogInformation(_constr);
            _logger.LogInformation(_configuration["key1"]);
            return "data from oracle database at " + _constr;
        }
    }
}
