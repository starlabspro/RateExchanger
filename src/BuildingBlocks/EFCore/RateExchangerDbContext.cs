using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.EFCore
{
    public class RateExchangerDbContext: DbContext
    {
        public DbSet<UserAttempts> userAttempts { get; set; }

        public RateExchangerDbContext (DbContextOptions<RateExchangerDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserAttempts>().HasKey(x => x.Id);
        }
    }
}
