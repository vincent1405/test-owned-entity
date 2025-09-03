using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace test_owned_entity
{
    internal class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));
            builder.HasKey(o => o.Id).HasName($"PK_{nameof(Order)}");

            builder.OwnsMany(builder => builder.Items, itemBuilder =>
            {
                itemBuilder.ToTable(nameof(OrderItem));
                itemBuilder.HasKey(item => new { item.OrderId, item.RowIdx }).HasName($"PK_{nameof(OrderItem)}");
                itemBuilder.Property(oi => oi.ArticleId).IsRequired();
                itemBuilder.Property(oi => oi.Quantity).IsRequired();

                itemBuilder.WithOwner()
                .HasPrincipalKey(order => order.Id)
                .HasForeignKey(item => item.OrderId)
                .HasConstraintName($"FK_{nameof(OrderItem)}_{nameof(Order)}");
            }).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
