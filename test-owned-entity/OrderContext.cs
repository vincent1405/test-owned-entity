using Microsoft.EntityFrameworkCore;

namespace test_owned_entity
{
    public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; private set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
