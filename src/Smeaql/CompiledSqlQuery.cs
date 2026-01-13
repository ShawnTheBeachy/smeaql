using System.Text;

namespace Smeaql;

public sealed class CompiledSqlQuery
{
    private readonly StringBuilder _builder = new();

    public override string ToString() => _builder.ToString();

    internal void Write(char value) => _builder.Append(value);

    internal void Write(string value) => _builder.Append(value);
}
