namespace Smeaql.Helpers;

internal static class StringExtensions
{
    private static int? AliasIndex(this ReadOnlySpan<char> value)
    {
        char? lastChar = null;

        for (var i = 0; i < value.Length; i++)
        {
            var c = value[i];

            switch (lastChar)
            {
                case null when c == ' ':
                    lastChar = c;
                    break;
                case ' ' when c is 'a' or 'A':
                    lastChar = 'A';
                    break;
                case 'A' when c is 's' or 'S':
                    lastChar = 'S';
                    break;
                case 's' or 'S' when c == ' ':
                    return i - 3;
            }
        }

        return null;
    }

    public static string Bracket(this string value)
    {
        if (value.Length < 1)
            return value;

        var valueSpan = value.AsSpan();

        if (valueSpan.Length == 1 && valueSpan[0] == '*')
            return value;

        if (valueSpan[0] == '[' && valueSpan[^1] == ']')
            return value;

        if (valueSpan[0] == '"' && valueSpan[^1] == '"')
            return value;

        var aliasIndex = valueSpan.AliasIndex();

        if (aliasIndex is null)
            return new string(BracketInternal(valueSpan));

        var aiv = aliasIndex.Value;
        return $"{BracketInternal(valueSpan[..aiv])}{valueSpan[aiv..(aiv + 4)]}{BracketInternal(valueSpan[(aiv + 4)..])}";
    }

    private static ReadOnlySpan<char> BracketInternal(ReadOnlySpan<char> value)
    {
        var periodCount = value.Count('.');
        var arr = new char[value.Length + (periodCount + 1) * 2];
        arr[0] = '[';
        var offset = 1;

        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] == '.')
            {
                arr[i + offset++] = ']';
                arr[i + offset++] = value[i];
                arr[i + offset] = '[';
            }
            else
                arr[i + offset] = value[i];
        }

        arr[^1] = ']';
        return arr;
    }
}
