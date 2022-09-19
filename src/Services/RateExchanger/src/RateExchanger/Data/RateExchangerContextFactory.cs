using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RateExchanger.Data;

public class RateExchangerContextFactory : IDesignTimeDbContextFactory<RateExchangerContext>
{
    public RateExchangerContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RateExchangerContext>();
        optionsBuilder.UseSqlServer("Server=.;Database=rateexchanger;MultipleActiveResultSets=true;User Id=sa;Password=sample123!;");

        return new RateExchangerContext(optionsBuilder.Options);
    }
}