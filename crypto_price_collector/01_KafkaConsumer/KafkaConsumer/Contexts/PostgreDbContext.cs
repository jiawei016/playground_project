using KafkaConsumer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Contexts
{
    public class PostgreDbContext : DbContext
    {
        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
        {

        }

        public DbSet<BitcoinPriceModel> BitcoinPriceModel { get; set; }
    }
}
