namespace Smeaql.Where;

internal enum WhereFlag
{
    And,
    Or,
}

internal enum ExistsFlag
{
    Exists,
    NotExists,
}

internal static class WhereEnumExtensions
{
    public static string ToSql(this ExistsFlag existsFlag) =>
        existsFlag switch
        {
            ExistsFlag.Exists => "EXISTS",
            ExistsFlag.NotExists => "NOT EXISTS",
            _ => throw new Exception(),
        };

    public static string ToSql(this WhereFlag whereFlag) =>
        whereFlag switch
        {
            WhereFlag.And => "AND",
            WhereFlag.Or => "OR",
            _ => throw new Exception(),
        };
}
