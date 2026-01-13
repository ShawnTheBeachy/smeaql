namespace Smeaql.Order;

internal abstract class OrderClause : SqlClause
{
    public OrderDirection Direction { get; init; } = OrderDirection.Asc;
}
