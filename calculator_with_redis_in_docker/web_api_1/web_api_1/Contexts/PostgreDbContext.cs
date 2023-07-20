using Microsoft.EntityFrameworkCore;
using web_api_1.Models;

namespace web_api_1.Contexts
{
    public class PostgreDbContext : DbContext
    {
        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
        {

        }

        public DbSet<ItemModel> ItemModel { get; set; }
    }
}
