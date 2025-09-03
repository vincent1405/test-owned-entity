namespace test_owned_entity
{
    public class Order
    {
        public Guid Id { get; private set; }
        private readonly List<OrderItem> items = [];
        public IReadOnlyCollection<OrderItem> Items => items;
    }
}
