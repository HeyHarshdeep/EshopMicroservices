using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;


namespace Ordering.Infrastructure.Data.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasConversion(customerId => customerId.Value,
            dbId => CustomerId.Of(dbId));

        builder.Property(t => t.Name).HasMaxLength(100).IsRequired();

        builder.Property(t => t.Email).HasMaxLength(255);

        builder.Property(t => t.Email).IsUnicode();
    }
}
