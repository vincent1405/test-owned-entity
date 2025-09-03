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
            builder.Property(o => o.Id).HasColumnName($"{nameof(Order)}Id");
            builder.OwnsMany(builder => builder.Items, itemBuilder =>
            {
                itemBuilder.ToTable(nameof(OrderItem));
                itemBuilder.HasKey(item => new { item.OrderId, item.RowIdx }).HasName($"PK_{nameof(OrderItem)}");
                // Add this row to prevent the migration to declare the column as IDENTITY

                //itemBuilder.Property(item => item.RowIdx).ValueGeneratedNever();

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
