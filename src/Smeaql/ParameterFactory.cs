namespace Smeaql;

internal sealed class ParameterFactory
{
    private int _counter;
    private readonly List<(string Name, object? Value)> _parameters = [];
    public IReadOnlyList<(string Name, object? Value)> Parameters => _parameters;

    public string CreateParameter(object? value)
    {
        var parameterName = $"@p{_counter++}";
        _parameters.Add((parameterName, value));
        return parameterName;
    }
}
