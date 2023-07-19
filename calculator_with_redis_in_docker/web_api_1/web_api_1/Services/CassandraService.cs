using Cassandra;
using Microsoft.Extensions.Options;
using System.Reflection;
using web_api_1.Contexts;
using web_api_1.Models;
using web_api_1.Services.Interfaces;
using web_api_1.Settings;

namespace web_api_1.Services
{
    public class CassandraService : ICassandraService
    {
        private readonly CassandraDbContext _cassandraDbContext;
        private readonly IOptions<CassandraDBConfigurationSetting> _MongoDBConfigurationSetting;

        public CassandraService(IOptions<CassandraDBConfigurationSetting> mongoDBConfigurationSetting)
        {
            _cassandraDbContext = new CassandraDbContext(mongoDBConfigurationSetting.Value.ConnectionString);

            CreateTables();
        }

        private async Task CreateTables()
        {
            await _cassandraDbContext.Session.ExecuteAsync(
                new SimpleStatement("CREATE TABLE IF NOT EXISTS usession.content (content_id text PRIMARY KEY, content_value text)")
            );
        }

        public async Task<RowSet> GetAsync(string query)
        {
            SimpleStatement statement = new SimpleStatement(query);
            RowSet _data = await _cassandraDbContext.Session.ExecuteAsync(statement);
            return _data;
        }

        public async Task<bool> ExecuteAsync(string query)
        {
            SimpleStatement statement = new SimpleStatement(query);
            await _cassandraDbContext.Session.ExecuteAsync(statement);
            return true;
        }
    }
}
