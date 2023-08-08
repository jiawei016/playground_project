using api_voucher_system.Context.Tables;
using Microsoft.EntityFrameworkCore;
using System;

namespace api_voucher_system.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<tblVoucher> tblVoucher { get; set; }
        public DbSet<tblProducerPlayer> tblProducerPlayer { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
