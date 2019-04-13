using CustomerInquiry.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerInquiry.DbContext
{
    public class SqlDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }
    }
}
