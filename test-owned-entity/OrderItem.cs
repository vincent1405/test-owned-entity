namespace test_owned_entity
{
    public record class OrderItem(Guid OrderId, int RowIdx, Guid ArticleId, int Quantity)
    {
    }
}