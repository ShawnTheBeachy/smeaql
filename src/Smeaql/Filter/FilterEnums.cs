namespace Smeaql.Filter;

internal enum FilterFlag
{
    And,
    Or,
}

internal static class WhereEnumExtensions
{
    public static string ToSql(this FilterFlag filterFlag) =>
        filterFlag switch
        {
            FilterFlag.And => "AND",
            FilterFlag.Or => "OR",
            _ => throw new Exception(),
        };
}
