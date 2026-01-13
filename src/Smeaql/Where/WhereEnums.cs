namespace Smeaql.Where;

internal enum WhereFlag
{
    And,
    Or,
}

internal static class WhereEnumExtensions
{
    public static string ToSql(this WhereFlag whereFlag) =>
        whereFlag switch
        {
            WhereFlag.And => "AND",
            WhereFlag.Or => "OR",
            _ => throw new Exception(),
        };
}
