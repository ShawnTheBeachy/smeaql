namespace Smeaql.Helpers;

internal static class StringExtensions
{
    public static string Bracket(this string value)
    {
        if (value.Length < 1)
            return value;

        if (value[0] == '[' && value[^1] == ']')
            return value;

        if (value[0] == '"' && value[^1] == '"')
            return value;

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
        return new string(arr);
    }

    private static int Count(this string value, char c)
    {
        var count = 0;

        foreach (var character in value)
            if (EqualityComparer<char>.Default.Equals(character, c))
                count++;

        return count;
    }
}
