using BuildingBlocks.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RateExchanger.Models;

public class RateExchange : BaseEntity
{
    public string FromCurrency { get; set; }

    public string ToCurrency { get; set; }

    public string Rate { get; set; }
}

public class RateExchangeConfiguration : IEntityTypeConfiguration<RateExchange>
{
    public void Configure(EntityTypeBuilder<RateExchange> builder)
    {
        builder.ToTable("RateExchange");

        builder.HasKey(r => r.Id);

        builder
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(r => r.FromCurrency)
            .IsRequired();

        builder
            .Property(r => r.ToCurrency)
            .IsRequired();

        builder
            .Property(r => r.Rate)
            .IsRequired();
    }
}