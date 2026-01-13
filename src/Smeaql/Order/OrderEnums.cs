namespace Smeaql.Order;

internal enum OrderDirection
{
    Asc,
    Desc,
}

internal static class OrderEnumExtensions
{
    public static string ToSql(this OrderDirection direction) =>
        direction switch
        {
            OrderDirection.Asc => "ASC",
            OrderDirection.Desc => "DESC",
            _ => throw new Exception(),
        };
}
