namespace Smeaql;

internal enum ConditionType
{
    And,
    Or,
}

internal static class EnumExtensions
{
    public static string ToSql(this ConditionType conditionType) =>
        conditionType switch
        {
            ConditionType.And => "AND",
            ConditionType.Or => "OR",
            _ => throw new Exception(),
        };
}
