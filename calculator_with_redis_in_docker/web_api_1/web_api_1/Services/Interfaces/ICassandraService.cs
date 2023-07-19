using Cassandra;
using web_api_1.Models;

namespace web_api_1.Services.Interfaces
{
    public interface ICassandraService
    {
        public Task<RowSet> GetAsync(string query);
        public Task<bool> ExecuteAsync(string query);
    }
}


