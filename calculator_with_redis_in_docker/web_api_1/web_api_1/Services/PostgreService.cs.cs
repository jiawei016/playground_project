using web_api_1.Contexts;
using web_api_1.Services.Interfaces;

namespace web_api_1.Services
{
    public class PostgreService : IPostgreService
    {
        private readonly PostgreDbContext _postgreDbContext;

        public PostgreService(PostgreDbContext postgreDbContext) {
            _postgreDbContext = postgreDbContext;
        }
    }
}
