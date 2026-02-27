namespace Smeaql.Where;

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
}
