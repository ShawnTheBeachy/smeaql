namespace Smeaql;

internal sealed class ParameterFactory
{
    private int _counter;
    public Dictionary<string, object?> Parameters { get; } = [];

    public string CreateParameter(object? value)
    {
        var parameterName = $"p{_counter++}";
        Parameters[parameterName] = value;
        return $"@{parameterName}";
    }

    public IEnumerable<string> CreateParameters(IEnumerable<object?> values)
    {
        foreach (var value in values)
        {
            var parameterName = $"p{_counter++}";
            Parameters[parameterName] = value;
            yield return $"@{parameterName}";
        }
    }
}
