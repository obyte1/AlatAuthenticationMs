using AlatAuth.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace AlatAuth.Data.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<LGA> LGAs { get; set; }
        public DbSet<OTPEntity> OTPs { get; set; }
    }
}
