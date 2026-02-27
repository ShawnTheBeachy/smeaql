namespace Smeaql.Helpers;

internal static class StringExtensions
{
    public static ReadOnlySpan<char> Bracket(this ReadOnlySpan<char> value)
    {
        if (value.Length < 1)
            return value;

        if (value[0] == '[' && value[^1] == ']')
            return value;

        if (value[0] == '"' && value[^1] == '"')
            return value;

        var arr = new char[value.Length + 2];
        arr[0] = '[';
        arr[^1] = ']';

        for (var i = 0; i < value.Length; i++)
            arr[i + 1] = value[i];

        return arr;
    }
}
