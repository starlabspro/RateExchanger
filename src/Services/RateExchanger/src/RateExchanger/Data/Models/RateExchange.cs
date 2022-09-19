using BuildingBlocks.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RateExchanger.Data.Models;

public class RateExchange : BaseEntity
{
    public string BaseCurrency { get; set; }

    public string Rates { get; set; }
}

public class RateExchangeConfiguration : IEntityTypeConfiguration<RateExchange>
{
    public void Configure(EntityTypeBuilder<RateExchange> builder)
    {
        builder.ToTable("RateExchange");

        builder.HasKey(r => r.Id);

        builder
            .Property(r => r.BaseCurrency)
            .IsRequired();

        builder
            .Property(r => r.Rates)
            .HasColumnType("varchar(max)")
            .IsRequired();
    }
}