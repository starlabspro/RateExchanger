using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.EFCore;

public abstract class GenericDbContext : DbContext, IDbContext
{
    protected GenericDbContext()
    {
    }
    protected GenericDbContext(DbContextOptions options) : base(options)
    {
    }
}