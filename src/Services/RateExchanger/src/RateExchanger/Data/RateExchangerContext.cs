using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using RateExchanger.Data.Models;

namespace RateExchanger.Data;

public class RateExchangerContext : GenericDbContext
{
    public DbSet<RateExchange> RateExchanges { get; set; }

    public RateExchangerContext() : base()
    {
    }

    public RateExchangerContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new RateExchangeConfiguration());
        base.OnModelCreating(builder);
    }
}