using Cassandra;
using ISession = Cassandra.ISession;

namespace web_api_1.Contexts
{
    public class CassandraDbContext
    {
        private readonly ISession _session;

        public CassandraDbContext(string connectionString)
        {
            var cluster = Cluster.Builder().AddContactPoints(connectionString.Split(';')[0].Split('=')[1]).Build();
            _session = cluster.Connect(connectionString.Split(';')[2].Split('=')[1]);
        }

        public ISession Session => _session;
    }
}
